using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace AlbumViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LoadAlbums();
        }

        private void LoadAlbums()
        {
            // Get all albums, including photos, from the database
            ReadOnlyCollection<Album> albums = Data.GetPhotoAlbums();

            // Now iterate through them and add to treeview
            foreach(Album album in albums)
            {
                TreeNode albumNode = new TreeNode(album.Name);
                
                // Add the album struct to the Tag for later
                // retrieval of info without database call
                albumNode.Tag = album;

                treeAlbums.Nodes.Add(albumNode);

                // Add each photo in album to treenode for the album
                foreach(Photo photo in album.Photos)
                {
                    TreeNode photoNode = new TreeNode(photo.Name);
                    photoNode.Tag = photo;

                    albumNode.Nodes.Add(photoNode);
                }                
            }
        }

        #region Menu Events Handlers

        /// <summary>
        /// Exit menu handler
        /// </summary>
        private void OnExit(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewAlbum(object sender, EventArgs e)
        {
            Album album = Data.AddAlbum();

            TreeNode node = treeAlbums.Nodes.Add(album.Name);
            node.Tag = album;
        }

        #endregion

        #region TreeView Context Menu Handlers 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            // We are only interested in right mouse clicks
            if(e.Button == MouseButtons.Right)
            {
                // Attempt to get the node the mouse clicked on
                TreeNode node = treeAlbums.GetNodeAt(e.X, e.Y);
                if(node != null)
                {
                    // Select the tree item
                    treeAlbums.SelectedNode = node;

                    // Check what type of node was clicked and edit
                    // context menu
                    if(node.Tag is Photo)
                    {
                        contextMenuAlbum.Items[0].Visible = false;
                    }
                    else
                    {
                        contextMenuAlbum.Items[0].Visible = true;
                    }
                }
            }
        }

        #endregion

        #region Album context menu handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddPhoto(object sender, EventArgs e)
        {
            if(DialogResult.OK == openFileDialog1.ShowDialog())
            {
                // Retrieve the Album to add photo(s) to
                Album album = (Album)treeAlbums.SelectedNode.Tag;

                // We allow multiple selections so loop through each one
                foreach(string file in openFileDialog1.FileNames)
                {
                    // Create a new stream to load this photo into
                    System.IO.FileStream stream = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    // Create a buffer to hold the stream bytes
                    byte[] buffer = new byte[stream.Length];
                    // Read the bytes from this stream
                    stream.Read(buffer, 0, (int)stream.Length);
                    // Now we can close the stream
                    stream.Close();

                    Photo photo = new Photo()
                    {
                        // Extract out the name of the file an use it for the name
                        // of the photo
                        Name = System.IO.Path.GetFileNameWithoutExtension(file),
                        Image = buffer
                    };

                    // Insert the image into the database and add it to the tree
                    Data.AddPhoto(album.Id, photo);
                    buffer = null;

                    // Add the photo to the album node
                    TreeNode node = treeAlbums.SelectedNode.Nodes.Add(photo.Name);
                    node.Tag = photo;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDelete(object sender, EventArgs e)
        {
            // Determine the type of node
            if(treeAlbums.SelectedNode.Tag is Album)
            {
                Data.DeleteAlbum(((Album)treeAlbums.SelectedNode.Tag).Id);
            }
            else if(treeAlbums.SelectedNode.Tag is Photo)
            {
                Data.DeletePhoto(((Photo)treeAlbums.SelectedNode.Tag).Id);
            }

            // Remove the node from the tree
            treeAlbums.SelectedNode.Remove();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterSelect(object sender, TreeViewEventArgs e)
        {
            DisplayPanel.Visible = true;

            if(treeAlbums.SelectedNode.Tag is Album)
            {
                Album album = (Album)treeAlbums.SelectedNode.Tag;

                DisplayName.Text = album.Name;
                DisplayDescription.Text = album.Description;

                pictureBox.Image = null;
            }
            else if(treeAlbums.SelectedNode.Tag is Photo)
            {
                Photo photo = (Photo)treeAlbums.SelectedNode.Tag;

                DisplayName.Text = photo.Name;
                DisplayDescription.Text = photo.Description;

                System.IO.MemoryStream stream = new System.IO.MemoryStream(photo.Image, true);
                stream.Write(photo.Image, 0, photo.Image.Length);

                // Draw photo to scale of picturebox
                DrawToScale(new Bitmap(stream));
            }
            else
            {
                DisplayPanel.Visible = false;
            }
        }

        /// <summary>
        /// This function will determine the scale the image should be rendered
        /// at from the size of the picture box and image
        /// </summary>
        /// <param name="bmp">Image to scale and assign to picture box</param>
        private void DrawToScale(Image bmp)
        {
            // The client rectangle
            Rectangle rc = pictureBox.ClientRectangle;

            // From Programming Windows with C#, by Charles Petzold
            // Figure out the scaling necessary for the image
            SizeF size = new SizeF(bmp.Width / bmp.HorizontalResolution, bmp.Height / bmp.VerticalResolution);
            float fScale = Math.Min(rc.Width / size.Width, rc.Height / size.Height);

            size.Width *= fScale;
            size.Height *= fScale;

            // Create a new bitmap of the proper size for the existing bitmap
            // and assign it to the picture box
            pictureBox.Image = new Bitmap(bmp, size.ToSize());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEdit(object sender, EventArgs e)
        {
            Edit.Visible = false;
            Save.Visible = true;
            Cancel.Visible = true;

            EditName.Text = DisplayName.Text;
            EditDescription.Text = DisplayDescription.Text;

            EditName.Visible = true;
            EditDescription.Visible = true;

            DisplayName.Visible = false;
            DisplayDescription.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSave(object sender, EventArgs e)
        {
            Edit.Visible = true;
            Save.Visible = false;
            Cancel.Visible = false;

            DisplayName.Text = EditName.Text;
            DisplayDescription.Text = EditDescription.Text;

            EditName.Visible = false;
            EditDescription.Visible = false;

            DisplayName.Visible = true;
            DisplayDescription.Visible = true;

            // Determine the type of node
            if(treeAlbums.SelectedNode.Tag is Album)
            {
                Album album = (Album)treeAlbums.SelectedNode.Tag;

                album.Name = EditName.Text;
                album.Description = EditDescription.Text;

                Data.UpdateAlbum(album);

                treeAlbums.SelectedNode.Tag = album;
            }
            else if(treeAlbums.SelectedNode.Tag is Photo)
            {
                Photo photo = (Photo)treeAlbums.SelectedNode.Tag;

                photo.Name = EditName.Text;
                photo.Description = EditDescription.Text;

                Data.UpdatePhoto(photo);

                treeAlbums.SelectedNode.Tag = photo;
            }

            treeAlbums.SelectedNode.Text = EditName.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancel(object sender, EventArgs e)
        {
            Edit.Visible = true;
            Save.Visible = false;
            Cancel.Visible = false;

            EditName.Visible = false;
            EditDescription.Visible = false;

            DisplayName.Visible = true;
            DisplayDescription.Visible = true;
        }        
    }
}
