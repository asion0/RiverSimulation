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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.waterPanel = new System.Windows.Forms.Panel();
            this.movableBedPanel = new System.Windows.Forms.Panel();
            this.mainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.waterPanel.SuspendLayout();
            this.movableBedPanel.SuspendLayout();
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
            this.typeWaterModelingRdo.Location = new System.Drawing.Point(33, 26);
            this.typeWaterModelingRdo.Name = "typeWaterModelingRdo";
            this.typeWaterModelingRdo.Size = new System.Drawing.Size(128, 96);
            this.typeWaterModelingRdo.TabIndex = 2;
            this.typeWaterModelingRdo.Text = "水理";
            this.typeWaterModelingRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.typeWaterModelingRdo.UseVisualStyleBackColor = true;
            this.typeWaterModelingRdo.CheckedChanged += new System.EventHandler(this.typeWaterModelingRdo_CheckedChanged);
            // 
            // typeMovableBedRdo
            // 
            this.typeMovableBedRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.typeMovableBedRdo.Location = new System.Drawing.Point(246, 26);
            this.typeMovableBedRdo.Name = "typeMovableBedRdo";
            this.typeMovableBedRdo.Size = new System.Drawing.Size(128, 96);
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
            // 
            // type3dRdo
            // 
            this.type3dRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.type3dRdo.Location = new System.Drawing.Point(246, 26);
            this.type3dRdo.Name = "type3dRdo";
            this.type3dRdo.Size = new System.Drawing.Size(128, 96);
            this.type3dRdo.TabIndex = 2;
            this.type3dRdo.Text = "3D";
            this.type3dRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.type3dRdo.UseVisualStyleBackColor = true;
            this.type3dRdo.CheckedChanged += new System.EventHandler(this.type3dRdo_CheckedChanged);
            // 
            // type2dRdo
            // 
            this.type2dRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.type2dRdo.Location = new System.Drawing.Point(33, 26);
            this.type2dRdo.Name = "type2dRdo";
            this.type2dRdo.Size = new System.Drawing.Size(128, 96);
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "擴散效應";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(13, 35);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "二次流效應";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(13, 57);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "乾床效應";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(13, 79);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(72, 16);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "浸沒邊界";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(13, 101);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(75, 16);
            this.checkBox5.TabIndex = 0;
            this.checkBox5.Text = "側出/入流";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(13, 123);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(84, 16);
            this.checkBox6.TabIndex = 0;
            this.checkBox6.Text = "高含砂效應";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(17, 10);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(48, 16);
            this.checkBox7.TabIndex = 0;
            this.checkBox7.Text = "岩床";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(17, 32);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(96, 16);
            this.checkBox8.TabIndex = 0;
            this.checkBox8.Text = "岩壁穩定分析";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(17, 54);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(84, 16);
            this.checkBox9.TabIndex = 0;
            this.checkBox9.Text = "高含砂水流";
            this.checkBox9.UseVisualStyleBackColor = true;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "動床";
            // 
            // waterPanel
            // 
            this.waterPanel.Controls.Add(this.checkBox1);
            this.waterPanel.Controls.Add(this.checkBox2);
            this.waterPanel.Controls.Add(this.checkBox3);
            this.waterPanel.Controls.Add(this.checkBox6);
            this.waterPanel.Controls.Add(this.checkBox4);
            this.waterPanel.Controls.Add(this.checkBox5);
            this.waterPanel.Location = new System.Drawing.Point(6, 40);
            this.waterPanel.Name = "waterPanel";
            this.waterPanel.Size = new System.Drawing.Size(194, 150);
            this.waterPanel.TabIndex = 2;
            // 
            // movableBedPanel
            // 
            this.movableBedPanel.Controls.Add(this.checkBox7);
            this.movableBedPanel.Controls.Add(this.checkBox8);
            this.movableBedPanel.Controls.Add(this.checkBox9);
            this.movableBedPanel.Enabled = false;
            this.movableBedPanel.Location = new System.Drawing.Point(214, 40);
            this.movableBedPanel.Name = "movableBedPanel";
            this.movableBedPanel.Size = new System.Drawing.Size(194, 150);
            this.movableBedPanel.TabIndex = 3;
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
            this.Text = "模擬模組";
            this.Load += new System.EventHandler(this.SimulationModuleForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowTypeGroup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.waterPanel.ResumeLayout(false);
            this.waterPanel.PerformLayout();
            this.movableBedPanel.ResumeLayout(false);
            this.movableBedPanel.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.Panel waterPanel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}