using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace MouseClickTracker
{
    public partial class mct: Form
    {


        // Variables to interact with the Win32 API
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelMouseProc _proc;
        private string logFilePath = "MouseClickLog.txt";

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        // Click and Heatmap storage

        private List<Point> clickPoints = new List<Point>();
        private Dictionary<Point, int> heatmapData = new Dictionary<Point, int>();





        public mct()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer backgroundUpdateTimer = new Timer();
            backgroundUpdateTimer.Interval = 1000; // 1s (to avoid lag)
            backgroundUpdateTimer.Tick += (s, es) => DrawHeatmap();
            backgroundUpdateTimer.Start();

            _proc = HookCallback;
            _hookID = SetHook(_proc);

            lblStatus.Text = "Tracking mouse clicks...";
            btnToggle.Text = "Stop Tracking";
            btnToggle.Click += BtnToggle_Click;

            if (File.Exists(logFilePath))
            {
                foreach (string line in File.ReadAllLines(logFilePath))
                {
                    var parts = line.Split(new[] { "Click at X:", ", Y:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 3 && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                    {
                        Point pt = new Point(x, y);
                        clickPoints.Add(pt);
                        if (heatmapData.ContainsKey(pt))
                            heatmapData[pt]++;
                        else
                            heatmapData[pt] = 1;
                    }
                }
                DrawHeatmap(); // Load heatmap
            }
        }



        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_RBUTTONDOWN))
            {
                POINT pt;
                GetCursorPos(out pt);
                Point screenPoint = new Point(pt.X, pt.Y);
                clickPoints.Add(screenPoint);

                if (heatmapData.ContainsKey(screenPoint))
                    heatmapData[screenPoint]++;
                else
                    heatmapData[screenPoint] = 1;

                this.BeginInvoke((Action)(() =>
                {
                    lstLog.Items.Add($"{DateTime.Now}: Click at X:{pt.X}, Y:{pt.Y}");
                    lstLog.TopIndex = lstLog.Items.Count - 1;
                    DrawHeatmap(); // Update heatmap
                }));
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        private struct POINT
        {
            public int X;
            public int Y;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            base.OnFormClosing(e);
        }

        private bool isTracking = true;












        private void DrawHeatmap()
        {
            if (heatmapData.Count == 0) return;

            Bitmap baseImage;

            if (chkShowBackground.Checked)
            {
                baseImage = CaptureDarkenedScreenshot(); // Use screen capture
            }
            else
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                baseImage = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(baseImage))
                {
                    g.Clear(Color.Black); // Use black background
                }
            }

            using (Graphics g = Graphics.FromImage(baseImage))
            {
                foreach (var entry in heatmapData)
                {
                    Point pt = entry.Key;
                    int intensity = entry.Value;
                    DrawBlurredCircle(g, pt.X, pt.Y, intensity);
                }
            }

            pictureBoxHeatmap.Image = new Bitmap(baseImage, pictureBoxHeatmap.Width, pictureBoxHeatmap.Height);
        }




        private void DrawBlurredCircle(Graphics g, int x, int y, int intensity)
        {
            int maxSize = 50; // Max blur radius
            float alphaStep = 255f / maxSize;

            for (int i = maxSize; i > 0; i -= 5)
            {
                int alpha = (int)(alphaStep * i * (intensity / 2.0));
                alpha = Math.Min(255, alpha);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, GetHeatmapColor(intensity))))
                {
                    g.FillEllipse(brush, x - i / 2, y - i / 2, i, i);
                }
            }
        }

        private Color GetHeatmapColor(int intensity)
        {
            int maxIntensity = 20;
            double ratio = Math.Min(1, intensity / (double)maxIntensity);

            int r = (int)(Math.Max(0, 255 * (ratio - 0.5) * 2));
            int g = (int)(Math.Min(255, 255 * (1 - Math.Abs(ratio - 0.5) * 2)));
            int b = (int)(Math.Max(0, 255 * (0.5 - ratio) * 2));

            return Color.FromArgb(r, g, b);
        }





        private Bitmap CaptureDarkenedScreenshot()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            Bitmap screenshot = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));

                // darkening
                using (SolidBrush darkenBrush = new SolidBrush(Color.FromArgb(180, Color.Black)))
                {
                    g.FillRectangle(darkenBrush, 0, 0, width, height);
                }
            }
            return screenshot;
        }










        private void BtnToggle_Click(object sender, EventArgs e)
        {
            if (isTracking)
            {
                UnhookWindowsHookEx(_hookID);
                lblStatus.Text = "Tracking stopped.";
                btnToggle.Text = "Start Tracking";
            }
            else
            {
                _hookID = SetHook(_proc);
                lblStatus.Text = "Tracking mouse clicks...";
                btnToggle.Text = "Stop Tracking";
            }
            isTracking = !isTracking;
        }



        private void btnSaveHeatmap_Click(object sender, EventArgs e)
        {
            if (pictureBoxHeatmap.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    saveFileDialog.Title = "Save Heatmap";
                    saveFileDialog.FileName = "heatmap.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBoxHeatmap.Image.Save(saveFileDialog.FileName);
                        MessageBox.Show("Heatmap saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No heatmap to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowBackground_CheckedChanged(object sender, EventArgs e)
        {
            DrawHeatmap();
        }

    }
}
