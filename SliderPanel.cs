using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiverSimulationApplication
{
    class SliderPanel
    {
        private static Timer slideTimer;
        private const int slideInterval = 30;  //ms
        private const int slideDuration = 480;      //ms
        private static Panel workPanel;
        private static Direction showDirection = Direction.ToRight;

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
            ToBottom
        }

        public void SlidePanel(Panel p, Direction d)
        {
            if(p != null)
            {
                workPanel = p;
            }


            sliderPanelWidth = workPanel.Width;
            sliderPanelHeight = workPanel.Height;
            showDirection = d;
            if (sliderPanelWidth > 0)
            {
                switch(showDirection)
                {
                    case Direction.ToRight:
                        workPanel.Top = 0;
                        workPanel.Left = 0 - p.Width;
                        workPanel.Visible = true;
                        workPanel.BringToFront();
                        break;
                    case Direction.ToLeft:
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
                    if (workPanel.Left <= 0 - sliderPanelWidth)
                    {
                        workPanel.Left = 0 - sliderPanelWidth;
                        finished = true;
                    }
                    break;
                default:
                    //To do ...
                    break;
            }
            if(finished)
            {
                slideTimer.Enabled = false;
                if (showDirection == Direction.ToLeft)
                {
                    workPanel.Visible = false;
                }
            }
            //Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }
}
