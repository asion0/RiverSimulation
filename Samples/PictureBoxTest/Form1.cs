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

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private PictureBoxCtrl.PictureBox pb;
		private PictureBoxCtrl.PictureBox pb2;
		private PictureBoxCtrl.PictureBox pb3;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor

		public Form1()
		{
			InitializeComponent();
			
			pb.Picture = @"..\..\sample pic\Blumen.jpeg";
			pb.Border = BorderStyle.FixedSingle;
			pb2.Picture = @"..\..\sample pic\FingerRight.gif";
			pb2.Border = BorderStyle.Fixed3D;
			pb3.Picture = @"..\..\sample pic\test.bmp";
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.pb = new PictureBoxCtrl.PictureBox();
			this.pb2 = new PictureBoxCtrl.PictureBox();
			this.pb3 = new PictureBoxCtrl.PictureBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(10, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(195, 30);
			this.label1.TabIndex = 1;
			this.label1.Text = "Border = BorderStyle.FixedSingle";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(220, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(195, 30);
			this.label2.TabIndex = 4;
			this.label2.Text = "Border = BorderStyle.Fixed3D";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(435, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 35);
			this.label3.TabIndex = 5;
			this.label3.Text = "Border = BorderStyle.None";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(5, 395);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(200, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Browse ...";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(215, 255);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(210, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "Browse ...";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(435, 170);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(110, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "Browse ...";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// pb
			// 
			this.pb.Border = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pb.Location = new System.Drawing.Point(10, 50);
			this.pb.Name = "pb";
			this.pb.Picture = "";
			this.pb.Size = new System.Drawing.Size(195, 335);
			this.pb.TabIndex = 9;
			// 
			// pb2
			// 
			this.pb2.Border = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pb2.Location = new System.Drawing.Point(215, 50);
			this.pb2.Name = "pb2";
			this.pb2.Picture = "";
			this.pb2.Size = new System.Drawing.Size(210, 195);
			this.pb2.TabIndex = 10;
			// 
			// pb3
			// 
			this.pb3.Border = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pb3.Location = new System.Drawing.Point(435, 55);
			this.pb3.Name = "pb3";
			this.pb3.Picture = "";
			this.pb3.Size = new System.Drawing.Size(110, 110);
			this.pb3.TabIndex = 11;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(562, 431);
			this.Controls.Add(this.pb3);
			this.Controls.Add(this.pb2);
			this.Controls.Add(this.pb);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
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
		/// Browse for the 1st PicturBox
		/// </summary>
		private void button1_Click(object sender, System.EventArgs e)
		{
			Browse ( ref pb );
		}

		/// <summary>
		/// Browse for the 2nd PicturBox
		/// </summary>
		private void button2_Click(object sender, System.EventArgs e)
		{
			Browse ( ref pb2 );
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
