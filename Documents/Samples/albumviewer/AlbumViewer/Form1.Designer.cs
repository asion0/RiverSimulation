namespace AlbumViewer
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
            if(disposing && (components != null))
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAlbumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeAlbums = new System.Windows.Forms.TreeView();
            this.contextMenuAlbum = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPhotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DisplayPanel = new System.Windows.Forms.Panel();
            this.Exit = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.DisplayDescription = new System.Windows.Forms.Label();
            this.DisplayName = new System.Windows.Forms.Label();
            this.EditDescription = new System.Windows.Forms.TextBox();
            this.EditName = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuAlbum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.DisplayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAlbumToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newAlbumToolStripMenuItem
            // 
            this.newAlbumToolStripMenuItem.Name = "newAlbumToolStripMenuItem";
            this.newAlbumToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.newAlbumToolStripMenuItem.Text = "&New Album";
            this.newAlbumToolStripMenuItem.Click += new System.EventHandler(this.OnNewAlbum);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // treeAlbums
            // 
            this.treeAlbums.ContextMenuStrip = this.contextMenuAlbum;
            this.treeAlbums.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeAlbums.Location = new System.Drawing.Point(0, 24);
            this.treeAlbums.Name = "treeAlbums";
            this.treeAlbums.Size = new System.Drawing.Size(225, 538);
            this.treeAlbums.TabIndex = 1;
            this.treeAlbums.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterSelect);
            this.treeAlbums.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // contextMenuAlbum
            // 
            this.contextMenuAlbum.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPhotoToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuAlbum.Name = "contextMenuAlbum";
            this.contextMenuAlbum.Size = new System.Drawing.Size(132, 48);
            // 
            // addPhotoToolStripMenuItem
            // 
            this.addPhotoToolStripMenuItem.Name = "addPhotoToolStripMenuItem";
            this.addPhotoToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.addPhotoToolStripMenuItem.Text = "Add Photo";
            this.addPhotoToolStripMenuItem.Click += new System.EventHandler(this.OnAddPhoto);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.OnDelete);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox.Location = new System.Drawing.Point(225, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(559, 300);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image files|*.gif;*.jpg;*.jpeg;*png";
            this.openFileDialog1.Multiselect = true;
            // 
            // DisplayPanel
            // 
            this.DisplayPanel.Controls.Add(this.Cancel);
            this.DisplayPanel.Controls.Add(this.Exit);
            this.DisplayPanel.Controls.Add(this.Save);
            this.DisplayPanel.Controls.Add(this.Edit);
            this.DisplayPanel.Controls.Add(this.DisplayDescription);
            this.DisplayPanel.Controls.Add(this.DisplayName);
            this.DisplayPanel.Controls.Add(this.EditDescription);
            this.DisplayPanel.Controls.Add(this.EditName);
            this.DisplayPanel.Location = new System.Drawing.Point(232, 331);
            this.DisplayPanel.Name = "DisplayPanel";
            this.DisplayPanel.Size = new System.Drawing.Size(540, 219);
            this.DisplayPanel.TabIndex = 5;
            this.DisplayPanel.Visible = false;
            // 
            // Exit
            // 
            this.Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Exit.Location = new System.Drawing.Point(462, 193);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 6;
            this.Exit.Text = "E&xit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.OnExit);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(381, 193);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 3;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Visible = false;
            this.Save.Click += new System.EventHandler(this.OnSave);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(381, 193);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(75, 23);
            this.Edit.TabIndex = 2;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.OnEdit);
            // 
            // DisplayDescription
            // 
            this.DisplayDescription.AutoSize = true;
            this.DisplayDescription.Location = new System.Drawing.Point(4, 27);
            this.DisplayDescription.Name = "DisplayDescription";
            this.DisplayDescription.Size = new System.Drawing.Size(94, 13);
            this.DisplayDescription.TabIndex = 1;
            this.DisplayDescription.Text = "DisplayDescription";
            // 
            // DisplayName
            // 
            this.DisplayName.AutoSize = true;
            this.DisplayName.Location = new System.Drawing.Point(4, 4);
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.Size = new System.Drawing.Size(69, 13);
            this.DisplayName.TabIndex = 0;
            this.DisplayName.Text = "DisplayName";
            // 
            // EditDescription
            // 
            this.EditDescription.Location = new System.Drawing.Point(9, 29);
            this.EditDescription.Multiline = true;
            this.EditDescription.Name = "EditDescription";
            this.EditDescription.Size = new System.Drawing.Size(214, 102);
            this.EditDescription.TabIndex = 5;
            this.EditDescription.Visible = false;
            // 
            // EditName
            // 
            this.EditName.Location = new System.Drawing.Point(9, 3);
            this.EditName.Name = "EditName";
            this.EditName.Size = new System.Drawing.Size(214, 20);
            this.EditName.TabIndex = 4;
            this.EditName.Visible = false;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(300, 193);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "&Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Visible = false;
            this.Cancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.DisplayPanel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.treeAlbums);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Album Viewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuAlbum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.DisplayPanel.ResumeLayout(false);
            this.DisplayPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAlbumToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView treeAlbums;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuAlbum;
        private System.Windows.Forms.ToolStripMenuItem addPhotoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Panel DisplayPanel;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Label DisplayDescription;
        private System.Windows.Forms.Label DisplayName;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.TextBox EditDescription;
        private System.Windows.Forms.TextBox EditName;
        private System.Windows.Forms.Button Cancel;
    }
}

