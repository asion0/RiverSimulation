namespace RiverSimulationApplication
{
    partial class RunSimulationForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.stopFlagChk = new System.Windows.Forms.CheckBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.msg = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.progTxt = new System.Windows.Forms.Label();
            this.maxIterationsNumTxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(11, 620);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(828, 32);
            this.progressBar.TabIndex = 0;
            // 
            // stopFlagChk
            // 
            this.stopFlagChk.AutoSize = true;
            this.stopFlagChk.Location = new System.Drawing.Point(12, 12);
            this.stopFlagChk.Name = "stopFlagChk";
            this.stopFlagChk.Size = new System.Drawing.Size(276, 16);
            this.stopFlagChk.TabIndex = 3;
            this.stopFlagChk.Text = "到達水理最大疊代次數後仍未收歛時，繼續模擬";
            this.stopFlagChk.UseVisualStyleBackColor = true;
            this.stopFlagChk.CheckedChanged += new System.EventHandler(this.stopFlagChk_CheckedChanged);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(11, 61);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(119, 31);
            this.startBtn.TabIndex = 4;
            this.startBtn.Text = "開始模擬";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // msg
            // 
            this.msg.Location = new System.Drawing.Point(290, 180);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(558, 13);
            this.msg.TabIndex = 5;
            // 
            // stopBtn
            // 
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(136, 61);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(119, 31);
            this.stopBtn.TabIndex = 4;
            this.stopBtn.Text = "停止模擬";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(11, 102);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(933, 512);
            this.chart1.TabIndex = 6;
            this.chart1.Text = "chart1";
            // 
            // progTxt
            // 
            this.progTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progTxt.BackColor = System.Drawing.SystemColors.Control;
            this.progTxt.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.progTxt.ForeColor = System.Drawing.Color.Black;
            this.progTxt.Location = new System.Drawing.Point(844, 626);
            this.progTxt.Name = "progTxt";
            this.progTxt.Size = new System.Drawing.Size(100, 23);
            this.progTxt.TabIndex = 7;
            this.progTxt.Text = "0%";
            this.progTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxIterationsNumTxt
            // 
            this.maxIterationsNumTxt.Location = new System.Drawing.Point(117, 33);
            this.maxIterationsNumTxt.MaxLength = 8;
            this.maxIterationsNumTxt.Name = "maxIterationsNumTxt";
            this.maxIterationsNumTxt.Size = new System.Drawing.Size(160, 22);
            this.maxIterationsNumTxt.TabIndex = 9;
            this.maxIterationsNumTxt.TextChanged += new System.EventHandler(this.maxIterationsNumTxt_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(10, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "水理最大疊代次數";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RunSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 690);
            this.Controls.Add(this.maxIterationsNumTxt);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.progTxt);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.stopFlagChk);
            this.Controls.Add(this.progressBar);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "RunSimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模擬作業";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RunSimulationForm_FormClosed);
            this.Load += new System.EventHandler(this.RunSimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox stopFlagChk;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label progTxt;
        private System.Windows.Forms.TextBox maxIterationsNumTxt;
        private System.Windows.Forms.Label label10;
    }
}