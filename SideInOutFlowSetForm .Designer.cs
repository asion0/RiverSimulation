namespace RiverSimulationApplication
{
    partial class SideInOutFlowSetForm
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.mapPicBox = new PictureBoxCtrl.GridPictureBox();
            this.editBtn = new System.Windows.Forms.Button();
            this.superTypeRdo = new System.Windows.Forms.RadioButton();
            this.subTypeRdo = new System.Windows.Forms.RadioButton();
            this.ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(13, 47);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(159, 592);
            this.listBox.TabIndex = 1;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // mapPicBox
            // 
            this.mapPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPicBox.Border = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPicBox.Grid = null;
            this.mapPicBox.Location = new System.Drawing.Point(178, 47);
            this.mapPicBox.Name = "mapPicBox";
            this.mapPicBox.SelectedI = -1;
            this.mapPicBox.SelectGroup = false;
            this.mapPicBox.SelectRow = false;
            this.mapPicBox.Size = new System.Drawing.Size(766, 592);
            this.mapPicBox.TabIndex = 2;
            this.mapPicBox.SelectedGroupChangedEvent += new PictureBoxCtrl.GridPictureBox.myDelegate3(this.mapPicBox_SelectedGroupChangedEvent);
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(13, 13);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(159, 23);
            this.editBtn.TabIndex = 3;
            this.editBtn.Text = "編輯";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // superTypeRdo
            // 
            this.superTypeRdo.AutoSize = true;
            this.superTypeRdo.Location = new System.Drawing.Point(178, 16);
            this.superTypeRdo.Name = "superTypeRdo";
            this.superTypeRdo.Size = new System.Drawing.Size(71, 16);
            this.superTypeRdo.TabIndex = 4;
            this.superTypeRdo.TabStop = true;
            this.superTypeRdo.Text = "超臨界流";
            this.superTypeRdo.UseVisualStyleBackColor = true;
            this.superTypeRdo.CheckedChanged += new System.EventHandler(this.superTypeRdo_CheckedChanged);
            // 
            // subTypeRdo
            // 
            this.subTypeRdo.AutoSize = true;
            this.subTypeRdo.Location = new System.Drawing.Point(294, 16);
            this.subTypeRdo.Name = "subTypeRdo";
            this.subTypeRdo.Size = new System.Drawing.Size(71, 16);
            this.subTypeRdo.TabIndex = 4;
            this.subTypeRdo.TabStop = true;
            this.subTypeRdo.Text = "亞臨界流";
            this.subTypeRdo.UseVisualStyleBackColor = true;
            this.subTypeRdo.CheckedChanged += new System.EventHandler(this.subTypeRdo_CheckedChanged);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ok.Location = new System.Drawing.Point(848, 652);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 12;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // SideInOutFlowSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 692);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.subTypeRdo);
            this.Controls.Add(this.superTypeRdo);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.mapPicBox);
            this.Controls.Add(this.listBox);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SideInOutFlowSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GridSelectForm";
            this.Load += new System.EventHandler(this.StructureSetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private PictureBoxCtrl.GridPictureBox mapPicBox;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.RadioButton superTypeRdo;
        private System.Windows.Forms.RadioButton subTypeRdo;
        private System.Windows.Forms.Button ok;
    }
}