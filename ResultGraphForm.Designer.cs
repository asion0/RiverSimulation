namespace RiverSimulationApplication
{
    partial class ResultGraphForm
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboText = new System.Windows.Forms.Label();
            this.combo1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(0, 36);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(957, 652);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            // 
            // comboText
            // 
            this.comboText.Location = new System.Drawing.Point(579, 11);
            this.comboText.Name = "comboText";
            this.comboText.Size = new System.Drawing.Size(189, 16);
            this.comboText.TabIndex = 8;
            this.comboText.Text = ":";
            this.comboText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combo1
            // 
            this.combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo1.FormattingEnabled = true;
            this.combo1.Location = new System.Drawing.Point(774, 8);
            this.combo1.Name = "combo1";
            this.combo1.Size = new System.Drawing.Size(170, 20);
            this.combo1.TabIndex = 9;
            this.combo1.SelectedIndexChanged += new System.EventHandler(this.combo1_SelectedIndexChanged);
            // 
            // ResultGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.Controls.Add(this.combo1);
            this.Controls.Add(this.comboText);
            this.Controls.Add(this.chart1);
            this.Name = "ResultGraphForm";
            this.Text = "模擬結果-圖形";
            this.Load += new System.EventHandler(this.ResultGraphForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label comboText;
        private System.Windows.Forms.ComboBox combo1;
    }
}