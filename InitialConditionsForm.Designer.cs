namespace RiverSimulationApplication
{
    partial class InitialConditionsForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.initialMovableBed3DPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.initialMovableBed2DPanel = new System.Windows.Forms.Panel();
            this.initialConcentrationBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.initialWater3DPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.initialWater2DPanel = new System.Windows.Forms.Panel();
            this.uFlowSpeedBtn = new System.Windows.Forms.Button();
            this.vFlowSpeedBtn = new System.Windows.Forms.Button();
            this.initialWaterLevelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mapPicBox = new PictureBoxCtrl.GridPictureBox();
            this.mainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.initialMovableBed3DPanel.SuspendLayout();
            this.initialMovableBed2DPanel.SuspendLayout();
            this.flowTypeGroup.SuspendLayout();
            this.initialWater3DPanel.SuspendLayout();
            this.initialWater2DPanel.SuspendLayout();
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
            this.comment.TabIndex = 8;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ok.Location = new System.Drawing.Point(848, 652);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 7;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.groupBox2);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(500, 680);
            this.mainPanel.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.initialMovableBed3DPanel);
            this.groupBox2.Controls.Add(this.initialMovableBed2DPanel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 198);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "動床模組";
            // 
            // initialMovableBed3DPanel
            // 
            this.initialMovableBed3DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialMovableBed3DPanel.Controls.Add(this.button2);
            this.initialMovableBed3DPanel.Enabled = false;
            this.initialMovableBed3DPanel.Location = new System.Drawing.Point(21, 130);
            this.initialMovableBed3DPanel.Name = "initialMovableBed3DPanel";
            this.initialMovableBed3DPanel.Size = new System.Drawing.Size(432, 55);
            this.initialMovableBed3DPanel.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(11, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 26);
            this.button2.TabIndex = 2;
            this.button2.Text = "垂向濃度剖面";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.initialConcentrationBtn_Click);
            // 
            // initialMovableBed2DPanel
            // 
            this.initialMovableBed2DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialMovableBed2DPanel.Controls.Add(this.initialConcentrationBtn);
            this.initialMovableBed2DPanel.Location = new System.Drawing.Point(21, 38);
            this.initialMovableBed2DPanel.Name = "initialMovableBed2DPanel";
            this.initialMovableBed2DPanel.Size = new System.Drawing.Size(432, 55);
            this.initialMovableBed2DPanel.TabIndex = 13;
            // 
            // initialConcentrationBtn
            // 
            this.initialConcentrationBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.initialConcentrationBtn.Location = new System.Drawing.Point(11, 16);
            this.initialConcentrationBtn.Name = "initialConcentrationBtn";
            this.initialConcentrationBtn.Size = new System.Drawing.Size(148, 26);
            this.initialConcentrationBtn.TabIndex = 2;
            this.initialConcentrationBtn.Text = "水深平均濃度";
            this.initialConcentrationBtn.UseVisualStyleBackColor = true;
            this.initialConcentrationBtn.Click += new System.EventHandler(this.initialConcentrationBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "3D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "2D";
            // 
            // flowTypeGroup
            // 
            this.flowTypeGroup.Controls.Add(this.initialWater3DPanel);
            this.flowTypeGroup.Controls.Add(this.initialWater2DPanel);
            this.flowTypeGroup.Controls.Add(this.label2);
            this.flowTypeGroup.Controls.Add(this.label1);
            this.flowTypeGroup.Location = new System.Drawing.Point(12, 11);
            this.flowTypeGroup.Name = "flowTypeGroup";
            this.flowTypeGroup.Size = new System.Drawing.Size(472, 227);
            this.flowTypeGroup.TabIndex = 0;
            this.flowTypeGroup.TabStop = false;
            this.flowTypeGroup.Text = "水理模組";
            // 
            // initialWater3DPanel
            // 
            this.initialWater3DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialWater3DPanel.Controls.Add(this.button1);
            this.initialWater3DPanel.Enabled = false;
            this.initialWater3DPanel.Location = new System.Drawing.Point(24, 151);
            this.initialWater3DPanel.Name = "initialWater3DPanel";
            this.initialWater3DPanel.Size = new System.Drawing.Size(432, 55);
            this.initialWater3DPanel.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(11, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "垂向流速剖面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.uFlowSpeedBtn_Click);
            // 
            // initialWater2DPanel
            // 
            this.initialWater2DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialWater2DPanel.Controls.Add(this.uFlowSpeedBtn);
            this.initialWater2DPanel.Controls.Add(this.vFlowSpeedBtn);
            this.initialWater2DPanel.Controls.Add(this.initialWaterLevelBtn);
            this.initialWater2DPanel.Location = new System.Drawing.Point(21, 38);
            this.initialWater2DPanel.Name = "initialWater2DPanel";
            this.initialWater2DPanel.Size = new System.Drawing.Size(435, 82);
            this.initialWater2DPanel.TabIndex = 13;
            // 
            // uFlowSpeedBtn
            // 
            this.uFlowSpeedBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.uFlowSpeedBtn.Location = new System.Drawing.Point(14, 12);
            this.uFlowSpeedBtn.Name = "uFlowSpeedBtn";
            this.uFlowSpeedBtn.Size = new System.Drawing.Size(148, 26);
            this.uFlowSpeedBtn.TabIndex = 2;
            this.uFlowSpeedBtn.Text = "水深平均流速-U";
            this.uFlowSpeedBtn.UseVisualStyleBackColor = true;
            this.uFlowSpeedBtn.Click += new System.EventHandler(this.uFlowSpeedBtn_Click);
            // 
            // vFlowSpeedBtn
            // 
            this.vFlowSpeedBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.vFlowSpeedBtn.Location = new System.Drawing.Point(182, 12);
            this.vFlowSpeedBtn.Name = "vFlowSpeedBtn";
            this.vFlowSpeedBtn.Size = new System.Drawing.Size(148, 26);
            this.vFlowSpeedBtn.TabIndex = 2;
            this.vFlowSpeedBtn.Text = "水深平均流速-V";
            this.vFlowSpeedBtn.UseVisualStyleBackColor = true;
            this.vFlowSpeedBtn.Click += new System.EventHandler(this.vFlowSpeedBtn_Click);
            // 
            // initialWaterLevelBtn
            // 
            this.initialWaterLevelBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.initialWaterLevelBtn.Location = new System.Drawing.Point(14, 44);
            this.initialWaterLevelBtn.Name = "initialWaterLevelBtn";
            this.initialWaterLevelBtn.Size = new System.Drawing.Size(148, 26);
            this.initialWaterLevelBtn.TabIndex = 2;
            this.initialWaterLevelBtn.Text = "水位";
            this.initialWaterLevelBtn.UseVisualStyleBackColor = true;
            this.initialWaterLevelBtn.Click += new System.EventHandler(this.initialWaterLevelBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "3D";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "2D";
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
            this.mapPicBox.TabIndex = 12;
            // 
            // InitialConditionsForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.Controls.Add(this.mapPicBox);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.ok);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "InitialConditionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "初始條件";
            this.Load += new System.EventHandler(this.InitialConditionsForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.initialMovableBed3DPanel.ResumeLayout(false);
            this.initialMovableBed2DPanel.ResumeLayout(false);
            this.flowTypeGroup.ResumeLayout(false);
            this.flowTypeGroup.PerformLayout();
            this.initialWater3DPanel.ResumeLayout(false);
            this.initialWater2DPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser comment;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button initialConcentrationBtn;
        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.Button initialWaterLevelBtn;
        private System.Windows.Forms.Button vFlowSpeedBtn;
        private System.Windows.Forms.Button uFlowSpeedBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel initialWater2DPanel;
        private System.Windows.Forms.Panel initialMovableBed3DPanel;
        private System.Windows.Forms.Panel initialMovableBed2DPanel;
        private System.Windows.Forms.Panel initialWater3DPanel;
        private PictureBoxCtrl.GridPictureBox mapPicBox;

    }
}