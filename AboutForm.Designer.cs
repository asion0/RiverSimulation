namespace RiverSimulationApplication
{
    partial class AboutForm
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
            this.mainName = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.logoPicBox = new System.Windows.Forms.PictureBox();
            this.build = new System.Windows.Forms.Label();
            this.copyright2 = new System.Windows.Forms.Label();
            this.copyright1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainName
            // 
            this.mainName.AutoSize = true;
            this.mainName.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.mainName.Location = new System.Drawing.Point(72, 289);
            this.mainName.Name = "mainName";
            this.mainName.Size = new System.Drawing.Size(278, 31);
            this.mainName.TabIndex = 2;
            this.mainName.Text = "水理動床模式使用者介面";
            // 
            // version
            // 
            this.version.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.version.Location = new System.Drawing.Point(13, 324);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(397, 25);
            this.version.TabIndex = 3;
            this.version.Text = "Version 1.0.0.1";
            this.version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logoPicBox
            // 
            this.logoPicBox.Image = global::RiverSimulationApplication.Properties.Resources.RiverSimulation;
            this.logoPicBox.Location = new System.Drawing.Point(82, 12);
            this.logoPicBox.Name = "logoPicBox";
            this.logoPicBox.Size = new System.Drawing.Size(256, 256);
            this.logoPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logoPicBox.TabIndex = 1;
            this.logoPicBox.TabStop = false;
            // 
            // build
            // 
            this.build.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.build.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.build.Location = new System.Drawing.Point(13, 349);
            this.build.Name = "build";
            this.build.Size = new System.Drawing.Size(397, 25);
            this.build.TabIndex = 3;
            this.build.Text = "Build: 07092014";
            this.build.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copyright2
            // 
            this.copyright2.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyright2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.copyright2.Location = new System.Drawing.Point(11, 549);
            this.copyright2.Name = "copyright2";
            this.copyright2.Size = new System.Drawing.Size(397, 25);
            this.copyright2.TabIndex = 3;
            this.copyright2.Text = "All rights reserved";
            this.copyright2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copyright1
            // 
            this.copyright1.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyright1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.copyright1.Location = new System.Drawing.Point(12, 524);
            this.copyright1.Name = "copyright1";
            this.copyright1.Size = new System.Drawing.Size(397, 25);
            this.copyright1.TabIndex = 3;
            this.copyright1.Text = "© 2014 National Chiao Tung University.";
            this.copyright1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 583);
            this.Controls.Add(this.build);
            this.Controls.Add(this.copyright1);
            this.Controls.Add(this.copyright2);
            this.Controls.Add(this.version);
            this.Controls.Add(this.mainName);
            this.Controls.Add(this.logoPicBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "關於";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPicBox;
        private System.Windows.Forms.Label mainName;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label build;
        private System.Windows.Forms.Label copyright2;
        private System.Windows.Forms.Label copyright1;
    }
}