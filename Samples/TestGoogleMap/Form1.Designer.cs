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
            this.useMapChk = new System.Windows.Forms.CheckBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.inputFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.inputFilePath = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.selBgColorBtn = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.selLineColorBtn = new System.Windows.Forms.Button();
            this.bgColorSamplePanel = new System.Windows.Forms.Panel();
            this.lineColorSamplePanel = new System.Windows.Forms.Panel();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.gPicBox = new PictureBoxCtrl.GridPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
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
            this.inputFilePath.Location = new System.Drawing.Point(13, 42);
            this.inputFilePath.Name = "inputFilePath";
            this.inputFilePath.Size = new System.Drawing.Size(1013, 23);
            this.inputFilePath.TabIndex = 3;
            this.inputFilePath.Text = "尚未選取檔案";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(910, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 33);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "線條厚度";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(303, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 22);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "2.0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // selBgColorBtn
            // 
            this.selBgColorBtn.Location = new System.Drawing.Point(442, 16);
            this.selBgColorBtn.Name = "selBgColorBtn";
            this.selBgColorBtn.Size = new System.Drawing.Size(90, 23);
            this.selBgColorBtn.TabIndex = 7;
            this.selBgColorBtn.Text = "底色選取";
            this.selBgColorBtn.UseVisualStyleBackColor = true;
            this.selBgColorBtn.Click += new System.EventHandler(this.selBgColorBtn_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            // 
            // selLineColorBtn
            // 
            this.selLineColorBtn.Location = new System.Drawing.Point(582, 15);
            this.selLineColorBtn.Name = "selLineColorBtn";
            this.selLineColorBtn.Size = new System.Drawing.Size(90, 23);
            this.selLineColorBtn.TabIndex = 7;
            this.selLineColorBtn.Text = "線條顏色選取";
            this.selLineColorBtn.UseVisualStyleBackColor = true;
            this.selLineColorBtn.Click += new System.EventHandler(this.selLineColorBtn_Click);
            // 
            // bgColorSamplePanel
            // 
            this.bgColorSamplePanel.Location = new System.Drawing.Point(415, 18);
            this.bgColorSamplePanel.Name = "bgColorSamplePanel";
            this.bgColorSamplePanel.Size = new System.Drawing.Size(21, 20);
            this.bgColorSamplePanel.TabIndex = 8;
            // 
            // lineColorSamplePanel
            // 
            this.lineColorSamplePanel.Location = new System.Drawing.Point(555, 17);
            this.lineColorSamplePanel.Name = "lineColorSamplePanel";
            this.lineColorSamplePanel.Size = new System.Drawing.Size(21, 20);
            this.lineColorSamplePanel.TabIndex = 8;
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(739, 16);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(96, 23);
            this.refreshBtn.TabIndex = 9;
            this.refreshBtn.Text = "重畫";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(849, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 33);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(720, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // xLabel
            // 
            this.xLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xLabel.Location = new System.Drawing.Point(301, 65);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(204, 17);
            this.xLabel.TabIndex = 5;
            this.xLabel.Text = "0";
            // 
            // yLabel
            // 
            this.yLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yLabel.Location = new System.Drawing.Point(511, 65);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(96, 17);
            this.yLabel.TabIndex = 5;
            this.yLabel.Text = "0";
            // 
            // gPicBox
            // 
            this.gPicBox.Border = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gPicBox.Grid = null;
            this.gPicBox.Location = new System.Drawing.Point(13, 99);
            this.gPicBox.Name = "gPicBox";
            this.gPicBox.SelectedI = -1;
            this.gPicBox.SelectRow = false;
            this.gPicBox.Size = new System.Drawing.Size(1013, 517);
            this.gPicBox.TabIndex = 13;
            this.gPicBox.GridChangedEvent += new PictureBoxCtrl.GridPictureBox.myDelegate(this.gPicBox_GridChangedEvent);
            this.gPicBox.SelectedRowChangedEvent += new PictureBoxCtrl.GridPictureBox.myDelegate2(this.gPicBox_SelectedRowChangedEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 628);
            this.Controls.Add(this.gPicBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.lineColorSamplePanel);
            this.Controls.Add(this.bgColorSamplePanel);
            this.Controls.Add(this.selLineColorBtn);
            this.Controls.Add(this.selBgColorBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.inputFilePath);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.useMapChk);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox useMapChk;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.OpenFileDialog inputFileDlg;
        private System.Windows.Forms.Label inputFilePath;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button selBgColorBtn;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button selLineColorBtn;
        private System.Windows.Forms.Panel bgColorSamplePanel;
        private System.Windows.Forms.Panel lineColorSamplePanel;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private PictureBoxCtrl.GridPictureBox gPicBox;
    }
}

