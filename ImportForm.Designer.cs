namespace RiverSimulationApplication
{
    partial class ImportForm
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
            this.comment = new System.Windows.Forms.WebBrowser();
            this.ok = new System.Windows.Forms.Button();
            this.setting = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectBgFilePath = new System.Windows.Forms.Label();
            this.selectBgBtn = new System.Windows.Forms.Button();
            this.noBgRdo = new System.Windows.Forms.RadioButton();
            this.selectBgRdo = new System.Windows.Forms.RadioButton();
            this.useGoogleBgRdo = new System.Windows.Forms.RadioButton();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.inputFilePath = new System.Windows.Forms.Label();
            this.inputFileBtn = new System.Windows.Forms.Button();
            this.inputGridBtn = new System.Windows.Forms.Button();
            this.runExcelBtn = new System.Windows.Forms.Button();
            this.runCcheMeshBtn = new System.Windows.Forms.Button();
            this.inputGridRdo = new System.Windows.Forms.RadioButton();
            this.inputFileRdo = new System.Windows.Forms.RadioButton();
            this.inputFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.selectBgDlg = new System.Windows.Forms.OpenFileDialog();
            this.mainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // comment
            // 
            this.comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comment.Location = new System.Drawing.Point(396, 13);
            this.comment.MinimumSize = new System.Drawing.Size(20, 20);
            this.comment.Name = "comment";
            this.comment.ScrollBarsEnabled = false;
            this.comment.Size = new System.Drawing.Size(488, 225);
            this.comment.TabIndex = 9;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(788, 639);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 8;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // setting
            // 
            this.setting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.setting.Location = new System.Drawing.Point(397, 264);
            this.setting.Name = "setting";
            this.setting.Size = new System.Drawing.Size(487, 360);
            this.setting.TabIndex = 7;
            this.setting.Text = "設定內容";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.groupBox1);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(391, 670);
            this.mainPanel.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectBgFilePath);
            this.groupBox1.Controls.Add(this.selectBgBtn);
            this.groupBox1.Controls.Add(this.noBgRdo);
            this.groupBox1.Controls.Add(this.selectBgRdo);
            this.groupBox1.Controls.Add(this.useGoogleBgRdo);
            this.groupBox1.Location = new System.Drawing.Point(12, 254);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 186);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "匯入航拍圖檔";
            // 
            // selectBgFilePath
            // 
            this.selectBgFilePath.Location = new System.Drawing.Point(31, 114);
            this.selectBgFilePath.Name = "selectBgFilePath";
            this.selectBgFilePath.Size = new System.Drawing.Size(303, 51);
            this.selectBgFilePath.TabIndex = 3;
            // 
            // selectBgBtn
            // 
            this.selectBgBtn.Enabled = false;
            this.selectBgBtn.Location = new System.Drawing.Point(106, 85);
            this.selectBgBtn.Name = "selectBgBtn";
            this.selectBgBtn.Size = new System.Drawing.Size(75, 26);
            this.selectBgBtn.TabIndex = 1;
            this.selectBgBtn.Text = "選取";
            this.selectBgBtn.UseVisualStyleBackColor = true;
            this.selectBgBtn.Click += new System.EventHandler(this.selectBgBtn_Click);
            // 
            // noBgRdo
            // 
            this.noBgRdo.AutoSize = true;
            this.noBgRdo.Checked = true;
            this.noBgRdo.Location = new System.Drawing.Point(17, 30);
            this.noBgRdo.Name = "noBgRdo";
            this.noBgRdo.Size = new System.Drawing.Size(83, 16);
            this.noBgRdo.TabIndex = 0;
            this.noBgRdo.TabStop = true;
            this.noBgRdo.Text = "不使用底圖";
            this.noBgRdo.UseVisualStyleBackColor = true;
            this.noBgRdo.CheckedChanged += new System.EventHandler(this.noBgRdo_CheckedChanged);
            // 
            // selectBgRdo
            // 
            this.selectBgRdo.AutoSize = true;
            this.selectBgRdo.Location = new System.Drawing.Point(17, 90);
            this.selectBgRdo.Name = "selectBgRdo";
            this.selectBgRdo.Size = new System.Drawing.Size(71, 16);
            this.selectBgRdo.TabIndex = 0;
            this.selectBgRdo.Text = "選取圖檔";
            this.selectBgRdo.UseVisualStyleBackColor = true;
            this.selectBgRdo.CheckedChanged += new System.EventHandler(this.selectBgRdo_CheckedChanged);
            // 
            // useGoogleBgRdo
            // 
            this.useGoogleBgRdo.AutoSize = true;
            this.useGoogleBgRdo.Location = new System.Drawing.Point(17, 60);
            this.useGoogleBgRdo.Name = "useGoogleBgRdo";
            this.useGoogleBgRdo.Size = new System.Drawing.Size(117, 16);
            this.useGoogleBgRdo.TabIndex = 0;
            this.useGoogleBgRdo.Text = "由Google地圖取得";
            this.useGoogleBgRdo.UseVisualStyleBackColor = true;
            this.useGoogleBgRdo.CheckedChanged += new System.EventHandler(this.useGoogleBgRdo_CheckedChanged);
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.inputFilePath);
            this.flowTypeGroup.Controls.Add(this.inputFileBtn);
            this.flowTypeGroup.Controls.Add(this.inputGridBtn);
            this.flowTypeGroup.Controls.Add(this.runExcelBtn);
            this.flowTypeGroup.Controls.Add(this.runCcheMeshBtn);
            this.flowTypeGroup.Controls.Add(this.inputGridRdo);
            this.flowTypeGroup.Controls.Add(this.inputFileRdo);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 11);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(366, 227);
            this.flowTypeGroup.TabIndex = 0;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "計算網格來源";
            // 
            // inputFilePath
            // 
            this.inputFilePath.Enabled = false;
            this.inputFilePath.Location = new System.Drawing.Point(31, 54);
            this.inputFilePath.Name = "inputFilePath";
            this.inputFilePath.Size = new System.Drawing.Size(303, 56);
            this.inputFilePath.TabIndex = 3;
            // 
            // inputFileBtn
            // 
            this.inputFileBtn.Enabled = false;
            this.inputFileBtn.Location = new System.Drawing.Point(106, 25);
            this.inputFileBtn.Name = "inputFileBtn";
            this.inputFileBtn.Size = new System.Drawing.Size(75, 26);
            this.inputFileBtn.TabIndex = 1;
            this.inputFileBtn.Text = "選取檔案";
            this.inputFileBtn.UseVisualStyleBackColor = true;
            this.inputFileBtn.Click += new System.EventHandler(this.inputFileBtn_Click);
            // 
            // inputGridBtn
            // 
            this.inputGridBtn.Enabled = false;
            this.inputGridBtn.Location = new System.Drawing.Point(106, 113);
            this.inputGridBtn.Name = "inputGridBtn";
            this.inputGridBtn.Size = new System.Drawing.Size(75, 26);
            this.inputGridBtn.TabIndex = 1;
            this.inputGridBtn.Text = "輸入格網";
            this.inputGridBtn.UseVisualStyleBackColor = true;
            this.inputGridBtn.Click += new System.EventHandler(this.inputGridBtn_Click);
            // 
            // runExcelBtn
            // 
            this.runExcelBtn.Location = new System.Drawing.Point(17, 187);
            this.runExcelBtn.Name = "runExcelBtn";
            this.runExcelBtn.Size = new System.Drawing.Size(130, 26);
            this.runExcelBtn.TabIndex = 1;
            this.runExcelBtn.Text = "執行Excel";
            this.runExcelBtn.UseVisualStyleBackColor = true;
            this.runExcelBtn.Click += new System.EventHandler(this.runExcel_Click);
            // 
            // runCcheMeshBtn
            // 
            this.runCcheMeshBtn.Location = new System.Drawing.Point(17, 150);
            this.runCcheMeshBtn.Name = "runCcheMeshBtn";
            this.runCcheMeshBtn.Size = new System.Drawing.Size(130, 26);
            this.runCcheMeshBtn.TabIndex = 1;
            this.runCcheMeshBtn.Text = "執行CCHE-Mesh";
            this.runCcheMeshBtn.UseVisualStyleBackColor = true;
            this.runCcheMeshBtn.Click += new System.EventHandler(this.runCcheMeshBtn_Click);
            // 
            // inputGridRdo
            // 
            this.inputGridRdo.AutoSize = true;
            this.inputGridRdo.Location = new System.Drawing.Point(17, 118);
            this.inputGridRdo.Name = "inputGridRdo";
            this.inputGridRdo.Size = new System.Drawing.Size(71, 16);
            this.inputGridRdo.TabIndex = 0;
            this.inputGridRdo.Text = "自行輸入";
            this.inputGridRdo.UseVisualStyleBackColor = true;
            this.inputGridRdo.CheckedChanged += new System.EventHandler(this.inputGridRdo_CheckedChanged);
            // 
            // inputFileRdo
            // 
            this.inputFileRdo.AutoSize = true;
            this.inputFileRdo.Location = new System.Drawing.Point(17, 30);
            this.inputFileRdo.Name = "inputFileRdo";
            this.inputFileRdo.Size = new System.Drawing.Size(83, 16);
            this.inputFileRdo.TabIndex = 0;
            this.inputFileRdo.Text = "由檔案匯入";
            this.inputFileRdo.UseVisualStyleBackColor = true;
            this.inputFileRdo.CheckedChanged += new System.EventHandler(this.inputFileRdo_CheckedChanged);
            // 
            // inputFileDlg
            // 
            this.inputFileDlg.Filter = "文字檔案(*.txt)|*.txt|Excel檔案(*.xls)|*.xls|SMS檔案(*.sms)|*.sms|CCHE_MESH檔案(*.geo)|*.g";
            this.inputFileDlg.Title = "選取匯入格網檔案";
            // 
            // selectBgDlg
            // 
            this.selectBgDlg.Filter = "所有檔案|*.*|Bmp檔案|*.bmp|PNG檔案|*.png|JPG檔案|*jpg";
            // 
            // ImportForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 682);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.setting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "匯入";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowTypeGroup.ResumeLayout(false);
            this.flowTypeGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser comment;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label setting;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label selectBgFilePath;
        private System.Windows.Forms.Button selectBgBtn;
        private System.Windows.Forms.RadioButton noBgRdo;
        private System.Windows.Forms.RadioButton selectBgRdo;
        private System.Windows.Forms.RadioButton useGoogleBgRdo;
        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.Label inputFilePath;
        private System.Windows.Forms.Button inputFileBtn;
        private System.Windows.Forms.Button inputGridBtn;
        private System.Windows.Forms.Button runExcelBtn;
        private System.Windows.Forms.Button runCcheMeshBtn;
        private System.Windows.Forms.RadioButton inputGridRdo;
        private System.Windows.Forms.RadioButton inputFileRdo;
        private System.Windows.Forms.OpenFileDialog inputFileDlg;
        private System.Windows.Forms.OpenFileDialog selectBgDlg;

    }
}