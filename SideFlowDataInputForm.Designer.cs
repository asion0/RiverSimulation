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
            this.averageBtn = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.unitLbl = new System.Windows.Forms.Label();
            this.selInputBtn = new System.Windows.Forms.Button();
            this.settingBtn = new System.Windows.Forms.Button();
            this.timeGapTxt = new System.Windows.Forms.TextBox();
            this.timeStepLbl = new System.Windows.Forms.Label();
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
            this.tableValueRdo.Location = new System.Drawing.Point(121, 12);
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
            this.selInputBtn.Location = new System.Drawing.Point(226, 9);
            this.selInputBtn.Name = "selInputBtn";
            this.selInputBtn.Size = new System.Drawing.Size(84, 23);
            this.selInputBtn.TabIndex = 6;
            this.selInputBtn.Text = "圈選填入";
            this.selInputBtn.UseVisualStyleBackColor = true;
            this.selInputBtn.Visible = false;
            this.selInputBtn.Click += new System.EventHandler(this.selInputBtn_Click);
            // 
            // settingBtn
            // 
            this.settingBtn.Location = new System.Drawing.Point(369, 656);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(120, 26);
            this.settingBtn.TabIndex = 7;
            this.settingBtn.Text = "設定";
            this.settingBtn.UseVisualStyleBackColor = true;
            this.settingBtn.Visible = false;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // timeGapTxt
            // 
            this.timeGapTxt.Location = new System.Drawing.Point(252, 658);
            this.timeGapTxt.Name = "timeGapTxt";
            this.timeGapTxt.Size = new System.Drawing.Size(100, 22);
            this.timeGapTxt.TabIndex = 8;
            this.timeGapTxt.Visible = false;
            // 
            // timeStepLbl
            // 
            this.timeStepLbl.AutoSize = true;
            this.timeStepLbl.Location = new System.Drawing.Point(193, 661);
            this.timeStepLbl.Name = "timeStepLbl";
            this.timeStepLbl.Size = new System.Drawing.Size(53, 12);
            this.timeStepLbl.TabIndex = 9;
            this.timeStepLbl.Text = "固定時距";
            this.timeStepLbl.Visible = false;
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
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(1, 81);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(954, 560);
            this.tabControl.TabIndex = 10;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(946, 534);
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
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(946, 534);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(946, 534);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Y";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ContextMenuStrip = this.copyPasteMenuStrip;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(946, 483);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(946, 534);
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
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(946, 534);
            this.dataGridView3.TabIndex = 0;
            // 
            // SideFlowDataInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.ContextMenuStrip = this.copyPasteMenuStrip;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.timeStepLbl);
            this.Controls.Add(this.timeGapTxt);
            this.Controls.Add(this.settingBtn);
            this.Controls.Add(this.selInputBtn);
            this.Controls.Add(this.unitLbl);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.averageBtn);
            this.Controls.Add(this.singleValueTxt);
            this.Controls.Add(this.singleValueRdo);
            this.Controls.Add(this.tableValueRdo);
            this.MinimumSize = new System.Drawing.Size(800, 600);
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
        private System.Windows.Forms.Button averageBtn;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ToolStripMenuItem ValueToolStripMenuItem;
        public System.Windows.Forms.Label unitLbl;
        private System.Windows.Forms.Button selInputBtn;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.TextBox timeGapTxt;
        private System.Windows.Forms.Label timeStepLbl;
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