using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

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
            InitialProgram("ResedModel.rmx");

            //Test function
#if DEBUG
//            string f1 = Environment.CurrentDirectory + "\\cchemesh.geo";
//            RiverSimulationProfile.profile.ReadInputGridGeo(f1);
#endif
            //string f2 = Environment.CurrentDirectory + "\\t.i";
            //RiverSimulationProfile.profile.GenerateInputFile(f2);

            Application.Run(new RiverSimulationForm());
        }

        public static string currentPath;   //執行檔所在目錄, 會判斷是否RAR包裝檔案
        public static string documentPath;  //本專案預設文件目錄 My Documents\FlowSimulation
        public static string projectBaseFolder;   //專案目錄
        public static string projectName;   //專案名稱
        public static ProgramVersion programVersion = new ProgramVersion(); //
        public static string projectFileName = @"";     // Ex: "新檔案001"
        public static string descriptionName = @"Description.xml";
        public static string resedName = @"\resed.exe";
        public static string projectFileExt = @".t2d";
        public static string projectFileFilter = @"*.t2d";



        public static ProgramSetting programSetting = null;
        public class ProgramSetting
        {
            public string feedMailAddress;
            public string feedMailKey;
            //public string feedMailPwd;
            public List<string> feedMailTo = new List<string>();

            public ProgramSetting()
            {
                SetDefault();

            }

            public ProgramSetting(string name)
            {
                try
                {
                    XmlTextReader reader = new XmlTextReader(name);
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element: // The node is an element.
                                if(reader.Name == "Feedback")
                                {
                                    feedMailAddress = reader.GetAttribute("MailAddress");
                                    feedMailKey = reader.GetAttribute("Key");
                                    //feedMailPwd = reader.GetAttribute("Pwd");
                                }
                                if (reader.Name == "FeedbackItem")
                                {
                                    string n, a;
                                    n = reader.GetAttribute("Name");
                                    a = reader.GetAttribute("MailAddress");
                                    feedMailTo.Add(n + "\t" + a);
                                }
                                
                                break;
                            case XmlNodeType.Text: //Display the text in each element.
                                //Console.WriteLine(reader.Value);
                                break;
                            case XmlNodeType.EndElement: //Display the end of the element.
                                //Console.Write("</" + reader.Name);
                                //Console.WriteLine(">");
                                break;
                        }
                    }
                    //Console.ReadLine();
                }
                catch
                {   //Parsing fail, using default
                    SetDefault(); 
                }
                //string kk = GmailUtility.Encrypt(s.Substring(16) + s.Substring(0, 16));
                feedMailKey = Utility.Decrypt(feedMailKey);
            }

            private void SetDefault()
            {
                feedMailAddress = "";
                feedMailKey = "";
                //feedMailPwd = "";
                feedMailTo.Clear();
            }
        }

        private static void InitialProgram(string name)
        {
            string path = Environment.CurrentDirectory + "\\" + name;
            programSetting = new ProgramSetting(path);
        }

        public class ProgramVersion
        {
            public ProgramVersion()
            {
#if _LITE_VERSION_
            _LiteVersion = true;
            _DemoVersion = true;
#elif _DEMO_VERSION_
            _LiteVersion = false;
            _DemoVersion = true;
#elif _FULL_VERSION_
            _LiteVersion = false;
            _DemoVersion = false;
#else
            _LiteVersion = false;
            _DemoVersion = false;
#endif
            }
            public bool LiteVersion
            { get { return _LiteVersion; } }

            public bool DemoVersion
            { get { return _DemoVersion; } }

            private bool _LiteVersion;
            private bool _DemoVersion;
        }

        //public static bool IsLiteVersion()
        //{
        //    return functionStruct.isLiteVersion;
        //}

        //public static bool IsLiteDemoVersion()
        //{
        //    return functionStruct.isLiteVersion;
        //}

        public static string GetVersionString()
        {
            string ver = "Version " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            if (Program.programVersion.LiteVersion)
            {
                ver += " Lite Version";
            }
            return ver;        
        }

        public static string GetBuildDayString()
        {
            DateTime buildTime = File.GetLastWriteTime(typeof(Program).Assembly.ManifestModule.FullyQualifiedName);
            return "Build " + buildTime.ToString("yyyy/MM/dd HH:mm:ss");
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

        public static void SaveDefaultProjectFolder()
        {
            RiverSimulationApplication.Properties.Settings s = RiverSimulationApplication.Properties.Settings.Default;
            if (Directory.Exists(Program.projectBaseFolder))
            {
                s.DefaultOpenProjectFolder = Program.projectBaseFolder;
                s.Save();
            }
        }

        public static string  GetProjectFullPath()
        {
            return projectBaseFolder + "\\" + projectName;
        }

        public static string GetProjectFileFullPath()
        {
            return GetProjectFullPath() + "\\" + projectFileName + projectFileExt;
        }

        public static string GetDescriptionFileFullPath()
        {
            return GetProjectFullPath() + "\\" + descriptionName;
        }

        public static string GetProjectFileWorkingPath()
        {
            string workingDirectory = Program.GetProjectFileFullPath() + ".working";
            if(!Directory.Exists(workingDirectory))
            {
                Directory.CreateDirectory(workingDirectory);
            }
            return workingDirectory;
        }
    }
}
