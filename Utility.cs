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
        public static bool IsAllInEmpty(List<Point>[] pts, List<Point> pl)
        {
            foreach (Point p in pl)
            {
                foreach (List<Point> ppl in pts)
                {
                    if (ppl == null)
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
                pl1.Add(p);
            }
        }     
    
    }
}
