using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;
using PictureBoxCtrl;
using System.Drawing;

namespace RiverSimulationApplication
{
    public static class Utility
    {
        const UInt32 THREAD_SUSPEND_RESUME = 0x0002;
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(UInt32 dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);

        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(THREAD_SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                SuspendThread(pOpenThread);
            }
        }

        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(THREAD_SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                ResumeThread(pOpenThread);
            }
        }

        private const int FO_DELETE = 0x0003;
        private const int FOF_ALLOWUNDO = 0x0040;           // Preserve undo information, if possible. 
        private const int FOF_NOCONFIRMATION = 0x0010;      // Show no confirmation dialog box to the user

        // Struct which contains information that the SHFileOperation function uses to perform file operations. 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        public static void DeleteFileOrFolder(string path)
        {
            SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
            fileop.wFunc = FO_DELETE;
            fileop.pFrom = path + '\0' + '\0';
            fileop.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            SHFileOperation(ref fileop);
        }
    }

    public static class ControllerUtility
    {
        public static void InitialGridPictureBoxByProfile(ref PictureBoxCtrl.GridPictureBox gp, RiverSimulationProfile p)
        {
            RiverSimulationProfile.BackgroundMapType t = p.GetBackgroundMapType();
            if (RiverSimulationProfile.profile.inputGrid != null)
            {
                gp.Grid = p.inputGrid;
            }
            if(RiverSimulationProfile.BackgroundMapType.ImportImage == t)
            {
                gp.SetMapBackground(p.imagePath, p.sourceE, p.sourceN, p.sourceW, p.sourceH);
            }
            else if(RiverSimulationProfile.BackgroundMapType.GoogleStaticMap == t)
            {
                gp.SetMapBackground(p.tl, p.tr, p.bl, p.br);
            }
            else //if (RiverSimulationProfile.BackgroundMapType.None == t)
            {
                gp.ClearMapBackground();
            }
        }
    }

    public static class GroupGridUtility
    {
        //檢查一群組所有的格網點是否連續(上下左右視為連續，對角與間隔視為不連續))
        public static bool IsContinuous(List<Point> pl)
        {
            Point[] workQueue = new Point[pl.Count];
            workQueue[0] = pl[0];
            for (int i = 1; i < workQueue.Length; ++i)
            {
                workQueue[i].X = -1;
                workQueue[i].Y = -1;
            }
            int ptr = 1;
            for (int i = 0; i < workQueue.Length; ++i)
            {
                Point p0 = workQueue[i];
                if (-1 == p0.X)
                {
                    break;
                }

                Point p1 = new Point(p0.X, p0.Y - 1);
                Point p2 = new Point(p0.X, p0.Y + 1);
                Point p3 = new Point(p0.X - 1, p0.Y);
                Point p4 = new Point(p0.X + 1, p0.Y);

                foreach (Point p in pl)
                {
                    if (p1 == p)
                    {
                        if (!workQueue.Contains(p1))
                        {
                            workQueue[ptr++] = p1;
                        }
                    }
                    if (p2 == p)
                    {
                        if (!workQueue.Contains(p2))
                        {
                            workQueue[ptr++] = p2;
                        }
                    }
                    if (p3 == p)
                    {
                        if (!workQueue.Contains(p3))
                        {
                            workQueue[ptr++] = p3;
                        }
                    }
                    if (p4 == p)
                    {
                        if (!workQueue.Contains(p4))
                        {
                            workQueue[ptr++] = p4;
                        }
                    }
                }
            }
            return (ptr == pl.Count);
        }

        //檢查一群組是否與現有群組重複
        public static bool IsOverlapping(List<Point>[] pts, List<Point> pl, int passIndex)
        {
            for (int i = 0; i < pts.Length; ++i)
            {
                if (i == passIndex || pts[i] == null)
                    continue;
                foreach (Point pt in pts[i])
                {
                    foreach (Point pp in pl)
                    {
                        if (pt == pp)
                            return true;
                    }
                }
            }
            return false;
        }

        //刪除一群組中與其他群組重複的格網點
        public static bool RemoveOverlapping(ref List<Point> pl, List<Point>[] pts, int passIndex)
        {
            //RiverSimulationProfile p = RiverSimulationProfile.profile;
            bool isRemove = false;
            //List<Point> ptsResult = new List<Point>(pts);
            for (int i = 0; i < pts.Length; ++i)
            {   //尋訪全部的乾床群組
                if (i == passIndex || pts[i] == null)
                    continue;
                foreach (Point pt in pts[i])
                {   //被尋訪的乾床群組內的每個點
                    foreach (Point pp in pl)
                    {   //尋訪此次選取的群組
                        if (pt == pp)
                        {
                            pl.Remove(pp);
                            isRemove = true;
                            break;
                        }
                    }
                }
            }
            return isRemove;
        }

        //檢查一群組是否與都位於空白處(不屬於任何群組)
        public static bool IsAllInEmpty(List<Point>[] pts, List<Point> pl, int passIndex)
        {
            foreach (Point p in pl)
            {
                for (int i = 0; i < pts.Length; ++i )
                {
                    List<Point> ppl = pts[i];

                    if (i == passIndex || ppl == null)
                        continue;

                    if (ppl.Contains(p))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        //查詢一格網點位於哪個群組？
        public static int WhichGroup(List<Point>[] pts, Point pt, List<Point> addional = null, int passIndex = -1)
        {
            for (int i = 0; i < pts.Length; ++i)
            {
                List<Point> pl = pts[i];
                if (pl == null || (passIndex != -1 && i == passIndex))
                    continue;
                if(pl.Contains(pt))
                {
                    return i;
                }
            }

            if (addional != null)
            {
                if (addional.Contains(pt))
                {
                    return passIndex;
                }
            }
            return -1;
        }

        //檢查pl2群組所有格網點是否完全包含在pl1群組中
        public static bool IsAllInclude(List<Point> pl1, List<Point> pl2)
        {
            foreach (Point p in pl2)
            {
                if (!pl1.Contains(p))
                {
                    return false;
                }
            }
            return true;
        }

        //移除在pl1群組中所有pl2群組包含的格網點
        public static void RemovePoints(ref List<Point> pl1, List<Point> pl2)
        {
            foreach (Point p in pl2)
            {
                pl1.Remove(p);
            }
        }

        //將pl2群組中所有的格網點加至pl2群組
        public static void MergePoints(ref List<Point> pl1, List<Point> pl2)
        {
            foreach (Point p in pl2)
            {
                if (!pl1.Contains(p))
                {
                    pl1.Add(p);
                }
            }
        }

        //傳回兩個群組是否相鄰
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

        //計算不同群組配色 使相鄰群組必不同色
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

    }

    public static class DataGridViewUtility
    {
        public static void PasteFromeExcel(DataGridView v)
        {
            try
            {
                string[] lines = Clipboard.GetText().Split('\n');
                int row = v.CurrentCell.RowIndex;
                int col = v.CurrentCell.ColumnIndex;
                foreach (string line in lines)
                {
                    if (row >= v.RowCount)
                    {
                        break;
                    }

                    string[] cells = line.Split('\t');
                    for (int i = 0; i < cells.Length; ++i)
                    {
                        if (col + i >= v.ColumnCount)
                        {
                            break;
                        }

                        DataGridViewCell c = v[col + i, row];
                        if (c == null || c.ReadOnly)
                        {
                            continue;
                        }

                        if (c.Value == null || c.Value.ToString() != cells[i])
                        {
                            c.Value = Convert.ChangeType(cells[i], c.ValueType);
                        }

                    }
                    ++row;
                }
            }
            catch (FormatException)
            {
                //MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }            

        }

        public static void CopyToClipboard(DataGridView v)
        {
            Clipboard.SetDataObject(v.GetClipboardContent());
        }

        public static void InitializeDataGridView(DataGridView v, int colCount, int rowCount, int columnWidth = 48, int rowHeadersWidth = 64)
        {
            v.Rows.Clear();
            // Create an unbound DataGridView by declaring a column count.
            v.ColumnCount = colCount;
            v.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            v.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            string[] row = new string[colCount];
            // Set the column header names.
            //int c = 1;
            for (int i = 0; i < colCount; ++i)
            {
                v.Columns[i].Name = (i + 1).ToString();
                v.Columns[i].Width = columnWidth;
                //row[i] = "1";
                //c++;
            }
            v.RowHeadersWidth = rowHeadersWidth;

            for (int i = 0; i < rowCount; i++)
            {
                v.Rows.Add(row);
                v.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        public static void FillDataByRiverGrid(CoorPoint[,] inputCoor, DataGridView xv, DataGridView yv, DataGridView zv)
        {

        }
    }
}
