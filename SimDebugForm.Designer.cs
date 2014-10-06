namespace RiverSimulationApplication
{
    partial class SimDebugForm
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
            this.case01Rdo = new System.Windows.Forms.RadioButton();
            this.case02Rdo = new System.Windows.Forms.RadioButton();
            this.case03Rdo = new System.Windows.Forms.RadioButton();
            this.case04Rdo = new System.Windows.Forms.RadioButton();
            this.case05Rdo = new System.Windows.Forms.RadioButton();
            this.externalCaseRdo = new System.Windows.Forms.RadioButton();
            this.importTextRdo = new System.Windows.Forms.RadioButton();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fileCombo = new System.Windows.Forms.ComboBox();
            this.importDurationTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.exeTxt = new System.Windows.Forms.TextBox();
            this.paramTxt = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // case01Rdo
            // 
            this.case01Rdo.AutoSize = true;
            this.case01Rdo.Location = new System.Drawing.Point(6, 21);
            this.case01Rdo.Name = "case01Rdo";
            this.case01Rdo.Size = new System.Drawing.Size(91, 16);
            this.case01Rdo.TabIndex = 0;
            this.case01Rdo.TabStop = true;
            this.case01Rdo.Text = "使用CASE 01";
            this.case01Rdo.UseVisualStyleBackColor = true;
            this.case01Rdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // case02Rdo
            // 
            this.case02Rdo.AutoSize = true;
            this.case02Rdo.Location = new System.Drawing.Point(6, 43);
            this.case02Rdo.Name = "case02Rdo";
            this.case02Rdo.Size = new System.Drawing.Size(91, 16);
            this.case02Rdo.TabIndex = 0;
            this.case02Rdo.TabStop = true;
            this.case02Rdo.Text = "使用CASE 02";
            this.case02Rdo.UseVisualStyleBackColor = true;
            this.case02Rdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // case03Rdo
            // 
            this.case03Rdo.AutoSize = true;
            this.case03Rdo.Location = new System.Drawing.Point(6, 65);
            this.case03Rdo.Name = "case03Rdo";
            this.case03Rdo.Size = new System.Drawing.Size(91, 16);
            this.case03Rdo.TabIndex = 0;
            this.case03Rdo.TabStop = true;
            this.case03Rdo.Text = "使用CASE 03";
            this.case03Rdo.UseVisualStyleBackColor = true;
            this.case03Rdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // case04Rdo
            // 
            this.case04Rdo.AutoSize = true;
            this.case04Rdo.Location = new System.Drawing.Point(6, 87);
            this.case04Rdo.Name = "case04Rdo";
            this.case04Rdo.Size = new System.Drawing.Size(91, 16);
            this.case04Rdo.TabIndex = 0;
            this.case04Rdo.TabStop = true;
            this.case04Rdo.Text = "使用CASE 04";
            this.case04Rdo.UseVisualStyleBackColor = true;
            this.case04Rdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // case05Rdo
            // 
            this.case05Rdo.AutoSize = true;
            this.case05Rdo.Location = new System.Drawing.Point(6, 109);
            this.case05Rdo.Name = "case05Rdo";
            this.case05Rdo.Size = new System.Drawing.Size(91, 16);
            this.case05Rdo.TabIndex = 0;
            this.case05Rdo.TabStop = true;
            this.case05Rdo.Text = "使用CASE 05";
            this.case05Rdo.UseVisualStyleBackColor = true;
            this.case05Rdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // externalCaseRdo
            // 
            this.externalCaseRdo.AutoSize = true;
            this.externalCaseRdo.Location = new System.Drawing.Point(181, 67);
            this.externalCaseRdo.Name = "externalCaseRdo";
            this.externalCaseRdo.Size = new System.Drawing.Size(275, 16);
            this.externalCaseRdo.TabIndex = 0;
            this.externalCaseRdo.TabStop = true;
            this.externalCaseRdo.Text = "使用外部資料(請將檔案和[本程式放置同一目錄)";
            this.externalCaseRdo.UseVisualStyleBackColor = true;
            this.externalCaseRdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // importTextRdo
            // 
            this.importTextRdo.AutoSize = true;
            this.importTextRdo.Location = new System.Drawing.Point(181, 21);
            this.importTextRdo.Name = "importTextRdo";
            this.importTextRdo.Size = new System.Drawing.Size(211, 16);
            this.importTextRdo.TabIndex = 0;
            this.importTextRdo.TabStop = true;
            this.importTextRdo.Text = "使用匯入文字檔(事先存好的輸出檔)";
            this.importTextRdo.UseVisualStyleBackColor = true;
            this.importTextRdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // tbResult
            // 
            this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResult.Location = new System.Drawing.Point(12, 268);
            this.tbResult.MaxLength = 999999;
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(700, 307);
            this.tbResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(700, 44);
            this.label1.TabIndex = 3;
            this.label1.Text = "本視窗為開發階段測試與除錯使用，將不會出現在正式發行的版本中。請先設定此視窗後再按下\"開始模擬\"";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.paramTxt);
            this.groupBox1.Controls.Add(this.exeTxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.importDurationTxt);
            this.groupBox1.Controls.Add(this.fileCombo);
            this.groupBox1.Controls.Add(this.case01Rdo);
            this.groupBox1.Controls.Add(this.case02Rdo);
            this.groupBox1.Controls.Add(this.case03Rdo);
            this.groupBox1.Controls.Add(this.importTextRdo);
            this.groupBox1.Controls.Add(this.case04Rdo);
            this.groupBox1.Controls.Add(this.externalCaseRdo);
            this.groupBox1.Controls.Add(this.case05Rdo);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 199);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "資料來源";
            // 
            // fileCombo
            // 
            this.fileCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileCombo.FormattingEnabled = true;
            this.fileCombo.Items.AddRange(new object[] {
            "output01.txt",
            "output02.txt",
            "output03.txt",
            "output04.txt",
            "output05.txt"});
            this.fileCombo.Location = new System.Drawing.Point(199, 42);
            this.fileCombo.Name = "fileCombo";
            this.fileCombo.Size = new System.Drawing.Size(110, 20);
            this.fileCombo.TabIndex = 1;
            this.fileCombo.SelectedIndexChanged += new System.EventHandler(this.fileCombo_SelectedIndexChanged);
            // 
            // importDurationTxt
            // 
            this.importDurationTxt.Location = new System.Drawing.Point(324, 42);
            this.importDurationTxt.MaxLength = 5;
            this.importDurationTxt.Name = "importDurationTxt";
            this.importDurationTxt.Size = new System.Drawing.Size(54, 22);
            this.importDurationTxt.TabIndex = 2;
            this.importDurationTxt.Text = "5";
            this.importDurationTxt.TextChanged += new System.EventHandler(this.importDurationTxt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "執行檔";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "參數";
            // 
            // exeTxt
            // 
            this.exeTxt.Location = new System.Drawing.Point(247, 87);
            this.exeTxt.Name = "exeTxt";
            this.exeTxt.Size = new System.Drawing.Size(209, 22);
            this.exeTxt.TabIndex = 4;
            this.exeTxt.Text = "10062.exe";
            this.exeTxt.TextChanged += new System.EventHandler(this.exeTxt_TextChanged);
            // 
            // paramTxt
            // 
            this.paramTxt.Location = new System.Drawing.Point(247, 115);
            this.paramTxt.Name = "paramTxt";
            this.paramTxt.Size = new System.Drawing.Size(209, 22);
            this.paramTxt.TabIndex = 5;
            this.paramTxt.Text = "ul101.i 123 3D ul101.dat out";
            this.paramTxt.TextChanged += new System.EventHandler(this.paramTxt_TextChanged);
            // 
            // SimDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 587);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbResult);
            this.Name = "SimDebugForm";
            this.Text = "除錯視窗";
            this.Load += new System.EventHandler(this.SimDebugForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton case01Rdo;
        private System.Windows.Forms.RadioButton case02Rdo;
        private System.Windows.Forms.RadioButton case03Rdo;
        private System.Windows.Forms.RadioButton case04Rdo;
        private System.Windows.Forms.RadioButton case05Rdo;
        private System.Windows.Forms.RadioButton externalCaseRdo;
        private System.Windows.Forms.RadioButton importTextRdo;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox importDurationTxt;
        private System.Windows.Forms.ComboBox fileCombo;
        private System.Windows.Forms.TextBox paramTxt;
        private System.Windows.Forms.TextBox exeTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}