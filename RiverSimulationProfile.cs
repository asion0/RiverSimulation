﻿using System;
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
 
        #region Constructor
        public RiverSimulationProfile()
        {
            Initialization();
        }
        #endregion

        //模組特殊功能
        #region SimulationFunction
        public enum DimensionType { None, Type2D, Type3D }
        public enum ModelingType { None, WaterModeling, MovableBed }

        public DimensionType dimensionType { get; set; }   //維度選擇
        public ModelingType modelingType { get; set; }      //模組選擇

        //Special Functions
        //水理
        public bool closeDiffusionEffectFunction { get; set; }              //關閉移流擴散效應
        public bool secondFlowEffectFunction { get; set; }                  //二次流效應
        public bool structureSetFunction { get; set; }                      //結構物設置
        public bool sideInOutFlowFunction { get; set; }                     //側出入流
        public bool waterHighSandContentEffectFunction { get; set; }        //水理高含砂效應

        //動床
        public bool bedrockFunction { get; set; }                           //岩床
        public bool quayStableAnalysisFunction { get; set; }                //岩壁穩定分析
        public bool movableBedHighSandContentEffectFunction { get; set; }   //動床高含砂效應
        #endregion

        //全域參數
        #region GlobalSetting
        public Int32 verticalLevelNumber { get; set; }      //0.1.1 垂向格網分層數目
        public double[] levelProportion { get; set; }       //0.1.1.1 分層比例 陣列大小_verticalLevelNumber
        #endregion

        //水理參數
        #region WaterModeling   
        public enum FlowType
        {
            None,
            ConstantFlow,
            VariableFlow,
        }
        public FlowType flowType = FlowType.None;               //1.0 定/變量流
        //1.1 數值參數 =========================================
        //1.1.1 時間
        public double totalSimulationTime { get; set; }         //1.1.1.1 總模擬時間
        public double timeSpan2d { get; set; }                  //1.1.1.2 二維時間間距
        public Int32 outputFrequency { get; set; }              //1.1.1.3 輸出頻率
        public Int32 steppingTimesInVertVslcTime { get; set; }  //1.1.1.4 垂直方向計算時間步進次數
        //1.1.2 收斂條件
        public double waterModelingConvergenceCriteria2d { get; set; }          //1.1.2.1 二維水理收斂標準
        public double waterModelingConvergenceCriteria3d { get; set; }          //1.1.2.2 三維水理收斂標準
        public Int32 waterModelingMaxIterationTimes { get; set; }               //1.1.2.3 水理最大疊代次數
        //1.1.3 輸出控制
        public double minWaterDeoth { get; set; }                               //1.1.4 最小水深 單一數值 m 0.0001 實數(>0) 實數 8 格 (隱藏版功能)
        public double viscosityFactorAdditionInMainstream { get; set; }         //1.1.5 主流方向黏滯係數加成比例 單一數值 1 實數(>=0) 實數 8 格 (隱藏版功能)
        public double viscosityFactorAdditionInSideDirection { get; set; }      //1.1.6 側方向黏滯係數加成比例 單一數值 1 實數(>=0) 實數 8 格 (隱藏版功能)
        //1.2 物理參數 =========================================
        public enum RoughnessType
        {   //糙度係數 種類
            None,
            ManningN,
            Chezy,
        }
        public RoughnessType roughnessType { get; set; }        //1.2.1 糙度係數 二選一 整數 8 格
        public double manningN { get; set; }                    //1.2.1.1 Manning n 二選一 -- 均一值
        public double[,] manningNArray { get; set; }             //1.2.1.1 Manning n 二選一 -- 矩陣[I,J]
        public double chezy { get; set; }                       //1.2.1.2 Chezy 二選一 -- 均一值
        public double[,] chezyArray { get; set; }               //1.2.1.2 Chezy 二選一 -- 矩陣[I,J]
        public double roughnessHeightKs { get; set; }           //1.2.1.3 粗糙高度 ks mm -- 均一值
        public double[,] roughnessHeightKsArray { get; set; }   //1.2.1.3 粗糙高度 ks mm -- 矩陣[I,J]

        public enum TurbulenceViscosityType     
        {   //紊流黏滯係數 種類
            None,
            UserDefine,
            ZeroEquation,
            SingleEquation,
            TwinEquation,
        }
        public TurbulenceViscosityType turbulenceViscosityType { get; set; }    //1.2.2 紊流黏滯係數 四選一 整數 8 格 
        //1.2.2.1 使用者輸入 模擬功能為二維或三維都可選擇此項輸入
        //1.2.2.1.1 紊流黏滯係數 Ns/m2 實數(>0) 實數 8 格
        public double tvInMainstreamDirection { get; set; }     //需確認
        public double tvInSideDirection { get; set; }           //需確認
        public enum ZeroEquationType
        {   //零方程 種類
            None,
            Constant,
            Parabolic1,
            Parabolic2,
            TypeA,
            TypeB,
        }
        public ZeroEquationType zeroEquationType { get; set; }  //1.2.2.2 零方程 五選一 總共 5 種選項
        //1.2.2.3 單方程 --
        //1.2.2.4 雙方程(k-ε) 三維 only，僅一項，不用下拉選單。

        //1.2.3 其他
        public double gravityConstant { get; set; }             //1.2.3.1 重力常數 單一數值 m/s2 9.81 實數 Free
        public double waterDensity { get; set; }                //1.2.3.2 水密度 單一數值 kg/m3 1000 實數(>0) Free
        
        //1.3 二次流效應 二維 only
        public enum CurvatureRadiusType
        {
            None,
            AutoCurvatureRadius,
            InputCurvatureRadius,
        }
        public CurvatureRadiusType curvatureRadiusType { get; set; }      //1.3.1 曲率半徑 是否自動計算
        public double[,] curvatureRadius { get; set; }      //1.3.1 曲率半徑 矩陣(I,J) m 0 實數 Free

        //1.4 結構物設置 四種結構物：丁壩、橋墩、固床工、攔河堰。
        public bool tBarSet { get; set; }                   //丁壩設置
        public bool bridgePierSet { get; set; }             //橋墩設置
        public bool groundsillWorkSet { get; set; }         //固床工設置
        public bool sedimentationWeirSet { get; set; }      //攔河堰設置
        //1.4.1 結構物數量
        public Int32 tBarNumber { get; set; }               //丁壩數量
        public Int32 bridgePierNumber { get; set; }         //橋墩數量
        public Int32 groundsillWorkNumber { get; set; }     //固床工數量
        public Int32 sedimentationWeirNumber { get; set; }  //攔河堰數量 
        //1.4.1.1 格網位置
        public List<Point>[] _tBarSets { get; set; }                //丁壩位置集合
        public List<Point>[] _bridgePierSets { get; set; }          //橋墩位置集合
        public List<Point>[] _groundsillWorkSets { get; set; }      //固床工位置集合
        public List<Point>[] _sedimentationWeirSets { get; set; }   //攔河堰位置集合

        //1.6 高含砂效應 供使用者輸入 6 個常數：α1、β1、c 1、α2、β2、c 2
        public double highSandEffectAlpha1 { get; set; }
        public double highSandEffectBeta1 { get; set; }
        public double highSandEffectC1 { get; set; }
        public double highSandEffectAlpha2 { get; set; }
        public double highSandEffectBeta2 { get; set; }          
        public double highSandEffectC2 { get; set; }
        #endregion

        //功能檢查
        #region Fuction Check
        public bool Is3DMode() { return dimensionType == DimensionType.Type3D; }
        public bool Is2DMode() { return dimensionType == DimensionType.Type2D; }
        public bool IsWaterModelingMode() { return modelingType == ModelingType.WaterModeling; }
        public bool IsMovableBedMode() { return modelingType == ModelingType.MovableBed; }
        public bool IsConstantFlowType() { return flowType == FlowType.ConstantFlow; }
        public bool IsVariableFlowType() { return flowType == FlowType.VariableFlow; }
        #endregion






























        public bool IsImportFinished() 
        {
            return importFinished;
        }

        public bool IsImportReady() 
        {
            return dimensionType != DimensionType.None && modelingType != ModelingType.None;
        }

        public bool IsSimulationModuleFinished() 
        {
            return dimensionType != DimensionType.None && modelingType != ModelingType.None;
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
            return IsImportFinished() && modelingType == ModelingType.MovableBed;
        }

        public bool IsInitialConditionsFinished() 
        { 
            return initialConditionsFinished; 
        }

        public bool IsInitialConditionsReady() 
        {
            if (modelingType == ModelingType.MovableBed)
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
            if (modelingType == ModelingType.MovableBed)
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

        public void SetModuleType1(DimensionType t) { dimensionType = t; }
        public DimensionType GetModuleType1() { return dimensionType; }
        public void SetModuleType2(ModelingType t) { modelingType = t; }
        public ModelingType GetModuleType2() { return modelingType; }
        
        ////Setting for special functions
        //public bool diffusionEffectFunction { get; set; }
        //public bool secFlowEffectFunction { get; set; }
        //public bool structureSetFunction { get; set; }
        ////public bool dryBedEffectFunction { get; set; }
        ////public bool immersedBoundaryFunction { get; set; }
        //public bool sideInOutFlowFunction { get; set; }
        //public bool highSandContentEffectFunction { get; set; }


        public bool HasMovableBedMode() { return modelingType == ModelingType.MovableBed; }

        public RiverGrid inputGrid = null;
        public int separateNum = 0;             //垂向格網分層數目0.1.1
        public double[] separateArray = null;

        //WaterModeling 數值參數
        public double convergenceCriteria2d;    //二維水裡收斂標準 
        public double convergenceCriteria3d;    //三維水裡收斂標準
        public int maxIterationsNum = 0;        //水理最大疊代次數。1.1.2.3

        //結構物設置
        public bool tBarCheck = false;
        public bool bridgePierCheck = false;
        public bool groundsillWorkCheck = false;
        public bool sedimentationWeirCheck = false;
        public int tBarNum = 0;
        public int bridgePierNum = 0;
        public int groundsillWorkNum = 0;
        public int sedimentationWeirNum = 0;

        // private int _dryBedNum = 0;
        private List<Point>[] _tBarPts = null;
        private List<Point>[] _bridgePierPts = null;
        private List<Point>[] _groundsillWorkPts = null;
        private List<Point>[] _sedimentationWeirPts = null;

        private void ResizeListPointArrayPts(ref List<Point>[] pts, int n)
        {
            if (n <= 0)
                return;

            if (pts == null)
            {
                pts = new List<Point>[n];
            }
            else if (n > pts.Length)
            {
                Array.Resize(ref pts, n);
            }
        }

        public void ResizeStructureSetPts(int n1, int n2, int n3, int n4)
        {
            ResizeListPointArrayPts(ref _tBarPts, n1);
            ResizeListPointArrayPts(ref _bridgePierPts, n2);
            ResizeListPointArrayPts(ref _groundsillWorkPts, n3);
            ResizeListPointArrayPts(ref _sedimentationWeirPts, n4);
        }

        public List<Point>[] TBarPts
        {
            get { return _tBarPts; }
            set { _tBarPts = (List<Point>[])value.Clone(); }
        }
        public List<Point>[] BridgePierPts
        {
            get { return _bridgePierPts; }
            set { _bridgePierPts = (List<Point>[])value.Clone(); }
        }
        public List<Point>[] GroundsillWorkPts
        {
            get { return _groundsillWorkPts; }
            set { _groundsillWorkPts = (List<Point>[])value.Clone(); }
        }
        public List<Point>[] SedimentationWeirPts
        {
            get { return _sedimentationWeirPts; }
            set { _sedimentationWeirPts = (List<Point>[])value.Clone(); }
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
            dimensionType = DimensionType.None;
            modelingType = ModelingType.None;
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

            sb.AppendFormat("{0,8}", (secondFlowEffectFunction ? 1 : 0).ToString());     //是否計算二次流效應
            sb.AppendFormat("{0,8}", (closeDiffusionEffectFunction ? 0 : 1).ToString());     //是否關閉移流擴散效應
            sb.AppendFormat("{0,8}", (1).ToString());     //是否計算傳輸(propogation)效應 不讓使用者更改。
            sb.AppendFormat("{0,8}", (sidewallBoundarySlip ? 1 : 0).ToString());     //1:滑移；0:非滑移。4.1.3.1
            sb.AppendFormat("{0,8}", (modelingType == ModelingType.MovableBed ? 1 : 0).ToString());     //對照“模擬功能”-“模組選擇”。若執行動床計算需產生SED.dat檔案。
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
