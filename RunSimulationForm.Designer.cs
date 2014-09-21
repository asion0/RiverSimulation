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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunSimulationForm));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbResult = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.stopFlagChk = new System.Windows.Forms.CheckBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.msg = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 365);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(558, 23);
            this.progressBar.TabIndex = 0;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(12, 72);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(559, 279);
            this.tbResult.TabIndex = 1;
            this.tbResult.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(558, 241);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
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
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(11, 35);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(119, 31);
            this.startBtn.TabIndex = 4;
            this.startBtn.Text = "開始模擬";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // msg
            // 
            this.msg.Location = new System.Drawing.Point(12, 401);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(558, 13);
            this.msg.TabIndex = 5;
            // 
            // stopBtn
            // 
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(136, 35);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(119, 31);
            this.stopBtn.TabIndex = 4;
            this.stopBtn.Text = "停止模擬";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // RunSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 402);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.stopFlagChk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.progressBar);
            this.Name = "RunSimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模擬作業";
            this.Load += new System.EventHandler(this.RunSimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox stopFlagChk;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Button stopBtn;
    }
}