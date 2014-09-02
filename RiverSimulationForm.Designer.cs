namespace RiverSimulationApplication
{
    partial class RiverSimulationForm
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
            this.movableBedBtn = new System.Windows.Forms.Button();
            this.boundaryConditionsBtn = new System.Windows.Forms.Button();
            this.initialConditionsBtn = new System.Windows.Forms.Button();
            this.waterModelingBtn = new System.Windows.Forms.Button();
            this.runSimulationBtn = new System.Windows.Forms.Button();
            this.simulationModuleBtn = new System.Windows.Forms.Button();
            this.importBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.langEngMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.feedMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationResultBtn = new System.Windows.Forms.Button();
            this.sampleDisableBtn = new System.Windows.Forms.Button();
            this.sampleReadyBtn = new System.Windows.Forms.Button();
            this.sampleFinishedBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // movableBedBtn
            // 
            this.movableBedBtn.BackColor = System.Drawing.Color.LimeGreen;
            this.movableBedBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.movableBedBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.movableBedBtn.Location = new System.Drawing.Point(497, 250);
            this.movableBedBtn.Name = "movableBedBtn";
            this.movableBedBtn.Size = new System.Drawing.Size(102, 64);
            this.movableBedBtn.TabIndex = 2;
            this.movableBedBtn.Text = "動床參數";
            this.movableBedBtn.UseVisualStyleBackColor = true;
            this.movableBedBtn.Click += new System.EventHandler(this.movableBedBtn_Click);
            // 
            // boundaryConditionsBtn
            // 
            this.boundaryConditionsBtn.BackColor = System.Drawing.Color.Goldenrod;
            this.boundaryConditionsBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.boundaryConditionsBtn.Location = new System.Drawing.Point(497, 374);
            this.boundaryConditionsBtn.Name = "boundaryConditionsBtn";
            this.boundaryConditionsBtn.Size = new System.Drawing.Size(102, 64);
            this.boundaryConditionsBtn.TabIndex = 3;
            this.boundaryConditionsBtn.Text = "邊界條件";
            this.boundaryConditionsBtn.UseVisualStyleBackColor = true;
            this.boundaryConditionsBtn.Click += new System.EventHandler(this.boundaryConditionsBtn_Click);
            // 
            // initialConditionsBtn
            // 
            this.initialConditionsBtn.BackColor = System.Drawing.Color.Goldenrod;
            this.initialConditionsBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.initialConditionsBtn.Location = new System.Drawing.Point(247, 374);
            this.initialConditionsBtn.Name = "initialConditionsBtn";
            this.initialConditionsBtn.Size = new System.Drawing.Size(102, 64);
            this.initialConditionsBtn.TabIndex = 4;
            this.initialConditionsBtn.Text = "初始條件";
            this.initialConditionsBtn.UseVisualStyleBackColor = true;
            this.initialConditionsBtn.Click += new System.EventHandler(this.initialConditionsBtn_Click);
            // 
            // waterModelingBtn
            // 
            this.waterModelingBtn.BackColor = System.Drawing.Color.LimeGreen;
            this.waterModelingBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.waterModelingBtn.Location = new System.Drawing.Point(247, 250);
            this.waterModelingBtn.Name = "waterModelingBtn";
            this.waterModelingBtn.Size = new System.Drawing.Size(102, 64);
            this.waterModelingBtn.TabIndex = 5;
            this.waterModelingBtn.Text = "水理參數";
            this.waterModelingBtn.UseVisualStyleBackColor = true;
            this.waterModelingBtn.Click += new System.EventHandler(this.waterModelingBtn_Click);
            // 
            // runSimulationBtn
            // 
            this.runSimulationBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.runSimulationBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.runSimulationBtn.Location = new System.Drawing.Point(372, 481);
            this.runSimulationBtn.Name = "runSimulationBtn";
            this.runSimulationBtn.Size = new System.Drawing.Size(102, 64);
            this.runSimulationBtn.TabIndex = 6;
            this.runSimulationBtn.Text = "模擬作業";
            this.runSimulationBtn.UseVisualStyleBackColor = true;
            this.runSimulationBtn.Click += new System.EventHandler(this.runSimulationBtn_Click);
            // 
            // simulationModuleBtn
            // 
            this.simulationModuleBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.simulationModuleBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.simulationModuleBtn.Location = new System.Drawing.Point(372, 47);
            this.simulationModuleBtn.Name = "simulationModuleBtn";
            this.simulationModuleBtn.Size = new System.Drawing.Size(102, 64);
            this.simulationModuleBtn.TabIndex = 7;
            this.simulationModuleBtn.Text = "模擬功能";
            this.simulationModuleBtn.UseVisualStyleBackColor = true;
            this.simulationModuleBtn.Click += new System.EventHandler(this.simulationModuleBtn_Click);
            // 
            // importBtn
            // 
            this.importBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.importBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.importBtn.Location = new System.Drawing.Point(372, 143);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(102, 64);
            this.importBtn.TabIndex = 8;
            this.importBtn.Text = "計算格網";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMnu,
            this.languageMnu,
            this.helpMnu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(844, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMnu
            // 
            this.fileMnu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMnuItem,
            this.openMnuItem,
            this.saveMnuItem,
            this.toolStripSeparator1,
            this.exitMnuItem});
            this.fileMnu.Name = "fileMnu";
            this.fileMnu.Size = new System.Drawing.Size(58, 20);
            this.fileMnu.Text = "檔案(F)";
            // 
            // newMnuItem
            // 
            this.newMnuItem.Name = "newMnuItem";
            this.newMnuItem.Size = new System.Drawing.Size(152, 22);
            this.newMnuItem.Text = "開新檔案(N)";
            // 
            // openMnuItem
            // 
            this.openMnuItem.Name = "openMnuItem";
            this.openMnuItem.Size = new System.Drawing.Size(152, 22);
            this.openMnuItem.Text = "開啟舊檔(O)";
            // 
            // saveMnuItem
            // 
            this.saveMnuItem.Name = "saveMnuItem";
            this.saveMnuItem.Size = new System.Drawing.Size(152, 22);
            this.saveMnuItem.Text = "儲存檔案(S)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitMnuItem
            // 
            this.exitMnuItem.Name = "exitMnuItem";
            this.exitMnuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMnuItem.Text = "結束(X)";
            this.exitMnuItem.Click += new System.EventHandler(this.exitMnuItem_Click);
            // 
            // languageMnu
            // 
            this.languageMnu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langEngMnuItem});
            this.languageMnu.Name = "languageMnu";
            this.languageMnu.Size = new System.Drawing.Size(82, 20);
            this.languageMnu.Text = "語系切換(L)";
            // 
            // langEngMnuItem
            // 
            this.langEngMnuItem.Name = "langEngMnuItem";
            this.langEngMnuItem.Size = new System.Drawing.Size(152, 22);
            this.langEngMnuItem.Text = "英文(E)";
            // 
            // helpMnu
            // 
            this.helpMnu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.feedMnuItem,
            this.aboutMnuItem});
            this.helpMnu.Name = "helpMnu";
            this.helpMnu.Size = new System.Drawing.Size(61, 20);
            this.helpMnu.Text = "說明(H)";
            // 
            // feedMnuItem
            // 
            this.feedMnuItem.Name = "feedMnuItem";
            this.feedMnuItem.Size = new System.Drawing.Size(152, 22);
            this.feedMnuItem.Text = "問題回饋(F)";
            this.feedMnuItem.Click += new System.EventHandler(this.feedMnuItem_Click);
            // 
            // aboutMnuItem
            // 
            this.aboutMnuItem.Name = "aboutMnuItem";
            this.aboutMnuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutMnuItem.Text = "版本說明(A)";
            this.aboutMnuItem.Click += new System.EventHandler(this.aboutMnuItem_Click);
            // 
            // simulationResultBtn
            // 
            this.simulationResultBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.simulationResultBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.simulationResultBtn.Location = new System.Drawing.Point(372, 577);
            this.simulationResultBtn.Name = "simulationResultBtn";
            this.simulationResultBtn.Size = new System.Drawing.Size(102, 64);
            this.simulationResultBtn.TabIndex = 6;
            this.simulationResultBtn.Text = "模擬結果";
            this.simulationResultBtn.UseVisualStyleBackColor = true;
            this.simulationResultBtn.Click += new System.EventHandler(this.runSimulationResultBtn_Click);
            // 
            // sampleDisableBtn
            // 
            this.sampleDisableBtn.Location = new System.Drawing.Point(13, 51);
            this.sampleDisableBtn.Name = "sampleDisableBtn";
            this.sampleDisableBtn.Size = new System.Drawing.Size(36, 23);
            this.sampleDisableBtn.TabIndex = 10;
            this.sampleDisableBtn.UseVisualStyleBackColor = true;
            this.sampleDisableBtn.Visible = false;
            // 
            // sampleReadyBtn
            // 
            this.sampleReadyBtn.Location = new System.Drawing.Point(13, 80);
            this.sampleReadyBtn.Name = "sampleReadyBtn";
            this.sampleReadyBtn.Size = new System.Drawing.Size(36, 23);
            this.sampleReadyBtn.TabIndex = 10;
            this.sampleReadyBtn.UseVisualStyleBackColor = true;
            this.sampleReadyBtn.Visible = false;
            // 
            // sampleFinishedBtn
            // 
            this.sampleFinishedBtn.Location = new System.Drawing.Point(13, 109);
            this.sampleFinishedBtn.Name = "sampleFinishedBtn";
            this.sampleFinishedBtn.Size = new System.Drawing.Size(36, 23);
            this.sampleFinishedBtn.TabIndex = 10;
            this.sampleFinishedBtn.UseVisualStyleBackColor = true;
            this.sampleFinishedBtn.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "無法使用，尚有其他參數未完成";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "未設定完成參數，請進入設定";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "已設定完成參數，可進入修改";
            this.label3.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(606, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 175);
            this.panel1.TabIndex = 12;
            this.panel1.Visible = false;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(3, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(226, 23);
            this.label17.TabIndex = 1;
            this.label17.Text = "尚未設定完成進行模擬作業";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(62, 126);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(101, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "水理模式尚未設定";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(62, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "U - 模式尚未設定";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(62, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "不須設定";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(62, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "數值參數尚未設定";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(62, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "計算格網來源尚未設定";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 126);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "邊界條件";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "初始條件";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "動床參數";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "水理參數";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "計算格網";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(62, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "已設定完成";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "模擬功能";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "參數設定狀況";
            // 
            // RiverSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 656);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sampleFinishedBtn);
            this.Controls.Add(this.sampleReadyBtn);
            this.Controls.Add(this.sampleDisableBtn);
            this.Controls.Add(this.movableBedBtn);
            this.Controls.Add(this.boundaryConditionsBtn);
            this.Controls.Add(this.initialConditionsBtn);
            this.Controls.Add(this.waterModelingBtn);
            this.Controls.Add(this.simulationResultBtn);
            this.Controls.Add(this.runSimulationBtn);
            this.Controls.Add(this.simulationModuleBtn);
            this.Controls.Add(this.importBtn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RiverSimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水理動床模式使用者介面";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RiverSimulationForm_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button movableBedBtn;
        private System.Windows.Forms.Button boundaryConditionsBtn;
        private System.Windows.Forms.Button initialConditionsBtn;
        private System.Windows.Forms.Button waterModelingBtn;
        private System.Windows.Forms.Button runSimulationBtn;
        private System.Windows.Forms.Button simulationModuleBtn;
        private System.Windows.Forms.Button importBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMnu;
        private System.Windows.Forms.ToolStripMenuItem newMnuItem;
        private System.Windows.Forms.ToolStripMenuItem openMnuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMnuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMnuItem;
        private System.Windows.Forms.ToolStripMenuItem languageMnu;
        private System.Windows.Forms.ToolStripMenuItem langEngMnuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMnu;
        private System.Windows.Forms.ToolStripMenuItem aboutMnuItem;
        private System.Windows.Forms.Button simulationResultBtn;
        private System.Windows.Forms.Button sampleDisableBtn;
        private System.Windows.Forms.Button sampleReadyBtn;
        private System.Windows.Forms.Button sampleFinishedBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem feedMnuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label17;
    }
}