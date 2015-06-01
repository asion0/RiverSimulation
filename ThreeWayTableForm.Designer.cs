namespace RiverSimulationApplication
{
    partial class ThreeWayTableForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ok = new System.Windows.Forms.Button();
            this.copyPasteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowTypeLbl = new System.Windows.Forms.Label();
            this.type1Rdo = new System.Windows.Forms.RadioButton();
            this.type2Rdo = new System.Windows.Forms.RadioButton();
            this.type2Btn = new System.Windows.Forms.Button();
            this.type1Btn = new System.Windows.Forms.Button();
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
            this.dataGridView.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.GridColor = System.Drawing.Color.DarkGray;
            this.dataGridView.Location = new System.Drawing.Point(0, 34);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(956, 606);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.Location = new System.Drawing.Point(824, 654);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(120, 26);
            this.ok.TabIndex = 5;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
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
            this.ValueToolStripMenuItem.Click += new System.EventHandler(this.ValueToolStripMenuItem_Click);
            // 
            // flowTypeLbl
            // 
            this.flowTypeLbl.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.flowTypeLbl.Location = new System.Drawing.Point(10, 10);
            this.flowTypeLbl.Name = "flowTypeLbl";
            this.flowTypeLbl.Size = new System.Drawing.Size(74, 21);
            this.flowTypeLbl.TabIndex = 6;
            this.flowTypeLbl.Text = "定量流";
            // 
            // type1Rdo
            // 
            this.type1Rdo.AutoSize = true;
            this.type1Rdo.Location = new System.Drawing.Point(95, 9);
            this.type1Rdo.Name = "type1Rdo";
            this.type1Rdo.Size = new System.Drawing.Size(14, 13);
            this.type1Rdo.TabIndex = 7;
            this.type1Rdo.TabStop = true;
            this.type1Rdo.UseVisualStyleBackColor = true;
            this.type1Rdo.CheckedChanged += new System.EventHandler(this.type1Rdo_CheckedChanged);
            // 
            // type2Rdo
            // 
            this.type2Rdo.AutoSize = true;
            this.type2Rdo.Location = new System.Drawing.Point(212, 9);
            this.type2Rdo.Name = "type2Rdo";
            this.type2Rdo.Size = new System.Drawing.Size(14, 13);
            this.type2Rdo.TabIndex = 7;
            this.type2Rdo.TabStop = true;
            this.type2Rdo.UseVisualStyleBackColor = true;
            this.type2Rdo.CheckedChanged += new System.EventHandler(this.type2Rdo_CheckedChanged);
            // 
            // type2Btn
            // 
            this.type2Btn.Location = new System.Drawing.Point(232, 5);
            this.type2Btn.Name = "type2Btn";
            this.type2Btn.Size = new System.Drawing.Size(75, 23);
            this.type2Btn.TabIndex = 8;
            this.type2Btn.Text = "非均勻入流";
            this.type2Btn.UseVisualStyleBackColor = true;
            this.type2Btn.Click += new System.EventHandler(this.type2Btn_Click);
            // 
            // type1Btn
            // 
            this.type1Btn.Location = new System.Drawing.Point(116, 5);
            this.type1Btn.Name = "type1Btn";
            this.type1Btn.Size = new System.Drawing.Size(75, 23);
            this.type1Btn.TabIndex = 9;
            this.type1Btn.Text = "均勻入流";
            this.type1Btn.UseVisualStyleBackColor = true;
            this.type1Btn.Click += new System.EventHandler(this.type1Btn_Click);
            // 
            // ThreeWayTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.ContextMenuStrip = this.copyPasteMenuStrip;
            this.Controls.Add(this.type1Btn);
            this.Controls.Add(this.type2Btn);
            this.Controls.Add(this.type2Rdo);
            this.Controls.Add(this.type1Rdo);
            this.Controls.Add(this.flowTypeLbl);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.dataGridView);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ThreeWayTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ThreeWayTableForm";
            this.Load += new System.EventHandler(this.ThreeWayTableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.copyPasteMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ContextMenuStrip copyPasteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ValueToolStripMenuItem;
        private System.Windows.Forms.Label flowTypeLbl;
        private System.Windows.Forms.RadioButton type1Rdo;
        private System.Windows.Forms.RadioButton type2Rdo;
        private System.Windows.Forms.Button type2Btn;
        private System.Windows.Forms.Button type1Btn;
    }
}