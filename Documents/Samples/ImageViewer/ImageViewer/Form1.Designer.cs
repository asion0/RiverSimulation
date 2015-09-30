namespace ImageViewer
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkpicture4 = new System.Windows.Forms.CheckBox();
            this.chkpicture3 = new System.Windows.Forms.CheckBox();
            this.chkpicture2 = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.chkpicture1 = new System.Windows.Forms.CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnAddFromFile = new System.Windows.Forms.Button();
            this.btnAddToFile = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chkpicture4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkpicture3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkpicture2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkpicture1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(628, 467);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chkpicture4
            // 
            this.chkpicture4.AutoSize = true;
            this.chkpicture4.Location = new System.Drawing.Point(317, 447);
            this.chkpicture4.Name = "chkpicture4";
            this.chkpicture4.Size = new System.Drawing.Size(64, 17);
            this.chkpicture4.TabIndex = 8;
            this.chkpicture4.Text = "picture4";
            this.chkpicture4.UseVisualStyleBackColor = true;
            this.chkpicture4.Visible = false;
            // 
            // chkpicture3
            // 
            this.chkpicture3.AutoSize = true;
            this.chkpicture3.Location = new System.Drawing.Point(3, 447);
            this.chkpicture3.Name = "chkpicture3";
            this.chkpicture3.Size = new System.Drawing.Size(64, 17);
            this.chkpicture3.TabIndex = 7;
            this.chkpicture3.Text = "picture3";
            this.chkpicture3.UseVisualStyleBackColor = true;
            this.chkpicture3.Visible = false;
            // 
            // chkpicture2
            // 
            this.chkpicture2.AutoSize = true;
            this.chkpicture2.Location = new System.Drawing.Point(317, 233);
            this.chkpicture2.Name = "chkpicture2";
            this.chkpicture2.Size = new System.Drawing.Size(64, 17);
            this.chkpicture2.TabIndex = 6;
            this.chkpicture2.Text = "picture2";
            this.chkpicture2.UseVisualStyleBackColor = true;
            this.chkpicture2.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(317, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(308, 224);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(308, 224);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(317, 256);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(308, 185);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // chkpicture1
            // 
            this.chkpicture1.AutoSize = true;
            this.chkpicture1.Location = new System.Drawing.Point(3, 233);
            this.chkpicture1.Name = "chkpicture1";
            this.chkpicture1.Size = new System.Drawing.Size(64, 17);
            this.chkpicture1.TabIndex = 5;
            this.chkpicture1.Text = "picture1";
            this.chkpicture1.UseVisualStyleBackColor = true;
            this.chkpicture1.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(3, 256);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(308, 185);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnAddFromFile
            // 
            this.btnAddFromFile.Location = new System.Drawing.Point(4, 473);
            this.btnAddFromFile.Name = "btnAddFromFile";
            this.btnAddFromFile.Size = new System.Drawing.Size(104, 27);
            this.btnAddFromFile.TabIndex = 4;
            this.btnAddFromFile.Text = "Add From File";
            this.btnAddFromFile.UseVisualStyleBackColor = true;
            this.btnAddFromFile.Click += new System.EventHandler(this.btnAddFromFile_Click);
            // 
            // btnAddToFile
            // 
            this.btnAddToFile.Location = new System.Drawing.Point(127, 473);
            this.btnAddToFile.Name = "btnAddToFile";
            this.btnAddToFile.Size = new System.Drawing.Size(104, 27);
            this.btnAddToFile.TabIndex = 5;
            this.btnAddToFile.Text = "Add To File";
            this.btnAddToFile.UseVisualStyleBackColor = true;
            this.btnAddToFile.Click += new System.EventHandler(this.btnAddToFile_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(246, 473);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(120, 27);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Upload to Database";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(381, 473);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(144, 27);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "Download from Database";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(532, 474);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 26);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(661, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 501);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnAddToFile);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnAddFromFile);
            this.Name = "Form1";
            this.Text = "Image Viewer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnAddFromFile;
        private System.Windows.Forms.Button btnAddToFile;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckBox chkpicture4;
        private System.Windows.Forms.CheckBox chkpicture3;
        private System.Windows.Forms.CheckBox chkpicture2;
        private System.Windows.Forms.CheckBox chkpicture1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

