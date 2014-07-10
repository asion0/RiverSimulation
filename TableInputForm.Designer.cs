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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableValueRdo = new System.Windows.Forms.RadioButton();
            this.singleValueRdo = new System.Windows.Forms.RadioButton();
            this.singleValueText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(-1, 91);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(781, 409);
            this.dataGridView.TabIndex = 0;
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
            this.tableValueRdo.CheckedChanged += new System.EventHandler(this.tableValue_CheckedChanged);
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
            this.singleValueRdo.CheckedChanged += new System.EventHandler(this.singleValue_CheckedChanged);
            // 
            // singleValueText
            // 
            this.singleValueText.Location = new System.Drawing.Point(32, 36);
            this.singleValueText.MaxLength = 8;
            this.singleValueText.Name = "singleValueText";
            this.singleValueText.Size = new System.Drawing.Size(141, 22);
            this.singleValueText.TabIndex = 2;
            // 
            // TableInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 501);
            this.Controls.Add(this.singleValueText);
            this.Controls.Add(this.singleValueRdo);
            this.Controls.Add(this.tableValueRdo);
            this.Controls.Add(this.dataGridView);
            this.Name = "TableInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TableInputForm";
            this.Load += new System.EventHandler(this.TableInputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.RadioButton tableValueRdo;
        private System.Windows.Forms.RadioButton singleValueRdo;
        private System.Windows.Forms.TextBox singleValueText;
    }
}