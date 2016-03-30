namespace RiverSimulationApplication
{
    partial class ResultTableForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.sel2Combo = new System.Windows.Forms.ComboBox();
            this.sel1Combo = new System.Windows.Forms.ComboBox();
            this.sel1Txt = new System.Windows.Forms.Label();
            this.sel2Txt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
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
            this.dataGridView.Location = new System.Drawing.Point(1, 39);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(953, 650);
            this.dataGridView.TabIndex = 1;
            // 
            // sel2Combo
            // 
            this.sel2Combo.FormattingEnabled = true;
            this.sel2Combo.Location = new System.Drawing.Point(567, 11);
            this.sel2Combo.Name = "sel2Combo";
            this.sel2Combo.Size = new System.Drawing.Size(121, 20);
            this.sel2Combo.TabIndex = 2;
            this.sel2Combo.Visible = false;
            // 
            // sel1Combo
            // 
            this.sel1Combo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sel1Combo.FormattingEnabled = true;
            this.sel1Combo.Location = new System.Drawing.Point(823, 12);
            this.sel1Combo.Name = "sel1Combo";
            this.sel1Combo.Size = new System.Drawing.Size(121, 20);
            this.sel1Combo.TabIndex = 3;
            this.sel1Combo.Visible = false;
            this.sel1Combo.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // sel1Txt
            // 
            this.sel1Txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sel1Txt.Location = new System.Drawing.Point(694, 15);
            this.sel1Txt.Name = "sel1Txt";
            this.sel1Txt.Size = new System.Drawing.Size(123, 16);
            this.sel1Txt.TabIndex = 5;
            this.sel1Txt.Text = "label2";
            this.sel1Txt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sel1Txt.Visible = false;
            // 
            // sel2Txt
            // 
            this.sel2Txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sel2Txt.Location = new System.Drawing.Point(438, 13);
            this.sel2Txt.Name = "sel2Txt";
            this.sel2Txt.Size = new System.Drawing.Size(123, 16);
            this.sel2Txt.TabIndex = 5;
            this.sel2Txt.Text = "label2";
            this.sel2Txt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sel2Txt.Visible = false;
            // 
            // ResultTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 689);
            this.Controls.Add(this.sel2Txt);
            this.Controls.Add(this.sel1Txt);
            this.Controls.Add(this.sel1Combo);
            this.Controls.Add(this.sel2Combo);
            this.Controls.Add(this.dataGridView);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ResultTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ResultTableForm";
            this.Load += new System.EventHandler(this.ResultTableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox sel2Combo;
        private System.Windows.Forms.ComboBox sel1Combo;
        private System.Windows.Forms.Label sel1Txt;
        private System.Windows.Forms.Label sel2Txt;
    }
}