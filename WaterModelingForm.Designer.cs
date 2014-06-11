namespace RiverSimulationApplication
{
    partial class WaterModelingForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.setting = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.comment = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "定/變量流設定";
            this.groupBox1.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(14, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 71);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "數值參數";
            this.groupBox2.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(13, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 71);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "二次流效應";
            this.groupBox3.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(14, 244);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(366, 71);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "3D基本設定";
            this.groupBox4.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(14, 321);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(366, 71);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "糙度係數";
            this.groupBox5.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(14, 398);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(366, 71);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "紊流模式";
            this.groupBox6.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(13, 475);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(367, 71);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "乾床處理";
            this.groupBox7.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(14, 552);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(366, 71);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "浸沒邊界";
            this.groupBox8.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // setting
            // 
            this.setting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.setting.Location = new System.Drawing.Point(396, 260);
            this.setting.Name = "setting";
            this.setting.Size = new System.Drawing.Size(400, 363);
            this.setting.TabIndex = 1;
            this.setting.Text = "設定內容";
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(701, 638);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(96, 32);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(587, 638);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 4;
            this.ok.Text = "確定";
            this.ok.UseVisualStyleBackColor = true;
            // 
            // comment
            // 
            this.comment.Location = new System.Drawing.Point(396, 13);
            this.comment.MinimumSize = new System.Drawing.Size(20, 20);
            this.comment.Name = "comment";
            this.comment.ScrollBarsEnabled = false;
            this.comment.Size = new System.Drawing.Size(400, 225);
            this.comment.TabIndex = 5;
            // 
            // WaterModelingForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(808, 682);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.setting);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WaterModelingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "水理";
            this.Load += new System.EventHandler(this.WaterModelingForm_Load);
            this.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label setting;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.WebBrowser comment;
    }
}