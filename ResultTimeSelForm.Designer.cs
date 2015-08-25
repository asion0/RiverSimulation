namespace RiverSimulationApplication
{
    partial class ResultTimeSelForm
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
            this.timeCmb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.descLbl = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timeCmb
            // 
            this.timeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeCmb.FormattingEnabled = true;
            this.timeCmb.Location = new System.Drawing.Point(14, 108);
            this.timeCmb.Name = "timeCmb";
            this.timeCmb.Size = new System.Drawing.Size(161, 20);
            this.timeCmb.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "選取時間：";
            // 
            // descLbl
            // 
            this.descLbl.Location = new System.Drawing.Point(12, 9);
            this.descLbl.Name = "descLbl";
            this.descLbl.Size = new System.Drawing.Size(519, 63);
            this.descLbl.TabIndex = 2;
            this.descLbl.Text = "label1";
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ok.Location = new System.Drawing.Point(435, 266);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 6;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // ResultTimeSelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 310);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.descLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeCmb);
            this.Name = "ResultTimeSelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選取時間";
            this.Load += new System.EventHandler(this.ResultTimeSelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox timeCmb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label descLbl;
        private System.Windows.Forms.Button ok;
    }
}