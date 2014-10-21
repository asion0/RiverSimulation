namespace RiverSimulationApplication
{
    partial class ImportImageForm
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.hTxt = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(175, 16);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(230, 177);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "左上角座標";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "E :";
            // 
            // eTxt
            // 
            this.eTxt.Location = new System.Drawing.Point(37, 29);
            this.eTxt.Name = "eTxt";
            this.eTxt.Size = new System.Drawing.Size(100, 22);
            this.eTxt.TabIndex = 3;
            this.eTxt.Text = "269958.74 ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "N :";
            // 
            // nTxt
            // 
            this.nTxt.Location = new System.Drawing.Point(37, 57);
            this.nTxt.Name = "nTxt";
            this.nTxt.Size = new System.Drawing.Size(100, 22);
            this.nTxt.TabIndex = 3;
            this.nTxt.Text = "2732868.61";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "寬";
            // 
            // wTxt
            // 
            this.wTxt.Location = new System.Drawing.Point(37, 107);
            this.wTxt.Name = "wTxt";
            this.wTxt.Size = new System.Drawing.Size(100, 22);
            this.wTxt.TabIndex = 3;
            this.wTxt.Text = "1129.091";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "圖形大小";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "高";
            // 
            // hTxt
            // 
            this.hTxt.Location = new System.Drawing.Point(37, 135);
            this.hTxt.Name = "hTxt";
            this.hTxt.Size = new System.Drawing.Size(100, 22);
            this.hTxt.TabIndex = 3;
            this.hTxt.Text = "648.143";
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(300, 199);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(105, 27);
            this.ok.TabIndex = 4;
            this.ok.Text = "確認";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // ImportImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 235);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.nTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.wTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.eTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "輸入航拍圖資訊";
            this.Load += new System.EventHandler(this.ImportImageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox eTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox wTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox hTxt;
        private System.Windows.Forms.Button ok;
    }
}