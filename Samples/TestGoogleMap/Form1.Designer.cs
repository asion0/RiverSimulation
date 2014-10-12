namespace TestGoogleMap
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.useMapChk = new System.Windows.Forms.CheckBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.inputFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.inputFilePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(13, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 479);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // useMapChk
            // 
            this.useMapChk.AutoSize = true;
            this.useMapChk.Location = new System.Drawing.Point(13, 76);
            this.useMapChk.Name = "useMapChk";
            this.useMapChk.Size = new System.Drawing.Size(130, 16);
            this.useMapChk.TabIndex = 1;
            this.useMapChk.Text = "Use Google Static Map";
            this.useMapChk.UseVisualStyleBackColor = true;
            this.useMapChk.CheckedChanged += new System.EventHandler(this.useMapChk_CheckedChanged);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(13, 13);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(138, 23);
            this.loadBtn.TabIndex = 2;
            this.loadBtn.Text = "選取格網檔案";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // inputFileDlg
            // 
            this.inputFileDlg.Filter = "CCHE_MESH檔案(*.geo)|*.geo";
            this.inputFileDlg.Title = "選取匯入格網檔案";
            // 
            // inputFilePath
            // 
            this.inputFilePath.Location = new System.Drawing.Point(13, 47);
            this.inputFilePath.Name = "inputFilePath";
            this.inputFilePath.Size = new System.Drawing.Size(1013, 23);
            this.inputFilePath.TabIndex = 3;
            this.inputFilePath.Text = "尚未選取檔案";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 628);
            this.Controls.Add(this.inputFilePath);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.useMapChk);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox useMapChk;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.OpenFileDialog inputFileDlg;
        private System.Windows.Forms.Label inputFilePath;
    }
}

