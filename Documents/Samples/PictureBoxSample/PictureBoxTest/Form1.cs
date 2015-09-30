#region Using

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using PictureBoxCtrl;  // namespace for PictureBox control

#endregion

namespace PictureBoxTest
{
	public class Form1 : System.Windows.Forms.Form
	{
		#region Memebers

        private System.Windows.Forms.Button button3;
		private PictureBoxCtrl.PictureBox pb3;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor

		public Form1()
		{
			InitializeComponent();
			pb3.Picture = @"test.bmp";
			pb3.Border = BorderStyle.None;
		}

		#endregion

		#region Disposing

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form-Designer

		private void InitializeComponent()
		{
            this.button3 = new System.Windows.Forms.Button();
            this.pb3 = new PictureBoxCtrl.PictureBox();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(117, 256);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 27);
            this.button3.TabIndex = 8;
            this.button3.Text = "Browse ...";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pb3
            // 
            this.pb3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb3.Border = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb3.Location = new System.Drawing.Point(12, 17);
            this.pb3.Name = "pb3";
            this.pb3.Picture = "";
            this.pb3.Size = new System.Drawing.Size(320, 233);
            this.pb3.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(344, 295);
            this.Controls.Add(this.pb3);
            this.Controls.Add(this.button3);
            this.Name = "Form1";
            this.Text = "PicturBoxCtrl Demo";
            this.ResumeLayout(false);

		}
		#endregion

		#region Main

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#endregion

		#region Methods

		/// <summary>
		/// Use an OpenFileDialog to enable the user to browse for an image to open
		/// in the PictureBox. 
		/// </summary>
		private void Browse ( ref PictureBoxCtrl.PictureBox pbref )
		{
			OpenFileDialog Dir = new OpenFileDialog ();
			Dir.InitialDirectory = "C:\\";
			Dir.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.tif)|" + 
				"*.bmp;*.jpg;*.jpeg;*.gif;*.tif|Bitmaps (*.bmp)|*.bmp|" + 
				"GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|TIFs (*.tif)|*.tif";
			Dir.FilterIndex = 1;
			if ( Dir.ShowDialog () == DialogResult.OK ) 
			{
				pbref.Picture = Dir.FileName;
			}
		}

		/// <summary>
		/// Browse for the 3rd PicturBox
		/// </summary>
		private void button3_Click(object sender, System.EventArgs e)
		{
			Browse ( ref pb3 );
		}
	
		#endregion
	}
}
