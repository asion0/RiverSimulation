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
            this.movableTypeGroup = new System.Windows.Forms.GroupBox();
            this.initialMovableBed3DPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.verticalConcentrationSliceCloseRdo = new System.Windows.Forms.RadioButton();
            this.verticalConcentrationSliceOpenRdo = new System.Windows.Forms.RadioButton();
            this.initialMovableBed2DPanel = new System.Windows.Forms.Panel();
            this.depthAverageConcentrationBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowTypeGroup = new System.Windows.Forms.GroupBox();
            this.initialWater3DPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.verticalVelocitySliceCloseRdo = new System.Windows.Forms.RadioButton();
            this.verticalVelocitySliceOpenRdo = new System.Windows.Forms.RadioButton();
            this.initialWater2DPanel = new System.Windows.Forms.Panel();
            this.depthAverageFlowSpeedUBtn = new System.Windows.Forms.Button();
            this.depthAverageFlowSpeedVBtn = new System.Windows.Forms.Button();
            this.waterLevelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mapPicBox = new PictureBoxCtrl.GridPictureBox();
            this.mainPanel.SuspendLayout();
            this.movableTypeGroup.SuspendLayout();
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
            this.mainPanel.Controls.Add(this.movableTypeGroup);
            this.mainPanel.Controls.Add(this.flowTypeGroup);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(500, 680);
            this.mainPanel.TabIndex = 9;
            // 
            // movableTypeGroup
            // 
            this.movableTypeGroup.Controls.Add(this.initialMovableBed3DPanel);
            this.movableTypeGroup.Controls.Add(this.initialMovableBed2DPanel);
            this.movableTypeGroup.Controls.Add(this.label4);
            this.movableTypeGroup.Controls.Add(this.label3);
            this.movableTypeGroup.Location = new System.Drawing.Point(12, 256);
            this.movableTypeGroup.Name = "movableTypeGroup";
            this.movableTypeGroup.Size = new System.Drawing.Size(472, 198);
            this.movableTypeGroup.TabIndex = 0;
            this.movableTypeGroup.TabStop = false;
            this.movableTypeGroup.Text = "動床模組";
            // 
            // initialMovableBed3DPanel
            // 
            this.initialMovableBed3DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialMovableBed3DPanel.Controls.Add(this.label6);
            this.initialMovableBed3DPanel.Controls.Add(this.verticalConcentrationSliceCloseRdo);
            this.initialMovableBed3DPanel.Controls.Add(this.verticalConcentrationSliceOpenRdo);
            this.initialMovableBed3DPanel.Enabled = false;
            this.initialMovableBed3DPanel.Location = new System.Drawing.Point(21, 130);
            this.initialMovableBed3DPanel.Name = "initialMovableBed3DPanel";
            this.initialMovableBed3DPanel.Size = new System.Drawing.Size(432, 55);
            this.initialMovableBed3DPanel.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "垂向濃度剖面";
            // 
            // verticalConcentrationSliceCloseRdo
            // 
            this.verticalConcentrationSliceCloseRdo.AutoSize = true;
            this.verticalConcentrationSliceCloseRdo.Location = new System.Drawing.Point(182, 19);
            this.verticalConcentrationSliceCloseRdo.Name = "verticalConcentrationSliceCloseRdo";
            this.verticalConcentrationSliceCloseRdo.Size = new System.Drawing.Size(35, 16);
            this.verticalConcentrationSliceCloseRdo.TabIndex = 2;
            this.verticalConcentrationSliceCloseRdo.TabStop = true;
            this.verticalConcentrationSliceCloseRdo.Text = "關";
            this.verticalConcentrationSliceCloseRdo.UseVisualStyleBackColor = true;
            this.verticalConcentrationSliceCloseRdo.CheckedChanged += new System.EventHandler(this.verticalConcentrationSliceRdo_CheckedChanged);
            // 
            // verticalConcentrationSliceOpenRdo
            // 
            this.verticalConcentrationSliceOpenRdo.AutoSize = true;
            this.verticalConcentrationSliceOpenRdo.Location = new System.Drawing.Point(127, 19);
            this.verticalConcentrationSliceOpenRdo.Name = "verticalConcentrationSliceOpenRdo";
            this.verticalConcentrationSliceOpenRdo.Size = new System.Drawing.Size(35, 16);
            this.verticalConcentrationSliceOpenRdo.TabIndex = 3;
            this.verticalConcentrationSliceOpenRdo.TabStop = true;
            this.verticalConcentrationSliceOpenRdo.Text = "開";
            this.verticalConcentrationSliceOpenRdo.UseVisualStyleBackColor = true;
            this.verticalConcentrationSliceOpenRdo.CheckedChanged += new System.EventHandler(this.verticalConcentrationSliceRdo_CheckedChanged);
            // 
            // initialMovableBed2DPanel
            // 
            this.initialMovableBed2DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialMovableBed2DPanel.Controls.Add(this.depthAverageConcentrationBtn);
            this.initialMovableBed2DPanel.Location = new System.Drawing.Point(21, 38);
            this.initialMovableBed2DPanel.Name = "initialMovableBed2DPanel";
            this.initialMovableBed2DPanel.Size = new System.Drawing.Size(432, 55);
            this.initialMovableBed2DPanel.TabIndex = 13;
            // 
            // depthAverageConcentrationBtn
            // 
            this.depthAverageConcentrationBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.depthAverageConcentrationBtn.Location = new System.Drawing.Point(11, 16);
            this.depthAverageConcentrationBtn.Name = "depthAverageConcentrationBtn";
            this.depthAverageConcentrationBtn.Size = new System.Drawing.Size(148, 26);
            this.depthAverageConcentrationBtn.TabIndex = 2;
            this.depthAverageConcentrationBtn.Text = "水深平均濃度";
            this.depthAverageConcentrationBtn.UseVisualStyleBackColor = true;
            this.depthAverageConcentrationBtn.Click += new System.EventHandler(this.depthAverageConcentrationBtn_Click);
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
            this.initialWater3DPanel.Controls.Add(this.label5);
            this.initialWater3DPanel.Controls.Add(this.verticalVelocitySliceCloseRdo);
            this.initialWater3DPanel.Controls.Add(this.verticalVelocitySliceOpenRdo);
            this.initialWater3DPanel.Enabled = false;
            this.initialWater3DPanel.Location = new System.Drawing.Point(24, 151);
            this.initialWater3DPanel.Name = "initialWater3DPanel";
            this.initialWater3DPanel.Size = new System.Drawing.Size(432, 55);
            this.initialWater3DPanel.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "垂向流速剖面";
            // 
            // verticalVelocitySliceCloseRdo
            // 
            this.verticalVelocitySliceCloseRdo.AutoSize = true;
            this.verticalVelocitySliceCloseRdo.Location = new System.Drawing.Point(179, 19);
            this.verticalVelocitySliceCloseRdo.Name = "verticalVelocitySliceCloseRdo";
            this.verticalVelocitySliceCloseRdo.Size = new System.Drawing.Size(35, 16);
            this.verticalVelocitySliceCloseRdo.TabIndex = 0;
            this.verticalVelocitySliceCloseRdo.TabStop = true;
            this.verticalVelocitySliceCloseRdo.Text = "關";
            this.verticalVelocitySliceCloseRdo.UseVisualStyleBackColor = true;
            this.verticalVelocitySliceCloseRdo.CheckedChanged += new System.EventHandler(this.verticalVelocitySliceRdo_CheckedChanged);
            // 
            // verticalVelocitySliceOpenRdo
            // 
            this.verticalVelocitySliceOpenRdo.AutoSize = true;
            this.verticalVelocitySliceOpenRdo.Location = new System.Drawing.Point(124, 19);
            this.verticalVelocitySliceOpenRdo.Name = "verticalVelocitySliceOpenRdo";
            this.verticalVelocitySliceOpenRdo.Size = new System.Drawing.Size(35, 16);
            this.verticalVelocitySliceOpenRdo.TabIndex = 0;
            this.verticalVelocitySliceOpenRdo.TabStop = true;
            this.verticalVelocitySliceOpenRdo.Text = "開";
            this.verticalVelocitySliceOpenRdo.UseVisualStyleBackColor = true;
            this.verticalVelocitySliceOpenRdo.CheckedChanged += new System.EventHandler(this.verticalVelocitySliceRdo_CheckedChanged);
            // 
            // initialWater2DPanel
            // 
            this.initialWater2DPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.initialWater2DPanel.Controls.Add(this.depthAverageFlowSpeedUBtn);
            this.initialWater2DPanel.Controls.Add(this.depthAverageFlowSpeedVBtn);
            this.initialWater2DPanel.Controls.Add(this.waterLevelBtn);
            this.initialWater2DPanel.Location = new System.Drawing.Point(21, 38);
            this.initialWater2DPanel.Name = "initialWater2DPanel";
            this.initialWater2DPanel.Size = new System.Drawing.Size(435, 82);
            this.initialWater2DPanel.TabIndex = 13;
            // 
            // depthAverageFlowSpeedUBtn
            // 
            this.depthAverageFlowSpeedUBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.depthAverageFlowSpeedUBtn.Location = new System.Drawing.Point(14, 12);
            this.depthAverageFlowSpeedUBtn.Name = "depthAverageFlowSpeedUBtn";
            this.depthAverageFlowSpeedUBtn.Size = new System.Drawing.Size(148, 26);
            this.depthAverageFlowSpeedUBtn.TabIndex = 2;
            this.depthAverageFlowSpeedUBtn.Text = "水深平均流速-U";
            this.depthAverageFlowSpeedUBtn.UseVisualStyleBackColor = true;
            this.depthAverageFlowSpeedUBtn.Click += new System.EventHandler(this.depthAverageFlowSpeedUBtn_Click);
            // 
            // depthAverageFlowSpeedVBtn
            // 
            this.depthAverageFlowSpeedVBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.depthAverageFlowSpeedVBtn.Location = new System.Drawing.Point(182, 12);
            this.depthAverageFlowSpeedVBtn.Name = "depthAverageFlowSpeedVBtn";
            this.depthAverageFlowSpeedVBtn.Size = new System.Drawing.Size(148, 26);
            this.depthAverageFlowSpeedVBtn.TabIndex = 2;
            this.depthAverageFlowSpeedVBtn.Text = "水深平均流速-V";
            this.depthAverageFlowSpeedVBtn.UseVisualStyleBackColor = true;
            this.depthAverageFlowSpeedVBtn.Click += new System.EventHandler(this.depthAverageFlowSpeedVBtn_Click);
            // 
            // waterLevelBtn
            // 
            this.waterLevelBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.waterLevelBtn.Location = new System.Drawing.Point(14, 44);
            this.waterLevelBtn.Name = "waterLevelBtn";
            this.waterLevelBtn.Size = new System.Drawing.Size(148, 26);
            this.waterLevelBtn.TabIndex = 2;
            this.waterLevelBtn.Text = "水位";
            this.waterLevelBtn.UseVisualStyleBackColor = true;
            this.waterLevelBtn.Click += new System.EventHandler(this.waterLevelBtn_Click);
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
            this.movableTypeGroup.ResumeLayout(false);
            this.movableTypeGroup.PerformLayout();
            this.initialMovableBed3DPanel.ResumeLayout(false);
            this.initialMovableBed3DPanel.PerformLayout();
            this.initialMovableBed2DPanel.ResumeLayout(false);
            this.flowTypeGroup.ResumeLayout(false);
            this.flowTypeGroup.PerformLayout();
            this.initialWater3DPanel.ResumeLayout(false);
            this.initialWater3DPanel.PerformLayout();
            this.initialWater2DPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser comment;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.GroupBox movableTypeGroup;
        private System.Windows.Forms.Button depthAverageConcentrationBtn;
        private System.Windows.Forms.GroupBox flowTypeGroup;
        private System.Windows.Forms.Button waterLevelBtn;
        private System.Windows.Forms.Button depthAverageFlowSpeedVBtn;
        private System.Windows.Forms.Button depthAverageFlowSpeedUBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel initialWater2DPanel;
        private System.Windows.Forms.Panel initialMovableBed3DPanel;
        private System.Windows.Forms.Panel initialMovableBed2DPanel;
        private System.Windows.Forms.Panel initialWater3DPanel;
        private PictureBoxCtrl.GridPictureBox mapPicBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton verticalVelocitySliceCloseRdo;
        private System.Windows.Forms.RadioButton verticalVelocitySliceOpenRdo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton verticalConcentrationSliceCloseRdo;
        private System.Windows.Forms.RadioButton verticalConcentrationSliceOpenRdo;

    }
}