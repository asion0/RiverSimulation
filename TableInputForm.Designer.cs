namespace RiverSimulationApplication
{
    partial class TableInputForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableValueRdo = new System.Windows.Forms.RadioButton();
            this.singleValueRdo = new System.Windows.Forms.RadioButton();
            this.singleValueTxt = new System.Windows.Forms.TextBox();
            this.copyPasteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.averageBtn = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.unitLbl = new System.Windows.Forms.Label();
            this.selInputBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.copyPasteMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 91);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(956, 554);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // tableValueRdo
            // 
            this.tableValueRdo.AutoSize = true;
            this.tableValueRdo.Location = new System.Drawing.Point(12, 69);
            this.tableValueRdo.Name = "tableValueRdo";
            this.tableValueRdo.Size = new System.Drawing.Size(71, 16);
            this.tableValueRdo.TabIndex = 1;
            this.tableValueRdo.Text = "逐點輸入";
            this.tableValueRdo.UseVisualStyleBackColor = true;
            this.tableValueRdo.CheckedChanged += new System.EventHandler(this.tableValueRdo_CheckedChanged);
            // 
            // singleValueRdo
            // 
            this.singleValueRdo.AutoSize = true;
            this.singleValueRdo.Location = new System.Drawing.Point(12, 12);
            this.singleValueRdo.Name = "singleValueRdo";
            this.singleValueRdo.Size = new System.Drawing.Size(59, 16);
            this.singleValueRdo.TabIndex = 1;
            this.singleValueRdo.Text = "均一值";
            this.singleValueRdo.UseVisualStyleBackColor = true;
            this.singleValueRdo.CheckedChanged += new System.EventHandler(this.singleValueRdo_CheckedChanged);
            // 
            // singleValueTxt
            // 
            this.singleValueTxt.Enabled = false;
            this.singleValueTxt.Location = new System.Drawing.Point(32, 36);
            this.singleValueTxt.MaxLength = 8;
            this.singleValueTxt.Name = "singleValueTxt";
            this.singleValueTxt.Size = new System.Drawing.Size(141, 22);
            this.singleValueTxt.TabIndex = 2;
            // 
            // copyPasteMenuStrip
            // 
            this.copyPasteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.ValueToolStripMenuItem});
            this.copyPasteMenuStrip.Name = "contextMenuStrip1";
            this.copyPasteMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "複製(C)";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pasteToolStripMenuItem.Text = "貼上(P)";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // ValueToolStripMenuItem
            // 
            this.ValueToolStripMenuItem.Name = "ValueToolStripMenuItem";
            this.ValueToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.ValueToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ValueToolStripMenuItem.Text = "填入數值(V)";
            this.ValueToolStripMenuItem.Click += new System.EventHandler(this.valueToolStripMenuItem_Click);
            // 
            // averageBtn
            // 
            this.averageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.averageBtn.Location = new System.Drawing.Point(12, 654);
            this.averageBtn.Name = "averageBtn";
            this.averageBtn.Size = new System.Drawing.Size(120, 26);
            this.averageBtn.TabIndex = 3;
            this.averageBtn.Text = "平均分配";
            this.averageBtn.UseVisualStyleBackColor = true;
            this.averageBtn.Visible = false;
            this.averageBtn.Click += new System.EventHandler(this.averageBtn_Click);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(824, 654);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(120, 26);
            this.ok.TabIndex = 4;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // unitLbl
            // 
            this.unitLbl.Location = new System.Drawing.Point(177, 41);
            this.unitLbl.Name = "unitLbl";
            this.unitLbl.Size = new System.Drawing.Size(33, 12);
            this.unitLbl.TabIndex = 5;
            this.unitLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selInputBtn
            // 
            this.selInputBtn.Enabled = false;
            this.selInputBtn.Location = new System.Drawing.Point(89, 64);
            this.selInputBtn.Name = "selInputBtn";
            this.selInputBtn.Size = new System.Drawing.Size(84, 23);
            this.selInputBtn.TabIndex = 6;
            this.selInputBtn.Text = "圈選填入";
            this.selInputBtn.UseVisualStyleBackColor = true;
            this.selInputBtn.Visible = false;
            this.selInputBtn.Click += new System.EventHandler(this.selInputBtn_Click);
            // 
            // TableInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.ContextMenuStrip = this.copyPasteMenuStrip;
            this.Controls.Add(this.selInputBtn);
            this.Controls.Add(this.unitLbl);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.averageBtn);
            this.Controls.Add(this.singleValueTxt);
            this.Controls.Add(this.singleValueRdo);
            this.Controls.Add(this.tableValueRdo);
            this.Controls.Add(this.dataGridView);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "TableInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TableInputForm";
            this.Load += new System.EventHandler(this.TableInputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.copyPasteMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.RadioButton tableValueRdo;
        private System.Windows.Forms.RadioButton singleValueRdo;
        private System.Windows.Forms.TextBox singleValueTxt;
        private System.Windows.Forms.ContextMenuStrip copyPasteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Button averageBtn;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ToolStripMenuItem ValueToolStripMenuItem;
        public System.Windows.Forms.Label unitLbl;
        private System.Windows.Forms.Button selInputBtn;
    }
}