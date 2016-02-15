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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterModelingForm));
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.variableFlowRdo = new System.Windows.Forms.RadioButton();
            this.constantFlowRdo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.valueParamBtn = new System.Windows.Forms.Button();
            this.secFlowEffectGrp = new System.Windows.Forms.GroupBox();
            this.autoCurvatureRadiusRdo = new System.Windows.Forms.RadioButton();
            this.inputCurvatureRadiusRdo = new System.Windows.Forms.RadioButton();
            this.curvatureRadiusBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.physicalParamBtn = new System.Windows.Forms.Button();
            this.highSandContentEffectGrp = new System.Windows.Forms.GroupBox();
            this.highSandEffectC2Txt = new System.Windows.Forms.TextBox();
            this.highSandEffectC1Txt = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.highSandEffectBeta2Txt = new System.Windows.Forms.TextBox();
            this.highSandEffectBeta1Txt = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.highSandEffectAlpha2Txt = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.highSandEffectAlpha1Txt = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chezyBtn = new System.Windows.Forms.Button();
            this.manningNBtn = new System.Windows.Forms.Button();
            this.chezyRdo = new System.Windows.Forms.RadioButton();
            this.manningNRdo = new System.Windows.Forms.RadioButton();
            this.structureSetGrp = new System.Windows.Forms.GroupBox();
            this.sedimentationWeirSetChk = new System.Windows.Forms.CheckBox();
            this.groundsillWorkSetChk = new System.Windows.Forms.CheckBox();
            this.bridgePierSetChk = new System.Windows.Forms.CheckBox();
            this.tBarSetChk = new System.Windows.Forms.CheckBox();
            this.structureSetBtn = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.sedimentationWeirNumberTxt = new System.Windows.Forms.TextBox();
            this.groundsillWorkNumberTxt = new System.Windows.Forms.TextBox();
            this.bridgePierNumberTxt = new System.Windows.Forms.TextBox();
            this.tBarNumberTxt = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.comment = new System.Windows.Forms.WebBrowser();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.valueParamBackBtn = new System.Windows.Forms.Button();
            this.valueParamPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.turbulenceViscosityPanel = new System.Windows.Forms.Panel();
            this.viscosityFactorAdditionInMainstreamTxt = new System.Windows.Forms.TextBox();
            this.viscosityFactorAdditionInSideDirectionTxt = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.minWaterDepthPanel = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.minWaterDeothTxt = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.minWaterDepthText = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.outputControl3dGrp = new System.Windows.Forms.GroupBox();
            this.outputControlVelocityInformation3DChk = new System.Windows.Forms.CheckBox();
            this.outputControl2dGrp = new System.Windows.Forms.GroupBox();
            this.outputControlInitialBottomElevationChk = new System.Windows.Forms.CheckBox();
            this.outputControlFlowChk = new System.Windows.Forms.CheckBox();
            this.outputControlLevelChk = new System.Windows.Forms.CheckBox();
            this.outputControlAverageDepthFlowRateChk = new System.Windows.Forms.CheckBox();
            this.outputControlBottomShearingStressChk = new System.Windows.Forms.CheckBox();
            this.outputControlDepthChk = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.maxIterationsNumTxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.waterModelingConvergenceCriteria2dTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.waterModelingConvergenceCriteria3dTxt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.valueTimePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalSimulationTimeTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.steppingTimesInVertVslcTimeTxt = new System.Windows.Forms.TextBox();
            this.timeSpan2dTxt = new System.Windows.Forms.TextBox();
            this.outputFrequencyTxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.physicalParamPanel = new System.Windows.Forms.Panel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.gravityConstantTxt = new System.Windows.Forms.TextBox();
            this.waterDensityTxt = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.twinEquationCombo = new System.Windows.Forms.ComboBox();
            this.userDefineRdo = new System.Windows.Forms.RadioButton();
            this.tvInSideDirectionTxt = new System.Windows.Forms.TextBox();
            this.tvInMainstreamDirectionTxt = new System.Windows.Forms.TextBox();
            this.zeroEquationRdo = new System.Windows.Forms.RadioButton();
            this.twinEquationRdo = new System.Windows.Forms.RadioButton();
            this.zeroEquationTypeCombo = new System.Windows.Forms.ComboBox();
            this.singleEquationRdo = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.roughnessHeightKsHelpBtn = new System.Windows.Forms.Button();
            this.roughnessHeightKsBtn = new System.Windows.Forms.Button();
            this.turbulenceBackBtn = new System.Windows.Forms.Button();
            this.mapPicBox = new PictureBoxCtrl.GridPictureBox();
            this.flowTypeGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.secFlowEffectGrp.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.highSandContentEffectGrp.SuspendLayout();
            this.structureSetGrp.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.valueParamPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.turbulenceViscosityPanel.SuspendLayout();
            this.minWaterDepthPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.outputControl3dGrp.SuspendLayout();
            this.outputControl2dGrp.SuspendLayout();
            this.panel5.SuspendLayout();
            this.valueTimePanel.SuspendLayout();
            this.physicalParamPanel.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.variableFlowRdo);
            this.flowTypeGroup.Controls.Add(this.constantFlowRdo);
            resources.ApplyResources(this.flowTypeGroup, "flowTypeGroup");
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // variableFlowRdo
            // 
            resources.ApplyResources(this.variableFlowRdo, "variableFlowRdo");
            this.variableFlowRdo.Name = "variableFlowRdo";
            this.variableFlowRdo.UseVisualStyleBackColor = true;
            this.variableFlowRdo.CheckedChanged += new System.EventHandler(this.flowTypeRdo_CheckedChanged);
            // 
            // constantFlowRdo
            // 
            resources.ApplyResources(this.constantFlowRdo, "constantFlowRdo");
            this.constantFlowRdo.Checked = true;
            this.constantFlowRdo.Name = "constantFlowRdo";
            this.constantFlowRdo.TabStop = true;
            this.constantFlowRdo.UseVisualStyleBackColor = true;
            this.constantFlowRdo.CheckedChanged += new System.EventHandler(this.flowTypeRdo_CheckedChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.valueParamBtn);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // valueParamBtn
            // 
            resources.ApplyResources(this.valueParamBtn, "valueParamBtn");
            this.valueParamBtn.Name = "valueParamBtn";
            this.valueParamBtn.UseVisualStyleBackColor = true;
            this.valueParamBtn.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // secFlowEffectGrp
            // 
            this.secFlowEffectGrp.Controls.Add(this.autoCurvatureRadiusRdo);
            this.secFlowEffectGrp.Controls.Add(this.inputCurvatureRadiusRdo);
            this.secFlowEffectGrp.Controls.Add(this.curvatureRadiusBtn);
            resources.ApplyResources(this.secFlowEffectGrp, "secFlowEffectGrp");
            this.secFlowEffectGrp.Name = "secFlowEffectGrp";
            this.secFlowEffectGrp.TabStop = false;
            this.secFlowEffectGrp.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // autoCurvatureRadiusRdo
            // 
            resources.ApplyResources(this.autoCurvatureRadiusRdo, "autoCurvatureRadiusRdo");
            this.autoCurvatureRadiusRdo.Name = "autoCurvatureRadiusRdo";
            this.autoCurvatureRadiusRdo.UseVisualStyleBackColor = true;
            this.autoCurvatureRadiusRdo.CheckedChanged += new System.EventHandler(this.autoCurvatureRdo_CheckedChanged);
            // 
            // inputCurvatureRadiusRdo
            // 
            resources.ApplyResources(this.inputCurvatureRadiusRdo, "inputCurvatureRadiusRdo");
            this.inputCurvatureRadiusRdo.Name = "inputCurvatureRadiusRdo";
            this.inputCurvatureRadiusRdo.UseVisualStyleBackColor = true;
            this.inputCurvatureRadiusRdo.CheckedChanged += new System.EventHandler(this.curvatureRadiusRdo_CheckedChanged);
            // 
            // curvatureRadiusBtn
            // 
            resources.ApplyResources(this.curvatureRadiusBtn, "curvatureRadiusBtn");
            this.curvatureRadiusBtn.Name = "curvatureRadiusBtn";
            this.curvatureRadiusBtn.UseVisualStyleBackColor = true;
            this.curvatureRadiusBtn.Click += new System.EventHandler(this.curvatureRadiusBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.physicalParamBtn);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // physicalParamBtn
            // 
            resources.ApplyResources(this.physicalParamBtn, "physicalParamBtn");
            this.physicalParamBtn.Name = "physicalParamBtn";
            this.physicalParamBtn.UseVisualStyleBackColor = true;
            this.physicalParamBtn.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // highSandContentEffectGrp
            // 
            this.highSandContentEffectGrp.Controls.Add(this.highSandEffectC2Txt);
            this.highSandContentEffectGrp.Controls.Add(this.highSandEffectC1Txt);
            this.highSandContentEffectGrp.Controls.Add(this.label33);
            this.highSandContentEffectGrp.Controls.Add(this.label19);
            this.highSandContentEffectGrp.Controls.Add(this.highSandEffectBeta2Txt);
            this.highSandContentEffectGrp.Controls.Add(this.highSandEffectBeta1Txt);
            this.highSandContentEffectGrp.Controls.Add(this.label32);
            this.highSandContentEffectGrp.Controls.Add(this.label22);
            this.highSandContentEffectGrp.Controls.Add(this.highSandEffectAlpha2Txt);
            this.highSandContentEffectGrp.Controls.Add(this.label24);
            this.highSandContentEffectGrp.Controls.Add(this.highSandEffectAlpha1Txt);
            this.highSandContentEffectGrp.Controls.Add(this.label18);
            resources.ApplyResources(this.highSandContentEffectGrp, "highSandContentEffectGrp");
            this.highSandContentEffectGrp.Name = "highSandContentEffectGrp";
            this.highSandContentEffectGrp.TabStop = false;
            this.highSandContentEffectGrp.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // highSandEffectC2Txt
            // 
            resources.ApplyResources(this.highSandEffectC2Txt, "highSandEffectC2Txt");
            this.highSandEffectC2Txt.Name = "highSandEffectC2Txt";
            // 
            // highSandEffectC1Txt
            // 
            resources.ApplyResources(this.highSandEffectC1Txt, "highSandEffectC1Txt");
            this.highSandEffectC1Txt.Name = "highSandEffectC1Txt";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // highSandEffectBeta2Txt
            // 
            resources.ApplyResources(this.highSandEffectBeta2Txt, "highSandEffectBeta2Txt");
            this.highSandEffectBeta2Txt.Name = "highSandEffectBeta2Txt";
            // 
            // highSandEffectBeta1Txt
            // 
            resources.ApplyResources(this.highSandEffectBeta1Txt, "highSandEffectBeta1Txt");
            this.highSandEffectBeta1Txt.Name = "highSandEffectBeta1Txt";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // highSandEffectAlpha2Txt
            // 
            resources.ApplyResources(this.highSandEffectAlpha2Txt, "highSandEffectAlpha2Txt");
            this.highSandEffectAlpha2Txt.Name = "highSandEffectAlpha2Txt";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // highSandEffectAlpha1Txt
            // 
            resources.ApplyResources(this.highSandEffectAlpha1Txt, "highSandEffectAlpha1Txt");
            this.highSandEffectAlpha1Txt.Name = "highSandEffectAlpha1Txt";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // chezyBtn
            // 
            resources.ApplyResources(this.chezyBtn, "chezyBtn");
            this.chezyBtn.Name = "chezyBtn";
            this.chezyBtn.UseVisualStyleBackColor = true;
            this.chezyBtn.Click += new System.EventHandler(this.chezyBtn_Click);
            // 
            // manningNBtn
            // 
            resources.ApplyResources(this.manningNBtn, "manningNBtn");
            this.manningNBtn.Name = "manningNBtn";
            this.manningNBtn.UseVisualStyleBackColor = true;
            this.manningNBtn.Click += new System.EventHandler(this.manningBtn_Click);
            // 
            // chezyRdo
            // 
            resources.ApplyResources(this.chezyRdo, "chezyRdo");
            this.chezyRdo.Name = "chezyRdo";
            this.chezyRdo.TabStop = true;
            this.chezyRdo.UseVisualStyleBackColor = true;
            this.chezyRdo.CheckedChanged += new System.EventHandler(this.roughnessType_CheckedChanged);
            // 
            // manningNRdo
            // 
            resources.ApplyResources(this.manningNRdo, "manningNRdo");
            this.manningNRdo.Name = "manningNRdo";
            this.manningNRdo.TabStop = true;
            this.manningNRdo.UseVisualStyleBackColor = true;
            this.manningNRdo.CheckedChanged += new System.EventHandler(this.roughnessType_CheckedChanged);
            // 
            // structureSetGrp
            // 
            this.structureSetGrp.Controls.Add(this.sedimentationWeirSetChk);
            this.structureSetGrp.Controls.Add(this.groundsillWorkSetChk);
            this.structureSetGrp.Controls.Add(this.bridgePierSetChk);
            this.structureSetGrp.Controls.Add(this.tBarSetChk);
            this.structureSetGrp.Controls.Add(this.structureSetBtn);
            this.structureSetGrp.Controls.Add(this.label36);
            this.structureSetGrp.Controls.Add(this.label28);
            this.structureSetGrp.Controls.Add(this.label21);
            this.structureSetGrp.Controls.Add(this.label20);
            this.structureSetGrp.Controls.Add(this.sedimentationWeirNumberTxt);
            this.structureSetGrp.Controls.Add(this.groundsillWorkNumberTxt);
            this.structureSetGrp.Controls.Add(this.bridgePierNumberTxt);
            this.structureSetGrp.Controls.Add(this.tBarNumberTxt);
            resources.ApplyResources(this.structureSetGrp, "structureSetGrp");
            this.structureSetGrp.Name = "structureSetGrp";
            this.structureSetGrp.TabStop = false;
            this.structureSetGrp.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            // 
            // sedimentationWeirSetChk
            // 
            resources.ApplyResources(this.sedimentationWeirSetChk, "sedimentationWeirSetChk");
            this.sedimentationWeirSetChk.Name = "sedimentationWeirSetChk";
            this.sedimentationWeirSetChk.UseVisualStyleBackColor = true;
            this.sedimentationWeirSetChk.CheckedChanged += new System.EventHandler(this.sedimentationWeirChk_CheckedChanged);
            // 
            // groundsillWorkSetChk
            // 
            resources.ApplyResources(this.groundsillWorkSetChk, "groundsillWorkSetChk");
            this.groundsillWorkSetChk.Name = "groundsillWorkSetChk";
            this.groundsillWorkSetChk.UseVisualStyleBackColor = true;
            this.groundsillWorkSetChk.CheckedChanged += new System.EventHandler(this.groundsillWorkChk_CheckedChanged);
            // 
            // bridgePierSetChk
            // 
            resources.ApplyResources(this.bridgePierSetChk, "bridgePierSetChk");
            this.bridgePierSetChk.Name = "bridgePierSetChk";
            this.bridgePierSetChk.UseVisualStyleBackColor = true;
            this.bridgePierSetChk.CheckedChanged += new System.EventHandler(this.bridgePierChk_CheckedChanged);
            // 
            // tBarSetChk
            // 
            resources.ApplyResources(this.tBarSetChk, "tBarSetChk");
            this.tBarSetChk.Name = "tBarSetChk";
            this.tBarSetChk.UseVisualStyleBackColor = true;
            this.tBarSetChk.CheckedChanged += new System.EventHandler(this.tBarChk_CheckedChanged);
            // 
            // structureSetBtn
            // 
            resources.ApplyResources(this.structureSetBtn, "structureSetBtn");
            this.structureSetBtn.Name = "structureSetBtn";
            this.structureSetBtn.UseVisualStyleBackColor = true;
            this.structureSetBtn.Click += new System.EventHandler(this.structureSetBtn_Click);
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // sedimentationWeirNumberTxt
            // 
            resources.ApplyResources(this.sedimentationWeirNumberTxt, "sedimentationWeirNumberTxt");
            this.sedimentationWeirNumberTxt.Name = "sedimentationWeirNumberTxt";
            // 
            // groundsillWorkNumberTxt
            // 
            resources.ApplyResources(this.groundsillWorkNumberTxt, "groundsillWorkNumberTxt");
            this.groundsillWorkNumberTxt.Name = "groundsillWorkNumberTxt";
            // 
            // bridgePierNumberTxt
            // 
            resources.ApplyResources(this.bridgePierNumberTxt, "bridgePierNumberTxt");
            this.bridgePierNumberTxt.Name = "bridgePierNumberTxt";
            // 
            // tBarNumberTxt
            // 
            resources.ApplyResources(this.tBarNumberTxt, "tBarNumberTxt");
            this.tBarNumberTxt.Name = "tBarNumberTxt";
            this.tBarNumberTxt.TextChanged += new System.EventHandler(this.tBarNumberTxt_TextChanged);
            // 
            // ok
            // 
            resources.ApplyResources(this.ok, "ok");
            this.ok.Name = "ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // comment
            // 
            this.comment.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.comment, "comment");
            this.comment.IsWebBrowserContextMenuEnabled = false;
            this.comment.Name = "comment";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.structureSetGrp);
            this.mainPanel.Controls.Add(this.highSandContentEffectGrp);
            this.mainPanel.Controls.Add(this.groupBox4);
            this.mainPanel.Controls.Add(this.secFlowEffectGrp);
            this.mainPanel.Controls.Add(this.groupBox2);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Name = "mainPanel";
            // 
            // valueParamBackBtn
            // 
            resources.ApplyResources(this.valueParamBackBtn, "valueParamBackBtn");
            this.valueParamBackBtn.Name = "valueParamBackBtn";
            this.valueParamBackBtn.UseVisualStyleBackColor = true;
            this.valueParamBackBtn.Click += new System.EventHandler(this.Back_Click);
            // 
            // valueParamPanel
            // 
            this.valueParamPanel.Controls.Add(this.groupBox1);
            this.valueParamPanel.Controls.Add(this.valueParamBackBtn);
            resources.ApplyResources(this.valueParamPanel, "valueParamPanel");
            this.valueParamPanel.Name = "valueParamPanel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.turbulenceViscosityPanel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.minWaterDepthPanel);
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.valueTimePanel);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // turbulenceViscosityPanel
            // 
            this.turbulenceViscosityPanel.Controls.Add(this.viscosityFactorAdditionInMainstreamTxt);
            this.turbulenceViscosityPanel.Controls.Add(this.viscosityFactorAdditionInSideDirectionTxt);
            this.turbulenceViscosityPanel.Controls.Add(this.label15);
            this.turbulenceViscosityPanel.Controls.Add(this.label16);
            resources.ApplyResources(this.turbulenceViscosityPanel, "turbulenceViscosityPanel");
            this.turbulenceViscosityPanel.Name = "turbulenceViscosityPanel";
            // 
            // viscosityFactorAdditionInMainstreamTxt
            // 
            resources.ApplyResources(this.viscosityFactorAdditionInMainstreamTxt, "viscosityFactorAdditionInMainstreamTxt");
            this.viscosityFactorAdditionInMainstreamTxt.Name = "viscosityFactorAdditionInMainstreamTxt";
            // 
            // viscosityFactorAdditionInSideDirectionTxt
            // 
            resources.ApplyResources(this.viscosityFactorAdditionInSideDirectionTxt, "viscosityFactorAdditionInSideDirectionTxt");
            this.viscosityFactorAdditionInSideDirectionTxt.Name = "viscosityFactorAdditionInSideDirectionTxt";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // minWaterDepthPanel
            // 
            this.minWaterDepthPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.minWaterDepthPanel.Controls.Add(this.label29);
            this.minWaterDepthPanel.Controls.Add(this.minWaterDeothTxt);
            this.minWaterDepthPanel.Controls.Add(this.label30);
            this.minWaterDepthPanel.Controls.Add(this.minWaterDepthText);
            resources.ApplyResources(this.minWaterDepthPanel, "minWaterDepthPanel");
            this.minWaterDepthPanel.Name = "minWaterDepthPanel";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // minWaterDeothTxt
            // 
            resources.ApplyResources(this.minWaterDeothTxt, "minWaterDeothTxt");
            this.minWaterDeothTxt.Name = "minWaterDeothTxt";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // minWaterDepthText
            // 
            resources.ApplyResources(this.minWaterDepthText, "minWaterDepthText");
            this.minWaterDepthText.Name = "minWaterDepthText";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel6.Controls.Add(this.outputControl3dGrp);
            this.panel6.Controls.Add(this.outputControl2dGrp);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // outputControl3dGrp
            // 
            this.outputControl3dGrp.Controls.Add(this.outputControlVelocityInformation3DChk);
            resources.ApplyResources(this.outputControl3dGrp, "outputControl3dGrp");
            this.outputControl3dGrp.Name = "outputControl3dGrp";
            this.outputControl3dGrp.TabStop = false;
            // 
            // outputControlVelocityInformation3DChk
            // 
            resources.ApplyResources(this.outputControlVelocityInformation3DChk, "outputControlVelocityInformation3DChk");
            this.outputControlVelocityInformation3DChk.Name = "outputControlVelocityInformation3DChk";
            this.outputControlVelocityInformation3DChk.UseVisualStyleBackColor = true;
            this.outputControlVelocityInformation3DChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // outputControl2dGrp
            // 
            this.outputControl2dGrp.Controls.Add(this.outputControlInitialBottomElevationChk);
            this.outputControl2dGrp.Controls.Add(this.outputControlFlowChk);
            this.outputControl2dGrp.Controls.Add(this.outputControlLevelChk);
            this.outputControl2dGrp.Controls.Add(this.outputControlAverageDepthFlowRateChk);
            this.outputControl2dGrp.Controls.Add(this.outputControlBottomShearingStressChk);
            this.outputControl2dGrp.Controls.Add(this.outputControlDepthChk);
            resources.ApplyResources(this.outputControl2dGrp, "outputControl2dGrp");
            this.outputControl2dGrp.Name = "outputControl2dGrp";
            this.outputControl2dGrp.TabStop = false;
            // 
            // outputControlInitialBottomElevationChk
            // 
            resources.ApplyResources(this.outputControlInitialBottomElevationChk, "outputControlInitialBottomElevationChk");
            this.outputControlInitialBottomElevationChk.Name = "outputControlInitialBottomElevationChk";
            this.outputControlInitialBottomElevationChk.UseVisualStyleBackColor = true;
            this.outputControlInitialBottomElevationChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // outputControlFlowChk
            // 
            resources.ApplyResources(this.outputControlFlowChk, "outputControlFlowChk");
            this.outputControlFlowChk.Name = "outputControlFlowChk";
            this.outputControlFlowChk.UseVisualStyleBackColor = true;
            this.outputControlFlowChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // outputControlLevelChk
            // 
            resources.ApplyResources(this.outputControlLevelChk, "outputControlLevelChk");
            this.outputControlLevelChk.Name = "outputControlLevelChk";
            this.outputControlLevelChk.UseVisualStyleBackColor = true;
            this.outputControlLevelChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // outputControlAverageDepthFlowRateChk
            // 
            resources.ApplyResources(this.outputControlAverageDepthFlowRateChk, "outputControlAverageDepthFlowRateChk");
            this.outputControlAverageDepthFlowRateChk.Name = "outputControlAverageDepthFlowRateChk";
            this.outputControlAverageDepthFlowRateChk.UseVisualStyleBackColor = true;
            this.outputControlAverageDepthFlowRateChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // outputControlBottomShearingStressChk
            // 
            resources.ApplyResources(this.outputControlBottomShearingStressChk, "outputControlBottomShearingStressChk");
            this.outputControlBottomShearingStressChk.Name = "outputControlBottomShearingStressChk";
            this.outputControlBottomShearingStressChk.UseVisualStyleBackColor = true;
            this.outputControlBottomShearingStressChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // outputControlDepthChk
            // 
            resources.ApplyResources(this.outputControlDepthChk, "outputControlDepthChk");
            this.outputControlDepthChk.Name = "outputControlDepthChk";
            this.outputControlDepthChk.UseVisualStyleBackColor = true;
            this.outputControlDepthChk.CheckedChanged += new System.EventHandler(this.outputControl_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel5.Controls.Add(this.maxIterationsNumTxt);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.waterModelingConvergenceCriteria2dTxt);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.waterModelingConvergenceCriteria3dTxt);
            this.panel5.Controls.Add(this.label14);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // maxIterationsNumTxt
            // 
            resources.ApplyResources(this.maxIterationsNumTxt, "maxIterationsNumTxt");
            this.maxIterationsNumTxt.Name = "maxIterationsNumTxt";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // waterModelingConvergenceCriteria2dTxt
            // 
            resources.ApplyResources(this.waterModelingConvergenceCriteria2dTxt, "waterModelingConvergenceCriteria2dTxt");
            this.waterModelingConvergenceCriteria2dTxt.Name = "waterModelingConvergenceCriteria2dTxt";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // waterModelingConvergenceCriteria3dTxt
            // 
            resources.ApplyResources(this.waterModelingConvergenceCriteria3dTxt, "waterModelingConvergenceCriteria3dTxt");
            this.waterModelingConvergenceCriteria3dTxt.Name = "waterModelingConvergenceCriteria3dTxt";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // valueTimePanel
            // 
            this.valueTimePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.valueTimePanel.Controls.Add(this.label1);
            this.valueTimePanel.Controls.Add(this.label2);
            this.valueTimePanel.Controls.Add(this.totalSimulationTimeTxt);
            this.valueTimePanel.Controls.Add(this.label3);
            this.valueTimePanel.Controls.Add(this.label5);
            this.valueTimePanel.Controls.Add(this.label4);
            this.valueTimePanel.Controls.Add(this.steppingTimesInVertVslcTimeTxt);
            this.valueTimePanel.Controls.Add(this.timeSpan2dTxt);
            this.valueTimePanel.Controls.Add(this.outputFrequencyTxt);
            this.valueTimePanel.Controls.Add(this.label13);
            resources.ApplyResources(this.valueTimePanel, "valueTimePanel");
            this.valueTimePanel.Name = "valueTimePanel";
            this.valueTimePanel.MouseHover += new System.EventHandler(this.valueTimePanel_MouseHover);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // totalSimulationTimeTxt
            // 
            resources.ApplyResources(this.totalSimulationTimeTxt, "totalSimulationTimeTxt");
            this.totalSimulationTimeTxt.Name = "totalSimulationTimeTxt";
            this.totalSimulationTimeTxt.Enter += new System.EventHandler(this.timeTxt_Enter);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // steppingTimesInVertVslcTimeTxt
            // 
            resources.ApplyResources(this.steppingTimesInVertVslcTimeTxt, "steppingTimesInVertVslcTimeTxt");
            this.steppingTimesInVertVslcTimeTxt.Name = "steppingTimesInVertVslcTimeTxt";
            this.steppingTimesInVertVslcTimeTxt.Enter += new System.EventHandler(this.timeTxt_Enter);
            // 
            // timeSpan2dTxt
            // 
            resources.ApplyResources(this.timeSpan2dTxt, "timeSpan2dTxt");
            this.timeSpan2dTxt.Name = "timeSpan2dTxt";
            this.timeSpan2dTxt.Enter += new System.EventHandler(this.timeTxt_Enter);
            // 
            // outputFrequencyTxt
            // 
            resources.ApplyResources(this.outputFrequencyTxt, "outputFrequencyTxt");
            this.outputFrequencyTxt.Name = "outputFrequencyTxt";
            this.outputFrequencyTxt.Enter += new System.EventHandler(this.timeTxt_Enter);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // physicalParamPanel
            // 
            this.physicalParamPanel.Controls.Add(this.groupBox10);
            this.physicalParamPanel.Controls.Add(this.turbulenceBackBtn);
            resources.ApplyResources(this.physicalParamPanel, "physicalParamPanel");
            this.physicalParamPanel.Name = "physicalParamPanel";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.panel10);
            this.groupBox10.Controls.Add(this.panel7);
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Controls.Add(this.label11);
            this.groupBox10.Controls.Add(this.label23);
            this.groupBox10.Controls.Add(this.panel8);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel10.Controls.Add(this.label35);
            this.panel10.Controls.Add(this.label34);
            this.panel10.Controls.Add(this.label12);
            this.panel10.Controls.Add(this.label17);
            this.panel10.Controls.Add(this.gravityConstantTxt);
            this.panel10.Controls.Add(this.waterDensityTxt);
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Name = "panel10";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // gravityConstantTxt
            // 
            resources.ApplyResources(this.gravityConstantTxt, "gravityConstantTxt");
            this.gravityConstantTxt.Name = "gravityConstantTxt";
            // 
            // waterDensityTxt
            // 
            resources.ApplyResources(this.waterDensityTxt, "waterDensityTxt");
            this.waterDensityTxt.Name = "waterDensityTxt";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel7.Controls.Add(this.label31);
            this.panel7.Controls.Add(this.label27);
            this.panel7.Controls.Add(this.twinEquationCombo);
            this.panel7.Controls.Add(this.userDefineRdo);
            this.panel7.Controls.Add(this.tvInSideDirectionTxt);
            this.panel7.Controls.Add(this.tvInMainstreamDirectionTxt);
            this.panel7.Controls.Add(this.zeroEquationRdo);
            this.panel7.Controls.Add(this.twinEquationRdo);
            this.panel7.Controls.Add(this.zeroEquationTypeCombo);
            this.panel7.Controls.Add(this.singleEquationRdo);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // twinEquationCombo
            // 
            this.twinEquationCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.twinEquationCombo, "twinEquationCombo");
            this.twinEquationCombo.FormattingEnabled = true;
            this.twinEquationCombo.Items.AddRange(new object[] {
            resources.GetString("twinEquationCombo.Items")});
            this.twinEquationCombo.Name = "twinEquationCombo";
            // 
            // userDefineRdo
            // 
            resources.ApplyResources(this.userDefineRdo, "userDefineRdo");
            this.userDefineRdo.Name = "userDefineRdo";
            this.userDefineRdo.TabStop = true;
            this.userDefineRdo.UseVisualStyleBackColor = true;
            this.userDefineRdo.CheckedChanged += new System.EventHandler(this.turbulenceViscosityType_CheckedChanged);
            // 
            // tvInSideDirectionTxt
            // 
            resources.ApplyResources(this.tvInSideDirectionTxt, "tvInSideDirectionTxt");
            this.tvInSideDirectionTxt.Name = "tvInSideDirectionTxt";
            // 
            // tvInMainstreamDirectionTxt
            // 
            resources.ApplyResources(this.tvInMainstreamDirectionTxt, "tvInMainstreamDirectionTxt");
            this.tvInMainstreamDirectionTxt.Name = "tvInMainstreamDirectionTxt";
            // 
            // zeroEquationRdo
            // 
            resources.ApplyResources(this.zeroEquationRdo, "zeroEquationRdo");
            this.zeroEquationRdo.Name = "zeroEquationRdo";
            this.zeroEquationRdo.TabStop = true;
            this.zeroEquationRdo.UseVisualStyleBackColor = true;
            this.zeroEquationRdo.CheckedChanged += new System.EventHandler(this.turbulenceViscosityType_CheckedChanged);
            // 
            // twinEquationRdo
            // 
            resources.ApplyResources(this.twinEquationRdo, "twinEquationRdo");
            this.twinEquationRdo.Name = "twinEquationRdo";
            this.twinEquationRdo.TabStop = true;
            this.twinEquationRdo.UseVisualStyleBackColor = true;
            this.twinEquationRdo.CheckedChanged += new System.EventHandler(this.turbulenceViscosityType_CheckedChanged);
            // 
            // zeroEquationTypeCombo
            // 
            this.zeroEquationTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.zeroEquationTypeCombo, "zeroEquationTypeCombo");
            this.zeroEquationTypeCombo.FormattingEnabled = true;
            this.zeroEquationTypeCombo.Items.AddRange(new object[] {
            resources.GetString("zeroEquationTypeCombo.Items"),
            resources.GetString("zeroEquationTypeCombo.Items1"),
            resources.GetString("zeroEquationTypeCombo.Items2"),
            resources.GetString("zeroEquationTypeCombo.Items3"),
            resources.GetString("zeroEquationTypeCombo.Items4")});
            this.zeroEquationTypeCombo.Name = "zeroEquationTypeCombo";
            this.zeroEquationTypeCombo.SelectedIndexChanged += new System.EventHandler(this.zeroEquationTypeCombo_SelectedIndexChanged);
            // 
            // singleEquationRdo
            // 
            resources.ApplyResources(this.singleEquationRdo, "singleEquationRdo");
            this.singleEquationRdo.Name = "singleEquationRdo";
            this.singleEquationRdo.TabStop = true;
            this.singleEquationRdo.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.manningNBtn);
            this.panel8.Controls.Add(this.roughnessHeightKsHelpBtn);
            this.panel8.Controls.Add(this.roughnessHeightKsBtn);
            this.panel8.Controls.Add(this.chezyBtn);
            this.panel8.Controls.Add(this.manningNRdo);
            this.panel8.Controls.Add(this.chezyRdo);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // roughnessHeightKsHelpBtn
            // 
            resources.ApplyResources(this.roughnessHeightKsHelpBtn, "roughnessHeightKsHelpBtn");
            this.roughnessHeightKsHelpBtn.Name = "roughnessHeightKsHelpBtn";
            this.roughnessHeightKsHelpBtn.UseVisualStyleBackColor = true;
            this.roughnessHeightKsHelpBtn.Click += new System.EventHandler(this.roughnessHeightKsHelpBtn_Click);
            // 
            // roughnessHeightKsBtn
            // 
            resources.ApplyResources(this.roughnessHeightKsBtn, "roughnessHeightKsBtn");
            this.roughnessHeightKsBtn.Name = "roughnessHeightKsBtn";
            this.roughnessHeightKsBtn.UseVisualStyleBackColor = true;
            this.roughnessHeightKsBtn.Click += new System.EventHandler(this.roughnessHeightKsBtn_Click);
            // 
            // turbulenceBackBtn
            // 
            resources.ApplyResources(this.turbulenceBackBtn, "turbulenceBackBtn");
            this.turbulenceBackBtn.Name = "turbulenceBackBtn";
            this.turbulenceBackBtn.UseVisualStyleBackColor = true;
            this.turbulenceBackBtn.Click += new System.EventHandler(this.Back_Click);
            // 
            // mapPicBox
            // 
            resources.ApplyResources(this.mapPicBox, "mapPicBox");
            this.mapPicBox.Border = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPicBox.Grid = null;
            this.mapPicBox.Name = "mapPicBox";
            this.mapPicBox.SelectedI = -1;
            this.mapPicBox.SelectGroup = false;
            this.mapPicBox.SelectRow = false;
            // 
            // WaterModelingForm
            // 
            this.AcceptButton = this.ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.physicalParamPanel);
            this.Controls.Add(this.valueParamPanel);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.mapPicBox);
            this.Name = "WaterModelingForm";
            this.Load += new System.EventHandler(this.WaterModelingForm_Load);
            this.MouseHover += new System.EventHandler(this.groupBox_MouseHover);
            this.flowTypeGroup.ResumeLayout(false);
            this.flowTypeGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.secFlowEffectGrp.ResumeLayout(false);
            this.secFlowEffectGrp.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.highSandContentEffectGrp.ResumeLayout(false);
            this.highSandContentEffectGrp.PerformLayout();
            this.structureSetGrp.ResumeLayout(false);
            this.structureSetGrp.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.valueParamPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.turbulenceViscosityPanel.ResumeLayout(false);
            this.turbulenceViscosityPanel.PerformLayout();
            this.minWaterDepthPanel.ResumeLayout(false);
            this.minWaterDepthPanel.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.outputControl3dGrp.ResumeLayout(false);
            this.outputControl3dGrp.PerformLayout();
            this.outputControl2dGrp.ResumeLayout(false);
            this.outputControl2dGrp.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.valueTimePanel.ResumeLayout(false);
            this.valueTimePanel.PerformLayout();
            this.physicalParamPanel.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.GroupBox secFlowEffectGrp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox highSandContentEffectGrp;
        private System.Windows.Forms.GroupBox structureSetGrp;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.WebBrowser comment;
        private System.Windows.Forms.RadioButton variableFlowRdo;
        private System.Windows.Forms.RadioButton constantFlowRdo;
        private System.Windows.Forms.Button valueParamBtn;
        private System.Windows.Forms.Button curvatureRadiusBtn;
        private System.Windows.Forms.RadioButton autoCurvatureRadiusRdo;
        private System.Windows.Forms.RadioButton inputCurvatureRadiusRdo;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button valueParamBackBtn;
        private System.Windows.Forms.Panel valueParamPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel physicalParamPanel;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button turbulenceBackBtn;
        private System.Windows.Forms.TextBox outputFrequencyTxt;
        private System.Windows.Forms.TextBox timeSpan2dTxt;
        private System.Windows.Forms.TextBox totalSimulationTimeTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maxIterationsNumTxt;
        private System.Windows.Forms.TextBox waterModelingConvergenceCriteria2dTxt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox steppingTimesInVertVslcTimeTxt;
        private System.Windows.Forms.TextBox waterModelingConvergenceCriteria3dTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button structureSetBtn;
        private System.Windows.Forms.TextBox tBarNumberTxt;
        private System.Windows.Forms.Button chezyBtn;
        private System.Windows.Forms.Button manningNBtn;
        private System.Windows.Forms.RadioButton chezyRdo;
        private System.Windows.Forms.RadioButton manningNRdo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button physicalParamBtn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel valueTimePanel;
        private System.Windows.Forms.TextBox waterDensityTxt;
        private System.Windows.Forms.TextBox gravityConstantTxt;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox highSandEffectBeta1Txt;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox highSandEffectAlpha1Txt;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button roughnessHeightKsHelpBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox viscosityFactorAdditionInSideDirectionTxt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox viscosityFactorAdditionInMainstreamTxt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel minWaterDepthPanel;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox minWaterDeothTxt;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox outputControlVelocityInformation3DChk;
        private System.Windows.Forms.CheckBox outputControlFlowChk;
        private System.Windows.Forms.CheckBox outputControlBottomShearingStressChk;
        private System.Windows.Forms.CheckBox outputControlLevelChk;
        private System.Windows.Forms.CheckBox outputControlDepthChk;
        private System.Windows.Forms.CheckBox outputControlInitialBottomElevationChk;
        private System.Windows.Forms.Label minWaterDepthText;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox twinEquationCombo;
        private System.Windows.Forms.RadioButton userDefineRdo;
        private System.Windows.Forms.RadioButton zeroEquationRdo;
        private System.Windows.Forms.RadioButton twinEquationRdo;
        private System.Windows.Forms.ComboBox zeroEquationTypeCombo;
        private System.Windows.Forms.RadioButton singleEquationRdo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox highSandEffectC2Txt;
        private System.Windows.Forms.TextBox highSandEffectC1Txt;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox highSandEffectBeta2Txt;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox highSandEffectAlpha2Txt;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tvInSideDirectionTxt;
        private System.Windows.Forms.TextBox tvInMainstreamDirectionTxt;
        private System.Windows.Forms.Panel turbulenceViscosityPanel;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox outputControl3dGrp;
        private System.Windows.Forms.GroupBox outputControl2dGrp;
        private System.Windows.Forms.CheckBox outputControlAverageDepthFlowRateChk;
        private PictureBoxCtrl.GridPictureBox mapPicBox;
        private System.Windows.Forms.CheckBox sedimentationWeirSetChk;
        private System.Windows.Forms.CheckBox groundsillWorkSetChk;
        private System.Windows.Forms.CheckBox bridgePierSetChk;
        private System.Windows.Forms.CheckBox tBarSetChk;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox sedimentationWeirNumberTxt;
        private System.Windows.Forms.TextBox groundsillWorkNumberTxt;
        private System.Windows.Forms.TextBox bridgePierNumberTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button roughnessHeightKsBtn;
    }
}