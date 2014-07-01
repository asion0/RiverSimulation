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
        private static Timer slidePanelTimer;
        private const int slidePanelInterval = 30;  //ms
        private const int slideDuration = 480;      //ms
        private static Panel slideWorkingPanel;
        private static Direction showDirection = Direction.ToRight;

        public SliderPanel()
        {
            // Create a timer with a ten second interval.
            slidePanelTimer = new System.Windows.Forms.Timer();
            // Hook up the Elapsed event for the timer.
            slidePanelTimer.Tick  += new EventHandler(OnTimedEvent);
            slidePanelTimer.Interval = slidePanelInterval;
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
            slideWorkingPanel = p;
            sliderPanelWidth = p.Width;
            sliderPanelHeight = p.Height;
            showDirection = d;
            if (sliderPanelWidth > 0)
            {
                switch(showDirection)
                {
                    case Direction.ToRight:
                        p.Top = 0;
                        p.Left = 0 - p.Width;
                        p.Visible = true;
                        p.BringToFront();
                        break;
                    case Direction.ToLeft:
                        break;
                    default:
                        //To do ...
                        break;

                }

            }
            slidePanelTimer.Enabled = true;
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
                    slideWorkingPanel.Left += sliderPanelWidth / (slideDuration / slidePanelInterval);
                    if (slideWorkingPanel.Left >= 0)
                    {
                        slideWorkingPanel.Left = 0;
                        finished = true;
                    }
                    break;
                case Direction.ToLeft:
                    slideWorkingPanel.Left -= sliderPanelWidth / (slideDuration / slidePanelInterval);
                    if (slideWorkingPanel.Left <= 0 - sliderPanelWidth)
                    {
                        slideWorkingPanel.Left = 0 - sliderPanelWidth;
                        finished = true;
                    }
                    break;
                default:
                    //To do ...
                    break;
            }
            if(finished)
            {
                slidePanelTimer.Enabled = false;
                if (showDirection == Direction.ToLeft)
                {
                    slideWorkingPanel.Visible = false;
                }
            }
            //Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }
}
