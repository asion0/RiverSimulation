using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace TestGoogleMap
{
    public partial class Form1 : Form
    {
        // ===========================Test Zoom Image Sample
        /*
        // Factor for zoom the image
        private float zoomFac = 1;
        //set Zoom allowed
        private bool zoomSet = false;
 
        //value for moving the image in X direction
        private float translateX=0;
        //value for moving the image in Y direction
        private float translateY = 0;
 
        //Flag to set the moving operation set
        private bool translateSet = false;
        //Flag to set mouse down on the image
        private bool translate = false;
 
        //set on the mouse down to know from where moving starts
        private float transStartX;
        private float transStartY;
 
        //Current Image position after moving 
        private float curImageX=0;
        private float curImageY=0;
 

        //temporary storage in bitmap
        Image bmp;
 
        float ratio;
        float translateRatio;
        */
        // ===========================Test Zoom Image Sample End
 
        public Form1()
        {
            InitializeComponent();
        }


        private RiverGrid rg = new RiverGrid();
        private void Form1_Load(object sender, EventArgs e)
        {
            // ===========================Test Zoom Image Sample
            /*
            //Picture box event handlers
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);

            pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            //Store the image in to bytes array
            //It is not advisable to store bitmap as such

            //Change the path name for your file
            if (System.IO.File.Exists(Application.StartupPath + "//test.jpg") != true)
            {
                MessageBox.Show("Your Image file does not exists");
                return;
            }

            if (bmp != null)
            {
                bmp.Dispose();
            }
            bmp = new Bitmap(Application.StartupPath + "//test.jpg");

            // Check potrait or landscape
            if (bmp.Width > bmp.Height)
            {
                ratio = (float)pictureBox1.Width / (float)bmp.Width;
                translateRatio = (float)bmp.Width / (float)pictureBox1.Width;

            }
            else
            {
                ratio = (float)pictureBox1.Height / (float)bmp.Height;
                translateRatio = (float)bmp.Height / (float)pictureBox1.Height;

            }


            this.Shown += new EventHandler(Form1_Shown);
            this.Disposed += new EventHandler(Form1_Disposed);
             * */
            // ===========================Test Zoom Image Sample End



            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseWheel);
            SetColorSample();
        }
        // ===========================Test Zoom Image Sample
        /*
        protected void Form1_Disposed(object sender, EventArgs e)
        {
            //Dispose the bmp when form is disposed.
            if (bmp != null)
            {
                bmp.Dispose();
            }
        }

        protected void Form1_Shown(object sender, EventArgs e)
        {
            //Draw the image initially
            zoomSet = true;
            pictureBox1.Refresh();
            zoomSet = false;

        }

        //Zoom Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || Single.TryParse(textBox1.Text, out zoomFac) == false) { return; }
            //set Zoom allowed
            zoomSet = true;

            //call the picture box paint
            pictureBox1.Refresh();
            pictureBox1.Cursor = Cursors.Arrow;

            //moving operation unset
            translateSet = false;

        }

        //Move / Pan button
        private void button2_Click(object sender, EventArgs e)
        {
            //Chnage the cursor
            pictureBox1.Cursor = Cursors.Hand;
            //set moving operation is allowed
            translateSet = true;

            zoomSet = false;
        }

        protected void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //If move button clicked
            if (translateSet == true)
            {
                //mouse down is true
                translate = true;
                //starting coordinates for move
                transStartX = e.X;
                transStartY = e.Y;

            }
        }
        protected void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //If mouse down is true
            if (translate == true)
            {
                //calculate the total distance to move from 0,0
                //previous image position+ current moving distance
                translateX = curImageX + ((e.X - transStartX) * (translateRatio / zoomFac));
                translateY = curImageY + ((e.Y - transStartY) * (translateRatio / zoomFac));
                //call picturebox to update the image in the new position
            }

            pictureBox1.Refresh();
            //set mouse down operation end
            translate = false;
            //set present position of the image after move.
            curImageX = translateX;
            curImageY = translateY;
        }
        protected void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Conditions to avoid to proceed further.
            if (bmp == null) { return; }
            if (translateSet == false && zoomSet == false) { return; }

            Graphics g = e.Graphics;

            //Scale transform operation on the picture box device context
            //zoomFac is global variable which can be used to get desired zoom
            g.ScaleTransform(ratio * zoomFac, ratio * zoomFac);

            //move image to new position
            if (translateSet == true)
            {
                g.TranslateTransform(translateX, translateY);
            }

            //Drawback the bitmap to the transformed decive context

            //Apply double buffering (Draw to a bitmap first and then draw to picturebox) if
            // using large image and experience flickering

            g.DrawImage(bmp, 0, 0);


        }
        */
        // ===========================Test Zoom Image Sample End
        private void SetColorSample()
        {
            bgColorSamplePanel.BackColor = bkColor;
            lineColorSamplePanel.BackColor = lineColor;
        }

        private  Bitmap gridBmp = new Bitmap(640 * 2, 640 * 2);
        Bitmap tlBmp, trBmp, blBmp, brBmp;

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

        private void useMapChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as CheckBox).Checked)
            {
                FreeStaticMaps();
                DrawGrid();
                pictureBox1.Refresh(); 
                return;
            }

            if(rg.IsReadFinished())
            {
                string tl = Environment.CurrentDirectory + "\\tl.jpg";
                string tr = Environment.CurrentDirectory + "\\tr.jpg";
                string bl = Environment.CurrentDirectory + "\\bl.jpg";
                string br = Environment.CurrentDirectory + "\\br.jpg";

                if (File.Exists(tl))
                {
                    File.Delete(tl);
                }
                if (File.Exists(tr))
                {
                    File.Delete(tr);
                } if (File.Exists(bl))
                {
                    File.Delete(bl);
                } if (File.Exists(br))
                {
                    File.Delete(br);
                }

                FreeStaticMaps();

                rg.DownloadGridMap(tl, tr, bl, br);
                tlBmp = new Bitmap(tl);
                trBmp = new Bitmap(tr);
                blBmp = new Bitmap(bl);
                brBmp = new Bitmap(br);

                DrawGrid();
                pictureBox1.Refresh();
            }
        }

        private void DrawGrid()
        {
            CoordinateTransform ct = new CoordinateTransform();
            Graphics g = Graphics.FromImage(gridBmp);

            CoorPoint lt = ct.CalLonLatDegToTwd97(rg.GetTopLeft.x, rg.GetTopLeft.y);
            CoorPoint rb = ct.CalLonLatDegToTwd97(rg.GetBottomRight.x, rg.GetBottomRight.y);

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
            for (int i = 0; i < rg.GetI; ++i)
            {
                for (int j = 0; j < rg.GetJ; ++j)
                {
                    int x1 = (int)(1280.0 * (rg.inputCoor[i, j].x - lt.x) / (rb.x - lt.x));
                    int y1 = (int)(1280.0 * (rg.inputCoor[i, j].y - lt.y) / (rb.y - lt.y));
                    int x2 = 0, y2 = 0;
                    if(j != rg.GetJ - 1)
                    {
                        x2 = (int)(1280.0 * (rg.inputCoor[i, j + 1].x - lt.x) / (rb.x - lt.x));
                        y2 = (int)(1280.0 * (rg.inputCoor[i, j + 1].y - lt.y) / (rb.y - lt.y));
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                    if (i != rg.GetI - 1)
                    {
                        x2 = (int)(1280.0 * (rg.inputCoor[i + 1, j].x - lt.x) / (rb.x - lt.x));
                        y2 = (int)(1280.0 * (rg.inputCoor[i + 1, j].y - lt.y) / (rb.y - lt.y));
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
            }
            g.Dispose();
            pictureBox1.BackgroundImage = gridBmp;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                inputFilePath.Text = inputFileDlg.FileName;
                if (!rg.ReadInputFile(inputFilePath.Text))
                {
                    MessageBox.Show("無法載入檔案!");
                    return;
                }
                if(rg.GetTopLeft.x < 120.0 || rg.GetTopLeft.y < 23.0)
                {
                    useMapChk.Enabled = false;
                }
                DrawGrid();
                pictureBox1.Refresh();
            }
        }

        private Color bkColor = Color.White;
        private void selBgColorBtn_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            colorDialog1.Color = bkColor;
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                bkColor = colorDialog1.Color;
                SetColorSample();
            }
        }

        private Color lineColor = Color.Orange;
        private void selLineColorBtn_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            colorDialog1.Color = lineColor;
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                lineColor = colorDialog1.Color;
                SetColorSample();
            }
        }

        float lineWidth = 2.0F;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                lineWidth = (float)Convert.ToDouble((sender as TextBox).Text);
                //DrawGrid();
               // panel1.Refresh();
            }
            catch
            {

            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            DrawGrid();
            pictureBox1.Refresh();
        }

        private int fontSize = 20;    
        private void panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Update the drawing based upon the mouse wheel scrolling.

            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            int numberOfPixelsToMove = numberOfTextLinesToMove * fontSize;

            //if (numberOfPixelsToMove != 0)
            //{
            //    System.Drawing.Drawing2D.Matrix translateMatrix = new System.Drawing.Drawing2D.Matrix();
            //    translateMatrix.Translate(0, numberOfPixelsToMove);
            //    mousePath.Transform(translateMatrix);
            //}
            pictureBox1.Invalidate();
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
