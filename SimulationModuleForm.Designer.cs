namespace RiverSimulationApplication
{
    partial class SimulationModuleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationModuleForm));
            this.ok = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.typeWaterModelingRdo = new System.Windows.Forms.CheckBox();
            this.typeMovableBedRdo = new System.Windows.Forms.CheckBox();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.type3dRdo = new System.Windows.Forms.CheckBox();
            this.type2dRdo = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.movableBedPanel = new System.Windows.Forms.Panel();
            this.bedrockChk = new System.Windows.Forms.CheckBox();
            this.quayStableAnalysisChk = new System.Windows.Forms.CheckBox();
            this.highSandContentFlowChk = new System.Windows.Forms.CheckBox();
            this.waterPanel = new System.Windows.Forms.Panel();
            this.fullPanel = new System.Windows.Forms.Panel();
            this.secFlowEffectChk = new System.Windows.Forms.CheckBox();
            this.diffusionEffectChk = new System.Windows.Forms.CheckBox();
            this.sideInOutFlowChk = new System.Windows.Forms.CheckBox();
            this.immersedBoundaryChk = new System.Windows.Forms.CheckBox();
            this.structureSetChk = new System.Windows.Forms.CheckBox();
            this.highSandContentEffectChk = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.movableBedPanel.SuspendLayout();
            this.waterPanel.SuspendLayout();
            this.fullPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(329, 519);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 8;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.groupBox1);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(441, 293);
            this.mainPanel.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.typeWaterModelingRdo);
            this.groupBox1.Controls.Add(this.typeMovableBedRdo);
            this.groupBox1.Location = new System.Drawing.Point(12, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模組選擇";
            // 
            // typeWaterModelingRdo
            // 
            this.typeWaterModelingRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.typeWaterModelingRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.typeWaterModelingRdo.Location = new System.Drawing.Point(33, 34);
            this.typeWaterModelingRdo.Name = "typeWaterModelingRdo";
            this.typeWaterModelingRdo.Size = new System.Drawing.Size(128, 80);
            this.typeWaterModelingRdo.TabIndex = 2;
            this.typeWaterModelingRdo.Text = "水理";
            this.typeWaterModelingRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.typeWaterModelingRdo.UseVisualStyleBackColor = true;
            this.typeWaterModelingRdo.CheckedChanged += new System.EventHandler(this.typeWaterModelingRdo_CheckedChanged);
            // 
            // typeMovableBedRdo
            // 
            this.typeMovableBedRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.typeMovableBedRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.typeMovableBedRdo.Location = new System.Drawing.Point(246, 34);
            this.typeMovableBedRdo.Name = "typeMovableBedRdo";
            this.typeMovableBedRdo.Size = new System.Drawing.Size(128, 80);
            this.typeMovableBedRdo.TabIndex = 2;
            this.typeMovableBedRdo.Text = "動床";
            this.typeMovableBedRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.typeMovableBedRdo.UseVisualStyleBackColor = true;
            this.typeMovableBedRdo.CheckedChanged += new System.EventHandler(this.typeMovableBedRdo_CheckedChanged);
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.type3dRdo);
            this.flowTypeGroup.Controls.Add(this.type2dRdo);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 11);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(414, 138);
            this.flowTypeGroup.TabIndex = 1;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "維度選擇";
            this.flowTypeGroup.Enter += new System.EventHandler(this.flowTypeGroup_Enter);
            // 
            // type3dRdo
            // 
            this.type3dRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.type3dRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.type3dRdo.Location = new System.Drawing.Point(246, 34);
            this.type3dRdo.Name = "type3dRdo";
            this.type3dRdo.Size = new System.Drawing.Size(128, 80);
            this.type3dRdo.TabIndex = 2;
            this.type3dRdo.Text = "3D";
            this.type3dRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.type3dRdo.UseVisualStyleBackColor = true;
            this.type3dRdo.CheckedChanged += new System.EventHandler(this.type3dRdo_CheckedChanged);
            // 
            // type2dRdo
            // 
            this.type2dRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.type2dRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.type2dRdo.Location = new System.Drawing.Point(33, 34);
            this.type2dRdo.Name = "type2dRdo";
            this.type2dRdo.Size = new System.Drawing.Size(128, 80);
            this.type2dRdo.TabIndex = 2;
            this.type2dRdo.Text = "2D";
            this.type2dRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.type2dRdo.UseVisualStyleBackColor = true;
            this.type2dRdo.CheckedChanged += new System.EventHandler(this.type2dRdo_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.movableBedPanel);
            this.groupBox2.Controls.Add(this.waterPanel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 205);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "特殊功能";
            // 
            // movableBedPanel
            // 
            this.movableBedPanel.Controls.Add(this.bedrockChk);
            this.movableBedPanel.Controls.Add(this.quayStableAnalysisChk);
            this.movableBedPanel.Controls.Add(this.highSandContentFlowChk);
            this.movableBedPanel.Enabled = false;
            this.movableBedPanel.Location = new System.Drawing.Point(214, 40);
            this.movableBedPanel.Name = "movableBedPanel";
            this.movableBedPanel.Size = new System.Drawing.Size(194, 150);
            this.movableBedPanel.TabIndex = 3;
            // 
            // bedrockChk
            // 
            this.bedrockChk.AutoSize = true;
            this.bedrockChk.Location = new System.Drawing.Point(17, 10);
            this.bedrockChk.Name = "bedrockChk";
            this.bedrockChk.Size = new System.Drawing.Size(48, 16);
            this.bedrockChk.TabIndex = 0;
            this.bedrockChk.Text = "岩床";
            this.bedrockChk.UseVisualStyleBackColor = true;
            this.bedrockChk.CheckedChanged += new System.EventHandler(this.bedrockChk_CheckedChanged);
            // 
            // quayStableAnalysisChk
            // 
            this.quayStableAnalysisChk.AutoSize = true;
            this.quayStableAnalysisChk.Location = new System.Drawing.Point(17, 32);
            this.quayStableAnalysisChk.Name = "quayStableAnalysisChk";
            this.quayStableAnalysisChk.Size = new System.Drawing.Size(96, 16);
            this.quayStableAnalysisChk.TabIndex = 0;
            this.quayStableAnalysisChk.Text = "岸壁穩定分析";
            this.quayStableAnalysisChk.UseVisualStyleBackColor = true;
            this.quayStableAnalysisChk.CheckedChanged += new System.EventHandler(this.quayStableAnalysisChk_CheckedChanged);
            // 
            // highSandContentFlowChk
            // 
            this.highSandContentFlowChk.AutoSize = true;
            this.highSandContentFlowChk.Location = new System.Drawing.Point(17, 54);
            this.highSandContentFlowChk.Name = "highSandContentFlowChk";
            this.highSandContentFlowChk.Size = new System.Drawing.Size(84, 16);
            this.highSandContentFlowChk.TabIndex = 0;
            this.highSandContentFlowChk.Text = "高含砂效應";
            this.highSandContentFlowChk.UseVisualStyleBackColor = true;
            this.highSandContentFlowChk.CheckedChanged += new System.EventHandler(this.highSandContentFlowChk_CheckedChanged);
            // 
            // waterPanel
            // 
            this.waterPanel.Controls.Add(this.fullPanel);
            this.waterPanel.Location = new System.Drawing.Point(6, 40);
            this.waterPanel.Name = "waterPanel";
            this.waterPanel.Size = new System.Drawing.Size(194, 150);
            this.waterPanel.TabIndex = 2;
            // 
            // fullPanel
            // 
            this.fullPanel.Controls.Add(this.secFlowEffectChk);
            this.fullPanel.Controls.Add(this.diffusionEffectChk);
            this.fullPanel.Controls.Add(this.sideInOutFlowChk);
            this.fullPanel.Controls.Add(this.immersedBoundaryChk);
            this.fullPanel.Controls.Add(this.structureSetChk);
            this.fullPanel.Controls.Add(this.highSandContentEffectChk);
            this.fullPanel.Location = new System.Drawing.Point(10, 3);
            this.fullPanel.Name = "fullPanel";
            this.fullPanel.Size = new System.Drawing.Size(181, 137);
            this.fullPanel.TabIndex = 1;
            // 
            // secFlowEffectChk
            // 
            this.secFlowEffectChk.AutoSize = true;
            this.secFlowEffectChk.Location = new System.Drawing.Point(3, 29);
            this.secFlowEffectChk.Name = "secFlowEffectChk";
            this.secFlowEffectChk.Size = new System.Drawing.Size(84, 16);
            this.secFlowEffectChk.TabIndex = 0;
            this.secFlowEffectChk.Text = "二次流效應";
            this.secFlowEffectChk.UseVisualStyleBackColor = true;
            this.secFlowEffectChk.CheckedChanged += new System.EventHandler(this.secFlowEffectChk_CheckedChanged);
            // 
            // diffusionEffectChk
            // 
            this.diffusionEffectChk.AutoSize = true;
            this.diffusionEffectChk.Location = new System.Drawing.Point(3, 7);
            this.diffusionEffectChk.Name = "diffusionEffectChk";
            this.diffusionEffectChk.Size = new System.Drawing.Size(120, 16);
            this.diffusionEffectChk.TabIndex = 0;
            this.diffusionEffectChk.Text = "關閉移流擴散效應";
            this.diffusionEffectChk.UseVisualStyleBackColor = true;
            this.diffusionEffectChk.CheckedChanged += new System.EventHandler(this.diffusionEffectChk_CheckedChanged);
            // 
            // sideInOutFlowChk
            // 
            this.sideInOutFlowChk.AutoSize = true;
            this.sideInOutFlowChk.Enabled = false;
            this.sideInOutFlowChk.Location = new System.Drawing.Point(3, 73);
            this.sideInOutFlowChk.Name = "sideInOutFlowChk";
            this.sideInOutFlowChk.Size = new System.Drawing.Size(75, 16);
            this.sideInOutFlowChk.TabIndex = 0;
            this.sideInOutFlowChk.Text = "側出/入流";
            this.sideInOutFlowChk.UseVisualStyleBackColor = true;
            this.sideInOutFlowChk.CheckedChanged += new System.EventHandler(this.sideInOutFlowChk_CheckedChanged);
            // 
            // immersedBoundaryChk
            // 
            this.immersedBoundaryChk.AutoSize = true;
            this.immersedBoundaryChk.Enabled = false;
            this.immersedBoundaryChk.Location = new System.Drawing.Point(3, 116);
            this.immersedBoundaryChk.Name = "immersedBoundaryChk";
            this.immersedBoundaryChk.Size = new System.Drawing.Size(140, 16);
            this.immersedBoundaryChk.TabIndex = 0;
            this.immersedBoundaryChk.Text = "結構物設置(浸沒邊界)";
            this.immersedBoundaryChk.UseVisualStyleBackColor = true;
            this.immersedBoundaryChk.Visible = false;
            // 
            // structureSetChk
            // 
            this.structureSetChk.AutoSize = true;
            this.structureSetChk.Location = new System.Drawing.Point(3, 51);
            this.structureSetChk.Name = "structureSetChk";
            this.structureSetChk.Size = new System.Drawing.Size(84, 16);
            this.structureSetChk.TabIndex = 0;
            this.structureSetChk.Text = "結構物設置";
            this.structureSetChk.UseVisualStyleBackColor = true;
            this.structureSetChk.CheckedChanged += new System.EventHandler(this.structureSetChk_CheckedChanged);
            // 
            // highSandContentEffectChk
            // 
            this.highSandContentEffectChk.AutoSize = true;
            this.highSandContentEffectChk.Enabled = false;
            this.highSandContentEffectChk.Location = new System.Drawing.Point(3, 95);
            this.highSandContentEffectChk.Name = "highSandContentEffectChk";
            this.highSandContentEffectChk.Size = new System.Drawing.Size(84, 16);
            this.highSandContentEffectChk.TabIndex = 0;
            this.highSandContentEffectChk.Text = "高含砂效應";
            this.highSandContentEffectChk.UseVisualStyleBackColor = true;
            this.highSandContentEffectChk.CheckedChanged += new System.EventHandler(this.highSandContentEffectChk_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "動床";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "水理";
            // 
            // SimulationModuleForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 563);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimulationModuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模擬功能";
            this.Load += new System.EventHandler(this.SimulationModuleForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowTypeGroup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.movableBedPanel.ResumeLayout(false);
            this.movableBedPanel.PerformLayout();
            this.waterPanel.ResumeLayout(false);
            this.fullPanel.ResumeLayout(false);
            this.fullPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox type3dRdo;
        private System.Windows.Forms.CheckBox type2dRdo;
        private System.Windows.Forms.CheckBox typeWaterModelingRdo;
        private System.Windows.Forms.CheckBox typeMovableBedRdo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel movableBedPanel;
        private System.Windows.Forms.CheckBox bedrockChk;
        private System.Windows.Forms.CheckBox quayStableAnalysisChk;
        private System.Windows.Forms.CheckBox highSandContentFlowChk;
        private System.Windows.Forms.Panel waterPanel;
        private System.Windows.Forms.CheckBox diffusionEffectChk;
        private System.Windows.Forms.CheckBox secFlowEffectChk;
        private System.Windows.Forms.CheckBox structureSetChk;
        private System.Windows.Forms.CheckBox highSandContentEffectChk;
        private System.Windows.Forms.CheckBox immersedBoundaryChk;
        private System.Windows.Forms.CheckBox sideInOutFlowChk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel fullPanel;
    }
}