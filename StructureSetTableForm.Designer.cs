namespace RiverSimulationApplication
{
    partial class StructureSetTableForm
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
            this.dataGv = new System.Windows.Forms.DataGridView();
            this.addBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGv)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGv
            // 
            this.dataGv.AllowUserToAddRows = false;
            this.dataGv.AllowUserToDeleteRows = false;
            this.dataGv.AllowUserToResizeColumns = false;
            this.dataGv.AllowUserToResizeRows = false;
            this.dataGv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGv.Location = new System.Drawing.Point(-1, 66);
            this.dataGv.Name = "dataGv";
            this.dataGv.ReadOnly = true;
            this.dataGv.RowTemplate.Height = 24;
            this.dataGv.Size = new System.Drawing.Size(970, 536);
            this.dataGv.TabIndex = 0;
            this.dataGv.SelectionChanged += new System.EventHandler(this.dataGv_SelectionChanged);
            // 
            // addBtn
            // 
            this.addBtn.Enabled = false;
            this.addBtn.Location = new System.Drawing.Point(159, 22);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(103, 33);
            this.addBtn.TabIndex = 1;
            this.addBtn.Text = "加入";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Enabled = false;
            this.removeBtn.Location = new System.Drawing.Point(268, 22);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(103, 33);
            this.removeBtn.TabIndex = 1;
            this.removeBtn.Text = "刪除";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(486, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "按住Ctrl或是Shift再點擊可以多重選取，選取空白區域可以加入，選取藍色區域可以刪除。";
            // 
            // selCombo
            // 
            this.selCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selCombo.FormattingEnabled = true;
            this.selCombo.Location = new System.Drawing.Point(17, 34);
            this.selCombo.Name = "selCombo";
            this.selCombo.Size = new System.Drawing.Size(132, 20);
            this.selCombo.TabIndex = 3;
            this.selCombo.SelectedIndexChanged += new System.EventHandler(this.dryBedCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "選取編輯項目";
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(377, 22);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(103, 33);
            this.editBtn.TabIndex = 1;
            this.editBtn.Text = "編輯高程";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.edit_Click);
            // 
            // StructureSetTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 603);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.dataGv);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "StructureSetTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GridGroupTableForm";
            this.Load += new System.EventHandler(this.GridGroupTableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGv;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button editBtn;
    }
}