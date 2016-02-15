namespace RiverSimulationApplication
{
    partial class SimulationResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationResultForm));
            this.ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.graphRdo = new System.Windows.Forms.RadioButton();
            this.tableRdo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.param2Cmb = new System.Windows.Forms.ComboBox();
            this.param1Cmb = new System.Windows.Forms.ComboBox();
            this.formGrp = new System.Windows.Forms.GroupBox();
            this.animChk = new System.Windows.Forms.CheckBox();
            this.graphType4Rdo = new System.Windows.Forms.RadioButton();
            this.graphType3Rdo = new System.Windows.Forms.RadioButton();
            this.graphType2Rdo = new System.Windows.Forms.RadioButton();
            this.graphType1Rdo = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.poKPanel = new System.Windows.Forms.Panel();
            this.posKLbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.posKchk = new System.Windows.Forms.CheckBox();
            this.posKTxt = new System.Windows.Forms.TextBox();
            this.pos_InJPanel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.posJLbl = new System.Windows.Forms.Label();
            this.posILbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.posJchk = new System.Windows.Forms.CheckBox();
            this.posJTxt = new System.Windows.Forms.TextBox();
            this.posITxt = new System.Windows.Forms.TextBox();
            this.posIchk = new System.Windows.Forms.CheckBox();
            this.posSelBtn = new System.Windows.Forms.Button();
            this.timeGrp = new System.Windows.Forms.GroupBox();
            this.timeChk = new System.Windows.Forms.CheckBox();
            this.timeBtn = new System.Windows.Forms.Button();
            this.axisGrp = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.generateResultBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.formGrp.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.poKPanel.SuspendLayout();
            this.pos_InJPanel.SuspendLayout();
            this.timeGrp.SuspendLayout();
            this.axisGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok
            // 
            resources.ApplyResources(this.ok, "ok");
            this.ok.Name = "ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.graphRdo);
            this.groupBox1.Controls.Add(this.tableRdo);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // graphRdo
            // 
            resources.ApplyResources(this.graphRdo, "graphRdo");
            this.graphRdo.Name = "graphRdo";
            this.graphRdo.TabStop = true;
            this.graphRdo.UseVisualStyleBackColor = true;
            this.graphRdo.CheckedChanged += new System.EventHandler(this.graphRdo_CheckedChanged);
            // 
            // tableRdo
            // 
            resources.ApplyResources(this.tableRdo, "tableRdo");
            this.tableRdo.Name = "tableRdo";
            this.tableRdo.TabStop = true;
            this.tableRdo.UseVisualStyleBackColor = true;
            this.tableRdo.CheckedChanged += new System.EventHandler(this.tableRdo_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.param2Cmb);
            this.groupBox2.Controls.Add(this.param1Cmb);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // param2Cmb
            // 
            this.param2Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.param2Cmb.FormattingEnabled = true;
            resources.ApplyResources(this.param2Cmb, "param2Cmb");
            this.param2Cmb.Name = "param2Cmb";
            // 
            // param1Cmb
            // 
            this.param1Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.param1Cmb.FormattingEnabled = true;
            resources.ApplyResources(this.param1Cmb, "param1Cmb");
            this.param1Cmb.Name = "param1Cmb";
            this.param1Cmb.SelectedIndexChanged += new System.EventHandler(this.param1Cmb_SelectedIndexChanged);
            // 
            // formGrp
            // 
            this.formGrp.Controls.Add(this.animChk);
            this.formGrp.Controls.Add(this.graphType4Rdo);
            this.formGrp.Controls.Add(this.graphType3Rdo);
            this.formGrp.Controls.Add(this.graphType2Rdo);
            this.formGrp.Controls.Add(this.graphType1Rdo);
            resources.ApplyResources(this.formGrp, "formGrp");
            this.formGrp.Name = "formGrp";
            this.formGrp.TabStop = false;
            // 
            // animChk
            // 
            resources.ApplyResources(this.animChk, "animChk");
            this.animChk.Name = "animChk";
            this.animChk.UseVisualStyleBackColor = true;
            this.animChk.CheckedChanged += new System.EventHandler(this.animChk_CheckedChanged);
            // 
            // graphType4Rdo
            // 
            resources.ApplyResources(this.graphType4Rdo, "graphType4Rdo");
            this.graphType4Rdo.Name = "graphType4Rdo";
            this.graphType4Rdo.TabStop = true;
            this.graphType4Rdo.UseVisualStyleBackColor = true;
            this.graphType4Rdo.CheckedChanged += new System.EventHandler(this.graphType4Rdo_CheckedChanged);
            // 
            // graphType3Rdo
            // 
            resources.ApplyResources(this.graphType3Rdo, "graphType3Rdo");
            this.graphType3Rdo.Name = "graphType3Rdo";
            this.graphType3Rdo.TabStop = true;
            this.graphType3Rdo.UseVisualStyleBackColor = true;
            this.graphType3Rdo.CheckedChanged += new System.EventHandler(this.graphType3Rdo_CheckedChanged);
            // 
            // graphType2Rdo
            // 
            resources.ApplyResources(this.graphType2Rdo, "graphType2Rdo");
            this.graphType2Rdo.Name = "graphType2Rdo";
            this.graphType2Rdo.TabStop = true;
            this.graphType2Rdo.UseVisualStyleBackColor = true;
            this.graphType2Rdo.CheckedChanged += new System.EventHandler(this.graphType2Rdo_CheckedChanged);
            // 
            // graphType1Rdo
            // 
            resources.ApplyResources(this.graphType1Rdo, "graphType1Rdo");
            this.graphType1Rdo.Name = "graphType1Rdo";
            this.graphType1Rdo.TabStop = true;
            this.graphType1Rdo.UseVisualStyleBackColor = true;
            this.graphType1Rdo.CheckedChanged += new System.EventHandler(this.graphType1Rdo_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.poKPanel);
            this.groupBox4.Controls.Add(this.pos_InJPanel);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // poKPanel
            // 
            this.poKPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.poKPanel.Controls.Add(this.posKLbl);
            this.poKPanel.Controls.Add(this.label8);
            this.poKPanel.Controls.Add(this.posKchk);
            this.poKPanel.Controls.Add(this.posKTxt);
            resources.ApplyResources(this.poKPanel, "poKPanel");
            this.poKPanel.Name = "poKPanel";
            // 
            // posKLbl
            // 
            resources.ApplyResources(this.posKLbl, "posKLbl");
            this.posKLbl.Name = "posKLbl";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // posKchk
            // 
            resources.ApplyResources(this.posKchk, "posKchk");
            this.posKchk.Name = "posKchk";
            this.posKchk.UseVisualStyleBackColor = true;
            this.posKchk.CheckedChanged += new System.EventHandler(this.posKchk_CheckedChanged);
            // 
            // posKTxt
            // 
            resources.ApplyResources(this.posKTxt, "posKTxt");
            this.posKTxt.Name = "posKTxt";
            // 
            // pos_InJPanel
            // 
            this.pos_InJPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pos_InJPanel.Controls.Add(this.label7);
            this.pos_InJPanel.Controls.Add(this.posJLbl);
            this.pos_InJPanel.Controls.Add(this.posILbl);
            this.pos_InJPanel.Controls.Add(this.label6);
            this.pos_InJPanel.Controls.Add(this.posJchk);
            this.pos_InJPanel.Controls.Add(this.posJTxt);
            this.pos_InJPanel.Controls.Add(this.posITxt);
            this.pos_InJPanel.Controls.Add(this.posIchk);
            this.pos_InJPanel.Controls.Add(this.posSelBtn);
            resources.ApplyResources(this.pos_InJPanel, "pos_InJPanel");
            this.pos_InJPanel.Name = "pos_InJPanel";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // posJLbl
            // 
            resources.ApplyResources(this.posJLbl, "posJLbl");
            this.posJLbl.Name = "posJLbl";
            // 
            // posILbl
            // 
            resources.ApplyResources(this.posILbl, "posILbl");
            this.posILbl.Name = "posILbl";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // posJchk
            // 
            resources.ApplyResources(this.posJchk, "posJchk");
            this.posJchk.Name = "posJchk";
            this.posJchk.UseVisualStyleBackColor = true;
            this.posJchk.CheckedChanged += new System.EventHandler(this.posJchk_CheckedChanged);
            // 
            // posJTxt
            // 
            resources.ApplyResources(this.posJTxt, "posJTxt");
            this.posJTxt.Name = "posJTxt";
            this.posJTxt.TextChanged += new System.EventHandler(this.posJTxt_TextChanged);
            // 
            // posITxt
            // 
            resources.ApplyResources(this.posITxt, "posITxt");
            this.posITxt.Name = "posITxt";
            this.posITxt.TextChanged += new System.EventHandler(this.posITxt_TextChanged);
            // 
            // posIchk
            // 
            resources.ApplyResources(this.posIchk, "posIchk");
            this.posIchk.Name = "posIchk";
            this.posIchk.UseVisualStyleBackColor = true;
            this.posIchk.CheckedChanged += new System.EventHandler(this.posIchk_CheckedChanged);
            // 
            // posSelBtn
            // 
            resources.ApplyResources(this.posSelBtn, "posSelBtn");
            this.posSelBtn.Name = "posSelBtn";
            this.posSelBtn.UseVisualStyleBackColor = true;
            // 
            // timeGrp
            // 
            this.timeGrp.Controls.Add(this.timeChk);
            this.timeGrp.Controls.Add(this.timeBtn);
            resources.ApplyResources(this.timeGrp, "timeGrp");
            this.timeGrp.Name = "timeGrp";
            this.timeGrp.TabStop = false;
            // 
            // timeChk
            // 
            resources.ApplyResources(this.timeChk, "timeChk");
            this.timeChk.Name = "timeChk";
            this.timeChk.UseVisualStyleBackColor = true;
            this.timeChk.CheckedChanged += new System.EventHandler(this.timeChk_CheckedChanged);
            // 
            // timeBtn
            // 
            resources.ApplyResources(this.timeBtn, "timeBtn");
            this.timeBtn.Name = "timeBtn";
            this.timeBtn.UseVisualStyleBackColor = true;
            this.timeBtn.Click += new System.EventHandler(this.timeBtn_Click);
            // 
            // axisGrp
            // 
            this.axisGrp.Controls.Add(this.label5);
            this.axisGrp.Controls.Add(this.button5);
            this.axisGrp.Controls.Add(this.label4);
            this.axisGrp.Controls.Add(this.textBox5);
            this.axisGrp.Controls.Add(this.textBox4);
            resources.ApplyResources(this.axisGrp, "axisGrp");
            this.axisGrp.Name = "axisGrp";
            this.axisGrp.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox5
            // 
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            // 
            // textBox4
            // 
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // generateResultBtn
            // 
            resources.ApplyResources(this.generateResultBtn, "generateResultBtn");
            this.generateResultBtn.Name = "generateResultBtn";
            this.generateResultBtn.UseVisualStyleBackColor = true;
            this.generateResultBtn.Click += new System.EventHandler(this.generateResultBtn_Click);
            // 
            // SimulationResultForm
            // 
            this.AcceptButton = this.ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.axisGrp);
            this.Controls.Add(this.timeGrp);
            this.Controls.Add(this.formGrp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.generateResultBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ok);
            this.Name = "SimulationResultForm";
            this.Load += new System.EventHandler(this.SimulationResultForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.formGrp.ResumeLayout(false);
            this.formGrp.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.poKPanel.ResumeLayout(false);
            this.poKPanel.PerformLayout();
            this.pos_InJPanel.ResumeLayout(false);
            this.pos_InJPanel.PerformLayout();
            this.timeGrp.ResumeLayout(false);
            this.timeGrp.PerformLayout();
            this.axisGrp.ResumeLayout(false);
            this.axisGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton graphRdo;
        private System.Windows.Forms.RadioButton tableRdo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox param2Cmb;
        private System.Windows.Forms.ComboBox param1Cmb;
        private System.Windows.Forms.GroupBox formGrp;
        private System.Windows.Forms.CheckBox animChk;
        private System.Windows.Forms.RadioButton graphType4Rdo;
        private System.Windows.Forms.RadioButton graphType3Rdo;
        private System.Windows.Forms.RadioButton graphType2Rdo;
        private System.Windows.Forms.RadioButton graphType1Rdo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel poKPanel;
        private System.Windows.Forms.Label posKLbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox posKchk;
        private System.Windows.Forms.TextBox posKTxt;
        private System.Windows.Forms.Panel pos_InJPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label posJLbl;
        private System.Windows.Forms.Label posILbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox posJchk;
        private System.Windows.Forms.TextBox posJTxt;
        private System.Windows.Forms.TextBox posITxt;
        private System.Windows.Forms.CheckBox posIchk;
        private System.Windows.Forms.Button posSelBtn;
        private System.Windows.Forms.GroupBox timeGrp;
        private System.Windows.Forms.CheckBox timeChk;
        private System.Windows.Forms.Button timeBtn;
        private System.Windows.Forms.GroupBox axisGrp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button generateResultBtn;
    }
}