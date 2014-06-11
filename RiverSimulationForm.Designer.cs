namespace RiverSimulationApplication
{
    partial class RiverSimulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RiverSimulation));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增專案NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開起專案OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.儲存專案SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存專案為AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.結束XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.語系切換LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.繁體中文TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.說明HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關於AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.import = new System.Windows.Forms.Button();
            this.simulationModule = new System.Windows.Forms.Button();
            this.waterModeling = new System.Windows.Forms.Button();
            this.movableBed = new System.Windows.Forms.Button();
            this.initialConditions = new System.Windows.Forms.Button();
            this.boundaryConditions = new System.Windows.Forms.Button();
            this.runSimulation = new System.Windows.Forms.Button();
            this.simulationResult = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案FToolStripMenuItem,
            this.語系切換LToolStripMenuItem,
            this.說明HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案FToolStripMenuItem
            // 
            this.檔案FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增專案NToolStripMenuItem,
            this.開起專案OToolStripMenuItem,
            this.儲存專案SToolStripMenuItem,
            this.另存專案為AToolStripMenuItem,
            this.toolStripSeparator1,
            this.結束XToolStripMenuItem});
            this.檔案FToolStripMenuItem.Name = "檔案FToolStripMenuItem";
            this.檔案FToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.檔案FToolStripMenuItem.Text = "檔案(F)";
            // 
            // 新增專案NToolStripMenuItem
            // 
            this.新增專案NToolStripMenuItem.Name = "新增專案NToolStripMenuItem";
            this.新增專案NToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.新增專案NToolStripMenuItem.Text = "新增專案(N)";
            // 
            // 開起專案OToolStripMenuItem
            // 
            this.開起專案OToolStripMenuItem.Name = "開起專案OToolStripMenuItem";
            this.開起專案OToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.開起專案OToolStripMenuItem.Text = "開起專案(O)";
            // 
            // 儲存專案SToolStripMenuItem
            // 
            this.儲存專案SToolStripMenuItem.Name = "儲存專案SToolStripMenuItem";
            this.儲存專案SToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.儲存專案SToolStripMenuItem.Text = "儲存專案(S)";
            // 
            // 另存專案為AToolStripMenuItem
            // 
            this.另存專案為AToolStripMenuItem.Name = "另存專案為AToolStripMenuItem";
            this.另存專案為AToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.另存專案為AToolStripMenuItem.Text = "另存專案為(A)...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // 結束XToolStripMenuItem
            // 
            this.結束XToolStripMenuItem.Name = "結束XToolStripMenuItem";
            this.結束XToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.結束XToolStripMenuItem.Text = "結束(X)";
            // 
            // 語系切換LToolStripMenuItem
            // 
            this.語系切換LToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eToolStripMenuItem,
            this.繁體中文TToolStripMenuItem});
            this.語系切換LToolStripMenuItem.Name = "語系切換LToolStripMenuItem";
            this.語系切換LToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.語系切換LToolStripMenuItem.Text = "語系切換(L)";
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.eToolStripMenuItem.Text = "English(E)";
            this.eToolStripMenuItem.Click += new System.EventHandler(this.eToolStripMenuItem_Click);
            // 
            // 繁體中文TToolStripMenuItem
            // 
            this.繁體中文TToolStripMenuItem.Name = "繁體中文TToolStripMenuItem";
            this.繁體中文TToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.繁體中文TToolStripMenuItem.Text = "繁體中文(T)";
            // 
            // 說明HToolStripMenuItem
            // 
            this.說明HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.關於AToolStripMenuItem});
            this.說明HToolStripMenuItem.Name = "說明HToolStripMenuItem";
            this.說明HToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.說明HToolStripMenuItem.Text = "說明(H)";
            // 
            // 關於AToolStripMenuItem
            // 
            this.關於AToolStripMenuItem.Name = "關於AToolStripMenuItem";
            this.關於AToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.關於AToolStripMenuItem.Text = "關於(A)";
            // 
            // import
            // 
            this.import.BackColor = System.Drawing.Color.Red;
            this.import.Location = new System.Drawing.Point(354, 90);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(102, 64);
            this.import.TabIndex = 1;
            this.import.Text = "匯入";
            this.import.UseVisualStyleBackColor = false;
            // 
            // simulationModule
            // 
            this.simulationModule.Location = new System.Drawing.Point(354, 192);
            this.simulationModule.Name = "simulationModule";
            this.simulationModule.Size = new System.Drawing.Size(102, 64);
            this.simulationModule.TabIndex = 1;
            this.simulationModule.Text = "模擬模組";
            this.simulationModule.UseVisualStyleBackColor = true;
            // 
            // waterModeling
            // 
            this.waterModeling.Location = new System.Drawing.Point(229, 294);
            this.waterModeling.Name = "waterModeling";
            this.waterModeling.Size = new System.Drawing.Size(102, 64);
            this.waterModeling.TabIndex = 1;
            this.waterModeling.Text = "水理";
            this.waterModeling.UseVisualStyleBackColor = true;
            this.waterModeling.Click += new System.EventHandler(this.waterModeling_Click);
            // 
            // movableBed
            // 
            this.movableBed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.movableBed.Location = new System.Drawing.Point(479, 294);
            this.movableBed.Name = "movableBed";
            this.movableBed.Size = new System.Drawing.Size(102, 64);
            this.movableBed.TabIndex = 1;
            this.movableBed.Text = "動床";
            this.movableBed.UseVisualStyleBackColor = true;
            // 
            // initialConditions
            // 
            this.initialConditions.Location = new System.Drawing.Point(229, 396);
            this.initialConditions.Name = "initialConditions";
            this.initialConditions.Size = new System.Drawing.Size(102, 64);
            this.initialConditions.TabIndex = 1;
            this.initialConditions.Text = "初始條件";
            this.initialConditions.UseVisualStyleBackColor = true;
            // 
            // boundaryConditions
            // 
            this.boundaryConditions.Location = new System.Drawing.Point(479, 396);
            this.boundaryConditions.Name = "boundaryConditions";
            this.boundaryConditions.Size = new System.Drawing.Size(102, 64);
            this.boundaryConditions.TabIndex = 1;
            this.boundaryConditions.Text = "邊界條件";
            this.boundaryConditions.UseVisualStyleBackColor = true;
            // 
            // runSimulation
            // 
            this.runSimulation.Location = new System.Drawing.Point(354, 498);
            this.runSimulation.Name = "runSimulation";
            this.runSimulation.Size = new System.Drawing.Size(102, 64);
            this.runSimulation.TabIndex = 1;
            this.runSimulation.Text = "開始模擬";
            this.runSimulation.UseVisualStyleBackColor = true;
            // 
            // simulationResult
            // 
            this.simulationResult.Location = new System.Drawing.Point(354, 600);
            this.simulationResult.Name = "simulationResult";
            this.simulationResult.Size = new System.Drawing.Size(102, 64);
            this.simulationResult.TabIndex = 1;
            this.simulationResult.Text = "模擬結果展示";
            this.simulationResult.UseVisualStyleBackColor = true;
            // 
            // RiverSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(808, 682);
            this.Controls.Add(this.movableBed);
            this.Controls.Add(this.boundaryConditions);
            this.Controls.Add(this.initialConditions);
            this.Controls.Add(this.waterModeling);
            this.Controls.Add(this.simulationResult);
            this.Controls.Add(this.runSimulation);
            this.Controls.Add(this.simulationModule);
            this.Controls.Add(this.import);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RiverSimulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水理動床模式使用者介面";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增專案NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開起專案OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 儲存專案SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存專案為AToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 結束XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 語系切換LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 說明HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關於AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 繁體中文TToolStripMenuItem;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.Button simulationModule;
        private System.Windows.Forms.Button waterModeling;
        private System.Windows.Forms.Button movableBed;
        private System.Windows.Forms.Button initialConditions;
        private System.Windows.Forms.Button boundaryConditions;
        private System.Windows.Forms.Button runSimulation;
        private System.Windows.Forms.Button simulationResult;
    }
}

