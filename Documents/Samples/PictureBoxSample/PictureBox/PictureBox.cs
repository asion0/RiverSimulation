/* Title:
 * PictureBox with zoom and scroll functionallity
 * 
 * Author:
 * Alexander Kloep Apr. 2005
 * Alexander.Kloep@gmx.net
 * 
 * Reason:
 * In a past project i designed a GUI with a PictureBox control on it. Because of the low screen 
 * resolution i couldn愒 make the GUI big enough to show the whole picture. So i decided to develop
 * my own scrollable picturebox with the special highlight of zooming functionallity.
 * 
 * The solution: 
 * When the mouse cursor enters the ctrl, the cursorstyle changes and you are able to zoom in or out 
 * with the mousewheel. The princip of the zooming effect is to raise or to lower the inner picturebox 
 * size by a fixed zooming factor. The scroolbars appear automatically when the inner picturebox
 * gets bigger than the ctrl.
 *  
 * Here it is...
 * 
 * Last modification: 06/04/2005
 */

#region Usings

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

#endregion

namespace PictureBoxCtrl
{
	/// <summary>
	/// Summary for the PictureBox Ctrl
	/// </summary>
	public class PictureBox : System.Windows.Forms.UserControl
	{
		#region Members

		private System.Windows.Forms.PictureBox PicBox;
		private System.Windows.Forms.Panel OuterPanel;
		private System.ComponentModel.Container components = null;
		private string m_sPicName = "";
        private System.Drawing.Point refPoint = new System.Drawing.Point(0, 0);
		#endregion

		#region Constants

		private double ZOOMFACTOR = 1.25;	// = 25% smaller or larger
		private int MINMAX = 5;				// 5 times bigger or smaller than the ctrl

		#endregion

		#region Designer generated code

		private void InitializeComponent()
		{
			this.PicBox = new System.Windows.Forms.PictureBox();
			this.OuterPanel = new System.Windows.Forms.Panel();
			this.OuterPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// PicBox
			// 
			this.PicBox.Location = new System.Drawing.Point(0, 0);
			this.PicBox.Name = "PicBox";
			this.PicBox.Size = new System.Drawing.Size(150, 140);
			this.PicBox.TabIndex = 3;
			this.PicBox.TabStop = false;
			// 
			// OuterPanel
			// 
			//this.OuterPanel.AutoScroll = true;
			this.OuterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OuterPanel.Controls.Add(this.PicBox);
			this.OuterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OuterPanel.Location = new System.Drawing.Point(0, 0);
			this.OuterPanel.Name = "OuterPanel";
			this.OuterPanel.Size = new System.Drawing.Size(210, 190);
			this.OuterPanel.TabIndex = 4;
			// 
			// PictureBox
			// 
			this.Controls.Add(this.OuterPanel);
			this.Name = "PictureBox";
			this.Size = new System.Drawing.Size(210, 190);
			this.OuterPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Constructors

		public PictureBox()
		{
			InitializeComponent ();
			InitCtrl ();	// my special settings for the ctrl
		}

		#endregion

		#region Properties

		/// <summary>
		/// Property to select the picture which is displayed in the picturebox. If the 
		/// file doesn愒 exist or we receive an exception, the picturebox displays 
		/// a red cross.
		/// </summary>
		/// <value>Complete filename of the picture, including path information</value>
		/// <remarks>Supported fileformat: *.gif, *.tif, *.jpg, *.bmp</remarks>
		[ Browsable ( false ) ]
		public string Picture
		{
			get { return m_sPicName; }
			set 
			{
				if ( null != value )
				{
					if ( System.IO.File.Exists ( value ) )
					{
						try
						{
							PicBox.Image = Image.FromFile ( value );
							m_sPicName = value;
						}
                        catch/* ( OutOfMemoryException ex )*/
						{
							RedCross ();
						}
					}
					else
					{				
						RedCross ();
					}
				}
			}
		}

		/// <summary>
		/// Set the frametype of the picturbox
		/// </summary>
		[ Browsable ( false ) ]
		public BorderStyle Border
		{
			get { return OuterPanel.BorderStyle; }
			set { OuterPanel.BorderStyle = value; }
		}

		#endregion

		#region Other Methods

		/// <summary>
		/// Special settings for the picturebox ctrl
		/// </summary>
		private void InitCtrl ()
		{
			PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
			PicBox.Location = new Point ( 0, 0 );
			OuterPanel.Dock = DockStyle.Fill;
			OuterPanel.Cursor = System.Windows.Forms.Cursors.NoMove2D;
			//OuterPanel.AutoScroll = true;
			OuterPanel.MouseEnter += new EventHandler(PicBox_MouseEnter);
			PicBox.MouseEnter += new EventHandler(PicBox_MouseEnter);
			OuterPanel.MouseWheel += new MouseEventHandler(PicBox_MouseWheel);
            PicBox.MouseDown += new MouseEventHandler(PicBox_MouseDown);
            PicBox.MouseUp += new MouseEventHandler(PicBox_MouseUp);
            PicBox.MouseMove += new MouseEventHandler(PicBox_MouseMove);
            PicBox.BackgroundImageChanged += new System.EventHandler(PicBox_BackgroundImageChanged);
        }

		/// <summary>
		/// Create a simple red cross as a bitmap and display it in the picturebox
		/// </summary>
		private void RedCross ()
		{
			Bitmap bmp = new Bitmap ( OuterPanel.Width, OuterPanel.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555 );
			Graphics gr;
			gr = Graphics.FromImage ( bmp );
			Pen pencil = new Pen ( Color.Red, 5 );
			gr.DrawLine ( pencil, 0, 0, OuterPanel.Width, OuterPanel.Height );
			gr.DrawLine ( pencil, 0, OuterPanel.Height, OuterPanel.Width, 0  );
			PicBox.Image = bmp;
			gr.Dispose ();
		}

		#endregion

		#region Zooming Methods

		/// <summary>
		/// Make the PictureBox dimensions larger to effect the Zoom.
		/// </summary>
		/// <remarks>Maximum 5 times bigger</remarks>
		private void ZoomIn() 
		{
			if ( ( PicBox.Width < ( MINMAX * OuterPanel.Width ) ) &&
				( PicBox.Height < ( MINMAX * OuterPanel.Height ) ) )
			{
				PicBox.Width = Convert.ToInt32 ( PicBox.Width * ZOOMFACTOR );
				PicBox.Height = Convert.ToInt32 ( PicBox.Height * ZOOMFACTOR );
				PicBox.SizeMode = PictureBoxSizeMode.StretchImage; 
			}
		}

		/// <summary>
		/// Make the PictureBox dimensions smaller to effect the Zoom.
		/// </summary>
		/// <remarks>Minimum 5 times smaller</remarks>
		private void ZoomOut() 
		{
			if ( ( PicBox.Width > ( OuterPanel.Width / MINMAX ) ) &&
				( PicBox.Height > ( OuterPanel.Height / MINMAX ) ) )
			{
				PicBox.SizeMode = PictureBoxSizeMode.StretchImage; 
				PicBox.Width = Convert.ToInt32 ( PicBox.Width / ZOOMFACTOR );
				PicBox.Height = Convert.ToInt32 ( PicBox.Height / ZOOMFACTOR );
			}		
		}

		#endregion

		#region Mouse events

		/// <summary>
		/// We use the mousewheel to zoom the picture in or out
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PicBox_MouseWheel(object sender, MouseEventArgs e)
		{
			if ( e.Delta < 0 )
			{
				ZoomIn ();
			}
			else
			{
				ZoomOut ();
			}
		}

		/// <summary>
		/// Make sure that the PicBox have the focus, otherwise it doesn愒 receive 
		/// mousewheel events !.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PicBox_MouseEnter(object sender, EventArgs e)
		{
			if ( PicBox.Focused == false )
			{
				PicBox.Focus ();
			}
		}

        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            refPoint.X = e.X;
            refPoint.Y = e.Y;
            PicBox.Capture = true;
        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if(PicBox.Capture)
            {
                System.Drawing.Point newLoc = new System.Drawing.Point(PicBox.Location.X + e.X - refPoint.X,
                    PicBox.Location.Y + e.Y - refPoint.Y);

                if (newLoc.X > 0) newLoc.X = 0;
                if (newLoc.Y > 0) newLoc.Y = 0;
               // if (newLoc.X > PicBox.Size.Width) newLoc.X = PicBox.Size.Width;
                //if (newLoc.Y > 0) newLoc.Y = 0;
                PicBox.Location = newLoc;
            }
        }

        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            PicBox.Capture = false;
        }

        #endregion

        #region Control events

        private void PicBox_BackgroundImageChanged(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            Image i = p.BackgroundImage;

            double imgRatio = i.Width / i.Height;
            double ctlRatio = p.Size..Width / p.Size.Height;

            if(imgRatio > ctlRatio)
            {   //圖寬 容器直 上下留空

            }
            else
            {   //圖直 容器寬 左右留空

            }
        }

		#endregion

		#region Disposing

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#endregion
	}
}
