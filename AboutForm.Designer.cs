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
            this.ok = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(206, 158);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 26);
            this.ok.TabIndex = 0;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RiverSimulationApplication.Properties.Resources.RiverSimulation;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(173, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "水理動床模式使用者介面";
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.Location = new System.Drawing.Point(179, 47);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(69, 25);
            this.version.TabIndex = 3;
            this.version.Text = "label2";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 196);
            this.ControlBox = false;
            this.Controls.Add(this.version);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ok);
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "關於";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label version;
    }
}