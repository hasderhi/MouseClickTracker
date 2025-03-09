namespace MouseClickTracker
{
    partial class mct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnToggle = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.pictureBoxHeatmap = new System.Windows.Forms.PictureBox();
            this.btnSaveHeatmap = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkShowBackground = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeatmap)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(128, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(135, 21);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Loading Status...";
            // 
            // btnToggle
            // 
            this.btnToggle.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggle.Location = new System.Drawing.Point(132, 29);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(165, 23);
            this.btnToggle.TabIndex = 1;
            this.btnToggle.Text = "Start Logging";
            this.btnToggle.UseVisualStyleBackColor = true;
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 19;
            this.lstLog.Location = new System.Drawing.Point(420, 5);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(314, 118);
            this.lstLog.TabIndex = 2;
            // 
            // pictureBoxHeatmap
            // 
            this.pictureBoxHeatmap.BackColor = System.Drawing.SystemColors.InfoText;
            this.pictureBoxHeatmap.Location = new System.Drawing.Point(12, 147);
            this.pictureBoxHeatmap.Name = "pictureBoxHeatmap";
            this.pictureBoxHeatmap.Size = new System.Drawing.Size(737, 442);
            this.pictureBoxHeatmap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHeatmap.TabIndex = 3;
            this.pictureBoxHeatmap.TabStop = false;
            // 
            // btnSaveHeatmap
            // 
            this.btnSaveHeatmap.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveHeatmap.Location = new System.Drawing.Point(132, 58);
            this.btnSaveHeatmap.Name = "btnSaveHeatmap";
            this.btnSaveHeatmap.Size = new System.Drawing.Size(165, 23);
            this.btnSaveHeatmap.TabIndex = 4;
            this.btnSaveHeatmap.Text = "Save Heatmap";
            this.btnSaveHeatmap.UseVisualStyleBackColor = true;
            this.btnSaveHeatmap.Click += new System.EventHandler(this.btnSaveHeatmap_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkShowBackground);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.btnSaveHeatmap);
            this.panel1.Controls.Add(this.lstLog);
            this.panel1.Controls.Add(this.btnToggle);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 129);
            this.panel1.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(119, 114);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Mouse\r\nClick\r\nTracker";
            // 
            // chkShowBackground
            // 
            this.chkShowBackground.AutoSize = true;
            this.chkShowBackground.Checked = true;
            this.chkShowBackground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBackground.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowBackground.Location = new System.Drawing.Point(132, 87);
            this.chkShowBackground.Name = "chkShowBackground";
            this.chkShowBackground.Size = new System.Drawing.Size(122, 20);
            this.chkShowBackground.TabIndex = 6;
            this.chkShowBackground.Text = "Show Background";
            this.chkShowBackground.UseVisualStyleBackColor = true;
            this.chkShowBackground.CheckedChanged += new System.EventHandler(this.chkShowBackground_CheckedChanged);
            // 
            // mct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 601);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxHeatmap);
            this.Name = "mct";
            this.Text = "MouseClickTracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeatmap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.PictureBox pictureBoxHeatmap;
        private System.Windows.Forms.Button btnSaveHeatmap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkShowBackground;
    }
}

