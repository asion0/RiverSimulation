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
        public Form1()
        {
            InitializeComponent();
        }


        private RiverGrid rg = new RiverGrid();
        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int x, y, w, h;
            if(panel1.Size.Width > panel1.Size.Height)
            {
                h = w = panel1.Size.Height;
                y = 0;
                x = (panel1.Size.Width - w) / 2;
            }
            else
            {
                h = w = panel1.Size.Width;
                x = 0;
                y = (panel1.Size.Height - h) / 2;
            }
            g.DrawImage(gridBmp, x, y, w, h);
            //Pen p = new Pen(Color.Black);
            //g.DrawLine(p, 0, 0, 100, 200);
            //g.DrawImage(gridBmp, 0, 0, 200, 200);
        }

        private  Bitmap gridBmp = new Bitmap(640 * 2, 640 * 2);
        private void useMapChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as CheckBox).Checked)
                return;

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
                rg.DownloadGridMap(tl, tr, bl, br);
                Bitmap tlBmp, trBmp, blBmp, brBmp;
                tlBmp = new Bitmap(tl);
                trBmp = new Bitmap(tr);
                blBmp = new Bitmap(bl);
                brBmp = new Bitmap(br);

                Graphics g = Graphics.FromImage(gridBmp);
                g.DrawImage(tlBmp, 640, 640);
                g.DrawImage(trBmp, 0, 640);
                g.DrawImage(blBmp, 640, 0);
                g.DrawImage(brBmp, 0, 0);
                //gridBmp.Save(Environment.CurrentDirectory + "\\Big.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                DrawGrid();
                panel1.Refresh();
            }
        }

        private void DrawGrid()
        {
            CoordinateTransform ct = new CoordinateTransform();
            Graphics g = Graphics.FromImage(gridBmp);
            //(rg.GetTopLeft.x, rg.GetTopLeft.y)
            CoorPoint lt = ct.CalLonLatDegToTwd97(rg.GetTopLeft.x, rg.GetTopLeft.y);
            CoorPoint rb = ct.CalLonLatDegToTwd97(rg.GetBottomRight.x, rg.GetBottomRight.y);

            Pen pen = new Pen(Color.Orange, 2.0F);
            //Point pt1 = new Point(0, 0);
            //Point pt2 = new Point(1280, 1280);
            //g.DrawLine(pen, pt1, pt2);
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

                    //g.DrawLine(pen, pt1, pt2);
                }

            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileDlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                inputFilePath.Text = inputFileDlg.FileName;
                if (!rg.ReadInputFile(Environment.CurrentDirectory + "\\cchemesh.geo"))
                {
                    MessageBox.Show("無法載入檔案!");
                    return;
                }
                if(rg.GetTopLeft.x < 120.0 || rg.GetTopLeft.y < 23.0)
                {
                    useMapChk.Enabled = false;
                }
                DrawGrid();
                panel1.Refresh();
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
                zoomScale = GetZoomScale(coorConv.CalTwd97ToLatLonCoorRad(maxX, maxY), coorConv.CalTwd97ToLatLonCoorRad(minX, minY));
                if(zoomScale ==0)
                {
                    return false;
                }
                CoorPoint center = coorConv.CalTwd97ToLatLonCoorRad((maxX + minX) / 2, (maxY + minY) / 2);

                bottomRight = coorConv.CalCenterLatLonToOffsetPixelLonLat(center.x, center.y, 640, 640, zoomScale).RadToDegree();
                topLeft = coorConv.CalCenterLatLonToOffsetPixelLonLat(center.x, center.y, -640, -640, zoomScale).RadToDegree();
                centerPoint = center.RadToDegree();
                //bool b = false;
                //b = DownloadStaticMap((tl.x + c.x) / 2, (tl.y + c.y) / 2, z, Environment.CurrentDirectory + "\\br.jpg");
                //b = DownloadStaticMap((br.x + c.x) / 2, (tl.y + c.y) / 2, z, Environment.CurrentDirectory + "\\bl.jpg");
                //b = DownloadStaticMap((tl.x + c.x) / 2, (br.y + c.y) / 2, z, Environment.CurrentDirectory + "\\tr.jpg");
                //b = DownloadStaticMap((br.x + c.x) / 2, (br.y + c.y) / 2, z, Environment.CurrentDirectory + "\\tl.jpg");

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

        private int GetZoomScale(CoorPoint tl, CoorPoint br)
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
            _x = 0.0;
            _y = 0.0;
	    }

        public CoorPoint(double x1, double y1)
        {
            _x = x1;
            _y = y1;
        }

        private double _x, _y;
        public double x
        {
            //set the person name
            set { this._x = value; }
            //get the person name 
            get { return this._x; }
        }

        public double y
        {
            //set the person name
            set { this._y = value; }
            //get the person name 
            get { return this._y; }
        }

        public override string ToString()
        {
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
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        ////給WGS84經緯度度分秒轉成TWD97坐標
        //public string lonlat_To_twd97(int lonD, int lonM, int lonS, int latD, int latM, int latS)
        //{
        //    double RadianLon = (double)(lonD) + (double)lonM / 60 + (double)lonS / 3600;
        //    double RadianLat = (double)(latD) + (double)latM / 60 + (double)latS / 3600;
        //    return Cal_lonlat_To_twd97(RadianLon, RadianLat);
        //}
        ////給WGS84經緯度弧度轉成TWD97坐標
        //public string lonlat_To_twd97(double RadianLon, double RadianLat)
        //{
        //    return Cal_lonlat_To_twd97(RadianLon, RadianLat);
        //}

        //給TWD97坐標 轉成 WGS84 度分秒字串 (type1傳度分秒 2傳弧度)
        //public string Twd97ToLonLatString(double XValue, double YValue, int Type)
        //{

        //    string lonlat = "";

        //    if (Type == 1)
        //    {
        //        string[] Answer = Cal_TWD97_To_lonlat(XValue, YValue).Split(',');
        //        int LonDValue = (int)double.Parse(Answer[0]);
        //        int LonMValue = (int)((double.Parse(Answer[0]) - LonDValue) * 60);
        //        double LonSValue = (((double.Parse(Answer[0]) - LonDValue) * 60) - LonMValue) * 60;

        //        int LatDValue = (int)double.Parse(Answer[1]);
        //        int LatMValue = (int)((double.Parse(Answer[1]) - LatDValue) * 60);
        //        double LatSValue = (((double.Parse(Answer[1]) - LatDValue) * 60) - LatMValue) * 60;

        //        lonlat = LonDValue + "度" + LonMValue + "分" + LonSValue + "秒," + LatDValue + "度" + LatMValue + "分" + LatSValue + "秒,";
        //    }
        //    else if (Type == 2)
        //    {
        //        lonlat = CalTwd97ToLonLatString(XValue, YValue);
        //    }

        //    return lonlat;
        //}

        public CoorPoint CalLonLatDegToTwd97(double lon, double lat)
        {
            //string TWD97 = "";

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

            //TWD97 = y.ToString() + "," + x.ToString();
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

        //public CoorPoint CalLatLonRadToLatLonDeg(double x, double y)
        //{
        //    return new CoorPoint((x * 180) / Math.PI, (y * 180) / Math.PI);
        //}
        
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
