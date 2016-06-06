namespace RiverSimulationApplication
{
    partial class SideFlowDataInputForm
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
            this.tableValueRdo = new System.Windows.Forms.RadioButton();
            this.singleValueRdo = new System.Windows.Forms.RadioButton();
            this.copyPasteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ok = new System.Windows.Forms.Button();
            this.unitLbl = new System.Windows.Forms.Label();
            this.selInputBtn = new System.Windows.Forms.Button();
            this.singleValueTxt = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.copyPasteMenuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // tableValueRdo
            // 
            this.tableValueRdo.AutoSize = true;
            this.tableValueRdo.Location = new System.Drawing.Point(161, 15);
            this.tableValueRdo.Margin = new System.Windows.Forms.Padding(4);
            this.tableValueRdo.Name = "tableValueRdo";
            this.tableValueRdo.Size = new System.Drawing.Size(88, 19);
            this.tableValueRdo.TabIndex = 1;
            this.tableValueRdo.Text = "逐點輸入";
            this.tableValueRdo.UseVisualStyleBackColor = true;
            this.tableValueRdo.CheckedChanged += new System.EventHandler(this.tableValueRdo_CheckedChanged);
            // 
            // singleValueRdo
            // 
            this.singleValueRdo.AutoSize = true;
            this.singleValueRdo.Location = new System.Drawing.Point(16, 15);
            this.singleValueRdo.Margin = new System.Windows.Forms.Padding(4);
            this.singleValueRdo.Name = "singleValueRdo";
            this.singleValueRdo.Size = new System.Drawing.Size(73, 19);
            this.singleValueRdo.TabIndex = 1;
            this.singleValueRdo.Text = "均一值";
            this.singleValueRdo.UseVisualStyleBackColor = true;
            this.singleValueRdo.CheckedChanged += new System.EventHandler(this.singleValueRdo_CheckedChanged);
            // 
            // copyPasteMenuStrip
            // 
            this.copyPasteMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.copyPasteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.ValueToolStripMenuItem});
            this.copyPasteMenuStrip.Name = "contextMenuStrip1";
            this.copyPasteMenuStrip.Size = new System.Drawing.Size(219, 82);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.copyToolStripMenuItem.Text = "複製(C)";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.pasteToolStripMenuItem.Text = "貼上(P)";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // ValueToolStripMenuItem
            // 
            this.ValueToolStripMenuItem.Name = "ValueToolStripMenuItem";
            this.ValueToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.ValueToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
            this.ValueToolStripMenuItem.Text = "填入數值(V)";
            this.ValueToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(1134, 785);
            this.ok.Margin = new System.Windows.Forms.Padding(4);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(128, 40);
            this.ok.TabIndex = 4;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // unitLbl
            // 
            this.unitLbl.Location = new System.Drawing.Point(236, 51);
            this.unitLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unitLbl.Name = "unitLbl";
            this.unitLbl.Size = new System.Drawing.Size(44, 15);
            this.unitLbl.TabIndex = 5;
            this.unitLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selInputBtn
            // 
            this.selInputBtn.Enabled = false;
            this.selInputBtn.Location = new System.Drawing.Point(301, 11);
            this.selInputBtn.Margin = new System.Windows.Forms.Padding(4);
            this.selInputBtn.Name = "selInputBtn";
            this.selInputBtn.Size = new System.Drawing.Size(112, 29);
            this.selInputBtn.TabIndex = 6;
            this.selInputBtn.Text = "圈選填入";
            this.selInputBtn.UseVisualStyleBackColor = true;
            this.selInputBtn.Visible = false;
            // 
            // singleValueTxt
            // 
            this.singleValueTxt.Enabled = false;
            this.singleValueTxt.Location = new System.Drawing.Point(492, 15);
            this.singleValueTxt.Margin = new System.Windows.Forms.Padding(4);
            this.singleValueTxt.MaxLength = 8;
            this.singleValueTxt.Name = "singleValueTxt";
            this.singleValueTxt.Size = new System.Drawing.Size(187, 25);
            this.singleValueTxt.TabIndex = 2;
            this.singleValueTxt.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(1, 51);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1261, 720);
            this.tabControl.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1253, 691);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "X";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.copyPasteMenuStrip;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1250, 688);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1253, 727);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Y";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ContextMenuStrip = this.copyPasteMenuStrip;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(1250, 724);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1253, 689);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Z";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.ContextMenuStrip = this.copyPasteMenuStrip;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(1250, 686);
            this.dataGridView3.TabIndex = 0;
            // 
            // SideFlowDataInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 861);
            this.ContextMenuStrip = this.copyPasteMenuStrip;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.selInputBtn);
            this.Controls.Add(this.unitLbl);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.singleValueTxt);
            this.Controls.Add(this.singleValueRdo);
            this.Controls.Add(this.tableValueRdo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1061, 738);
            this.Name = "SideFlowDataInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SideFlowDataInputForm";
            this.Load += new System.EventHandler(this.TableInputForm_Load);
            this.copyPasteMenuStrip.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton tableValueRdo;
        private System.Windows.Forms.RadioButton singleValueRdo;
        private System.Windows.Forms.ContextMenuStrip copyPasteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ToolStripMenuItem ValueToolStripMenuItem;
        public System.Windows.Forms.Label unitLbl;
        private System.Windows.Forms.Button selInputBtn;
        private System.Windows.Forms.TextBox singleValueTxt;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView3;
    }
}