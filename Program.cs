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
            functionStruct = new FunctionStruct();
            Application.Run(new RiverSimulationForm());
        }

        public static string currentPath;   //執行檔所在目錄, 會判對是否RAR包裝檔案
        public static string documentPath;  //本專案預設文件目錄 My Documents\FlowSimulation
        public static string projectFolder;   //專案目錄
        public static FunctionStruct functionStruct;
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

        public class FunctionStruct
        {
            public FunctionStruct()
            {
#if _LITE_VERSION_
            isLiteVersion = true;
            isLiteDemoVersion = true;
#else
            isLiteVersion = false;
            isLiteDemoVersion = false;
#endif
            }

            public bool isLiteVersion;
            public bool isLiteDemoVersion;
        }

        public static bool IsLiteVersion()
        {
            return functionStruct.isLiteVersion;
        }

        public static bool IsLiteDemoVersion()
        {
            return functionStruct.isLiteVersion;
        }

        public static string GetVersionString()
        {
            string ver = "Version " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            if (Program.IsLiteVersion())
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
    }
}
