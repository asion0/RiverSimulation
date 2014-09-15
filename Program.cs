using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RiverSimulationApplication
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitialPath();
            Application.Run(new RiverSimulationForm());
        }

        public static string currentPath;   //執行檔所在目錄, 會判對是否RAR包裝檔案
        public static string documentPath;  //本專案預設文件目錄 My Documents\FlowSimulation
        public static string projectFolder;   //專案目錄
        public static bool IsLiteVersion()
        {
#if _LITE_VERSION_
            return true;
#else
            return false;
#endif
        }

        private static void InitialPath()
        {
            if (Environment.GetEnvironmentVariable("sfxname") == null)
            {
                currentPath = Environment.CurrentDirectory;
            }
            else
            {   //For WinRar sfx package.
                currentPath = Path.GetDirectoryName(Environment.GetEnvironmentVariable("sfxname"));
            }
            documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FlowSimulation";
            if (!Directory.Exists(documentPath))
            {
                Directory.CreateDirectory(documentPath);
            }

        }
    }
}
