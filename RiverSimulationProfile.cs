using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using PictureBoxCtrl;

namespace RiverSimulationApplication
{
    public class RiverSimulationProfile
    {
        public static RiverSimulationProfile profile = new RiverSimulationProfile();

        public RiverSimulationProfile()
        {
            Initialization();
        }

        public bool IsImportFinished() 
        {
            return importFinished;
        }

        public bool IsImportReady() 
        {
            return moduleType1 != ModuleType1.NoSelect && moduleType2 != ModuleType2.NoSelect;
        }

        public bool IsSimulationModuleFinished() 
        {
            return moduleType1 != ModuleType1.NoSelect && moduleType2 != ModuleType2.NoSelect;
        }

        public bool IsSimulationModuleReady() 
        {
            return true;
        }

        public bool IsWaterModelingFinished() 
        { 
            return waterModelingFinished; 
        }

        public bool IsWaterModelingReady() 
        {
            return IsImportFinished(); 
        }

        public bool IsMovableBedFinished() 
        { 
            return movableBedFinished; 
        }

        public bool IsMovableBedReady() 
        { 
            return IsImportFinished() && moduleType2 == ModuleType2.MovableBed;
        }

        public bool IsInitialConditionsFinished() 
        { 
            return initialConditionsFinished; 
        }

        public bool IsInitialConditionsReady() 
        {
            if (moduleType2 == ModuleType2.MovableBed)
                return IsWaterModelingFinished() && IsMovableBedFinished();
            else
                return IsWaterModelingFinished();
        }

        public bool IsBoundaryConditionsFinished() 
        { 
            return boundaryConditionsFinished; 
        }

        public bool IsBoundaryConditionsReady() 
        {
            if (moduleType2 == ModuleType2.MovableBed)
                return IsWaterModelingFinished() && IsMovableBedFinished(); 
            else
                return IsWaterModelingFinished();
        }

        public bool IsRunSimulationFinished() 
        { 
            return runSimulationFinished; 
        }

        public bool IsRunSimulationReady() 
        { 
            return IsBoundaryConditionsFinished() && IsInitialConditionsFinished(); 
        }

        public bool IsSimulationResultFinished()
        { 
            return IsRunSimulationFinished(); 
        }

        public bool IsSimulationResultReady() 
        { 
            return IsRunSimulationFinished(); 
        }
        
        //Temp variable for demo version
        public bool importFinished = false;
        public bool simulationModuleFinished = false;
        public bool waterModelingFinished = false;
        public bool movableBedFinished = false;
        public bool initialConditionsFinished = false;
        public bool boundaryConditionsFinished = false;
        public bool runSimulationFinished = false;

        //public bool ModuleSelectUsability() { return importFinished; }
        public enum ModuleType1 { NoSelect, Type2D, Type3D }
        public enum ModuleType2 { NoSelect, WaterModeling, MovableBed }

        public void SetModuleType1(ModuleType1 t) { moduleType1 = t; }
        public ModuleType1 GetModuleType1() { return moduleType1; }
        public void SetModuleType2(ModuleType2 t) { moduleType2 = t; }
        public ModuleType2 GetModuleType2() { return moduleType2; }
        
        private ModuleType1 moduleType1 = ModuleType1.NoSelect;
        private ModuleType2 moduleType2 = ModuleType2.NoSelect;
        //Setting for special functions
        public bool diffusionEffectFunction { get; set; }
        public bool secFlowEffectFunction { get; set; }
        public bool dryBedEffectFunction { get; set; }
        public bool immersedBoundaryFunction { get; set; }
        public bool sideInOutFlowFunction { get; set; }
        public bool highSandContentEffectFunction { get; set; }

        public bool bedrockFunction { get; set; }
        public bool quayStableAnalysisFunction { get; set; }
        public bool highSandContentFlowFunction { get; set; }

        public bool Is3DMode() { return moduleType1 == ModuleType1.Type3D; }
        public bool HasMovableBedMode() { return moduleType2 == ModuleType2.MovableBed; }

        public RiverGrid inputGrid = null;
        public int separateNum = 0;             //垂向格網分層數目0.1.1
        public double[] separateArray = null;

        //WaterModeling 數值參數
        public double convergenceCriteria2d;    //二維水裡收斂標準 
        public double convergenceCriteria3d;    //三維水裡收斂標準
        public int maxIterationsNum = 0;        //水理最大疊代次數。1.1.2.3

        //乾床資訊
       // private int _dryBedNum = 0;
        private List<Point>[] _dryBedPts = null;
        public void ResizeDryBedPts(int n)
        {
            if (n <= 0)
                return;

            if (_dryBedPts == null)
            {
                _dryBedPts = new List<Point>[n];
            }
            else if (n > _dryBedPts.Length)
            {
                Array.Resize(ref _dryBedPts, n);
            }
        }

        public List<Point>[] DryBedPts
        {
            get { return _dryBedPts; }
            set { _dryBedPts = (List<Point>[])value.Clone(); }
        }

        //浸沒邊界資訊
        //private int _immersedBoundaryNum = 0;
        private List<Point>[] _immersedBoundaryPts = null;
        public bool sidewallBoundarySlip = false;      //4.1.3.1

        public void ResizeImmersedBoundary(int n)
        {
            if (n <= 0)
                return;

            if (_immersedBoundaryPts == null)
            {
                _immersedBoundaryPts = new List<Point>[n];
            }
            else if (n > _immersedBoundaryPts.Length)
            {
                Array.Resize(ref _immersedBoundaryPts, n);
            }
        }

        public List<Point>[] ImmersedBoundaryPts
        {
            get { return _immersedBoundaryPts; }
            set { _immersedBoundaryPts = (List<Point>[])value.Clone(); }
        }

        private void Initialization()
        {
            moduleType1 = ModuleType1.NoSelect;
            moduleType2 = ModuleType2.NoSelect;
        }

        public bool ReadInputGridGeo(string s)
        {
            inputGrid = new RiverGrid();
            return inputGrid.ReadInputFile(s);
        }

        public void ClearInputGrid()
        {
            inputGrid = null;
        }

        public bool IsMapPosition()
        {
            if (null == inputGrid || inputGrid.GetTopLeft.x < 120.0 || inputGrid.GetTopLeft.y < 23.0)
            {
                return false;
            }
            return true;
        }


        public enum BackgroundMapType
        {
            None,
            GoogleStaticMap,
            ImportImage
        };
        private BackgroundMapType bkImgType = BackgroundMapType.None;
        public BackgroundMapType GetBackgroundMapType() 
        {
            //if (bkImgType == BackgroundMapType.ImportImage)
            //{
            //    return BackgroundMapType.None;
            //}
            return bkImgType; 
        }
        //private Bitmap importBmp;
        /*
         private Bitmap gridBmp = new Bitmap(640 * 2, 640 * 2);
         private Bitmap tlBmp, trBmp, blBmp, brBmp;
         public Bitmap GetGridBitmap()  
         {
    
             switch (bkImgType)
             {
                 case BackgroundMapType.None:
                     return gridBmp;

                 case BackgroundMapType.GoogleStaticMap:
                     return gridBmp;

                 case BackgroundMapType.ImportImage:
                     return importBmp;

             }
             return null;
         }
        
         private void FreeStaticMaps()
         {
             if (tlBmp != null)
             {
                 tlBmp.Dispose();
                 tlBmp = null;
             }
             if (trBmp != null)
             {
                 trBmp.Dispose();
                 trBmp = null;
             }
             if (blBmp != null)
             {
                 blBmp.Dispose();
                 blBmp = null;
             }
             if (brBmp != null)
             {
                 brBmp.Dispose();
                 brBmp = null;
             }
         }
         
        public void ClearBackgroundBitmap()
        {
            FreeStaticMaps();
            bkImgType = BackgroundMapType.None;
        }
        
        public CoorPoint GetTopLeft()
        {
            CoordinateTransform ct = new CoordinateTransform();
            CoorPoint pt = new CoorPoint();
            switch (bkImgType)
            {
                case BackgroundMapType.None:
                    pt = ct.CalLonLatDegToTwd97(inputGrid.GetTopLeft.x, inputGrid.GetTopLeft.y);
                    break;
                case BackgroundMapType.GoogleStaticMap:
                    pt = ct.CalLonLatDegToTwd97(inputGrid.GetTopLeft.x, inputGrid.GetTopLeft.y);
                    break;
                case BackgroundMapType.ImportImage:
                    pt = topLeft;
                    break;
            }
            return pt;
        }

        public CoorPoint GetBottomRight()
        {
            CoordinateTransform ct = new CoordinateTransform();
            CoorPoint pt = new CoorPoint();
            switch (bkImgType)
            {
                case BackgroundMapType.None:
                    pt = ct.CalLonLatDegToTwd97(inputGrid.GetBottomRight.x, inputGrid.GetBottomRight.y);
                    break;
                case BackgroundMapType.GoogleStaticMap:
                    pt = ct.CalLonLatDegToTwd97(inputGrid.GetBottomRight.x, inputGrid.GetBottomRight.y);
                    break;
                case BackgroundMapType.ImportImage:
                    pt = bottomRight;
                    break;
            }
            return pt;
        }
        */
        public string tl = Environment.CurrentDirectory + "\\tl.jpg";
        public string tr = Environment.CurrentDirectory + "\\tr.jpg";
        public string bl = Environment.CurrentDirectory + "\\bl.jpg";
        public string br = Environment.CurrentDirectory + "\\br.jpg";
        public bool DownloadGoogleStaticMap()
        {

            if (File.Exists(tl))
            {
                File.Delete(tl);
            }
            if (File.Exists(tr))
            {
                File.Delete(tr);
            } if (File.Exists(bl))
            {
                File.Delete(bl);
            } if (File.Exists(br))
            {
                File.Delete(br);
            }
            inputGrid.DownloadGridMap(tl, tr, bl, br);
            bkImgType = BackgroundMapType.GoogleStaticMap;
            return true;
        }

        public void SetImportImageMode()
        {
            bkImgType = BackgroundMapType.ImportImage;
        }

        //private CoorPoint bottomRight = new CoorPoint();
        //private CoorPoint topLeft = new CoorPoint();
        public string imagePath;
        public double sourceE;
        public double sourceN;
        public double sourceW;
        public double sourceH;
        public void SetImportImage(string s, double e, double n, double w, double h)
        {
            imagePath = s;
            sourceE = e;
            sourceN = n;
            sourceW = w;
            sourceH = h;
        }

        //動床參數 - 物理參數頁面
        public int sedimentParticlesNum = 3;            //2.2.4 泥砂顆粒數目
        public int seabedLevelNum = 6;
        public double[] seabedLevelArray = null;
        public double[,] sedimentCompositionRatioArray = null;


        public bool GenerateInputFile(string file)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("2011m4.i       m4.dat         \n");

            sb.AppendFormat("{0,8}", inputGrid.GetI.ToString());
            sb.AppendFormat("{0,8}", inputGrid.GetJ.ToString());
            sb.AppendFormat("{0,8}", sedimentParticlesNum.ToString());
            sb.AppendFormat("{0,8}", 10.ToString());    //模式預設值
            sb.AppendFormat("{0,8}", 5.ToString());     //模式預設值
            sb.AppendFormat("{0,8}", 0.ToString());     //模式預設值
            sb.AppendFormat("{0,8}", separateNum.ToString());     //垂向格網分層數目0.1.1
            sb.Append("\n");
            //**模式列印輸出格式，建議採預設值
            sb.Append("       0       1       0       0       0       0               0                \n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("       0  100000      20       0      20       0      20       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0       0  100000\n");
            sb.Append("      20       0      20       0      20       1       4       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("      20       0      20       0      20       0      20       0      20       0\n");
            sb.Append("       1       1       1       1       1       1       1       1       1       0\n");
            sb.Append("      20       0                                                                \n");

            sb.AppendFormat("{0,8}", (secFlowEffectFunction ? 1 : 0).ToString());     //是否計算二次流效應
            sb.AppendFormat("{0,8}", (diffusionEffectFunction ? 1 : 0).ToString());     //是否計算二次流效應
            sb.AppendFormat("{0,8}", (1).ToString());     //是否計算傳輸(propogation)效應 不讓使用者更改。
            sb.AppendFormat("{0,8}", (sidewallBoundarySlip ? 1 : 0).ToString());     //1:滑移；0:非滑移。4.1.3.1
            sb.AppendFormat("{0,8}", (moduleType2 == ModuleType2.MovableBed ? 1 : 0).ToString());     //對照“模擬功能”-“模組選擇”。若執行動床計算需產生SED.dat檔案。
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.AppendFormat("{0,8}", (quayStableAnalysisFunction ? 1 : 0).ToString());     //是否計算岸壁崩塌。1:是；0:否。參考介面“模擬功能”-“特殊功能”的“岸壁穩定分析”。
            sb.AppendFormat("{0,8}", maxIterationsNum.ToString());     //水理最大疊代次數。1.1.2.3

            
            using (StreamWriter outfile = new StreamWriter(file))
            {
                outfile.Write(sb.ToString());


                outfile.Close();
            }

            return true;
        }
    }
}
