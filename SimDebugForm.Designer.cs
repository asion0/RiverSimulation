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
            this.SuspendLayout();
            // 
            // case01Rdo
            // 
            this.case01Rdo.AutoSize = true;
            this.case01Rdo.Location = new System.Drawing.Point(12, 12);
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
            this.case02Rdo.Location = new System.Drawing.Point(12, 34);
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
            this.case03Rdo.Location = new System.Drawing.Point(12, 56);
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
            this.case04Rdo.Location = new System.Drawing.Point(12, 78);
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
            this.case05Rdo.Location = new System.Drawing.Point(12, 100);
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
            this.externalCaseRdo.Location = new System.Drawing.Point(12, 122);
            this.externalCaseRdo.Name = "externalCaseRdo";
            this.externalCaseRdo.Size = new System.Drawing.Size(95, 16);
            this.externalCaseRdo.TabIndex = 0;
            this.externalCaseRdo.TabStop = true;
            this.externalCaseRdo.Text = "使用外部資料";
            this.externalCaseRdo.UseVisualStyleBackColor = true;
            this.externalCaseRdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // importTextRdo
            // 
            this.importTextRdo.AutoSize = true;
            this.importTextRdo.Location = new System.Drawing.Point(12, 167);
            this.importTextRdo.Name = "importTextRdo";
            this.importTextRdo.Size = new System.Drawing.Size(107, 16);
            this.importTextRdo.TabIndex = 0;
            this.importTextRdo.TabStop = true;
            this.importTextRdo.Text = "使用匯入文字檔";
            this.importTextRdo.UseVisualStyleBackColor = true;
            this.importTextRdo.CheckedChanged += new System.EventHandler(this.source_CheckedChanged);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(12, 189);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(700, 195);
            this.tbResult.TabIndex = 2;
            // 
            // SimDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 396);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.importTextRdo);
            this.Controls.Add(this.externalCaseRdo);
            this.Controls.Add(this.case05Rdo);
            this.Controls.Add(this.case04Rdo);
            this.Controls.Add(this.case03Rdo);
            this.Controls.Add(this.case02Rdo);
            this.Controls.Add(this.case01Rdo);
            this.Name = "SimDebugForm";
            this.Text = "SimDebugForm";
            this.Load += new System.EventHandler(this.SimDebugForm_Load);
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
    }
}