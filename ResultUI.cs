using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiverSimulationApplication
{
    public partial class ResultUI : Form
    {
        protected string title = "";
        protected int colStart;
        protected int colEnd;
        protected int rowStart;
        protected int rowEnd;
        protected int iStart;
        protected int iEnd;
        protected int jStart;
        protected int jEnd;
        protected int kStart = -1;
        protected int kEnd = -1;
        protected int tStart = -1;
        protected int tEnd = -1;
        protected string tableName = "";
        protected string colName = "";
        protected string rowName = "";
        protected int colWidth = 0;
        protected int rowHeadersWidth = 0;
        protected object initData = null;
        protected object initData2 = null;
        protected int xDim = 0;     
        protected int yDim = 1;
        protected int sel1Dim = -1;
        protected int sel2Dim = -1;
        protected int sel1Index = -1;
        protected int sel2Index = -1;
        protected string sel1Title = "";
        protected string sel2Title = "";
        protected double[] timeList = null;
        protected int[] timeSel = null;
        protected int m = -1;
    }
}
