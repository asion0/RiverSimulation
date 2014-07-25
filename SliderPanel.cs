using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RiverSimulationApplication
{
    class SliderPanel
    {
        private static Timer slideTimer;
        private const int slideInterval = 30;  //ms
        private const int slideDuration = 480;      //ms
        private static Panel workPanel;
        private static Direction showDirection = Direction.ToRight;
        private static bool backMode = false;
        private static Size parentSize = new Size();

        public SliderPanel()
        {
            // Create a timer with a ten second interval.
            slideTimer = new System.Windows.Forms.Timer();
            // Hook up the Elapsed event for the timer.
            slideTimer.Tick += new EventHandler(OnTimedEvent);
            slideTimer.Interval = slideInterval;
        }

        public enum Direction
        {
            ToRight,
            ToLeft,
            ToTop,
            ToBottom,
            Back
        }

        public void SlidePanel(Panel p, Direction d, Size ps)
        {
            if(p != null)
            {
                workPanel = p;
            }

            parentSize = ps;
            sliderPanelWidth = workPanel.Width;
            sliderPanelHeight = workPanel.Height;
            if (sliderPanelWidth > 0)
            {
                switch(d)
                {
                    case Direction.ToRight:
                        workPanel.Top = 0;
                        workPanel.Left = 0 - p.Width;
                        workPanel.Visible = true;
                        workPanel.BringToFront();
                        showDirection = d;
                        break;
                    case Direction.ToLeft:
                        workPanel.Top = 0;
                        workPanel.Left = parentSize.Width;
                        workPanel.Visible = true;
                        workPanel.BringToFront();
                        showDirection = d;
                        break;
                    case Direction.Back:
                        backMode = true;
                        break;
                    default:
                        //To do ...
                        break;
                }

            }
            slideTimer.Enabled = true;
        }

        private static int sliderPanelWidth = 0;
        private static int sliderPanelHeight = 0;
        // Specify what you want to happen when the Elapsed event is 
        // raised.
        private void OnTimedEvent(object sender, EventArgs e)
        {
            bool finished = false;
            if (backMode)
            {
                switch (showDirection)
                {
                    case Direction.ToRight:
                        workPanel.Left -= sliderPanelWidth / (slideDuration / slideInterval);
                        if (workPanel.Left <= 0 - sliderPanelWidth)
                        {
                            workPanel.Left = 0 - sliderPanelWidth;
                            finished = true;
                        }
                        break;
                    case Direction.ToLeft:
                        workPanel.Left += sliderPanelWidth / (slideDuration / slideInterval);
                        if (workPanel.Left >= parentSize.Width)
                        {
                            workPanel.Left = parentSize.Width;
                            finished = true;
                        }
                        break;
                    default:
                        //To do ...
                        break;
                }

            }
            else
            {
                switch (showDirection)
                {
                    case Direction.ToRight:
                        workPanel.Left += sliderPanelWidth / (slideDuration / slideInterval);
                        if (workPanel.Left >= 0)
                        {
                            workPanel.Left = 0;
                            finished = true;
                        }
                        break;
                    case Direction.ToLeft:
                        workPanel.Left -= sliderPanelWidth / (slideDuration / slideInterval);
                        if (workPanel.Left + workPanel.Width <= parentSize.Width)
                        {
                            workPanel.Left = parentSize.Width - workPanel.Width;
                            finished = true;
                        }
                        break;
                    default:
                        //To do ...
                        break;
                }
            }
            if(finished)
            {
                slideTimer.Enabled = false;
                if (backMode)
                {
                    workPanel.Visible = false;
                    backMode = false;
                }
            }
            //Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }
}
