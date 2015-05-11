﻿namespace RiverSimulationApplication
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.bitmapGrp = new System.Windows.Forms.GroupBox();
            this.selectBgFilePath = new System.Windows.Forms.Label();
            this.imgInfoBtn = new System.Windows.Forms.Button();
            this.selectBgBtn = new System.Windows.Forms.Button();
            this.noBgRdo = new System.Windows.Forms.RadioButton();
            this.selectBgRdo = new System.Windows.Forms.RadioButton();
            this.useGoogleBgRdo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.runCcheMeshBtn = new System.Windows.Forms.Button();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.levelProportionBtn = new System.Windows.Forms.Button();
            this.verticalLevelNumberTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputFileBtn = new System.Windows.Forms.Button();
            this.reverseGridBtn = new System.Windows.Forms.Button();
            this.gridDataBtn = new System.Windows.Forms.Button();
            this.inputGridBtn = new System.Windows.Forms.Button();
            this.inputGridRdo = new System.Windows.Forms.RadioButton();
            this.inputFileRdo = new System.Windows.Forms.RadioButton();
            this.runExcelBtn = new System.Windows.Forms.Button();
            this.runCcheMeshBtn2 = new System.Windows.Forms.Button();
            this.inputFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.selectBgDlg = new System.Windows.Forms.OpenFileDialog();
            this.previewSpratePanel = new System.Windows.Forms.Panel();
            this.previewSpratePicBox = new System.Windows.Forms.PictureBox();
            this.previewCombo = new System.Windows.Forms.ComboBox();
            this.mapPicBox = new PictureBoxCtrl.GridPictureBox();
            this.coorSelCombo = new System.Windows.Forms.ComboBox();
            this.mainPanel.SuspendLayout();
            this.bitmapGrp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.previewSpratePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewSpratePicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // comment
            // 
            this.comment.AllowWebBrowserDrop = false;
            this.comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comment.IsWebBrowserContextMenuEnabled = false;
            this.comment.Location = new System.Drawing.Point(506, 12);
            this.comment.Name = "comment";
            this.comment.ScrollBarsEnabled = false;
            this.comment.Size = new System.Drawing.Size(438, 224);
            this.comment.TabIndex = 9;
            this.comment.WebBrowserShortcutsEnabled = false;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(848, 652);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 8;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.bitmapGrp);
            this.mainPanel.Controls.Add(this.groupBox2);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            this.mainPanel.Controls.Add(this.runExcelBtn);
            this.mainPanel.Controls.Add(this.runCcheMeshBtn2);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(500, 680);
            this.mainPanel.TabIndex = 10;
            // 
            // bitmapGrp
            // 
            this.bitmapGrp.Controls.Add(this.coorSelCombo);
            this.bitmapGrp.Controls.Add(this.selectBgFilePath);
            this.bitmapGrp.Controls.Add(this.imgInfoBtn);
            this.bitmapGrp.Controls.Add(this.selectBgBtn);
            this.bitmapGrp.Controls.Add(this.noBgRdo);
            this.bitmapGrp.Controls.Add(this.selectBgRdo);
            this.bitmapGrp.Controls.Add(this.useGoogleBgRdo);
            this.bitmapGrp.Enabled = false;
            this.bitmapGrp.Location = new System.Drawing.Point(12, 305);
            this.bitmapGrp.Name = "bitmapGrp";
            this.bitmapGrp.Size = new System.Drawing.Size(472, 177);
            this.bitmapGrp.TabIndex = 0;
            this.bitmapGrp.TabStop = false;
            this.bitmapGrp.Text = "底圖設定";
            // 
            // selectBgFilePath
            // 
            this.selectBgFilePath.Location = new System.Drawing.Point(31, 114);
            this.selectBgFilePath.Name = "selectBgFilePath";
            this.selectBgFilePath.Size = new System.Drawing.Size(303, 51);
            this.selectBgFilePath.TabIndex = 3;
            // 
            // imgInfoBtn
            // 
            this.imgInfoBtn.Enabled = false;
            this.imgInfoBtn.Location = new System.Drawing.Point(208, 85);
            this.imgInfoBtn.Name = "imgInfoBtn";
            this.imgInfoBtn.Size = new System.Drawing.Size(96, 26);
            this.imgInfoBtn.TabIndex = 1;
            this.imgInfoBtn.Text = "輸入圖檔資訊";
            this.imgInfoBtn.UseVisualStyleBackColor = true;
            this.imgInfoBtn.Click += new System.EventHandler(this.imgInfoBtn_Click);
            this.imgInfoBtn.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
            // 
            // selectBgBtn
            // 
            this.selectBgBtn.Enabled = false;
            this.selectBgBtn.Location = new System.Drawing.Point(106, 85);
            this.selectBgBtn.Name = "selectBgBtn";
            this.selectBgBtn.Size = new System.Drawing.Size(96, 26);
            this.selectBgBtn.TabIndex = 1;
            this.selectBgBtn.Text = "選取";
            this.selectBgBtn.UseVisualStyleBackColor = true;
            this.selectBgBtn.Click += new System.EventHandler(this.selectBgBtn_Click);
            this.selectBgBtn.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
            // 
            // noBgRdo
            // 
            this.noBgRdo.AutoSize = true;
            this.noBgRdo.Checked = true;
            this.noBgRdo.Location = new System.Drawing.Point(17, 30);
            this.noBgRdo.Name = "noBgRdo";
            this.noBgRdo.Size = new System.Drawing.Size(59, 16);
            this.noBgRdo.TabIndex = 0;
            this.noBgRdo.TabStop = true;
            this.noBgRdo.Text = "不使用";
            this.noBgRdo.UseVisualStyleBackColor = true;
            this.noBgRdo.CheckedChanged += new System.EventHandler(this.noBgRdo_CheckedChanged);
            this.noBgRdo.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
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
            this.selectBgRdo.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
            // 
            // useGoogleBgRdo
            // 
            this.useGoogleBgRdo.AutoSize = true;
            this.useGoogleBgRdo.Location = new System.Drawing.Point(17, 60);
            this.useGoogleBgRdo.Name = "useGoogleBgRdo";
            this.useGoogleBgRdo.Size = new System.Drawing.Size(81, 16);
            this.useGoogleBgRdo.TabIndex = 0;
            this.useGoogleBgRdo.Text = "Google地圖";
            this.useGoogleBgRdo.UseVisualStyleBackColor = true;
            this.useGoogleBgRdo.CheckedChanged += new System.EventHandler(this.useGoogleBgRdo_CheckedChanged);
            this.useGoogleBgRdo.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.runCcheMeshBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 144);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "格網產生程式";
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(119, 102);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 26);
            this.button5.TabIndex = 2;
            this.button5.Text = "操作方法";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(119, 61);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 26);
            this.button4.TabIndex = 2;
            this.button4.Text = "操作方法";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(119, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 26);
            this.button3.TabIndex = 2;
            this.button3.Text = "操作方法";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(17, 102);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "MeshLab";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(17, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "SMS";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // runCcheMeshBtn
            // 
            this.runCcheMeshBtn.Location = new System.Drawing.Point(17, 21);
            this.runCcheMeshBtn.Name = "runCcheMeshBtn";
            this.runCcheMeshBtn.Size = new System.Drawing.Size(96, 26);
            this.runCcheMeshBtn.TabIndex = 1;
            this.runCcheMeshBtn.Text = "CCHE-Mesh";
            this.runCcheMeshBtn.UseVisualStyleBackColor = true;
            this.runCcheMeshBtn.Click += new System.EventHandler(this.runCcheMeshBtn_Click);
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.levelProportionBtn);
            this.flowTypeGroup.Controls.Add(this.verticalLevelNumberTxt);
            this.flowTypeGroup.Controls.Add(this.label1);
            this.flowTypeGroup.Controls.Add(this.inputFileBtn);
            this.flowTypeGroup.Controls.Add(this.reverseGridBtn);
            this.flowTypeGroup.Controls.Add(this.gridDataBtn);
            this.flowTypeGroup.Controls.Add(this.inputGridBtn);
            this.flowTypeGroup.Controls.Add(this.inputGridRdo);
            this.flowTypeGroup.Controls.Add(this.inputFileRdo);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 162);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(472, 137);
            this.flowTypeGroup.TabIndex = 0;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "計算網格來源";
            // 
            // levelProportionBtn
            // 
            this.levelProportionBtn.Location = new System.Drawing.Point(253, 96);
            this.levelProportionBtn.Name = "levelProportionBtn";
            this.levelProportionBtn.Size = new System.Drawing.Size(96, 26);
            this.levelProportionBtn.TabIndex = 4;
            this.levelProportionBtn.Text = "分層比例";
            this.levelProportionBtn.UseVisualStyleBackColor = true;
            this.levelProportionBtn.Click += new System.EventHandler(this.separateProportionBtn_Click);
            this.levelProportionBtn.MouseHover += new System.EventHandler(this.showSeparateCtrls_MouseHover);
            // 
            // verticalLevelNumberTxt
            // 
            this.verticalLevelNumberTxt.Location = new System.Drawing.Point(151, 100);
            this.verticalLevelNumberTxt.Name = "verticalLevelNumberTxt";
            this.verticalLevelNumberTxt.Size = new System.Drawing.Size(96, 22);
            this.verticalLevelNumberTxt.TabIndex = 3;
            this.verticalLevelNumberTxt.MouseHover += new System.EventHandler(this.showSeparateCtrls_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "垂向格網分層數目";
            this.label1.MouseHover += new System.EventHandler(this.showSeparateCtrls_MouseHover);
            // 
            // inputFileBtn
            // 
            this.inputFileBtn.Enabled = false;
            this.inputFileBtn.Location = new System.Drawing.Point(151, 25);
            this.inputFileBtn.Name = "inputFileBtn";
            this.inputFileBtn.Size = new System.Drawing.Size(96, 26);
            this.inputFileBtn.TabIndex = 1;
            this.inputFileBtn.Text = "選取檔案";
            this.inputFileBtn.UseVisualStyleBackColor = true;
            this.inputFileBtn.Click += new System.EventHandler(this.inputFileBtn_Click);
            // 
            // reverseGridBtn
            // 
            this.reverseGridBtn.Enabled = false;
            this.reverseGridBtn.Location = new System.Drawing.Point(253, 62);
            this.reverseGridBtn.Name = "reverseGridBtn";
            this.reverseGridBtn.Size = new System.Drawing.Size(96, 26);
            this.reverseGridBtn.TabIndex = 1;
            this.reverseGridBtn.Text = "反轉格網";
            this.reverseGridBtn.UseVisualStyleBackColor = true;
            this.reverseGridBtn.Click += new System.EventHandler(this.reverseGridBtn_Click);
            // 
            // gridDataBtn
            // 
            this.gridDataBtn.Enabled = false;
            this.gridDataBtn.Location = new System.Drawing.Point(253, 25);
            this.gridDataBtn.Name = "gridDataBtn";
            this.gridDataBtn.Size = new System.Drawing.Size(96, 26);
            this.gridDataBtn.TabIndex = 1;
            this.gridDataBtn.Text = "格網資料";
            this.gridDataBtn.UseVisualStyleBackColor = true;
            this.gridDataBtn.Click += new System.EventHandler(this.inputGridBtn_Click);
            // 
            // inputGridBtn
            // 
            this.inputGridBtn.Enabled = false;
            this.inputGridBtn.Location = new System.Drawing.Point(151, 62);
            this.inputGridBtn.Name = "inputGridBtn";
            this.inputGridBtn.Size = new System.Drawing.Size(96, 26);
            this.inputGridBtn.TabIndex = 1;
            this.inputGridBtn.Text = "輸入格網";
            this.inputGridBtn.UseVisualStyleBackColor = true;
            this.inputGridBtn.Click += new System.EventHandler(this.inputGridBtn_Click);
            // 
            // inputGridRdo
            // 
            this.inputGridRdo.AutoSize = true;
            this.inputGridRdo.Location = new System.Drawing.Point(17, 67);
            this.inputGridRdo.Name = "inputGridRdo";
            this.inputGridRdo.Size = new System.Drawing.Size(119, 16);
            this.inputGridRdo.TabIndex = 0;
            this.inputGridRdo.Text = "線上輸入水平格網";
            this.inputGridRdo.UseVisualStyleBackColor = true;
            this.inputGridRdo.CheckedChanged += new System.EventHandler(this.inputGridRdo_CheckedChanged);
            this.inputGridRdo.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
            // 
            // inputFileRdo
            // 
            this.inputFileRdo.AutoSize = true;
            this.inputFileRdo.Location = new System.Drawing.Point(17, 30);
            this.inputFileRdo.Name = "inputFileRdo";
            this.inputFileRdo.Size = new System.Drawing.Size(131, 16);
            this.inputFileRdo.TabIndex = 0;
            this.inputFileRdo.Text = "由檔案匯入水平格網";
            this.inputFileRdo.UseVisualStyleBackColor = true;
            this.inputFileRdo.CheckedChanged += new System.EventHandler(this.inputFileRdo_CheckedChanged);
            this.inputFileRdo.MouseHover += new System.EventHandler(this.showGridMapCtrls_MouseHover);
            // 
            // runExcelBtn
            // 
            this.runExcelBtn.Enabled = false;
            this.runExcelBtn.Location = new System.Drawing.Point(17, 520);
            this.runExcelBtn.Name = "runExcelBtn";
            this.runExcelBtn.Size = new System.Drawing.Size(130, 26);
            this.runExcelBtn.TabIndex = 1;
            this.runExcelBtn.Text = "執行Excel";
            this.runExcelBtn.UseVisualStyleBackColor = true;
            this.runExcelBtn.Visible = false;
            this.runExcelBtn.Click += new System.EventHandler(this.runExcel_Click);
            // 
            // runCcheMeshBtn2
            // 
            this.runCcheMeshBtn2.Location = new System.Drawing.Point(153, 520);
            this.runCcheMeshBtn2.Name = "runCcheMeshBtn2";
            this.runCcheMeshBtn2.Size = new System.Drawing.Size(130, 26);
            this.runCcheMeshBtn2.TabIndex = 1;
            this.runCcheMeshBtn2.Text = "執行CCHE-Mesh";
            this.runCcheMeshBtn2.UseVisualStyleBackColor = true;
            this.runCcheMeshBtn2.Visible = false;
            this.runCcheMeshBtn2.Click += new System.EventHandler(this.runCcheMeshBtn_Click);
            // 
            // inputFileDlg
            // 
            this.inputFileDlg.Filter = "CCHE_MESH檔案(*.geo)|*.geo|文字檔案(*.txt)|*.txt|Excel檔案(*.xls)|*.xls|SMS檔案(*.sms)|*.sm" +
    "s";
            this.inputFileDlg.Title = "選取匯入格網檔案";
            // 
            // selectBgDlg
            // 
            this.selectBgDlg.Filter = "所有檔案|*.*|Bmp檔案|*.bmp|PNG檔案|*.png|JPG檔案|*jpg";
            // 
            // previewSpratePanel
            // 
            this.previewSpratePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewSpratePanel.Controls.Add(this.previewSpratePicBox);
            this.previewSpratePanel.Location = new System.Drawing.Point(706, 650);
            this.previewSpratePanel.Name = "previewSpratePanel";
            this.previewSpratePanel.Size = new System.Drawing.Size(59, 37);
            this.previewSpratePanel.TabIndex = 12;
            this.previewSpratePanel.Visible = false;
            // 
            // previewSpratePicBox
            // 
            this.previewSpratePicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewSpratePicBox.Location = new System.Drawing.Point(3, 3);
            this.previewSpratePicBox.Name = "previewSpratePicBox";
            this.previewSpratePicBox.Size = new System.Drawing.Size(53, 27);
            this.previewSpratePicBox.TabIndex = 13;
            this.previewSpratePicBox.TabStop = false;
            // 
            // previewCombo
            // 
            this.previewCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.previewCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.previewCombo.DropDownWidth = 168;
            this.previewCombo.FormattingEnabled = true;
            this.previewCombo.Location = new System.Drawing.Point(507, 652);
            this.previewCombo.Name = "previewCombo";
            this.previewCombo.Size = new System.Drawing.Size(193, 20);
            this.previewCombo.TabIndex = 13;
            this.previewCombo.SelectedIndexChanged += new System.EventHandler(this.previewCombo_SelectedIndexChanged);
            // 
            // mapPicBox
            // 
            this.mapPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPicBox.Border = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPicBox.Grid = null;
            this.mapPicBox.Location = new System.Drawing.Point(506, 252);
            this.mapPicBox.Name = "mapPicBox";
            this.mapPicBox.SelectedI = -1;
            this.mapPicBox.SelectGroup = false;
            this.mapPicBox.SelectRow = false;
            this.mapPicBox.Size = new System.Drawing.Size(438, 392);
            this.mapPicBox.TabIndex = 11;
            // 
            // coorSelCombo
            // 
            this.coorSelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coorSelCombo.FormattingEnabled = true;
            this.coorSelCombo.Items.AddRange(new object[] {
            "TWD97",
            "TWD67"});
            this.coorSelCombo.Location = new System.Drawing.Point(106, 56);
            this.coorSelCombo.Name = "coorSelCombo";
            this.coorSelCombo.Size = new System.Drawing.Size(121, 20);
            this.coorSelCombo.TabIndex = 4;
            this.coorSelCombo.SelectedIndexChanged += new System.EventHandler(this.coorSelCombo_SelectedIndexChanged);
            // 
            // ImportForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.Controls.Add(this.previewCombo);
            this.Controls.Add(this.previewSpratePanel);
            this.Controls.Add(this.mapPicBox);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.ok);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "計算格網";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.bitmapGrp.ResumeLayout(false);
            this.bitmapGrp.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flowTypeGroup.ResumeLayout(false);
            this.flowTypeGroup.PerformLayout();
            this.previewSpratePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewSpratePicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser comment;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox bitmapGrp;
        private System.Windows.Forms.Label selectBgFilePath;
        private System.Windows.Forms.Button selectBgBtn;
        private System.Windows.Forms.RadioButton noBgRdo;
        private System.Windows.Forms.RadioButton selectBgRdo;
        private System.Windows.Forms.RadioButton useGoogleBgRdo;
        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.Button inputFileBtn;
        private System.Windows.Forms.Button inputGridBtn;
        private System.Windows.Forms.Button runExcelBtn;
        private System.Windows.Forms.Button runCcheMeshBtn2;
        private System.Windows.Forms.RadioButton inputGridRdo;
        private System.Windows.Forms.RadioButton inputFileRdo;
        private System.Windows.Forms.OpenFileDialog inputFileDlg;
        private System.Windows.Forms.OpenFileDialog selectBgDlg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button runCcheMeshBtn;
        private System.Windows.Forms.Button levelProportionBtn;
        private System.Windows.Forms.TextBox verticalLevelNumberTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button imgInfoBtn;
        private PictureBoxCtrl.GridPictureBox mapPicBox;
        private System.Windows.Forms.Panel previewSpratePanel;
        private System.Windows.Forms.PictureBox previewSpratePicBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox previewCombo;
        private System.Windows.Forms.Button reverseGridBtn;
        private System.Windows.Forms.Button gridDataBtn;
        private System.Windows.Forms.ComboBox coorSelCombo;

    }
}