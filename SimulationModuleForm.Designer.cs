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
            this.movableBedRdo = new System.Windows.Forms.RadioButton();
            this.waterModelingRdo = new System.Windows.Forms.RadioButton();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.dimension3dRdo = new System.Windows.Forms.RadioButton();
            this.dimension2dRdo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.movableBedPanel = new System.Windows.Forms.Panel();
            this.bedrockFunctionChk = new System.Windows.Forms.CheckBox();
            this.quayStableAnalysisFunctionChk = new System.Windows.Forms.CheckBox();
            this.movableBedHighSandContentEffectFunctionChk = new System.Windows.Forms.CheckBox();
            this.waterPanel = new System.Windows.Forms.Panel();
            this.fullPanel = new System.Windows.Forms.Panel();
            this.secondFlowEffectFunctionChk = new System.Windows.Forms.CheckBox();
            this.closeDiffusionEffectFunctionChk = new System.Windows.Forms.CheckBox();
            this.sideInOutFlowFunctionChk = new System.Windows.Forms.CheckBox();
            this.structureSetFunctionChk = new System.Windows.Forms.CheckBox();
            this.waterHighSandContentEffectFunctionChk = new System.Windows.Forms.CheckBox();
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
            this.mainPanel.Size = new System.Drawing.Size(440, 294);
            this.mainPanel.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.movableBedRdo);
            this.groupBox1.Controls.Add(this.waterModelingRdo);
            this.groupBox1.Location = new System.Drawing.Point(12, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模組選擇";
            // 
            // movableBedRdo
            // 
            this.movableBedRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.movableBedRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.movableBedRdo.Location = new System.Drawing.Point(246, 34);
            this.movableBedRdo.Name = "movableBedRdo";
            this.movableBedRdo.Size = new System.Drawing.Size(128, 80);
            this.movableBedRdo.TabIndex = 3;
            this.movableBedRdo.TabStop = true;
            this.movableBedRdo.Text = "動床";
            this.movableBedRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.movableBedRdo.UseVisualStyleBackColor = true;
            this.movableBedRdo.CheckedChanged += new System.EventHandler(this.modelingRdo_CheckedChanged);
            // 
            // waterModelingRdo
            // 
            this.waterModelingRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.waterModelingRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.waterModelingRdo.Location = new System.Drawing.Point(33, 34);
            this.waterModelingRdo.Name = "waterModelingRdo";
            this.waterModelingRdo.Size = new System.Drawing.Size(128, 80);
            this.waterModelingRdo.TabIndex = 3;
            this.waterModelingRdo.TabStop = true;
            this.waterModelingRdo.Text = "水理";
            this.waterModelingRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.waterModelingRdo.UseVisualStyleBackColor = true;
            this.waterModelingRdo.CheckedChanged += new System.EventHandler(this.modelingRdo_CheckedChanged);
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.dimension3dRdo);
            this.flowTypeGroup.Controls.Add(this.dimension2dRdo);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 11);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(414, 138);
            this.flowTypeGroup.TabIndex = 1;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "維度選擇";
            // 
            // dimension3dRdo
            // 
            this.dimension3dRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.dimension3dRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dimension3dRdo.Location = new System.Drawing.Point(246, 34);
            this.dimension3dRdo.Name = "dimension3dRdo";
            this.dimension3dRdo.Size = new System.Drawing.Size(128, 80);
            this.dimension3dRdo.TabIndex = 3;
            this.dimension3dRdo.TabStop = true;
            this.dimension3dRdo.Text = "3D";
            this.dimension3dRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dimension3dRdo.UseVisualStyleBackColor = true;
            this.dimension3dRdo.CheckedChanged += new System.EventHandler(this.dimensionRdo_CheckedChanged);
            // 
            // dimension2dRdo
            // 
            this.dimension2dRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.dimension2dRdo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dimension2dRdo.Location = new System.Drawing.Point(33, 34);
            this.dimension2dRdo.Name = "dimension2dRdo";
            this.dimension2dRdo.Size = new System.Drawing.Size(128, 80);
            this.dimension2dRdo.TabIndex = 3;
            this.dimension2dRdo.TabStop = true;
            this.dimension2dRdo.Text = "2D";
            this.dimension2dRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dimension2dRdo.UseVisualStyleBackColor = true;
            this.dimension2dRdo.CheckedChanged += new System.EventHandler(this.dimensionRdo_CheckedChanged);
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
            this.movableBedPanel.Controls.Add(this.bedrockFunctionChk);
            this.movableBedPanel.Controls.Add(this.quayStableAnalysisFunctionChk);
            this.movableBedPanel.Controls.Add(this.movableBedHighSandContentEffectFunctionChk);
            this.movableBedPanel.Enabled = false;
            this.movableBedPanel.Location = new System.Drawing.Point(214, 40);
            this.movableBedPanel.Name = "movableBedPanel";
            this.movableBedPanel.Size = new System.Drawing.Size(194, 150);
            this.movableBedPanel.TabIndex = 3;
            // 
            // bedrockFunctionChk
            // 
            this.bedrockFunctionChk.AutoSize = true;
            this.bedrockFunctionChk.Location = new System.Drawing.Point(17, 10);
            this.bedrockFunctionChk.Name = "bedrockFunctionChk";
            this.bedrockFunctionChk.Size = new System.Drawing.Size(48, 16);
            this.bedrockFunctionChk.TabIndex = 0;
            this.bedrockFunctionChk.Text = "岩床";
            this.bedrockFunctionChk.UseVisualStyleBackColor = true;
            this.bedrockFunctionChk.CheckedChanged += new System.EventHandler(this.bedrockFunctionChk_CheckedChanged);
            // 
            // quayStableAnalysisFunctionChk
            // 
            this.quayStableAnalysisFunctionChk.AutoSize = true;
            this.quayStableAnalysisFunctionChk.Location = new System.Drawing.Point(17, 32);
            this.quayStableAnalysisFunctionChk.Name = "quayStableAnalysisFunctionChk";
            this.quayStableAnalysisFunctionChk.Size = new System.Drawing.Size(96, 16);
            this.quayStableAnalysisFunctionChk.TabIndex = 0;
            this.quayStableAnalysisFunctionChk.Text = "岸壁穩定分析";
            this.quayStableAnalysisFunctionChk.UseVisualStyleBackColor = true;
            this.quayStableAnalysisFunctionChk.CheckedChanged += new System.EventHandler(this.quayStableAnalysisFunctionChk_CheckedChanged);
            // 
            // movableBedHighSandContentEffectFunctionChk
            // 
            this.movableBedHighSandContentEffectFunctionChk.AutoSize = true;
            this.movableBedHighSandContentEffectFunctionChk.Location = new System.Drawing.Point(17, 54);
            this.movableBedHighSandContentEffectFunctionChk.Name = "movableBedHighSandContentEffectFunctionChk";
            this.movableBedHighSandContentEffectFunctionChk.Size = new System.Drawing.Size(84, 16);
            this.movableBedHighSandContentEffectFunctionChk.TabIndex = 0;
            this.movableBedHighSandContentEffectFunctionChk.Text = "高含砂效應";
            this.movableBedHighSandContentEffectFunctionChk.UseVisualStyleBackColor = true;
            this.movableBedHighSandContentEffectFunctionChk.CheckedChanged += new System.EventHandler(this.movableBedHighSandContentEffectFunctionChk_CheckedChanged);
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
            this.fullPanel.Controls.Add(this.secondFlowEffectFunctionChk);
            this.fullPanel.Controls.Add(this.closeDiffusionEffectFunctionChk);
            this.fullPanel.Controls.Add(this.sideInOutFlowFunctionChk);
            this.fullPanel.Controls.Add(this.structureSetFunctionChk);
            this.fullPanel.Controls.Add(this.waterHighSandContentEffectFunctionChk);
            this.fullPanel.Location = new System.Drawing.Point(10, 3);
            this.fullPanel.Name = "fullPanel";
            this.fullPanel.Size = new System.Drawing.Size(181, 137);
            this.fullPanel.TabIndex = 1;
            // 
            // secondFlowEffectFunctionChk
            // 
            this.secondFlowEffectFunctionChk.AutoSize = true;
            this.secondFlowEffectFunctionChk.Location = new System.Drawing.Point(3, 29);
            this.secondFlowEffectFunctionChk.Name = "secondFlowEffectFunctionChk";
            this.secondFlowEffectFunctionChk.Size = new System.Drawing.Size(84, 16);
            this.secondFlowEffectFunctionChk.TabIndex = 0;
            this.secondFlowEffectFunctionChk.Text = "二次流效應";
            this.secondFlowEffectFunctionChk.UseVisualStyleBackColor = true;
            this.secondFlowEffectFunctionChk.CheckedChanged += new System.EventHandler(this.secondFlowEffectFunctionChk_CheckedChanged);
            // 
            // closeDiffusionEffectFunctionChk
            // 
            this.closeDiffusionEffectFunctionChk.AutoSize = true;
            this.closeDiffusionEffectFunctionChk.Location = new System.Drawing.Point(3, 7);
            this.closeDiffusionEffectFunctionChk.Name = "closeDiffusionEffectFunctionChk";
            this.closeDiffusionEffectFunctionChk.Size = new System.Drawing.Size(120, 16);
            this.closeDiffusionEffectFunctionChk.TabIndex = 0;
            this.closeDiffusionEffectFunctionChk.Text = "關閉移流擴散效應";
            this.closeDiffusionEffectFunctionChk.UseVisualStyleBackColor = true;
            this.closeDiffusionEffectFunctionChk.CheckedChanged += new System.EventHandler(this.closeDiffusionEffectFunctionChk_CheckedChanged);
            // 
            // sideInOutFlowFunctionChk
            // 
            this.sideInOutFlowFunctionChk.AutoSize = true;
            this.sideInOutFlowFunctionChk.Enabled = false;
            this.sideInOutFlowFunctionChk.Location = new System.Drawing.Point(3, 73);
            this.sideInOutFlowFunctionChk.Name = "sideInOutFlowFunctionChk";
            this.sideInOutFlowFunctionChk.Size = new System.Drawing.Size(75, 16);
            this.sideInOutFlowFunctionChk.TabIndex = 0;
            this.sideInOutFlowFunctionChk.Text = "側出/入流";
            this.sideInOutFlowFunctionChk.UseVisualStyleBackColor = true;
            this.sideInOutFlowFunctionChk.CheckedChanged += new System.EventHandler(this.sideInOutFlowFunctionChk_CheckedChanged);
            // 
            // structureSetFunctionChk
            // 
            this.structureSetFunctionChk.AutoSize = true;
            this.structureSetFunctionChk.Location = new System.Drawing.Point(3, 51);
            this.structureSetFunctionChk.Name = "structureSetFunctionChk";
            this.structureSetFunctionChk.Size = new System.Drawing.Size(84, 16);
            this.structureSetFunctionChk.TabIndex = 0;
            this.structureSetFunctionChk.Text = "結構物設置";
            this.structureSetFunctionChk.UseVisualStyleBackColor = true;
            this.structureSetFunctionChk.CheckedChanged += new System.EventHandler(this.structureSetFunctionChk_CheckedChanged);
            // 
            // waterHighSandContentEffectFunctionChk
            // 
            this.waterHighSandContentEffectFunctionChk.AutoSize = true;
            this.waterHighSandContentEffectFunctionChk.Enabled = false;
            this.waterHighSandContentEffectFunctionChk.Location = new System.Drawing.Point(3, 95);
            this.waterHighSandContentEffectFunctionChk.Name = "waterHighSandContentEffectFunctionChk";
            this.waterHighSandContentEffectFunctionChk.Size = new System.Drawing.Size(84, 16);
            this.waterHighSandContentEffectFunctionChk.TabIndex = 0;
            this.waterHighSandContentEffectFunctionChk.Text = "高含砂效應";
            this.waterHighSandContentEffectFunctionChk.UseVisualStyleBackColor = true;
            this.waterHighSandContentEffectFunctionChk.CheckedChanged += new System.EventHandler(this.waterHighSandContentEffectFunctionChk_CheckedChanged);
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel movableBedPanel;
        private System.Windows.Forms.CheckBox bedrockFunctionChk;
        private System.Windows.Forms.CheckBox quayStableAnalysisFunctionChk;
        private System.Windows.Forms.CheckBox movableBedHighSandContentEffectFunctionChk;
        private System.Windows.Forms.Panel waterPanel;
        private System.Windows.Forms.CheckBox closeDiffusionEffectFunctionChk;
        private System.Windows.Forms.CheckBox secondFlowEffectFunctionChk;
        private System.Windows.Forms.CheckBox structureSetFunctionChk;
        private System.Windows.Forms.CheckBox waterHighSandContentEffectFunctionChk;
        private System.Windows.Forms.CheckBox sideInOutFlowFunctionChk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel fullPanel;
        private System.Windows.Forms.RadioButton dimension3dRdo;
        private System.Windows.Forms.RadioButton dimension2dRdo;
        private System.Windows.Forms.RadioButton waterModelingRdo;
        private System.Windows.Forms.RadioButton movableBedRdo;
    }
}