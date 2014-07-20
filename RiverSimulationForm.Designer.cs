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
            this.saveAsMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.langEngMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langTwMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationResultBtn = new System.Windows.Forms.Button();
            this.sampleDisableBtn = new System.Windows.Forms.Button();
            this.sampleReadyBtn = new System.Windows.Forms.Button();
            this.sampleFinishedBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.feedMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // movableBedBtn
            // 
            this.movableBedBtn.BackColor = System.Drawing.Color.LimeGreen;
            this.movableBedBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.movableBedBtn.Location = new System.Drawing.Point(497, 263);
            this.movableBedBtn.Name = "movableBedBtn";
            this.movableBedBtn.Size = new System.Drawing.Size(102, 64);
            this.movableBedBtn.TabIndex = 2;
            this.movableBedBtn.Text = "動床";
            this.movableBedBtn.UseVisualStyleBackColor = true;
            this.movableBedBtn.Click += new System.EventHandler(this.movableBedBtn_Click);
            // 
            // boundaryConditionsBtn
            // 
            this.boundaryConditionsBtn.BackColor = System.Drawing.Color.Goldenrod;
            this.boundaryConditionsBtn.Location = new System.Drawing.Point(497, 369);
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
            this.initialConditionsBtn.Location = new System.Drawing.Point(247, 369);
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
            this.waterModelingBtn.Location = new System.Drawing.Point(247, 263);
            this.waterModelingBtn.Name = "waterModelingBtn";
            this.waterModelingBtn.Size = new System.Drawing.Size(102, 64);
            this.waterModelingBtn.TabIndex = 5;
            this.waterModelingBtn.Text = "水理";
            this.waterModelingBtn.UseVisualStyleBackColor = true;
            this.waterModelingBtn.Click += new System.EventHandler(this.waterModelingBtn_Click);
            // 
            // runSimulationBtn
            // 
            this.runSimulationBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.runSimulationBtn.Location = new System.Drawing.Point(372, 475);
            this.runSimulationBtn.Name = "runSimulationBtn";
            this.runSimulationBtn.Size = new System.Drawing.Size(102, 64);
            this.runSimulationBtn.TabIndex = 6;
            this.runSimulationBtn.Text = "開始模擬";
            this.runSimulationBtn.UseVisualStyleBackColor = true;
            this.runSimulationBtn.Click += new System.EventHandler(this.runSimulationBtn_Click);
            // 
            // simulationModuleBtn
            // 
            this.simulationModuleBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.simulationModuleBtn.Location = new System.Drawing.Point(372, 157);
            this.simulationModuleBtn.Name = "simulationModuleBtn";
            this.simulationModuleBtn.Size = new System.Drawing.Size(102, 64);
            this.simulationModuleBtn.TabIndex = 7;
            this.simulationModuleBtn.Text = "模擬模組";
            this.simulationModuleBtn.UseVisualStyleBackColor = true;
            this.simulationModuleBtn.Click += new System.EventHandler(this.simulationModuleBtn_Click);
            // 
            // importBtn
            // 
            this.importBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.importBtn.Location = new System.Drawing.Point(372, 51);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(102, 64);
            this.importBtn.TabIndex = 8;
            this.importBtn.Text = "匯入";
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
            this.menuStrip1.Size = new System.Drawing.Size(847, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMnu
            // 
            this.fileMnu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMnuItem,
            this.openMnuItem,
            this.saveMnuItem,
            this.saveAsMnuItem,
            this.toolStripSeparator1,
            this.exitMnuItem});
            this.fileMnu.Name = "fileMnu";
            this.fileMnu.Size = new System.Drawing.Size(58, 20);
            this.fileMnu.Text = "檔案(F)";
            // 
            // newMnuItem
            // 
            this.newMnuItem.Name = "newMnuItem";
            this.newMnuItem.Size = new System.Drawing.Size(161, 22);
            this.newMnuItem.Text = "新增專案(N)";
            // 
            // openMnuItem
            // 
            this.openMnuItem.Name = "openMnuItem";
            this.openMnuItem.Size = new System.Drawing.Size(161, 22);
            this.openMnuItem.Text = "開啟專案(O)";
            // 
            // saveMnuItem
            // 
            this.saveMnuItem.Name = "saveMnuItem";
            this.saveMnuItem.Size = new System.Drawing.Size(161, 22);
            this.saveMnuItem.Text = "儲存專案(S)";
            // 
            // saveAsMnuItem
            // 
            this.saveAsMnuItem.Name = "saveAsMnuItem";
            this.saveAsMnuItem.Size = new System.Drawing.Size(161, 22);
            this.saveAsMnuItem.Text = "另存專案為(A)...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // exitMnuItem
            // 
            this.exitMnuItem.Name = "exitMnuItem";
            this.exitMnuItem.Size = new System.Drawing.Size(161, 22);
            this.exitMnuItem.Text = "結束(X)";
            this.exitMnuItem.Click += new System.EventHandler(this.exitMnuItem_Click);
            // 
            // languageMnu
            // 
            this.languageMnu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langEngMnuItem,
            this.langTwMnuItem});
            this.languageMnu.Name = "languageMnu";
            this.languageMnu.Size = new System.Drawing.Size(82, 20);
            this.languageMnu.Text = "語系切換(L)";
            // 
            // langEngMnuItem
            // 
            this.langEngMnuItem.Name = "langEngMnuItem";
            this.langEngMnuItem.Size = new System.Drawing.Size(139, 22);
            this.langEngMnuItem.Text = "English(E)";
            // 
            // langTwMnuItem
            // 
            this.langTwMnuItem.Name = "langTwMnuItem";
            this.langTwMnuItem.Size = new System.Drawing.Size(139, 22);
            this.langTwMnuItem.Text = "繁體中文(T)";
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
            // aboutMnuItem
            // 
            this.aboutMnuItem.Name = "aboutMnuItem";
            this.aboutMnuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutMnuItem.Text = "關於(A)";
            this.aboutMnuItem.Click += new System.EventHandler(this.aboutMnuItem_Click);
            // 
            // simulationResultBtn
            // 
            this.simulationResultBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.simulationResultBtn.Location = new System.Drawing.Point(372, 581);
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
            // feedMnuItem
            // 
            this.feedMnuItem.Name = "feedMnuItem";
            this.feedMnuItem.Size = new System.Drawing.Size(152, 22);
            this.feedMnuItem.Text = "問題回饋(F)";
            this.feedMnuItem.Click += new System.EventHandler(this.feedMnuItem_Click);
            // 
            // RiverSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 656);
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
        private System.Windows.Forms.ToolStripMenuItem saveAsMnuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMnuItem;
        private System.Windows.Forms.ToolStripMenuItem languageMnu;
        private System.Windows.Forms.ToolStripMenuItem langEngMnuItem;
        private System.Windows.Forms.ToolStripMenuItem langTwMnuItem;
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
    }
}