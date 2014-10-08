using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGoogleMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CoorPoint[,] inputCoor;
        double maxX = double.MinValue, minX = double.MinValue;
        double maxY = double.MinValue, minY = double.MinValue;
        private bool ReadInputFile(string path)
        {
            const int MaxLineWord = 3;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                char[] charSeparators = new char[] { '\t', ' ' };

                string line = file.ReadLine();
                string[] words = line.Split(charSeparators, MaxLineWord);
                int j_count = Convert.ToInt32(words[0]);
                int i_count = Convert.ToInt32(words[1]);

                inputCoor = new CoorPoint[i_count, j_count];
                int i = 0, j = j_count - 1;
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
                        j = j_count - 1;
                        ++i;
                    }
                    if (i == i_count)
                    {
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ReadInputFile(Environment.CurrentDirectory + "\\cchemesh.geo"))
            {
                MessageBox.Show("Finished!");
            }
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
    }
}
