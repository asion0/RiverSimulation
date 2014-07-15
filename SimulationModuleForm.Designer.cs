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
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.type2dRdo = new System.Windows.Forms.CheckBox();
            this.type3dRdo = new System.Windows.Forms.CheckBox();
            this.typeMovableBedRdo = new System.Windows.Forms.CheckBox();
            this.typeWaterModelingRdo = new System.Windows.Forms.CheckBox();
            this.mainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(330, 310);
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
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.type3dRdo);
            this.flowTypeGroup.Controls.Add(this.type2dRdo);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 11);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(414, 138);
            this.flowTypeGroup.TabIndex = 1;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "模組選擇";
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
            // typeMovableBedRdo
            // 
            this.typeMovableBedRdo.Appearance = System.Windows.Forms.Appearance.Button;
            this.typeMovableBedRdo.Location = new System.Drawing.Point(246, 26);
            this.typeMovableBedRdo.Name = "typeMovableBedRdo";
            this.typeMovableBedRdo.Size = new System.Drawing.Size(128, 96);
            this.typeMovableBedRdo.TabIndex = 2;
            this.typeMovableBedRdo.Text = "水理+動床";
            this.typeMovableBedRdo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.typeMovableBedRdo.UseVisualStyleBackColor = true;
            this.typeMovableBedRdo.CheckedChanged += new System.EventHandler(this.typeMovableBedRdo_CheckedChanged);
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
            // SimulationModuleForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 354);
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
    }
}