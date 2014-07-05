namespace RiverSimulationApplication
{
    partial class ImportTableForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageX = new System.Windows.Forms.TabPage();
            this.dataGridViewX = new System.Windows.Forms.DataGridView();
            this.tabPageY = new System.Windows.Forms.TabPage();
            this.dataGridViewY = new System.Windows.Forms.DataGridView();
            this.tabPageZ = new System.Windows.Forms.TabPage();
            this.dataGridViewZ = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tabPageX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX)).BeginInit();
            this.tabPageY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewY)).BeginInit();
            this.tabPageZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZ)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageX);
            this.tabControl.Controls.Add(this.tabPageY);
            this.tabControl.Controls.Add(this.tabPageZ);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(973, 676);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageX
            // 
            this.tabPageX.Controls.Add(this.dataGridViewX);
            this.tabPageX.Location = new System.Drawing.Point(4, 22);
            this.tabPageX.Name = "tabPageX";
            this.tabPageX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageX.Size = new System.Drawing.Size(965, 650);
            this.tabPageX.TabIndex = 0;
            this.tabPageX.Text = "X";
            this.tabPageX.UseVisualStyleBackColor = true;
            // 
            // dataGridViewX
            // 
            this.dataGridViewX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX.Name = "dataGridViewX";
            this.dataGridViewX.RowTemplate.Height = 24;
            this.dataGridViewX.Size = new System.Drawing.Size(965, 650);
            this.dataGridViewX.TabIndex = 0;
            // 
            // tabPageY
            // 
            this.tabPageY.Controls.Add(this.dataGridViewY);
            this.tabPageY.Location = new System.Drawing.Point(4, 22);
            this.tabPageY.Name = "tabPageY";
            this.tabPageY.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageY.Size = new System.Drawing.Size(965, 650);
            this.tabPageY.TabIndex = 1;
            this.tabPageY.Text = "Y";
            this.tabPageY.UseVisualStyleBackColor = true;
            // 
            // dataGridViewY
            // 
            this.dataGridViewY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewY.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewY.Name = "dataGridViewY";
            this.dataGridViewY.RowTemplate.Height = 24;
            this.dataGridViewY.Size = new System.Drawing.Size(965, 650);
            this.dataGridViewY.TabIndex = 0;
            // 
            // tabPageZ
            // 
            this.tabPageZ.Controls.Add(this.dataGridViewZ);
            this.tabPageZ.Location = new System.Drawing.Point(4, 22);
            this.tabPageZ.Name = "tabPageZ";
            this.tabPageZ.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageZ.Size = new System.Drawing.Size(965, 650);
            this.tabPageZ.TabIndex = 2;
            this.tabPageZ.Text = "Z";
            this.tabPageZ.UseVisualStyleBackColor = true;
            // 
            // dataGridViewZ
            // 
            this.dataGridViewZ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewZ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewZ.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewZ.Name = "dataGridViewZ";
            this.dataGridViewZ.RowTemplate.Height = 24;
            this.dataGridViewZ.Size = new System.Drawing.Size(965, 650);
            this.dataGridViewZ.TabIndex = 0;
            // 
            // ImportTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 676);
            this.Controls.Add(this.tabControl);
            this.Name = "ImportTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "網格輸入";
            this.Load += new System.EventHandler(this.ImportTableForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX)).EndInit();
            this.tabPageY.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewY)).EndInit();
            this.tabPageZ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageX;
        private System.Windows.Forms.DataGridView dataGridViewX;
        private System.Windows.Forms.TabPage tabPageY;
        private System.Windows.Forms.DataGridView dataGridViewY;
        private System.Windows.Forms.TabPage tabPageZ;
        private System.Windows.Forms.DataGridView dataGridViewZ;
    }
}