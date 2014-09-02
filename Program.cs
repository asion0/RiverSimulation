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

        public static string currentPath;
        public static string documentPath;
        public static string projectFile;

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
