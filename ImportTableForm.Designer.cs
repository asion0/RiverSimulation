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
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageX = new System.Windows.Forms.TabPage();
            this.dataGridViewX = new System.Windows.Forms.DataGridView();
            this.tabPageY = new System.Windows.Forms.TabPage();
            this.dataGridViewY = new System.Windows.Forms.DataGridView();
            this.tabPageZ = new System.Windows.Forms.TabPage();
            this.dataGridViewZ = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.yGridNum = new System.Windows.Forms.TextBox();
            this.xGridNum = new System.Windows.Forms.TextBox();
            this.generateGridBtn = new System.Windows.Forms.Button();
            this.copyPasteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabPageX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX)).BeginInit();
            this.tabPageY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewY)).BeginInit();
            this.tabPageZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZ)).BeginInit();
            this.copyPasteMenuStrip.SuspendLayout();
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
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(2, 88);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(971, 588);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageX
            // 
            this.tabPageX.Controls.Add(this.dataGridViewX);
            this.tabPageX.Location = new System.Drawing.Point(4, 22);
            this.tabPageX.Name = "tabPageX";
            this.tabPageX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageX.Size = new System.Drawing.Size(963, 562);
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
            this.dataGridViewX.ContextMenuStrip = this.copyPasteMenuStrip;
            this.dataGridViewX.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX.Name = "dataGridViewX";
            this.dataGridViewX.RowTemplate.Height = 24;
            this.dataGridViewX.Size = new System.Drawing.Size(963, 562);
            this.dataGridViewX.TabIndex = 0;
            // 
            // tabPageY
            // 
            this.tabPageY.Controls.Add(this.dataGridViewY);
            this.tabPageY.Location = new System.Drawing.Point(4, 22);
            this.tabPageY.Name = "tabPageY";
            this.tabPageY.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageY.Size = new System.Drawing.Size(963, 562);
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
            this.tabPageZ.Size = new System.Drawing.Size(963, 562);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "縱向格網數";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "橫向格網數";
            // 
            // yGridNum
            // 
            this.yGridNum.Location = new System.Drawing.Point(79, 9);
            this.yGridNum.Name = "yGridNum";
            this.yGridNum.Size = new System.Drawing.Size(100, 22);
            this.yGridNum.TabIndex = 2;
            this.yGridNum.TextChanged += new System.EventHandler(this.GridNum_TextChanged);
            // 
            // xGridNum
            // 
            this.xGridNum.Location = new System.Drawing.Point(285, 9);
            this.xGridNum.Name = "xGridNum";
            this.xGridNum.Size = new System.Drawing.Size(100, 22);
            this.xGridNum.TabIndex = 3;
            this.xGridNum.TextChanged += new System.EventHandler(this.GridNum_TextChanged);
            // 
            // generateGridBtn
            // 
            this.generateGridBtn.Location = new System.Drawing.Point(14, 47);
            this.generateGridBtn.Name = "generateGridBtn";
            this.generateGridBtn.Size = new System.Drawing.Size(165, 23);
            this.generateGridBtn.TabIndex = 4;
            this.generateGridBtn.Text = "產生格網";
            this.generateGridBtn.UseVisualStyleBackColor = true;
            this.generateGridBtn.Click += new System.EventHandler(this.generateGridBtn_Click);
            // 
            // copyPasteMenuStrip
            // 
            this.copyPasteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.copyPasteMenuStrip.Name = "contextMenuStrip1";
            this.copyPasteMenuStrip.Size = new System.Drawing.Size(161, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.copyToolStripMenuItem.Text = "複製(C)";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.pasteToolStripMenuItem.Text = "貼上(P)";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // ImportTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 676);
            this.Controls.Add(this.generateGridBtn);
            this.Controls.Add(this.xGridNum);
            this.Controls.Add(this.yGridNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl);
            this.Name = "ImportTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "格網輸入";
            this.Load += new System.EventHandler(this.ImportTableForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX)).EndInit();
            this.tabPageY.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewY)).EndInit();
            this.tabPageZ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewZ)).EndInit();
            this.copyPasteMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageX;
        private System.Windows.Forms.DataGridView dataGridViewX;
        private System.Windows.Forms.TabPage tabPageY;
        private System.Windows.Forms.DataGridView dataGridViewY;
        private System.Windows.Forms.TabPage tabPageZ;
        private System.Windows.Forms.DataGridView dataGridViewZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox yGridNum;
        private System.Windows.Forms.TextBox xGridNum;
        private System.Windows.Forms.Button generateGridBtn;
        private System.Windows.Forms.ContextMenuStrip copyPasteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}