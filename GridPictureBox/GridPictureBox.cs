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
        private float lineWidth = 2.0F;
        //private int fontSize = 20;
        private int selectedI = -1;
        //private int selectedJ = -1;
        private bool selectRow = false;
        private bool selectGroup = false;
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
            this.PicBox.BackgroundImageLayout = ImageLayout.Zoom;
            this.PicBox.ErrorImage = null;
            this.PicBox.InitialImage = null;
            this.PicBox.Location = new System.Drawing.Point(0, 0);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(138, 135);
            this.PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBox.TabIndex = 3;
            this.PicBox.TabStop = false;
            this.PicBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PicBox_Paint);
            this.PicBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseClick);
            this.PicBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseDown);
            this.PicBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseMove);
            this.PicBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseUp);
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
        private BackgroundMapType GetBackgroundMapType()
        {
            //if (bkImgType == BackgroundMapType.ImportImage)
            //{
            //    return BackgroundMapType.None;
            //}
            return bkImgType;
        }

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
 
        private Bitmap LoadBitmapWithoutLockFile(string s)
        {
            Bitmap tmp = new Bitmap(s);
            Bitmap img = new Bitmap(tmp);
            tmp.Dispose();
            return img;
        }

        public void SetMapBackground(string tl, string tr, string bl, string br)
        {
            FreeStaticMaps();
            tlBmp = LoadBitmapWithoutLockFile(tl);
            trBmp = LoadBitmapWithoutLockFile(tr);
            blBmp = LoadBitmapWithoutLockFile(bl);
            brBmp = LoadBitmapWithoutLockFile(br);
            bkImgType = BackgroundMapType.GoogleStaticMap;

            Graphics g = Graphics.FromImage(gridBmp );
            g.DrawImage(tlBmp, 640, 640);
            g.DrawImage(trBmp, 0, 640);
            g.DrawImage(blBmp, 640, 0);
            g.DrawImage(brBmp, 0, 0);
            g.Dispose();

            DrawGrid();
            PicBox.Refresh();
        }

        private Bitmap importBmp;
        public void SetMapBackground(string s, double e, double n, double w, double h)
        {
            if(s == null || s.Length == 0)
            {
                bkImgType = BackgroundMapType.None;
                DrawGrid();
                PicBox.Refresh();
                return;
            }
            importBmp = LoadBitmapWithoutLockFile(s);
            if (importBmp == null || importBmp.Width < 100 || importBmp.Height < 100)
            {
                bkImgType = BackgroundMapType.None;
                DrawGrid();
                PicBox.Refresh();
                return;
            }
            importBmp.SetResolution(96.0F, 96.0F);
            topLeft = new CoorPoint(e, n);
            bottomRight = new CoorPoint(e + w, n - h);

            bkImgType = BackgroundMapType.ImportImage;
            DrawGrid();
            PicBox.Refresh();
        }

        public Bitmap GetGridBitmap()
        {

            switch (bkImgType)
            {
                case BackgroundMapType.None:
                    return gridBmp;

                case BackgroundMapType.GoogleStaticMap:
                    return gridBmp;

                case BackgroundMapType.ImportImage:
                    return importBmp;

            }
            return null;
        }

        public bool SelectRow
        {
            get { return selectRow; }
            set { selectRow = value; }
        }

        public bool SelectGroup
        {
            get { return selectGroup; }
            set { selectGroup = value; }
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
            bkImgType = BackgroundMapType.None;
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
            CoorPoint lt = GetTopLeft();
            CoorPoint rb = GetBottomRight(); ;

            float w = 0;
            float h = 0;
            if (BackgroundMapType.GoogleStaticMap == bkImgType)
            {
                w = GetGridBitmap().Width;
                h = GetGridBitmap().Height;
            }
            else if (BackgroundMapType.ImportImage == bkImgType)
            {
                w = GetGridBitmap().Width;
                h = GetGridBitmap().Height;
            }
            else //if (BackgroundMapType.None == bkImgType)
            {
                w = 640 * 2;
                h = 640 * 2;
            }



            Matrix m = new Matrix(1f, 0, 0, -1f, 0, 0);
            float xScale = w / (float)(rb.x - lt.x);
            float yScale = h / (float)(lt.y - rb.y);

            m.Scale(xScale, yScale);
            m.Translate((float)-lt.x, (float)-lt.y);
            return m;
        }

        PointF[] GetArrowPoint(int w, int h, double p1x, double p1y, double p2x, double p2y)
        {
            PointF[] pts = new PointF[2]; 
            PointF p1 = new PointF((float)p1x, (float)p1y);
            PointF p2 = new PointF((float)p2x, (float)p2y);
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float dd12 = (float)Math.Sqrt(dx * dx + dy * dy);
            float ddAll = (float)Math.Sqrt(w * w + h * h);
            float m1 = ddAll / (300 * dd12);
            float m2 = ddAll / (40 * dd12);
            pts[1] = new PointF(p1.X + m1 * (p1.X - p2.X), p1.Y + m1 * (p1.Y - p2.Y));
            pts[0] = new PointF(p1.X + m2 * (p1.X - p2.X), p1.Y + m2 * (p1.Y - p2.Y));

            return pts;
        }

        private void DrawGrid()
        {
            Matrix m = GetMatrix();
            if (m == null)
            {
                return;
            }

            Graphics g;
            Bitmap picBoxBmp;
            int w = 0;
            int h = 0;
            if (BackgroundMapType.GoogleStaticMap == bkImgType)
            {
                w = GetGridBitmap().Width;
                h = GetGridBitmap().Height;
                picBoxBmp = new Bitmap(w, h);
                g = Graphics.FromImage(picBoxBmp);
                g.DrawImage(GetGridBitmap(), 0, 0);
            }
            else if (BackgroundMapType.ImportImage == bkImgType)
            {
                w = GetGridBitmap().Width;
                h = GetGridBitmap().Height;
                picBoxBmp = new Bitmap(w, h);
                g = Graphics.FromImage(picBoxBmp);
                g.DrawImage(GetGridBitmap(), 0, 0);
            }
            else //if (BackgroundMapType.None == bkImgType)
            {
                w = 640 * 2;
                h = 640 * 2;
                picBoxBmp = new Bitmap(w, h);
                g = Graphics.FromImage(picBoxBmp);
                g.Clear(bkColor);
            }

            Pen pen = new Pen(lineColor, lineWidth);
            Pen selPen = new Pen(Color.Red, lineWidth);
            g.Transform = m;
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    float x1 = (float)rg.inputCoor[i, j].x;
                    float y1 = (float)rg.inputCoor[i, j].y;
                    PointF[] p = new PointF[] { new PointF(x1, y1) };
                    //m.TransformPoints(p);

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
            //Draw input arrow
            pen.StartCap = LineCap.Flat;
            pen.EndCap = LineCap.Custom;
            pen.CustomEndCap = new AdjustableArrowCap(4.0f, 4.0f);
            //pen.SetLineCap
            pen.Width = 3.5f;

            const int arrowCount = 4;
            PointF p0 = new PointF();
            PointF p1 = new PointF(); 

            PointF[] ptStart = GetArrowPoint(w, h, rg.inputCoor[0, 0].x, rg.inputCoor[0, 0].y, rg.inputCoor[1, 0].x, rg.inputCoor[1, 0].y);
            PointF[] ptEnd = GetArrowPoint(w, h, rg.inputCoor[0, rg.GetJ - 1].x, rg.inputCoor[0, rg.GetJ - 1].y, rg.inputCoor[1, rg.GetJ - 1].x, rg.inputCoor[1, rg.GetJ - 1].y);
            for (int i = 0; i <= arrowCount; ++i)
            {
                p0.X = ptStart[0].X + i * (ptEnd[0].X - ptStart[0].X) / arrowCount;
                p0.Y = ptStart[0].Y + i * (ptEnd[0].Y - ptStart[0].Y) / arrowCount;
                p1.X = ptStart[1].X + i * (ptEnd[1].X - ptStart[1].X) / arrowCount;
                p1.Y = ptStart[1].Y + i * (ptEnd[1].Y - ptStart[1].Y) / arrowCount;

                g.DrawLine(pen, p0, p1);
            }

            ptStart = GetArrowPoint(w, h, rg.inputCoor[rg.GetI - 1, 0].x, rg.inputCoor[rg.GetI - 1, 0].y, rg.inputCoor[rg.GetI - 2, 0].x, rg.inputCoor[rg.GetI - 2, 0].y);
            ptEnd = GetArrowPoint(w, h, rg.inputCoor[rg.GetI - 1, rg.GetJ - 1].x, rg.inputCoor[rg.GetI - 1, rg.GetJ - 1].y, rg.inputCoor[rg.GetI - 2, rg.GetJ - 1].x, rg.inputCoor[rg.GetI - 2, rg.GetJ - 1].y);
            for (int i = 0; i <= arrowCount; ++i)
            {
                p0.X = ptStart[0].X + i * (ptEnd[0].X - ptStart[0].X) / arrowCount;
                p0.Y = ptStart[0].Y + i * (ptEnd[0].Y - ptStart[0].Y) / arrowCount;
                p1.X = ptStart[1].X + i * (ptEnd[1].X - ptStart[1].X) / arrowCount;
                p1.Y = ptStart[1].Y + i * (ptEnd[1].Y - ptStart[1].Y) / arrowCount;

                g.DrawLine(pen, p1, p0);
            }

            if(hilightIndexType == -1)
            {
                g.Dispose();
                PicBox.Image = picBoxBmp;
                return;
            }

            for (int tp = 0; tp < HilightGridNumber; ++tp)
            {
                if (hilightGrid[tp] == null)
                    continue;

                for (int i = 0; i < hilightGrid[tp].Length; ++i)
                {
                    System.Collections.Generic.List<Point> pl = hilightGrid[tp][i];
                    if (pl == null || pl.Count == 0)
                    {
                        continue;
                    }

                    
                    Pen grpPen = null;
                    if (tp == hilightIndexType && i == hilightIndexCount)
                    {   //被選取的點集合標示不同顏色
                        grpPen = new Pen((showAlert) ? alertColor : selectedColor, selectedPenW - 2);
                    }
                    else
                    {   //否則各自有所屬的顏色
                        int colorIndex = tp;
                        grpPen = new Pen(colorTable[colorIndex], selectedPenW - 2);
                    }

                    foreach (Point p in pl)
                    {
                        float x1 = (float)rg.inputCoor[p.X, p.Y].x;
                        float y1 = (float)rg.inputCoor[p.X, p.Y].y;
                        float ew = selectedPenW;

                        g.DrawEllipse(grpPen, x1 - ew / 2, y1 - ew / 2, ew, ew);
                    }
                    
                }
            }
            g.Dispose();
            PicBox.Image = picBoxBmp;
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

        const int HilightGridNumber = 4;
        System.Collections.Generic.List<Point>[][] hilightGrid = new System.Collections.Generic.List<Point>[HilightGridNumber][];
        int hilightIndexType = -1;
        int hilightIndexCount = -1;
        private int[] hilightColor = null;
        public int[] GetGroupColors() { return hilightColor; }
        public Color[] GetColorTable() { return colorTable; }
        bool showAlert = false;
        public void SetSelectedGrid(System.Collections.Generic.List<Point>[] pts1,
            System.Collections.Generic.List<Point>[] pts2,
            System.Collections.Generic.List<Point>[] pts3,
            System.Collections.Generic.List<Point>[] pts4, 
            int indexType, int indexCount, bool alert)
        {

            
            showAlert = alert;
            hilightGrid[0] = pts1;
            hilightGrid[1] = pts2;
            hilightGrid[2] = pts3;
            hilightGrid[3] = pts4;
            hilightIndexType = indexType;
            hilightIndexCount = indexCount;
            //hilightColor = GroupColoringTool.ColoringGrid(hilightGrid, hilightIndex);
            //ColoringGrid();
            DrawGrid();
            PicBox.Refresh();
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

        public delegate void myDelegate3(System.Collections.Generic.List<Point> pts);
        public event myDelegate3 SelectedGroupChangedEvent;
        
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
            //Image img = PicBox.BackgroundImage;
            Image img = PicBox.Image;
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

        private void DoSelectRow(MouseEventArgs e)
        {
            Matrix m = GetMatrix();
            if (m == null)
            {
                return;
            }

            PointF p0 = TranslateZoomMousePosition(e.X, e.Y);
            PointF[] p = new PointF[1] { p0 };

            m.Invert();
            m.TransformPoints(p);

            this.GridChangedEvent(e.X.ToString() + "/" + p0.X.ToString() + "/" + p[0].X.ToString() + ", " +
                e.Y.ToString() + "/" + p0.Y.ToString() + "/" + p[0].Y.ToString());

            int sel = GetNearestI(p[0].X, p[0].Y);
            if (SelectedI != sel)
            {
                SelectedI = sel;
            }
        }

        private void DoSelectGroup(MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (groupPath.Count == 0)
            {
                groupPath.Add(p);
                return;
            }

            Point p0 = groupPath[groupPath.Count - 1];

            if (p0.X != e.X && p0.Y != e.Y)
            {
                groupPath.Add(p);
                PicBox.Refresh();
            }
        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectRow)
            {
                DoSelectRow(e);
                return;
            }

            if(selectGroup && mouseDown)
            {
                DoSelectGroup(e);
                return;
            }
        }

        private void PicBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectRow && SelectedI != -1)
            {
                SelectedRowChangedEvent(SelectedI);
                return;
            }
        }

        bool mouseDown = false;
        System.Collections.Generic.List<Point> groupPath = new System.Collections.Generic.List<Point>();
        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectGroup)
            {
                mouseDown = true;
                PicBox.Capture = true;
                groupPath.Clear();
            }
        }

        private void DoCloseGroupPath()
        {
            Matrix m = GetMatrix();
            if (m == null)
            {
                return;
            }

            //將Mouse Move圈選的線段連線為封閉區域gp, gp內的Point為PictureBox的實際螢幕座標
            GraphicsPath gp = new GraphicsPath();
            PointF p0 = groupPath[0], p1;
            bool first = true;
            foreach (Point p in groupPath)
            {
                p1 = TranslateZoomMousePosition(p.X, p.Y);
                if (!first)
                {
                    gp.AddLine(p0, p1);
                }
                first = false;
                p0 = p1;
            }
            gp.CloseFigure();

            //尋訪所有格網點，判斷是否被gp所圈選，計算出被圈選的所有格網點ptList，ptList內的Point為格網點的I,J索引值
            System.Collections.Generic.List<Point> ptList = new System.Collections.Generic.List<Point>();
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    PointF[] p = new PointF[1] { new PointF((float)rg.inputCoor[i, j].x, (float)rg.inputCoor[i, j].y) };
                    m.TransformPoints(p);
                    if (gp.IsVisible(p[0].X, p[0].Y))
                    {
                        ptList.Add(new Point(i, j));
                    }
                }
            }
            groupPath.Clear();

            //有找到被圈選的格網點則出發通知事件
            if(ptList.Count > 0 && SelectedGroupChangedEvent != null)
            {
                SelectedGroupChangedEvent(ptList);
            }
        }

        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectGroup && mouseDown)
            {
                mouseDown = false;
                PicBox.Capture = false;
                //Notify
                if(groupPath.Count > 3)
                {
                    DoCloseGroupPath();
                }
                groupPath.Clear();
            }
        }

        private void PicBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (selectGroup && groupPath.Count > 1)
            {
               // g.Transform = null;
                g.DrawLines(groupPen, groupPath.ToArray());
            }
/*            
            Matrix m = GetMatrix();
            if (m == null)
            {
                return;
            }

            Pen pen = new Pen(lineColor, lineWidth);
            Pen selPen = new Pen(Color.Red, lineWidth);
            //g.Transform = m;
 
            float x1 = (float)(rg.inputCoor[0, 0].x + rg.inputCoor[0, rg.inputCoor.GetLength(1) - 1].x) / 2.0f;
            float y1 = (float)(rg.inputCoor[0, 0].y + rg.inputCoor[0, rg.inputCoor.GetLength(1) - 1].y) / 2.0f;
            PointF[] p = new PointF[3] { new PointF(x1, y1), new PointF((float)rg.inputCoor[0, 0].x, (float)rg.inputCoor[0, 0].y),
                new PointF((float)rg.inputCoor[0, rg.inputCoor.GetLength(1) - 1].x, (float)rg.inputCoor[0, rg.inputCoor.GetLength(1) - 1].y)};
           
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            //Pen pen = new Pen(Color.Black, 1.0f);
            SolidBrush brush = new SolidBrush(Color.Black);
            string txt = "入流方向";
            Font leftFont = new Font("微軟正黑體", 12, FontStyle.Regular, GraphicsUnit.Point);
            m.TransformPoints(p);
            RectangleF rcText = new RectangleF(p[0].X, p[0].Y, 120, 16); ;

            g.DrawString(txt, leftFont, brush, rcText, stringFormat);
            g.DrawLine(selPen, p[1], p[2]);
            g.DrawLine(selPen, 0, 0, 400f, 400f);
            //g.Dispose();
 */
        }

        private Color bkColor = Color.White;
        ColorConverter cc = new ColorConverter();
        private Color lineColor = Color.FromArgb(0xF9A00F | -16777216);
        Pen groupPen = new Pen(Color.Blue, 2.0f);
        private const float selectedPenW = 4.0f;
        private Color selectedColor = Color.Blue;
        private Color alertColor = Color.Red;
        private Color[] colorTable = new Color[] { 
            Color.FromArgb(0xC289FC | -16777216),
            Color.FromArgb(0xD2E27E | -16777216),
            Color.FromArgb(0xE289B8 | -16777216),
            Color.FromArgb(0x0C8728 | -16777216),
                Color.LightSeaGreen, Color.LightCoral, Color.HotPink,  Color.LightSkyBlue, 
                Color.Gold, Color.Tan, Color.PeachPuff, Color.LightSalmon };
    }

    [Serializable]
    public class RiverGrid
    {
        public RiverGrid()
        {

        }

        public RiverGrid(RiverGrid g)
        {
            this.inputCoor = (CoorPoint[,])g.inputCoor.Clone();
            ConvertGrid(inputCoor);
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

                ConvertGrid(inputCoor);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
        
        private bool ConvertGrid(CoorPoint[,] grid)
        {
            _j = grid.GetLength(1);
            _i = grid.GetLength(0);
            maxX = double.MinValue;
            minX = double.MaxValue;
            maxY = double.MinValue;
            minY = double.MaxValue;

            foreach(CoorPoint pt in grid)
            {
                if (pt.x > maxX)
                    maxX = pt.x;
                if (pt.x < minX)
                    minX = pt.x;
                if (pt.y > maxY)
                    maxY = pt.y;
                if (pt.y < minY)
                    minY = pt.y;
            }


            //Finished read
            CoordinateTransform coorConv = new CoordinateTransform();
            //CoorPoint topLeft = coorConv.CalTwd97ToLatLonCoorRad(maxX, maxY);
            //CoorPoint bottomRight = coorConv.CalTwd97ToLatLonCoorRad(minX, minY);
            zoomScale = CalZoomScale(coorConv.CalTwd97ToLatLonCoorRad(maxX, maxY), coorConv.CalTwd97ToLatLonCoorRad(minX, minY));
            if (zoomScale == 0)
            {
                return false;
            }

            CoorPoint center = coorConv.CalTwd97ToLatLonCoorRad((maxX + minX) / 2, (maxY + minY) / 2);
            bottomRight = coorConv.CalCenterLatLonToOffsetPixelLonLat(center.x, center.y, 640, 640, zoomScale).RadToDegree();
            topLeft = coorConv.CalCenterLatLonToOffsetPixelLonLat(center.x, center.y, -640, -640, zoomScale).RadToDegree();
            centerPoint = center.RadToDegree();

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

    public static class GroupColoringTool
    {
        public static int[] ColoringGrid(System.Collections.Generic.List<Point>[] pts, int selIndex)
        {
            int[] groupColors = new int[pts.Length];
            bool[,] adj = new bool[pts.Length, pts.Length];
            if (groupColors == null || groupColors.Length != pts.Length)
            {
                groupColors = new int[pts.Length];
            }

            for (int i = 0; i < pts.Length; ++i)
            {
                groupColors[i] = -1;
                //adj[i, i] = false;
                for (int j = i + 1; j < pts.Length; j++)
                {
                    if (pts[i] == null || pts[j] == null || i == selIndex || j == selIndex)
                    {   //尚未設定的Group必不相鄰
                        adj[i, j] = false;
                    }
                    else
                    {   //已設定的Group需檢查相鄰
                        adj[i, j] = IsNeighboring(pts, i, j);
                        adj[j, i] = adj[i, j];
                    }
                }
            }

            int[] degree = new int[pts.Length];
            for (int i = 0; i < pts.Length; ++i)
            {
                for (int j = 0; j < pts.Length; ++j)
                {
                    if (i != j && adj[i, j])
                    {
                        degree[i]++;
                    }
                }
            }

            for (int i = 0; i < pts.Length; ++i)
            {
                if (degree[i] > 0)
                    break;
                if (i == pts.Length - 1)
                    return groupColors;
            }
            bool[] used = new bool[pts.Length];
            // 依照順序替各個點塗色。O(V^2)。
            for (int i = 0; i < pts.Length; ++i)
            {
                // 先把鄰點所用的顏色都記錄起來
                Array.Clear(used, 0, used.Length);
                for (int j = 0; j < pts.Length; ++j)
                {
                    if (i != j && adj[i, j] && groupColors[j] != -1)
                    {
                        used[groupColors[j]] = true;
                    }
                }

                // 最差的情況就是此顏色與所有鄰點都不同色
                for (int j = 0; j < degree[i] + 1; ++j)
                {
                    if (!used[j])
                    {
                        groupColors[i] = j;
                        break;
                    }
                }
            }
            return groupColors;
        }

        private static bool IsNeighboring(System.Collections.Generic.List<Point>[] pts, int i, int j)
        {
            System.Collections.Generic.List<Point> ps = pts[i];
            System.Collections.Generic.List<Point> pt = pts[j];
            foreach (Point p in ps)
            {
                Point p1 = new Point(p.X, p.Y - 1);
                Point p2 = new Point(p.X, p.Y + 1);
                Point p3 = new Point(p.X - 1, p.Y);
                Point p4 = new Point(p.X + 1, p.Y);

                foreach (Point pp in pt)
                {
                    if (pp == p1 || pp == p2 | pp == p3 || pp == p4)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
    
    [Serializable]
    public class CoorPoint
    {
        public CoorPoint()
	    {
            _x = 0.0; 
            _y = 0.0;
            _z = 0.0;
	    }

        public CoorPoint(double x1, double y1, double z1 = 0.0)
        {
            _x = x1; 
            _y = y1;
            _z = z1;
        }

        private double _x, _y, _z;
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

        public double z
        {
            set { this._z = value; }
            get { return this._z; }
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
