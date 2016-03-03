namespace RiverSimulationApplication
{
    partial class Delft3dMeshForm
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
            this.rgfGridBtn = new System.Windows.Forms.Button();
            this.quickInBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rgfGridBtn
            // 
            this.rgfGridBtn.Location = new System.Drawing.Point(140, 13);
            this.rgfGridBtn.Name = "rgfGridBtn";
            this.rgfGridBtn.Size = new System.Drawing.Size(96, 26);
            this.rgfGridBtn.TabIndex = 0;
            this.rgfGridBtn.Text = "RGFGRID";
            this.rgfGridBtn.UseVisualStyleBackColor = true;
            this.rgfGridBtn.Click += new System.EventHandler(this.rgfGridBtn_Click);
            // 
            // quickInBtn
            // 
            this.quickInBtn.Location = new System.Drawing.Point(140, 42);
            this.quickInBtn.Name = "quickInBtn";
            this.quickInBtn.Size = new System.Drawing.Size(96, 26);
            this.quickInBtn.TabIndex = 0;
            this.quickInBtn.Text = "QUICKIN";
            this.quickInBtn.UseVisualStyleBackColor = true;
            this.quickInBtn.Click += new System.EventHandler(this.quickInBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "格網產生器(xy)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "底床高程內插工具";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "(以上工具著作權 © Deltares公司 版權所有)";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(203, 126);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(96, 26);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "完成";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // Delft3dMeshForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 164);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.quickInBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.rgfGridBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Delft3dMeshForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delft3D-Mesh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rgfGridBtn;
        private System.Windows.Forms.Button quickInBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okBtn;
    }
}