namespace RiverSimulationApplication
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.exitBtn = new System.Windows.Forms.Button();
            this.openFileBtn = new System.Windows.Forms.Button();
            this.selectProjectBtn = new System.Windows.Forms.Button();
            this.newProjectBtn = new System.Windows.Forms.Button();
            this.folderOpen = new System.Windows.Forms.FolderBrowserDialog();
            this.fileOpen = new System.Windows.Forms.OpenFileDialog();
            this.newFileBtn = new System.Windows.Forms.Button();
            this.projectTitle = new System.Windows.Forms.Label();
            this.projectDescriptBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(617, 454);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(124, 40);
            this.exitBtn.TabIndex = 0;
            this.exitBtn.Text = "離開";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // openFileBtn
            // 
            this.openFileBtn.Enabled = false;
            this.openFileBtn.Location = new System.Drawing.Point(617, 387);
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(124, 40);
            this.openFileBtn.TabIndex = 0;
            this.openFileBtn.Text = "開啟案例";
            this.openFileBtn.UseVisualStyleBackColor = true;
            this.openFileBtn.Click += new System.EventHandler(this.openFileBtn_Click);
            // 
            // selectProjectBtn
            // 
            this.selectProjectBtn.Location = new System.Drawing.Point(617, 257);
            this.selectProjectBtn.Name = "selectProjectBtn";
            this.selectProjectBtn.Size = new System.Drawing.Size(124, 40);
            this.selectProjectBtn.TabIndex = 0;
            this.selectProjectBtn.Text = "選取專案";
            this.selectProjectBtn.UseVisualStyleBackColor = true;
            this.selectProjectBtn.Click += new System.EventHandler(this.selectProject_Click);
            // 
            // newProjectBtn
            // 
            this.newProjectBtn.Location = new System.Drawing.Point(617, 192);
            this.newProjectBtn.Name = "newProjectBtn";
            this.newProjectBtn.Size = new System.Drawing.Size(124, 40);
            this.newProjectBtn.TabIndex = 0;
            this.newProjectBtn.Text = "建立專案";
            this.newProjectBtn.UseVisualStyleBackColor = true;
            this.newProjectBtn.Click += new System.EventHandler(this.newProjectBtn_Click);
            // 
            // fileOpen
            // 
            this.fileOpen.FileName = "fileOpen";
            // 
            // newFileBtn
            // 
            this.newFileBtn.Enabled = false;
            this.newFileBtn.Location = new System.Drawing.Point(617, 322);
            this.newFileBtn.Name = "newFileBtn";
            this.newFileBtn.Size = new System.Drawing.Size(124, 40);
            this.newFileBtn.TabIndex = 0;
            this.newFileBtn.Text = "新增案例";
            this.newFileBtn.UseVisualStyleBackColor = true;
            this.newFileBtn.Click += new System.EventHandler(this.newFileBtn_Click);
            // 
            // projectTitle
            // 
            this.projectTitle.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.projectTitle.Location = new System.Drawing.Point(36, 319);
            this.projectTitle.Name = "projectTitle";
            this.projectTitle.Size = new System.Drawing.Size(347, 48);
            this.projectTitle.TabIndex = 3;
            this.projectTitle.Text = "專案：";
            this.projectTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectDescriptBtn
            // 
            this.projectDescriptBtn.BackColor = System.Drawing.SystemColors.Control;
            this.projectDescriptBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("projectDescriptBtn.BackgroundImage")));
            this.projectDescriptBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.projectDescriptBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.projectDescriptBtn.Enabled = false;
            this.projectDescriptBtn.FlatAppearance.BorderSize = 0;
            this.projectDescriptBtn.Location = new System.Drawing.Point(389, 316);
            this.projectDescriptBtn.Name = "projectDescriptBtn";
            this.projectDescriptBtn.Size = new System.Drawing.Size(86, 52);
            this.projectDescriptBtn.TabIndex = 2;
            this.projectDescriptBtn.UseVisualStyleBackColor = false;
            this.projectDescriptBtn.Click += new System.EventHandler(this.projectDescriptBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(801, 540);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(801, 539);
            this.Controls.Add(this.projectTitle);
            this.Controls.Add(this.projectDescriptBtn);
            this.Controls.Add(this.newFileBtn);
            this.Controls.Add(this.newProjectBtn);
            this.Controls.Add(this.selectProjectBtn);
            this.Controls.Add(this.openFileBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水理動床模式使用介面";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button openFileBtn;
        private System.Windows.Forms.Button selectProjectBtn;
        private System.Windows.Forms.Button newProjectBtn;
        private System.Windows.Forms.FolderBrowserDialog folderOpen;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog fileOpen;
        private System.Windows.Forms.Button newFileBtn;
        private System.Windows.Forms.Button projectDescriptBtn;
        private System.Windows.Forms.Label projectTitle;
    }
}