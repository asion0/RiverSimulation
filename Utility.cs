using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using PictureBoxCtrl;
using System.Drawing;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Drawing.Drawing2D;
using System.Xml;

namespace RiverSimulationApplication
{
    public static class Utility
    {
        private const UInt32 THREAD_SUSPEND_RESUME = 0x0002;
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
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEOPSTRUCT
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

        private static string kid = @"Philae is a robotic European Space Agency lander that accompanied the Rosetta spacecraft until its designated landing on Comet 67P/Churyumov–Gerasimenko";
        public static string Encrypt(string toBeEncrypt)
        {
            byte[] k = new byte[8];
            for (int i = 0; i < kid.Length; ++i)
            {
                k[i % 8] += (byte)kid[i];
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(toBeEncrypt);
            des.Key = k;
            des.IV = k;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        public static string Decrypt(string toBeDecrypt)
        {
            byte[] k = new byte[8];
            for (int i = 0; i < kid.Length; ++i)
            {
                k[i % 8] += (byte)kid[i];
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[toBeDecrypt.Length / 2];
            for (int x = 0; x < toBeDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(toBeDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = k;
            des.IV = k;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        
        public static string GetOSType()
        {
            Version ver = System.Environment.OSVersion.Version;
            string OSType = "";

             System.OperatingSystem osInfo =System.Environment.OSVersion;
         
             // Determine the platform.
             switch(osInfo.Platform)
             {
                // Platform is Windows 95, Windows 98, 
                // Windows 98 Second Edition, or Windows Me.
                case System.PlatformID.Win32Windows:
                   switch (osInfo.Version.Minor)
                   {
                      case 0:
                         Console.WriteLine ("Windows 95");
                         break;
                      case 10:
                         if(osInfo.Version.Revision.ToString()=="2222A")
                            OSType = "Windows 98 Second Edition";
                         else
                            OSType = "Windows 98";
                         break;
                      case  90:
                         OSType = "Windows Me";
                         break;
                   }
                   break;
         
                // Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000,
                // or Windows XP.
                case System.PlatformID.Win32NT:
                   switch(osInfo.Version.Major)
                   {
                      case 3:
                         OSType = "Windows NT 3.51";
                         break;
                      case 4:
                         OSType = "Windows NT 4.0";
                         break;
                      case 5:
                         if (osInfo.Version.Minor==0) 
                            OSType = "Windows 2000";
                         else
                           OSType = "Windows XP";
                         break;
                      case 6:
                         if (osInfo.Version.Minor == 0)
                             OSType = "Windows Vista";
                         else if (osInfo.Version.Minor == 1)
                             OSType = "Windows 7";
                         else if (osInfo.Version.Minor == 2)
                             OSType = "Windows 8";
                         else if (osInfo.Version.Minor == 3)
                             OSType = "Windows 8.1";
                         break;
                       default:
                         OSType = "Unknown Windows version";
                         break;
                   }
                   break;
             }
            return OSType;
        }

        public static bool ShellExecute(string file)
        {
            ProcessStartInfo psi = new ProcessStartInfo(file);
            psi.UseShellExecute = true;
            Process p = Process.Start(psi);
            p.WaitForExit();
            string s = p.ExitCode.ToString();
            return s == "1";
        }

        public static string GetDescriptionText(string path, string name)
        {
            XmlDocument desc = new XmlDocument();
            desc.Load(path);
            XmlNodeList nodeLists = desc.SelectNodes("Files/Item/Name");
            foreach (XmlNode node in nodeLists)
            {
                if(node.InnerText == name)
                {
                    return node.ParentNode.SelectSingleNode("Text").InnerText;
                }
            }
            return "";
        }

        public static bool UpdateDescriptionText(string path, string name, string text)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList nodeLists = doc.SelectNodes("Files/Item/Name");
            bool found = false;
            foreach (XmlNode node in nodeLists)
            {
                if(node.InnerText == name)
                {
                    node.ParentNode.SelectSingleNode("Text").InnerText = text;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                XmlNode files = doc.SelectSingleNode("Files");//選擇節點
                if (files == null)
                {
                    return false;
                }

                XmlElement fileItem = doc.CreateElement("Item");
                files.AppendChild(fileItem);

                XmlElement n = doc.CreateElement("Name");
                n.InnerText = name;
                fileItem.AppendChild(n);
                XmlElement t = doc.CreateElement("Text");
                t.InnerText = text;
                fileItem.AppendChild(t);
            }
            doc.Save(path);
            return true;
        }

        public static bool ConvertDescriptionText(string srcPath, string dstPath)
        {
            StreamWriter fs = new StreamWriter(dstPath);
            fs.WriteLine("專案名稱：" + Program.projectName);
            fs.WriteLine("\r\n\r\n");

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(srcPath);
            XmlNodeList nodeLists = XmlDoc.SelectNodes("Files/Item/Name");
            foreach (XmlNode node in nodeLists)
            {
                String name = node.InnerText;
                String text = node.ParentNode.SelectSingleNode("Text").InnerText;
                fs.WriteLine(name + "\t\t" + text);
            }
            fs.Close();
            return true;
        }

        public static bool CreateDescriptionText(string path, string name, string text)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement files = doc.CreateElement(@"Files");//選擇節點
            files.SetAttribute("Version", Program.GetDescriptionFileVersion());    //設定屬性
            doc.AppendChild(files);

            XmlElement fileItem = doc.CreateElement("Item");
            files.AppendChild(fileItem);
            XmlElement n = doc.CreateElement("Name");
            n.InnerText = Program.projectFileName;
            fileItem.AppendChild(n);
            XmlElement t = doc.CreateElement("Text");
            t.InnerText = text;
            fileItem.AppendChild(t);
            doc.Save(Program.GetDescriptionFileFullPath());
            return true;
        }

        public static string GetDescriptXMLVersion(string path)
        {
            XmlDocument desc = new XmlDocument();
            desc.Load(path);
            XmlNode info = desc.SelectSingleNode(@"Files");
            if (info != null)
            {
                XmlElement element = (XmlElement)info;
                return element.GetAttribute("Version");
            }
            return "0";
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

        public enum CheckType
        {
            NoCheck,
            NotNegative,
            GreaterThanZero,
            GreaterThanOne,
            GreaterThanTwo,
            GreaterThanThree,
        }

        public static bool CheckConvertInt32(ref int data, TextBox txt, string alertMsg, CheckType t)
        {
            if (!txt.Enabled)
            {
                return true;
            }

            Int32 n = 0;
            bool converted = false;
            try
            {
                n = Convert.ToInt32(txt.Text);
                switch (t)
                {
                    case CheckType.NoCheck:
                        converted = true;
                        break;
                    case CheckType.NotNegative:
                        if (n >= 0)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanZero:
                        if (n > 0)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanOne:
                        if (n > 1)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanTwo:
                        if (n > 2)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanThree:
                        if (n > 3)
                        {
                            converted = true;
                        }
                        break;
                }
            }
            catch
            {
            }

            if (converted)
            {
                data = n;
            }
            else
            {
                MessageBox.Show(alertMsg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return converted;
        }

        public static bool CheckConvertDouble(ref double data, TextBox txt, string alertMsg, CheckType t)
        {
            if (!txt.Enabled)
            {
                return true;
            }

            double d = 0;
            bool converted = false;
            try
            {
                d = Convert.ToDouble(txt.Text);
                switch (t)
                {
                    case CheckType.NoCheck:
                        converted = true;
                        break;
                    case CheckType.NotNegative:
                        if (d >= 0f)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanZero:
                        if (d > 0f)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanOne:
                        if (d > 1f)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanTwo:
                        if (d > 2f)
                        {
                            converted = true;
                        }
                        break;
                    case CheckType.GreaterThanThree:
                        if (d > 3f)
                        {
                            converted = true;
                        }
                        break;
                }
            }
            catch
            {
            }

            if (converted)
            {
                data = d;
            }
            else
            {
                MessageBox.Show(alertMsg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return converted;
        }
        
        public static void SetHtmlUrl(WebBrowser b, string u)
        {
            string url = "file:///" + Environment.CurrentDirectory.Replace('\\', '/') + "/" + u;
            if (b.Url == null || b.Url.ToString() != url)
            {
                b.Navigate(new Uri(url));
            }
        }

        /// <summary>
        /// Create and apply the region on the supplied control
        /// </summary>
        /// <param name="control">The Control object to apply the region to</param>
        /// <param name="bitmap">The Bitmap object to create the region from</param>
        public static void CreateControlRegion(Control control, Bitmap bitmap)
        {
            // Return if control and bitmap are null
            if (control == null || bitmap == null)
                return;

            // Set our control's size to be the same as the bitmap
            control.Width = bitmap.Width;
            control.Height = bitmap.Height;

            // Check if we are dealing with Form here
            if (control is System.Windows.Forms.Form)
            {
                // Cast to a Form object
                Form form = (Form)control;

                // Set our form's size to be a little larger that the bitmap just 
                // in case the form's border style is not set to none in the first place
                form.Width += 15;
                form.Height += 35;

                // No border
                form.FormBorderStyle = FormBorderStyle.None;

                // Set bitmap as the background image
                form.BackgroundImage = bitmap;

                // Calculate the graphics path based on the bitmap supplied
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

                // Apply new region
                form.Region = new Region(graphicsPath);
            }

            // Check if we are dealing with Button here
            else if (control is System.Windows.Forms.Button)
            {
                // Cast to a button object
                Button button = (Button)control;

                // Do not show button text
                button.Text = "";

                // Change cursor to hand when over button
                button.Cursor = Cursors.Hand;

                // Set background image of button
                button.BackgroundImage = bitmap;

                // Calculate the graphics path based on the bitmap supplied
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);

                // Apply new region
                button.Region = new Region(graphicsPath);
            }
        }

        /// <summary>
        /// Calculate the graphics path that representing the figure in the bitmap 
        /// excluding the transparent color which is the top left pixel.
        /// </summary>
        /// <param name="bitmap">The Bitmap object to calculate our graphics path from</param>
        /// <returns>Calculated graphics path</returns>
        private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
        {
            // Create GraphicsPath for our bitmap calculation
            GraphicsPath graphicsPath = new GraphicsPath();

            // Use the top left pixel as our transparent color
            Color colorTransparent = bitmap.GetPixel(0, 0);

            // This is to store the column value where an opaque pixel is first found.
            // This value will determine where we start scanning for trailing opaque pixels.
            int colOpaquePixel = 0;

            // Go through all rows (Y axis)
            for (int row = 0; row < bitmap.Height; row++)
            {
                // Reset value
                colOpaquePixel = 0;

                // Go through all columns (X axis)
                for (int col = 0; col < bitmap.Width; col++)
                {
                    // If this is an opaque pixel, mark it and search for anymore trailing behind
                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        // Opaque pixel found, mark current position
                        colOpaquePixel = col;

                        // Create another variable to set the current pixel position
                        int colNext = col;

                        // Starting from current found opaque pixel, search for anymore opaque pixels 
                        // trailing behind, until a transparent pixel is found or minimum width is reached
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;

                        // Form a rectangle for line of opaque pixels found and add it to our graphics path
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));

                        // No need to scan the line of opaque pixels just found
                        col = colNext;
                    }
                }
            }

            // Return calculated graphics path
            return graphicsPath;
        }

    }

    public static class StructureSetUtility
    {
        //檢查一群組所有的格網點是否連續(上下左右視為連續，對角與間隔視為不連續))
        public static bool IsContinuous(List<Point> pl)
        {
            if(pl.Count == 0)
            {
                return false;
            }

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
        public static bool IsAllInEmpty(RiverSimulationProfile profile, List<Point> pl, int passType, int passIndex)
        {
            foreach (Point p in pl)
            {
                for (int n = 0; n < (int)RiverSimulationProfile.StructureType.StructureTypeSize; ++n)
                {
                    List<Point>[] pts = null;
                    switch (n)
                    {
                        case 0:
                            pts = profile.tBarSets;
                            break;
                        case 1:
                            pts = profile.bridgePierSets;
                            break;
                        case 2:
                            pts = profile.groundsillWorkSets;
                            break;
                        case 3:
                            pts = profile.sedimentationWeirSets;
                            break;
                        default:
                            break;
                    }

                    if (null == pts)
                    {
                        continue;
                    }

                    for (int i = 0; i < pts.Length; ++i)
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
            }
            return true;
        }

        //查詢一格網點位於哪個結構物群組？
        //public static int WhichGroup(List<Point>[] pts, Point pt, List<Point> addional = null, int passIndex = -1)
        //{
        //    for (int i = 0; i < pts.Length; ++i)
        //    {
        //        List<Point> pl = pts[i];
        //        if (pl == null || (passIndex != -1 && i == passIndex))
        //            continue;
        //        if(pl.Contains(pt))
        //        {
        //            return i;
        //        }
        //    }

        //    if (addional != null)
        //    {
        //        if (addional.Contains(pt))
        //        {
        //            return passIndex;
        //        }
        //    }
        //    return -1;
        //}
        public static List<Point>[] GetStructureSets(RiverSimulationProfile profile, int type)
        {
            List<Point>[] pts = null;
            switch (type)
            {
                case 0:
                    pts = profile.tBarSets;
                    break;
                case 1:
                    pts = profile.bridgePierSets;
                    break;
                case 2:
                    pts = profile.groundsillWorkSets;
                    break;
                case 3:
                    pts = profile.sedimentationWeirSets;
                    break;
                default:
                    break;
            }
            return pts;
        }

        public static List<Point> GetStructureSet(RiverSimulationProfile profile, int type, int index)
        {
            List<Point>[] pts = GetStructureSets(profile, type);
            if (pts == null)
            {
                return null;
            }
            else
            {
                return pts[index];
            }
        }

        //查詢一格網點位於哪個結構物群組？
        public static Point WhichGroup(RiverSimulationProfile profile, Point pt, List<Point> addional = null, int passType = -1, int passIndex = -1)
        {
            for (int n = 0; n < (int)RiverSimulationProfile.StructureType.StructureTypeSize; ++n)
            {
                List<Point>[] pts = GetStructureSets(profile, n); 
                if (null == pts)
                {
                    continue;
                }

                for (int i = 0; i < pts.Length; ++i)
                {
                    List<Point> pl = pts[i];
                    if (pl == null || (passIndex != -1 && i == passIndex))
                        continue;
                    if (pl.Contains(pt))
                    {
                        return new Point(n, i);
                    }
                }
            }

            if (addional != null)
            {
                if (addional.Contains(pt))
                {
                    return new Point(passType, passIndex);
                }
            }
            return new Point(-1, -1);
        }

        //檢查pl2群組所有格網點是否完全包含在pl1群組中
        public static bool IsAllInclude(List<Point> pl1, List<Point> pl2)
        {   //pl2 - 被選取的, pl1 - 原群組
            foreach (Point p in pl2)
            {
                if (pl1.Contains(p))
                {
                    return true;
                }
            }
            return false;
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

        //檢查結構物清單，得知是哪種結構物的第幾個？
        public static void CalcTypeCount(int index, ref int type, ref int count, RiverSimulationProfile.StructureType[] typeIndex)
        {
            if (index >= typeIndex.Length)
                return;

            RiverSimulationProfile.StructureType lastType = RiverSimulationProfile.StructureType.StructureTypeSize;
            int c = 0;

            for (int i = 0; i <= index; ++i)
            {
                if (typeIndex[i] != lastType)
                {
                    c = 0;
                    lastType = typeIndex[i];
                }
                else
                {
                    ++c;
                }

            }
            type = (lastType == RiverSimulationProfile.StructureType.StructureTypeSize) ? -1 : (int)lastType;
            count = c;
        }

        public static void EditBottomElevation(RiverSimulationProfile profile, string title, int type, int index)
        {

            TableInputForm form = new TableInputForm();
            form.SetFormMode(title, profile.inputGrid.GetJ, profile.inputGrid.GetI, "", "", "",
                TableInputForm.InputFormType.BottomElevationForm, 90, 120, true, false, false, profile.inputGrid.inputCoor);
            form.SetFormModeExtraData(GetStructureSet(profile, type, index));

            DialogResult r = form.ShowDialog();
            if (DialogResult.OK == r)
            {
                //p.levelProportion = (double[])form.SeparateData().Clone();
                //ShowGridMap(PicBoxType.Sprate);
                //DrawPreview();
            }
        }
    }

    public static class SideFlowtUtility
    {        
        //檢查一群組所有的格網點是否連續(上下左右視為連續，對角與間隔視為不連續))
        public static bool TrimToEdge(ref List<Point> pl, int maxI, int maxJ)
        {
            List<Point> newPl = new List<Point>();
            foreach (Point p in pl)
            {
                if( p.Y == 0 || p.Y == maxJ - 1)
                {
                    newPl.Add(p);
                }
            }
            if(newPl.Count == 0)
            {
                pl = null;
                return false;
            }
            pl = newPl;
            return true;
        }

        //檢查一群組所有的格網點是否連續(上下左右視為連續，對角與間隔視為不連續))
        public static bool IsContinuous(List<Point> pl)
        {
            if (pl.Count == 0)
            {
                return false;
            }

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
        public static bool IsAllInEmpty(RiverSimulationProfile profile, List<Point> pl, int passType, int passIndex)
        {
            foreach (Point p in pl)
            {
                for (int n = 0; n < (int)RiverSimulationProfile.StructureType.StructureTypeSize; ++n)
                {
                    List<Point>[] pts = null;
                    switch (n)
                    {
                        case 0:
                            pts = profile.tBarSets;
                            break;
                        case 1:
                            pts = profile.bridgePierSets;
                            break;
                        case 2:
                            pts = profile.groundsillWorkSets;
                            break;
                        case 3:
                            pts = profile.sedimentationWeirSets;
                            break;
                        default:
                            break;
                    }

                    if (null == pts)
                    {
                        continue;
                    }

                    for (int i = 0; i < pts.Length; ++i)
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
            }
            return true;
        }

        //查詢一格網點位於哪個結構物群組？
        //public static int WhichGroup(List<Point>[] pts, Point pt, List<Point> addional = null, int passIndex = -1)
        //{
        //    for (int i = 0; i < pts.Length; ++i)
        //    {
        //        List<Point> pl = pts[i];
        //        if (pl == null || (passIndex != -1 && i == passIndex))
        //            continue;
        //        if(pl.Contains(pt))
        //        {
        //            return i;
        //        }
        //    }

        //    if (addional != null)
        //    {
        //        if (addional.Contains(pt))
        //        {
        //            return passIndex;
        //        }
        //    }
        //    return -1;
        //}
        public static List<Point>[] GetSideFlowSets(RiverSimulationProfile.SideFlowObject[] objects)
        {
            List<Point>[] pts = new List<Point>[objects.Length];
            int i = 0;
            foreach (RiverSimulationProfile.SideFlowObject o in objects)
            {
                pts[i++] = o.sideFlowPoints;
            }
            return pts;
        }
        
        //public static List<Point> GetSideFlowSet(RiverSimulationProfile profile, int type, int index)
        //{
        //    List<Point>[] pts = GetSideFlowSets(profile, type);
        //    if (pts == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return pts[index];
        //    }
        //}

        //查詢一格網點位於哪個結構物群組？
        public static Point WhichGroup(RiverSimulationProfile.SideFlowObject[] sideOutObjects, RiverSimulationProfile.SideFlowObject[] sideInObjects, Point pt )
        {
            if (sideOutObjects != null)
            {
                for (int i = 0; i < sideOutObjects.Length; ++i)
                {
                    if (sideOutObjects[i].sideFlowPoints != null && sideOutObjects[i].sideFlowPoints.Contains(pt))
                    {
                        return new Point(0, i);
                    }
                }
            }

            if (sideInObjects != null)
            {
                for (int i = 0; i < sideInObjects.Length; ++i)
                {
                    if (sideInObjects[i].sideFlowPoints != null && sideInObjects[i].sideFlowPoints.Contains(pt))
                    {
                        return new Point(1, i);
                    }
                }
            }
            return new Point(-1, -1);
        }

        //檢查pl2群組所有格網點是否完全包含在pl1群組中
        public static bool IsAllInclude(List<Point> pl1, List<Point> pl2)
        {   //pl2 - 被選取的, pl1 - 原群組
            foreach (Point p in pl2)
            {
                if (pl1.Contains(p))
                {
                    return true;
                }
            }
            return false;
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

        //檢查側出入流清單，得知是哪種側流的第幾個？
        //public static void CalcTypeCount(int index, ref int type, ref int count, RiverSimulationProfile.SideFlowType[] typeIndex)
        //{
        //    if (index >= typeIndex.Length)
        //        return;

        //    RiverSimulationProfile.SideFlowType lastType = RiverSimulationProfile.SideFlowType.SideFlowSize;
        //    int c = 0;

        //    for (int i = 0; i <= index; ++i)
        //    {
        //        if (typeIndex[i] != lastType)
        //        {
        //            c = 0;
        //            lastType = typeIndex[i];
        //        }
        //        else
        //        {
        //            ++c;
        //        }

        //    }
        //    type = (lastType == RiverSimulationProfile.SideFlowType.SideFlowSize) ? -1 : (int)lastType;
        //    count = c;
        //}

        //public static void EditBottomElevation(RiverSimulationProfile profile, string title, int type, int index)
        //{

        //    TableInputForm form = new TableInputForm();
        //    form.SetFormMode(title, profile.inputGrid.GetJ, profile.inputGrid.GetI, "", "", "",
        //        TableInputForm.InputFormType.BottomElevationForm, 90, 120, true, false, false, profile.inputGrid.inputCoor);
        //    form.SetFormModeExtraData(GetSideFlowSet(profile, type, index));

        //    DialogResult r = form.ShowDialog();
        //    if (DialogResult.OK == r)
        //    {
        //        //p.levelProportion = (double[])form.SeparateData().Clone();
        //        //ShowGridMap(PicBoxType.Sprate);
        //        //DrawPreview();
        //    }
        //}
    }

    public static class DataGridViewUtility
    {
        public static void PasteFromeExcel(DataGridView v)
        {
            try
            {
                string[] lines = Clipboard.GetText().Split('\n');
                if(v.IsCurrentCellInEditMode && lines.Length == 1)
                {
                    string[] cells = lines[0].Split('\t');
                    DataGridViewTextBoxEditingControl tb = v.EditingControl as DataGridViewTextBoxEditingControl;
                    tb.SelectedText = cells[0];
                    return;
                }

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
            object o = v.GetClipboardContent();
            if (o != null)
            {   //Select in cell
                Clipboard.SetDataObject(o);
            }
            else if (v.IsCurrentCellInEditMode && v.CurrentCell.EditType.Name == "DataGridViewTextBoxEditingControl")
            {   //Select in text
                DataGridViewTextBoxEditingControl tb = v.EditingControl as DataGridViewTextBoxEditingControl;
                string s = tb.SelectedText;
                if (s.Length > 0)
                {
                    Clipboard.SetText(s);
                }
            }
        }

        public static void FillSelectedValue(DataGridView v)
        {
            InputForm dlg = new InputForm();
            dlg.Text = "填入數值";
            dlg.desc.Text = "請輸入數值";
            dlg.inputTxt.Text = "";
            if (DialogResult.OK != dlg.ShowDialog())
            {
                return;
            }
            if (dlg.inputTxt.Text.Length > 0)
            {
                foreach (DataGridViewCell c in v.SelectedCells)
                {
                    if (c == null || c.ReadOnly)
                    {
                        continue;
                    }
                    c.Value = dlg.inputTxt.Text;
                }
            }
        }
        
        public static void InitializeDataGridView(DataGridView v, 
            int colCount, int rowCount, 
            int columnWidth = 48, int rowHeadersWidth = 64,
            string tableName = "", string colName = "", string rowName = "", 
            bool nocolNum = false, bool noRowNum = false,
            bool invertCol = false, bool invertRow = false)
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
                string name = colName;
                if (!nocolNum)
                {
                    if (invertCol)
                    {
                        name += " " + (colCount - i).ToString();
                    }
                    else
                    {
                        name += " " + (i + 1).ToString();
                    }
                }
                v.Columns[i].Name = name;
                v.Columns[i].Width = columnWidth;
            }
            v.RowHeadersWidth = rowHeadersWidth;
            v.TopLeftHeaderCell.Value = tableName;
            for (int i = 0; i < rowCount; i++)
            {
                v.Rows.Add(row);
                string name = rowName;
                if (!noRowNum)
                {
                    if (invertRow)
                    {
                        name += " " + (rowCount - i).ToString();
                    }
                    else
                    {
                        name += " " + (i + 1).ToString();
                    }
                }
                v.Rows[i].HeaderCell.Value = name;
            }
            v.PerformLayout();
        }

        public static void InitializeDataGridView2(
            DataGridView v,     //
            int colStart, int colEnd,
            int rowStart, int rowEnd,
            int columnWidth = 48, int rowHeadersWidth = 64,
            string tableName = "", string colName = "", string rowName = "",
            bool nocolNum = false, bool noRowNum = false,
            bool invertCol = false, bool invertRow = false,
            double[] rowNameArray = null)
        {
            v.Rows.Clear();
            // Create an unbound DataGridView by declaring a column count.
            v.ColumnCount = colEnd - colStart;
            v.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            v.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            string[] row = new string[colEnd - colStart];
            // Set the column header names.
            //int c = 1;
            for (int i = colStart; i < colEnd; ++i)
            {
                string name = colName;
                if (!nocolNum)
                {
                    if (invertCol)
                    {
                        name += " " + (colEnd - i).ToString();
                    }
                    else
                    {
                        name += " " + (i + 1).ToString();
                    }
                }
                v.Columns[i - colStart].Name = name;
                v.Columns[i - colStart].Width = columnWidth;
            }
            v.RowHeadersWidth = rowHeadersWidth;
            v.TopLeftHeaderCell.Value = tableName;
            for (int i = rowStart; i < rowEnd; i++)
            {
                v.Rows.Add(row);
                string name = rowName;
                if (!noRowNum)
                {
                    if (invertRow)
                    {
                        if (rowNameArray == null)
                            name += " " + (rowEnd - i).ToString();
                        else
                            name += " " + rowNameArray[rowEnd - i - 1].ToString();
                    }
                    else
                    {
                        if (rowNameArray == null)
                            name += " " + (i + 1).ToString();
                        else
                            name += " " + rowNameArray[i].ToString();
                    }
                }
                v.Rows[i - rowStart].HeaderCell.Value = name;
            }
            v.PerformLayout();
        }

        public static void FillDataByRiverGrid(CoorPoint[,] inputCoor, DataGridView xv, DataGridView yv, DataGridView zv)
        {

        }
    }

    public static class GmailUtility
    {
        public static bool IsMailAddressValidate(string addr)
        {
            try
            {
                MailAddress from = new MailAddress(addr);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void SendGmail(string mail, string pwd, string[] address, string cc, string subject, string msg, string[] attachments)
        {
            MailAddress from = new MailAddress(mail, "Resed Model Feedback");
            MailAddress[] to = new MailAddress[address.Length];
            for (int i = 0; i < address.Length; ++i)
            {
                string[] ss = address[i].Split('\t');
                to[i] = new MailAddress(ss[1], ss[0]);
            }


            MailMessage message = new MailMessage(from, to[0]);
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Priority = MailPriority.Normal;
            message.Subject = subject;
            var m = new System.Net.Mail.MailMessage();
            m.Body = msg;
            message.Body = m.Body;
            if(to.Length > 1)
            {
                for(int i=1; i<to.Length; ++i)
                {
                    message.To.Add(to[i]);
                }
            }

            if (cc.Length > 0)
            {
                message.CC.Add(new MailAddress(cc));
            }

            foreach (string s in attachments)
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(s));
            }

            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
            MySmtp.Credentials = new System.Net.NetworkCredential(mail, pwd);
            
            MySmtp.EnableSsl = true;
            MySmtp.Send(message);

            MySmtp = null;
            message.Dispose();
        }


    }

    public static class FunctionlUtility
    {
        private static string FindNewProjectName(string projectFolder)
        {
            int index = 0;
            string folder;
            do
            {
                index++;
                folder = "新專案" + index.ToString("D03");
            } while (System.IO.Directory.Exists(projectFolder + "\\" + folder));
            return folder;
        }

        private static string FindNewProjectFileName(string projectFolder)
        {
            int index = 0;
            string file;
            do
            {
                index++;
                file = "新檔案" + index.ToString("D03");
            } while (System.IO.File.Exists(projectFolder + "\\" + file + Program.projectFileExt));
            return file;
        }       
 
        public static bool IsEmptyProject(string p)
        {
	        try	
	        {
                foreach (string f in Directory.GetFiles(p, Program.projectFileFilter)) 
                {
                    return false;
                }
	        }
            catch
            {
                //Console.WriteLine(excpt.Message);
            }          
            return true;
        }

        public static bool SaveProject(RiverSimulationProfile p)
        {
            if (p == null)
            {
                return false;
            }

            RiverSimulationProfile.SerializeBinary(p, Program.GetProjectFileFullPath());
            return true;
        }

        public static bool NewProject(IWin32Window w)
        {
            FolderBrowserDialog folderOpen = new FolderBrowserDialog();
            RiverSimulationApplication.Properties.Settings s = RiverSimulationApplication.Properties.Settings.Default;
            folderOpen.ShowNewFolderButton = false;
            if (s.DefaultOpenProjectFolder != null && s.DefaultOpenProjectFolder.Length > 0)
            {
                folderOpen.SelectedPath = s.DefaultOpenProjectFolder;
            }
            else
            {
                folderOpen.SelectedPath = Program.documentPath;
            }
            //folderOpen.RootFolder = Environment.SpecialFolder.MyComputer;
            SendKeys.Send("{TAB}{TAB}{RIGHT}");

            string projectFolder;
            if (folderOpen.ShowDialog(w) == DialogResult.OK)
            {
                projectFolder = folderOpen.SelectedPath;
                Program.projectBaseFolder = folderOpen.SelectedPath;
            }
            else
            {
                return false;
            }

            //FindNewProjectName(projectFolder);
            InputForm dlg = new InputForm();
            dlg.Text = "建立專案";
            dlg.desc.Text = "請輸入新專案名稱";
            dlg.inputTxt.Text = FindNewProjectName(projectFolder);
            if (DialogResult.OK != dlg.ShowDialog())
            {
                return false;
            }

            if (System.IO.Directory.Exists(projectFolder + "\\" + dlg.inputTxt.Text))
            {
                MessageBox.Show("此專案已存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            try
            {
                Program.projectName = dlg.inputTxt.Text;
                System.IO.Directory.CreateDirectory(Program.GetProjectFullPath());
            }
            catch (Exception)
            {
                MessageBox.Show("無法建立專案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            //RiverSimulationProfile.profile = new RiverSimulationProfile();
            //Program.projectFolder = Program.documentPath + "\\" + dlg.inputTxt.Text;
            Program.SaveDefaultProjectFolder();
            return true;
        }

        public static bool OpenProject(IWin32Window w)
        {
            FolderBrowserDialog folderOpen = new FolderBrowserDialog();
            RiverSimulationApplication.Properties.Settings s = RiverSimulationApplication.Properties.Settings.Default;
            folderOpen.ShowNewFolderButton = false;
            if (s.DefaultOpenProjectFolder != null && s.DefaultOpenProjectFolder.Length > 0)
            {
                folderOpen.SelectedPath = s.DefaultOpenProjectFolder;
            }
            else
            {
                folderOpen.SelectedPath = Program.documentPath;
            }
            SendKeys.Send("{TAB}{TAB}{RIGHT}");

            string projectFolder;
            if (folderOpen.ShowDialog(w) == DialogResult.OK)
            {
                projectFolder = folderOpen.SelectedPath;
                Program.projectBaseFolder = Path.GetDirectoryName(projectFolder);
                Program.projectName = Path.GetFileName(projectFolder);

                //Program.projectBaseFolder = folderOpen.SelectedPath;
            }
            else
            {
                return false;
            }

            Program.SaveDefaultProjectFolder();
            return true;
        }

        public static bool NewProjectFile(IWin32Window w, string projectPath)
        {
            //FindNewProjectName(projectFolder);
            InputForm dlg = new InputForm();
            dlg.Text = "新增檔案";
            dlg.desc.Text = "請輸入檔案名稱";
            dlg.inputTxt.Text = FindNewProjectFileName(projectPath);
            if (DialogResult.OK != dlg.ShowDialog())
            {
                return false;
            }

            if (System.IO.Directory.Exists(projectPath + "\\" + dlg.inputTxt.Text))
            {
                MessageBox.Show("此檔案已存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            try
            {
                Program.projectFileName = dlg.inputTxt.Text;
                RiverSimulationProfile.profile = new RiverSimulationProfile();
                SaveProject(RiverSimulationProfile.profile);
            }
            catch (Exception)
            {
                MessageBox.Show("無法建立檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            //RiverSimulationProfile.profile = new RiverSimulationProfile();
            //Program.projectFolder = Program.documentPath + "\\" + dlg.inputTxt.Text;
            //Program.SaveDefaultProjectFolder();
            return true;
        }

        public static bool OpenProjectFile(IWin32Window w, string projectPath)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Program.GetProjectFullPath();
            dlg.Title = "開啟檔案";
            // Set filter for file extension and default file extension
            dlg.Filter = "resed file|" + Program.projectFileFilter;
            string projectFile;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK && dlg.FileName != null)
            {
                // Open document
                projectFile = dlg.FileName;
                if(Path.GetDirectoryName(projectFile) != Program.GetProjectFullPath())
                {
                    MessageBox.Show("請選取專案目錄內的檔案！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                Program.projectFileName = Path.GetFileNameWithoutExtension(projectFile);
            }
            else
            {
                return false;
            }
 

            try
            {
                RiverSimulationProfile.profile = RiverSimulationProfile.DeSerialize(projectFile);
                RiverSimulationProfile.profile.ResetGoogleStaticMap();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
