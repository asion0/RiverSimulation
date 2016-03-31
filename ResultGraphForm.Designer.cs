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
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sel1Lbl = new System.Windows.Forms.Label();
            this.combo1 = new System.Windows.Forms.ComboBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(407, -1);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(109, 50);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            // 
            // sel1Lbl
            // 
            this.sel1Lbl.Location = new System.Drawing.Point(607, 11);
            this.sel1Lbl.Name = "sel1Lbl";
            this.sel1Lbl.Size = new System.Drawing.Size(189, 16);
            this.sel1Lbl.TabIndex = 8;
            this.sel1Lbl.Text = ":";
            this.sel1Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combo1
            // 
            this.combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo1.FormattingEnabled = true;
            this.combo1.Location = new System.Drawing.Point(802, 7);
            this.combo1.Name = "combo1";
            this.combo1.Size = new System.Drawing.Size(170, 20);
            this.combo1.TabIndex = 9;
            this.combo1.SelectedIndexChanged += new System.EventHandler(this.combo1_SelectedIndexChanged);
            // 
            // pic1
            // 
            this.pic1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic1.Location = new System.Drawing.Point(12, 47);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(960, 630);
            this.pic1.TabIndex = 10;
            this.pic1.TabStop = false;
            // 
            // ResultGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 689);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.combo1);
            this.Controls.Add(this.sel1Lbl);
            this.Controls.Add(this.chart1);
            this.Name = "ResultGraphForm";
            this.Text = "模擬結果-圖形";
            this.Load += new System.EventHandler(this.ResultGraphForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label sel1Lbl;
        private System.Windows.Forms.ComboBox combo1;
        private System.Windows.Forms.PictureBox pic1;
    }
}