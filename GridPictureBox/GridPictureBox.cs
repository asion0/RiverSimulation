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
using System.IO;
using System.Net;
using System.Drawing.Drawing2D;
#endregion

namespace PictureBoxCtrl
{
	/// <summary>
	/// Summary for the PictureBox Ctrl
	/// </summary>
	public class GridPictureBox : System.Windows.Forms.UserControl
	{
		#region Members

        private enum BackgroundMapType
        {
            None,
            GoogleStaticMap,
            ImportImage
        };

		private System.Windows.Forms.PictureBox PicBox;
		private System.Windows.Forms.Panel OuterPanel;
		private System.ComponentModel.Container components = null;
        private PictureBoxCtrl.RiverGrid rg = null;
        private Bitmap gridBmp = new Bitmap(640 * 2, 640 * 2);
        private Bitmap tlBmp, trBmp, blBmp, brBmp;
        private Color bkColor = Color.White;
        private Color lineColor = Color.Orange;
        private float lineWidth = 2.0F;
        //private int fontSize = 20;
        private int selectedI = -1;
        //private int selectedJ = -1;
        private bool selectRow = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            this.OuterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicBox
            // 
            this.PicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PicBox.ErrorImage = null;
            this.PicBox.InitialImage = null;
            this.PicBox.Location = new System.Drawing.Point(0, 0);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(138, 135);
            this.PicBox.TabIndex = 3;
            this.PicBox.TabStop = false;
            this.PicBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseClick);
            this.PicBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseMove);
            // 
            // OuterPanel
            // 
            this.OuterPanel.AutoScroll = true;
            this.OuterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OuterPanel.Controls.Add(this.PicBox);
            this.OuterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OuterPanel.Location = new System.Drawing.Point(0, 0);
            this.OuterPanel.Name = "OuterPanel";
            this.OuterPanel.Size = new System.Drawing.Size(210, 190);
            this.OuterPanel.TabIndex = 4;
            this.OuterPanel.Resize += new System.EventHandler(this.OuterPanel_Resize);
            // 
            // GridPictureBox
            // 
            this.Controls.Add(this.OuterPanel);
            this.Name = "GridPictureBox";
            this.Size = new System.Drawing.Size(210, 190);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            this.OuterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Constructors

        public GridPictureBox()
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
		public RiverGrid Grid
		{
			get { return rg; }
			set 
			{
				if ( null != value )
				{
				    rg = value;
                    DrawGrid();
                    Refresh();
    			}
			}
		}

        public int SelectedI
        {
            get { return selectedI; }
            set
            { 
                selectedI = value;
                DrawGrid();
                Refresh();
            }
        }

        private CoorPoint bottomRight = new CoorPoint();
        private CoorPoint topLeft = new CoorPoint();
        private BackgroundMapType bkImgType = BackgroundMapType.None;
        private CoorPoint GetTopLeft()
        {
            CoordinateTransform ct = new CoordinateTransform();
            CoorPoint pt = new CoorPoint();
            switch (bkImgType)
            {
                case BackgroundMapType.None:
                    pt = ct.CalLonLatDegToTwd97(rg.GetTopLeft.x, rg.GetTopLeft.y);
                    break;
                case BackgroundMapType.GoogleStaticMap:
                    pt = ct.CalLonLatDegToTwd97(rg.GetTopLeft.x, rg.GetTopLeft.y);
                    break;
                case BackgroundMapType.ImportImage:
                    pt = topLeft;
                    break;
            }
            return pt;
        }

        private CoorPoint GetBottomRight()
        {
            CoordinateTransform ct = new CoordinateTransform();
            CoorPoint pt = new CoorPoint();
            switch (bkImgType)
            {
                case BackgroundMapType.None:
                    pt = ct.CalLonLatDegToTwd97(rg.GetBottomRight.x, rg.GetBottomRight.y);
                    break;
                case BackgroundMapType.GoogleStaticMap:
                    pt = ct.CalLonLatDegToTwd97(rg.GetBottomRight.x, rg.GetBottomRight.y);
                    break;
                case BackgroundMapType.ImportImage:
                    pt = bottomRight;
                    break;
            }
            return pt;
        }
 
        public void SetMapBackground(string tl, string tr, string bl, string br)
        {
            FreeStaticMaps();
            tlBmp = new Bitmap(tl);
            trBmp = new Bitmap(tr);
            blBmp = new Bitmap(bl);
            brBmp = new Bitmap(br);
            DrawGrid();
            PicBox.Refresh();
        }

        private Bitmap importBmp;
        public void SetMapBackground(string s, double e, double n, double w, double h)
        {
            importBmp = new Bitmap(s);
            importBmp.SetResolution(96.0F, 96.0F);
            topLeft = new CoorPoint(e, n);
            bottomRight = new CoorPoint(e + w, n - h);

            importBmp = new Bitmap(s);
            importBmp.SetResolution(96.0F, 96.0F);
            //topLeft = new CoorPoint(e, n + h);
            //bottomRight = new CoorPoint(e + w, n);
            topLeft = new CoorPoint(e, n);
            bottomRight = new CoorPoint(e + w, n - h);
        }

        public bool SelectRow
        {
            get { return selectRow; }
            set { selectRow = value; }
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
        private void FreeStaticMaps()
        {
            if (tlBmp != null)
            {
                tlBmp.Dispose();
                tlBmp = null;
            }
            if (trBmp != null)
            {
                trBmp.Dispose();
                trBmp = null;
            }
            if (blBmp != null)
            {
                blBmp.Dispose();
                blBmp = null;
            }
            if (brBmp != null)
            {
                brBmp.Dispose();
                brBmp = null;
            }
        }

        public void ClearMapBackground()
        {
            FreeStaticMaps();
            DrawGrid();
            PicBox.Refresh(); 
        }

        public Matrix GetMatrix()
        {
            if (rg==null || !rg.IsReadFinished())
            {
                return null;
            }
            CoordinateTransform ct = new CoordinateTransform();
            //CoorPoint lt = ct.CalLonLatDegToTwd97(rg.GetTopLeft.x, rg.GetTopLeft.y);
            //CoorPoint rb = ct.CalLonLatDegToTwd97(rg.GetBottomRight.x, rg.GetBottomRight.y);
            CoorPoint lt = GetTopLeft();
            CoorPoint rb = GetBottomRight(); ;

            Matrix m = new Matrix(1f, 0, 0, -1f, 0, 0);
            float xScale = 1280.0f / (float)(rb.x - lt.x);
            float yScale = 1280.0f / (float)(lt.y - rb.y);

            m.Scale(xScale, yScale);
            m.Translate((float)-lt.x, (float)-lt.y);
            return m;
        }

        private void DrawGrid()
        {
            //CoordinateTransform ct = new CoordinateTransform();
            Matrix m = GetMatrix();
            if (m == null)
            {
                return;
            }

            Graphics g = Graphics.FromImage(gridBmp);

            if (tlBmp != null && trBmp != null && blBmp != null && brBmp != null)
            {
                g.DrawImage(tlBmp, 640, 640);
                g.DrawImage(trBmp, 0, 640);
                g.DrawImage(blBmp, 640, 0);
                g.DrawImage(brBmp, 0, 0);
            }
            else
            {
                g.Clear(bkColor);
            }

            Pen pen = new Pen(lineColor, lineWidth);
            Pen selPen = new Pen(Color.Red, lineWidth);
            //Pen testPen = new Pen(Color.Green, 10.0f);
            //g.DrawLine(testPen, 0, 0, 894, 70);
            g.Transform = m;
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    float x1 = (float)rg.inputCoor[i, j].x;
                    float y1 = (float)rg.inputCoor[i, j].y;
                    PointF[] p = new PointF[] { new PointF(x1, y1) };
                    m.TransformPoints(p);

                    float x2 = 0, y2 = 0;
                    if (j != rg.GetJ - 1)
                    {
                        x2 = (float)rg.inputCoor[i, j + 1].x;
                        y2 = (float)rg.inputCoor[i, j + 1].y;
                        if (i == selectedI)
                        {
                            g.DrawLine(selPen, x1, y1, x2, y2);
                        }
                        else
                        {
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                    }
                    if (i != rg.GetI - 1)
                    {
                        x2 = (float)rg.inputCoor[i + 1, j].x;
                        y2 = (float)rg.inputCoor[i + 1, j].y;
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }
            g.Dispose();
            PicBox.BackgroundImage = gridBmp;
        }

		/// <summary>
		/// Special settings for the picturebox ctrl
		/// </summary>
		private void InitCtrl ()
		{
			PicBox.SizeMode = PictureBoxSizeMode.Zoom;
			PicBox.Location = new Point (0, 0);
			OuterPanel.Dock = DockStyle.Fill;
			OuterPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
			OuterPanel.AutoScroll = true;
			OuterPanel.MouseEnter += new EventHandler(PicBox_MouseEnter);
			PicBox.MouseEnter += new EventHandler(PicBox_MouseEnter);
			OuterPanel.MouseWheel += new MouseEventHandler(PicBox_MouseWheel);
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
				//ZoomIn ();
			}
			else
			{
				//ZoomOut ();
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
        public delegate void myDelegate(string s);
        public event myDelegate GridChangedEvent;

        public delegate void myDelegate2(int row);
        public event myDelegate2 SelectedRowChangedEvent;
        
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

        private void OuterPanel_Resize(object sender, EventArgs e)
        {
            PicBox.Width = Width;
            PicBox.Height = Height;
        }

        protected PointF TranslateZoomMousePosition(int x, int y)
        {
            Image img = PicBox.BackgroundImage;
            int w = PicBox.Width;
            int h = PicBox.Height;
            // test to make sure our image is not null
            if (img == null) return new PointF(x, y);
            // Make sure our control width and height are not 0 and our 
            // image width and height are not 0
            if (w == 0 || h == 0 || img.Width == 0 || img.Height == 0) return new PointF(x, y);
            // This is the one that gets a little tricky. Essentially, need to check 
            // the aspect ratio of the image to the aspect ratio of the control
            // to determine how it is being rendered
            float imageAspect = (float)img.Width / img.Height;
            float controlAspect = (float)w / h;
            float newX = x;
            float newY = y;
            if (imageAspect > controlAspect)
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                float ratioWidth = (float)img.Width / w;
                newX *= ratioWidth;
                float scale = (float)w / img.Width;
                float displayHeight = scale * img.Height;
                float diffHeight = h - displayHeight;
                diffHeight /= 2;
                newY -= diffHeight;
                newY /= scale;
            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                float ratioHeight = (float)img.Height / h;
                newY *= ratioHeight;
                float scale = (float)h / img.Height;
                float displayWidth = scale * img.Width;
                float diffWidth = w - displayWidth;
                diffWidth /= 2;
                newX -= diffWidth;
                newX /= scale;
            }
            return new Point((int)newX, (int)newY);
        }

        private int GetNearestI(float x, float y)
        {
            int foundI = -1;
            int foundJ = -1;
            float minDis = float.MaxValue;
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    float x0 = (float)rg.inputCoor[i, j].x;
                    float y0 = (float)rg.inputCoor[i, j].y;
                    float dis = (x0 - x) * (x0 - x) + (y0 - y) * (y0 - y);
                    if (dis < minDis)
                    {
                        minDis = dis;
                        foundI = i;
                        foundJ = j;
                    }
                }
            }
            return foundI;
        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!selectRow)
            {
                return;
            }

            Matrix m = GetMatrix();
            if (m == null)
            {
                return;
            }

            PointF p0 = TranslateZoomMousePosition(e.X, e.Y);
            PointF[] p = new PointF[1] { p0 };
            //PointF[] p = new PointF[4] { new PointF(609, 29), new PointF(664, 67) ,new PointF(404, 490) ,new PointF(349, 380) };
            //p[0] = new PointF(609, 29); p[1] = new PointF(664, 67); p[2] = new PointF(404, 490); p[3] = new PointF(349, 380);

            //PointF[] p1 = new PointF[4] { new PointF(270746.457f, 2733465.149f), new PointF(664, 67), new PointF(404, 490), new PointF(349, 380) };
            //PointF[] p2 = (PointF[])p.Clone();
            //PointF[] p3 = (PointF[])p.Clone();
            //PointF[] p4 = (PointF[])p.Clone();

            //m.TransformPoints(p1);
            //m.TransformVectors(p2);
            m.Invert();
            m.TransformPoints(p);
            //m.TransformVectors(p4);
            this.GridChangedEvent(e.X.ToString() + "/" + p0.X.ToString() + "/" + p[0].X.ToString() + ", " +
                e.Y.ToString() + "/" + p0.Y.ToString() + "/" + p[0].Y.ToString());
            
            //xLabel.Text = e.X.ToString() + "/" + p0.X.ToString() + "/" + p[0].X.ToString();
            //yLabel.Text = e.Y.ToString() + "/" + p0.Y.ToString() + "/" + p[0].Y.ToString();
            
            int sel = GetNearestI(p[0].X, p[0].Y);
            if (SelectedI != sel)
            {
                SelectedI = sel;
                //lastSelI = sel;
                //DrawGrid();
                //pictureBox1.Refresh();
            }
            
        }

        private void PicBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectRow && SelectedI != -1)
            {
                SelectedRowChangedEvent(SelectedI);
            }
        }
	}

    public class RiverGrid
    {
        public RiverGrid()
        {

        }
        public CoorPoint[,] inputCoor;
        private int _i = 0, _j = 0;

        private double maxX = double.MinValue, minX = double.MaxValue;
        private double maxY = double.MinValue, minY = double.MaxValue;
        private int zoomScale = 0;

        private CoorPoint topLeft;
        private CoorPoint bottomRight;
        private CoorPoint centerPoint;

        public int GetI
        {
            get { return _i; }
        }

        public int GetJ
        {
            get { return _j; }
        }

        public CoorPoint GetTopLeft
        {
            get { return this.topLeft;  }
        }

        public CoorPoint GetBottomRight
        {
            get { return this.bottomRight; }
        }

        public bool IsReadFinished()
        {
            return zoomScale != 0;
        }

        public bool ReadInputFile(string path)
        {
            const int MaxLineWord = 3;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                char[] charSeparators = new char[] { '\t', ' ' };

                string line = file.ReadLine();
                string[] words = line.Split(charSeparators, MaxLineWord);
                _j = Convert.ToInt32(words[0]);
                _i = Convert.ToInt32(words[1]);

                inputCoor = new CoorPoint[_i, _j];
                int i = 0, j = _j - 1;
                while ((line = file.ReadLine()) != null)
                {
                    words = line.Split(charSeparators, MaxLineWord);
                    inputCoor[i, j] = new CoorPoint(Convert.ToDouble(words[0]), Convert.ToDouble(words[1]));
                    if (inputCoor[i, j].x > maxX)
                        maxX = inputCoor[i, j].x;
                    if (inputCoor[i, j].x < minX)
                        minX = inputCoor[i, j].x;
                    if (inputCoor[i, j].y > maxY)
                        maxY = inputCoor[i, j].y;
                    if (inputCoor[i, j].y < minY)
                        minY = inputCoor[i, j].y;

                    if (--j < 0)
                    {
                        j = _j - 1;
                        ++i;
                    }
                    if (i == _i)
                    {
                        break;
                    }
                }
                //Finished read
                CoordinateTransform coorConv = new CoordinateTransform();
                //CoorPoint topLeft = coorConv.CalTwd97ToLatLonCoorRad(maxX, maxY);
                //CoorPoint bottomRight = coorConv.CalTwd97ToLatLonCoorRad(minX, minY);
                zoomScale = CalZoomScale(coorConv.CalTwd97ToLatLonCoorRad(maxX, maxY), coorConv.CalTwd97ToLatLonCoorRad(minX, minY));
                if(zoomScale ==0)
                {
                    return false;
                }

                CoorPoint center = coorConv.CalTwd97ToLatLonCoorRad((maxX + minX) / 2, (maxY + minY) / 2);
                bottomRight = coorConv.CalCenterLatLonToOffsetPixelLonLat(center.x, center.y, 640, 640, zoomScale).RadToDegree();
                topLeft = coorConv.CalCenterLatLonToOffsetPixelLonLat(center.x, center.y, -640, -640, zoomScale).RadToDegree();
                centerPoint = center.RadToDegree();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
        
        public bool DownloadGridMap(string tl, string tr, string bl, string br)
        {
            bool b = false;
            b = DownloadStaticMap((topLeft.x + centerPoint.x) / 2, (topLeft.y + centerPoint.y) / 2, zoomScale, br);
            b = DownloadStaticMap((bottomRight.x + centerPoint.x) / 2, (topLeft.y + centerPoint.y) / 2, zoomScale, bl);
            b = DownloadStaticMap((topLeft.x + centerPoint.x) / 2, (bottomRight.y + centerPoint.y) / 2, zoomScale, tr);
            b = DownloadStaticMap((bottomRight.x + centerPoint.x) / 2, (bottomRight.y + centerPoint.y) / 2, zoomScale, tl);
            return true;
        }

        private int CalZoomScale(CoorPoint tl, CoorPoint br)
        {
            CoordinateTransform coorConv = new CoordinateTransform();
            for (int i = 21; i > 1; --i)
            {
                CoorPoint c2 = coorConv.CalLonLatRadToCoorPixel(tl.x, tl.y, i);
                CoorPoint c1 = coorConv.CalLonLatRadToCoorPixel(br.x, br.y, i);
                if ((c2.x - c1.x) < 1280 && c2.y - c1.y < 1280)
                {
                    return i;
                }
            }
            return 0;
        }

        private bool DownloadStaticMap(double lat, double lon, int zoom, string file)
        {
            if (File.Exists(file))
            {
                return false;
            }

            var urlFormat = "http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&maptype=satellite&size=640x640&sensor=false";
            var url = String.Format(urlFormat, lon.ToString(), lat.ToString(), zoom);
            using (var wc = new WebClient())
            {
                wc.DownloadFile(url, file);
            }

            return true;
        }

    }
    public class CoorPoint
    {
        public CoorPoint()
	    {
            _x = 0.0; _y = 0.0;
	    }

        public CoorPoint(double x1, double y1)
        {
            _x = x1; _y = y1;
        }

        private double _x, _y;
        public double x
        {
            set { this._x = value; }
            get { return this._x; }
        }

        public double y
        {
            set { this._y = value; }
            get { return this._y; }
        }

        public override string ToString()
        {   //Format for Google API 
            return y.ToString() + ", " + x.ToString();
        }

        public CoorPoint RadToDegree()
        {
            return new CoorPoint((x * 180) / Math.PI, (y * 180) / Math.PI);
        }
    }

    public class CoordinateTransform
    {
        //轉換網站
        //http://gis.cy1000.com.tw/transcoor/transcoor.asp
        //二度分帶座標 
        //座標系統 TWD97, 中央經線 台灣地區121以公尺為單位
        //轉為WGS84 for Google Map
        private static double a = 6378137.0;
        private static double b = 6356752.3142451;
        private double lon0 = 121 * Math.PI / 180;
        private double k0 = 0.9999;
        private int dx = 250000;
        private int dy = 0;
        private double e = 1 - Math.Pow(b, 2) / Math.Pow(a, 2);
        private double e2 = (1 - Math.Pow(b, 2) / Math.Pow(a, 2)) / (Math.Pow(b, 2) / Math.Pow(a, 2));

        public CoordinateTransform()
        {

        }

        public CoorPoint CalLonLatDegToTwd97(double lon, double lat)
        {
            lon = (lon - Math.Floor((lon + 180) / 360) * 360) * Math.PI / 180;
            lat = lat * Math.PI / 180;

            double V = a / Math.Sqrt(1 - e * Math.Pow(Math.Sin(lat), 2));
            double T = Math.Pow(Math.Tan(lat), 2);
            double C = e2 * Math.Pow(Math.Cos(lat), 2);
            double A = Math.Cos(lat) * (lon - lon0);
            double M = a * ((1.0 - e / 4.0 - 3.0 * Math.Pow(e, 2) / 64.0 - 5.0 * Math.Pow(e, 3) / 256.0) * lat -
                  (3.0 * e / 8.0 + 3.0 * Math.Pow(e, 2) / 32.0 + 45.0 * Math.Pow(e, 3) / 1024.0) *
                  Math.Sin(2.0 * lat) + (15.0 * Math.Pow(e, 2) / 256.0 + 45.0 * Math.Pow(e, 3) / 1024.0) *
                  Math.Sin(4.0 * lat) - (35.0 * Math.Pow(e, 3) / 3072.0) * Math.Sin(6.0 * lat));
            // x
            double x = dx + k0 * V * (A + (1 - T + C) * Math.Pow(A, 3) / 6 + (5 - 18 * T + Math.Pow(T, 2) + 72 * C - 58 * e2) * Math.Pow(A, 5) / 120);
            // y
            double y = dy + k0 * (M + V * Math.Tan(lat) * (Math.Pow(A, 2) / 2 + (5 - T + 9 * C + 4 * Math.Pow(C, 2)) * Math.Pow(A, 4) / 24 + (61 - 58 * T + Math.Pow(T, 2) + 600 * C - 330 * e2) * Math.Pow(A, 6) / 720));

            return new CoorPoint(x, y);
        }

        public CoorPoint CalTwd97ToLatLonCoorRad(double x, double y)
        {
            x -= dx;
            y -= dy;

            // Calculate the Meridional Arc
            double M = y / k0;

            // Calculate Footprint Latitude
            double mu = M / (a * (1.0 - e / 4.0 - 3 * Math.Pow(e, 2) / 64.0 - 5 * Math.Pow(e, 3) / 256.0));
            double e1 = (1.0 - Math.Sqrt(1.0 - e)) / (1.0 + Math.Sqrt(1.0 - e));

            double J1 = (3 * e1 / 2 - 27 * Math.Pow(e1, 3) / 32.0);
            double J2 = (21 * Math.Pow(e1, 2) / 16 - 55 * Math.Pow(e1, 4) / 32.0);
            double J3 = (151 * Math.Pow(e1, 3) / 96.0);
            double J4 = (1097 * Math.Pow(e1, 4) / 512.0);

            double fp = mu + J1 * Math.Sin(2 * mu) + J2 * Math.Sin(4 * mu) + J3 * Math.Sin(6 * mu) + J4 * Math.Sin(8 * mu);

            // Calculate Latitude and Longitude
            double C1 = e2 * Math.Pow(Math.Cos(fp), 2);
            double T1 = Math.Pow(Math.Tan(fp), 2);
            double R1 = a * (1 - e) / Math.Pow((1 - e * Math.Pow(Math.Sin(fp), 2)), (3.0 / 2.0));
            double N1 = a / Math.Pow((1 - e * Math.Pow(Math.Sin(fp), 2)), 0.5);

            double D = x / (N1 * k0);

            // 計算緯度
            double Q1 = N1 * Math.Tan(fp) / R1;
            double Q2 = (Math.Pow(D, 2) / 2.0);
            double Q3 = (5 + 3 * T1 + 10 * C1 - 4 * Math.Pow(C1, 2) - 9 * e2) * Math.Pow(D, 4) / 24.0;
            double Q4 = (61 + 90 * T1 + 298 * C1 + 45 * Math.Pow(T1, 2) - 3 * Math.Pow(C1, 2) - 252 * e2) * Math.Pow(D, 6) / 720.0;
            double lat = fp - Q1 * (Q2 - Q3 + Q4);

            // 計算經度
            double Q5 = D;
            double Q6 = (1 + 2 * T1 + C1) * Math.Pow(D, 3) / 6;
            double Q7 = (5 - 2 * C1 + 28 * T1 - 3 * Math.Pow(C1, 2) + 8 * e2 + 24 * Math.Pow(T1, 2)) * Math.Pow(D, 5) / 120.0;
            double lon = lon0 + (Q5 - Q6 + Q7) / Math.Cos(fp);
            return new CoorPoint(lon, lat);
        }
        
        public CoorPoint CalTwd97ToLonLatCoorDeg(double x, double y)
        {
            return CalTwd97ToLatLonCoorRad(x, y).RadToDegree();
        }

        public double Atanh(double x) //in rad
        {
            return Math.Log((1.0 + x) / (1.0 - x)) / 2.0 ;
        }

        public CoorPoint CalLonLatRadToCoorPixel(double x, double y, int zoomScale)
        {
            double xPixel = 128 * Math.Pow(2, zoomScale) * (1.0 + x / Math.PI);
            double yPixel = 128 * Math.Pow(2, zoomScale) * (1.0 - Atanh(Math.Sin(y)) / Math.PI);
            return new CoorPoint(xPixel, yPixel);
        }

        public CoorPoint CalCenterLatLonToOffsetPixelLonLat(double cx, double cy, int dx, int dy, int zoomScale)
        {
            double lat = Math.PI * dx / (128 * Math.Pow(2, zoomScale)) + cx;
            double lon = Math.Asin(Math.Tanh((Atanh(Math.Sin(cy)) - Math.PI * dy / (128 * Math.Pow(2, zoomScale)))));
            return  new CoorPoint(lat, lon);
        }
    }

}
