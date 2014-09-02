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
            this.exitBtn = new System.Windows.Forms.Button();
            this.delBtn = new System.Windows.Forms.Button();
            this.openBtn = new System.Windows.Forms.Button();
            this.newBtn = new System.Windows.Forms.Button();
            this.folderOpen = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(644, 461);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(124, 40);
            this.exitBtn.TabIndex = 0;
            this.exitBtn.Text = "離開";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(644, 399);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(124, 40);
            this.delBtn.TabIndex = 0;
            this.delBtn.Text = "刪除專案";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(644, 333);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(124, 40);
            this.openBtn.TabIndex = 0;
            this.openBtn.Text = "開啟專案";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(644, 271);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(124, 40);
            this.newBtn.TabIndex = 0;
            this.newBtn.Text = "建立專案";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 544);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.exitBtn);
            this.Name = "LoginForm";
            this.Text = "水理動床模式使用介面";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.FolderBrowserDialog folderOpen;
    }
}