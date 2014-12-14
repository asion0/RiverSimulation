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
        public RiverGrid inputGrid = null;
        //public int separateNum = 0;             //垂向格網分層數目0.1.1
        //public double[] separateArray = null;

        public Int32 verticalLevelNumber;      //0.1.1 垂向格網分層數目
        public double[] levelProportion;       //0.1.1.1 分層比例 陣列大小_verticalLevelNumber

        public class TwoInOne                //二選一資料類別
        {
            public TwoInOne()
            {
                type = Type.None;
                dataValue = -1;
                dataArray = null;
            }

            public TwoInOne(int i, int j, double v = -1.0, Type t = Type.None)
            {
                type = t;
                dataValue = v;
                dataArray = new double[i, j];
            }

            public TwoInOne(double v, double[,] a, Type t)
            {
                type = t;
                dataValue = v;
                dataArray = (double[,])a.Clone();
            }

            public TwoInOne(TwoInOne o)
            {
                type = o.type;
                dataValue = o.dataValue;
                if (o.dataArray == null)
                {
                    dataArray = null;
                }
                else
                {
                    dataArray = (double[,])o.dataArray.Clone();
                }
            }

            public enum Type
            {
                None,
                UseValue,
                UseArray,
            };
            public Type type;
            public double dataValue;
            public double[,] dataArray;
        }
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
        public double totalSimulationTime;         //1.1.1.1 總模擬時間
        public double timeSpan2d;                  //1.1.1.2 二維時間間距
        public Int32 outputFrequency;              //1.1.1.3 輸出頻率
        public Int32 steppingTimesInVertVslcTime;  //1.1.1.4 垂直方向計算時間步進次數
        //1.1.2 收斂條件
        public double waterModelingConvergenceCriteria2d;          //1.1.2.1 二維水理收斂標準
        public double waterModelingConvergenceCriteria3d;          //1.1.2.2 三維水理收斂標準
        public Int32 waterModelingMaxIterationTimes;               //1.1.2.3 水理最大疊代次數
        //1.1.3 輸出控制
        //2D
        public bool outputControlInitialBottomElevation { get; set; }   //1.1.3 輸出控制 初始底床高程
        public bool outputControlLevel { get; set; }                    //1.1.3 輸出控制 水位
        public bool outputControlDepth { get; set; }                    //1.1.3 輸出控制 水深
        public bool outputControlAverageDepthFlowRate { get; set; }     //1.1.3 輸出控制 水深平均流速
        public bool outputControlFlow { get; set; }                     //1.1.3 輸出控制 流量
        public bool outputControlBottomShearingStress { get; set; }     //1.1.3 輸出控制 底床剪應力
        //3D
        public bool outputControlVelocityInformation3D { get; set; }    //1.1.3 輸出控制 三維流速資訊

        public double minWaterDeoth;                               //1.1.4 最小水深 單一數值 m 0.0001 實數(>0) 實數 8 格 (隱藏版功能)
        public double viscosityFactorAdditionInMainstream;         //1.1.5 主流方向黏滯係數加成比例 單一數值 1 實數(>=0) 實數 8 格 (隱藏版功能)
        public double viscosityFactorAdditionInSideDirection;      //1.1.6 側方向黏滯係數加成比例 單一數值 1 實數(>=0) 實數 8 格 (隱藏版功能)
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
        public double gravityConstant;             //1.2.3.1 重力常數 單一數值 m/s2 9.81 實數 Free
        public double waterDensity;                //1.2.3.2 水密度 單一數值 kg/m3 1000 實數(>0) Free
        
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
        public List<Point>[] tBarSets;                 //丁壩位置集合
        public List<Point>[] bridgePierSets;           //橋墩位置集合
        public List<Point>[] groundsillWorkSets;       //固床工位置集合
        public List<Point>[] sedimentationWeirSets;    //攔河堰位置集合

        public enum StructureType
        {
            TBar,
            BridgePier,
            GroundSillWork,
            SedimentationWeir,
            StructureTypeSize,
        };

        //1.6 高含砂效應 供使用者輸入 6 個常數：α1、β1、c 1、α2、β2、c 2
        public double highSandEffectAlpha1 { get; set; }
        public double highSandEffectBeta1 { get; set; }
        public double highSandEffectC1 { get; set; }
        public double highSandEffectAlpha2 { get; set; }
        public double highSandEffectBeta2 { get; set; }          
        public double highSandEffectC2 { get; set; }

        //Support Functions
        private void ResizeListPointArraySets(ref List<Point>[] pts, int n)
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

        public void ResizeStructureSets(int n1, int n2, int n3, int n4)
        {
            ResizeListPointArraySets(ref tBarSets, n1);
            ResizeListPointArraySets(ref bridgePierSets, n2);
            ResizeListPointArraySets(ref groundsillWorkSets, n3);
            ResizeListPointArraySets(ref sedimentationWeirSets, n4);
        }

        public List<Point>[] BridgePierSets
        {
            get { return bridgePierSets; }
            set { bridgePierSets = (List<Point>[])value.Clone(); }
        }
        public List<Point>[] GroundsillWorkSets
        {
            get { return groundsillWorkSets; }
            set { groundsillWorkSets = (List<Point>[])value.Clone(); }
        }
        public List<Point>[] SedimentationWeirSets
        {
            get { return sedimentationWeirSets; }
            set { sedimentationWeirSets = (List<Point>[])value.Clone(); }
        }

        public void UpdateStructureSet(List<Point> pts, int type, int index)
        {
            switch (type)
            {
                case 0:
                    tBarSets[index] = (pts == null) ? null : new List<Point>(pts);
                    break;
                case 1:
                    bridgePierSets[index] = (pts == null) ? null : new List<Point>(pts);
                    break;
                case 2:
                    groundsillWorkSets[index] = (pts == null) ? null : new List<Point>(pts);
                    break;
                case 3:
                    sedimentationWeirSets[index] = (pts == null) ? null : new List<Point>(pts);
                    break;
                default:
                    break;
            }
        }

        #endregion

        //動床參數
        //2.1 數值參數 =========================================
        public double waterTimeSpan;                //2.1.1 時間間距
        public Int32 waterOutputFrequency;          //2.1.2 輸出頻率

        //2.1.3 輸出控制
        //2D
        public bool outputControlBottomElevation { get; set; }      //2.1.3 輸出控制 初始底床高程
        public bool outputControlAverageDepthDensity { get; set; }  //2.1.3 輸出控制 水深平均流速
        public bool outputControlErosionDepth { get; set; }         //2.1.3 沖淤深度

        //3D
        public bool outputControlDensityInformation3D { get; set; }  //2.1.3 輸出控制 三維流速資訊

        //2.1.4 選擇擴散公式
        public enum DiffusionFormulaType
        {   //紊流黏滯係數 種類
            None,
            Formula1,
            Formula2,
            Formula3,
        }
        public bool diffusionFormulaUse;                //2.1.4 擴散公式
        public DiffusionFormulaType diffusionFormula;   //2.1.4 擴散公式

        public double diffusionBonusProportionalInMainstream;   //2.1.5 主流方向擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
        public double diffusionBonusProportionalInSideflow;     //2.1.6 側方向擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
        public double diffusionBonusProportionalInSurface;      //2.1.7 水面擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
        public double diffusionBonusProportionalInBottom;       //2.1.8 底床擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
        //2.2 物理參數
        public double kinematicViscosityCoefficient;    //2.2.1 動力黏滯係數單一數值 秒 1.12e-6 實數(>=0) 實數16 格
        public double sedimentPoreRatio;                //2.2.2 泥砂孔隙比單一數值 -- 0.4 實數(>=0) 實數8 格
        public double sedimentDensity;                  //2.2.3 泥砂密度單一數值 Kg/m3 2700 實數(>=0) 實數8 格
        public Int32 sedimentParticlesNumber;           //2.2.4 泥砂顆粒數目單一數值K 3 整數(>2) 最優先設定
        public double[,] sedimentParticleSize;          //2.2.4.1 泥砂粒徑矩陣(K) m 實數(>0) 實數16 格矩陣(K)為泥砂顆粒數目
        //2.3 底床組成
        public Int32 bottomLevelNumber;                 //2.3.1 底床分層數目單一數值 整數(>0) a. 使用者輸入底床分層數目後
        public double[] bottomLevelArray;               //2.3.1.1 底床分層厚度矩陣(L) m 實數(>0) 矩陣(L)為底床分層數目
        public double[,] sedimentCompositionArray;      //2.3.1.2 泥砂組成比例矩陣(K,L) 實數(>0) 矩陣(K,L)為(泥砂顆粒數目, 底床分層數目)

        public bool shenCohesiveSediment { get; set; }  //2.3.2 凝聚性沉滓option

        public bool surfaceErosion;                         //2.3.2.1 表層沖刷 -- 實數(>0) 供者用者輸入係數及臨界剪應力(N/m2)兩個值
        public double surfaceErosionCoefficient;            //2.3.2.1 表層沖刷 -- 實數(>0) 供者用者輸入係數及臨界剪應力(N/m2)兩個值
        public double surfaceErosionCriticalShearStress;    //2.3.2.1 表層沖刷 -- 實數(>0) 供者用者輸入係數及臨界剪應力(N/m2)兩個值
        
        public bool massiveErosion;                         //2.3.2.2 塊狀沖蝕 單一數值 N/m2。 -- 實數(>0) 供者用者輸入臨界剪應力(N/m2)
        public double massiveErosionCriticalShearStress;    //2.3.2.2 塊狀沖蝕 單一數值 N/m2。 -- 實數(>0) 供者用者輸入臨界剪應力(N/m2)
        public bool noErosionElevation;                     //2.3.3 不可沖刷高程 二選一 m 實數 a. option 用 check box
        public double noErosionElevationValue;                //b. 0：均一值，逐點給：-1
        public double[,] noErosionElevationArray;                //若為逐點給，則參數形式為矩陣(I,J)

        //2.4 輸砂公式 當特殊功能動床有勾選高含砂效應時，為6選1，否則僅一般輸砂公式中 3 選 1。
        public enum SandTransportEquationType
        {
            None,
            SandTransportEquation1,
            SandTransportEquation2,
            SandTransportEquation3,
            HighSandTransportEquation4,
            HighSandTransportEquation5,
            HighSandTransportEquation6,
        }
        //2.4.1 一般輸砂公式 多選一 -- -- 整數 8 格 共 3 種選項
        //2.4.2 高含砂輸砂公式 多選一 -- -- 整數 8 格 共 3 種選項
        public SandTransportEquationType sandTransportEquation; 

        //2.5 岩床
        public bool waterJetting;           //2.5.1 水力沖刷
        public double waterJettingAlpha;    //2.5.1 水力沖刷 實數 供使用者輸入α及β兩個常數。
        public double waterJettingBeta;     //2.5.1 水力沖刷 實數 供使用者輸入α及β兩個常數。
       
        public bool sedimentErosion;                            //2.5.2 泥砂磨蝕
        public double sedimentErosionElasticModulusValue;       //2.5.2.1 彈性模數 二選一 pa 實數(>=0) a. 0：均一值，逐點給：-1
        public double[,] sedimentErosionElasticModulusArray;    //2.5.2.1 彈性模數 二選一 pa 實數(>=0) a. 若為逐點給，則參數形式為矩陣(I,J)
        public double sedimentErosionTensileStrengthValue;      //2.5.2.2 張力強度 二選一 pa 實數(>=0) a. 0：均一值，逐點給：-1
        public double[,] sedimentErosionTensileStrengthArray;   //2.5.2.2 張力強度 二選一 pa 實數(>=0) a. 若為逐點給，則參數形式為矩陣(I,J)

        public bool bedrockElevation;           //2.5.3 岩床高程
        public double bedrockElevationValue;    //2.5.3 岩床高程 二選一 m 實數 a. 0：均一值，逐點給：-1。
        public double[,] bedrockElevationArray; //2.5.3 岩床高程 二選一 m 實數 a. 為逐點給，則參數形式為矩陣(I,J)

        //2.6 岸壁穩定分析 option
        //2.6.1 分析位置
        public bool positionAnalysis { get; set; }   //2.6.1 分析位置
        public enum PositionAnalysisType
        {
            None,
            GlobalSimulation,
            LocalSimulation,
        }
        public PositionAnalysisType positionAnalysisType;   //2.6.1 分析位置二選一 -- a. 僅供介面用，不用輸入到input 檔。此選項為提供全部模擬與局部模擬兩個選項
        public int localBlockNumber;                        //2.6.1.1 數值格網數目的表格供使用者填入欲分析位置數目IB，其中IB 的數目不可超過格網數目I。
        public int[,] localBlockArray;                         //2.6.1.1 局部區塊數目矩陣(2, IB) -- a. 矩陣(2, IB)，1 代表左岸，2 代表右岸。僅為0、1 兩個數目字可供選擇，若1 為計算，若0 為不計算。

        //2.6.2 入滲效應
        public bool infiltrationEffect { get; set; }         //2.6.2 入滲效應 option
        public enum InfiltrationEffectTimeFormat
        {
            Minute,
            Hour,
        }
        public InfiltrationEffectTimeFormat infiltrationEffectTimeFormat;   //2.6.2.1 時間格式二選一 小時/分鐘，二選一。
        public double infiltrationEffectTimeSpacing;                        //2.6.2.2 間距單一數值 實數(>0) 使用者自行輸入數值ex：1.5 小時or 90 分鐘
        public double[] rainfall;                                           //2.6.2.2.1 降雨量矩陣 mm 實數(>0) Free a. 矩陣大小需計算：首先將間距換為秒，然後“總模擬時間” (秒)除於間距(秒)，即為矩陣大小

        //2.6.3 岸壁幾何條件
        public bool quayGeometry { get; set; }      //2.6.3 岸壁幾何條件
        public int soilStratificationNumber;        //2.6.3.1 岸壁土壤分層數目單一數值 整數(>0) option
        public double[,] layerThicknessArray;       //2.6.3.1.1 分層厚度矩陣(LBK)m 實數(>0) 矩陣(LBK)為岸壁土壤分層數目
        public double[,] quayHeightArray;           //2.6.3.2 岸壁高度矩陣(2, IB) m 實數
        public double[,] dikeToWharfLengthArray;    //2.6.3.3 堤防到岸壁的長度矩陣(2, IB)m 實數(>0)

        //2.6.4 岸壁土壤性質
        public bool quaySoilProperties { get; set; }    //2.6.4 岸壁土壤性質
        public double cohesion;                         //2.6.4.1 凝聚力 二選一 pa 實數(>0) a. 0：均一值，逐點給：-1
        public double[,,] cohesionArray;                //2.6.4.1 凝聚力 若為逐點給，則參數形式為矩陣(2,IB,LBK)

        public double reposeAngle;                      //2.6.4.2 安息角 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
        public double[,,] reposeAngleArray;             //2.6.4.2 安息角 若為逐點給，則參數形式為矩陣(2,IB,LBK)

        public double frictionAngle;                    //2.6.4.3 內摩擦角 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
        public double[,,] frictionAngleArray;           //2.6.4.3 內摩擦角 若為逐點給，則參數形式為矩陣(2,IB,LBK)

        public double flowRateRatio;                    //2.6.4.3 比流率 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
        public double[, ,] flowRateRatioArray;          //2.6.4.3 比流率 若為逐點給，則參數形式為矩陣(2,IB,LBK)

        public double porosityRatio;                    //2.6.4.5 孔隙率二選一 -- 實數(>0) a. 0：均一值，逐點給：-1
        public double[,,] porosityRatioArray;           //若為逐點給，則參數形式為矩陣(2,IB,LBK)

        public double soilProportion;                  //2.6.4.6 土壤比重二選一 -- 實數(>0) a. 0：均一值，逐點給：-1
        public double[,,] soilProportionArray;         //若為逐點給，則參數形式為矩陣(2,IB,LBK)

        public double ShearStrengthAngle;              //2.6.4.7 岸壁未飽和基值吸力造成剪力強度增加所對應角度 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
        public double[, ,] ShearStrengthAngleArray;         //2.6.4.7 岸壁未飽和基值吸力造成剪力強度增加所對應角度 若為逐點給，則參數形式為矩陣(2,IB,LBK)

        //3. 初始條件
        //3.1 水理模組 =========================================
        public TwoInOne depthAverageFlowSpeedU;           //3.1.1 水深平均流速-U 二選一m/s 實數 實數 8 格a. 0：均一值，逐點給：-1
        //public double[,] depthAverageFlowSpeedUArray;      //3.1.1 水深平均流速-U 二選一m/s 實數 實數 8 格a. 逐點給，參數 形式為矩陣(I,J)

        public TwoInOne depthAverageFlowSpeedV;           //3.1.2 水深平均流速-V 二選一m/s 實數 實數 8 格a. 0：均一值，逐點給：-1
        //public double[,] depthAverageFlowSpeedVArray;      //3.1.2 水深平均流速-V 二選一m/s 實數 實數 8 格a. 逐點給，參數 形式為矩陣(I,J)

        public double waterLevel;           //3.1.3 水位 二選一 m 實數 實數 8 格a. 0：均一值，逐點給：-1
        public double[,] waterLevelArray;      //3.1.4 水位 二選一 m 實數 實數 8 格a. 若為逐 點給，則參數形式為矩陣(I,J)

        public enum VerticalVelocitySliceType
        {
            None,
            Open,
            Close,
        }
        public VerticalVelocitySliceType verticalVelocitySlice;         //3.1.4 垂向流速剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

        //3.2 動床模組
        public List<double> depthAverageConcentration;              //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入
        public List<double[,]> depthAverageConcentrationList;       //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入。

        public enum VerticalConcentrationSliceType
        {
            None,
            Open,
            Close,
        }
        public VerticalConcentrationSliceType verticalConcentrationSlice;         //3.2.2 垂向濃度剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

        //4. 邊界條件
        //4.1 水理模組
        //4.1.1 上游
        public enum FlowConditionType
        {
            None,
            SuperCriticalFlow,
            SubCriticalFlow,
        }
        public FlowConditionType upFlowCondition;         //4.1.1.1 流況設定 二選一
        //4.1.1.1.1 超臨界流
        public int superBoundaryConditionNumber;        //4.1.1.1.1.0 邊界條件數目 T 整數(>1) 定量流不輸入
        public TwoInOne superFlowQuantity;              //4.1.1.1.1.1 流量 m3/s 實數(>=0) a. 圖 5，“即時互動處”呈現流量歷線圖，根
        public TwoInOne superWaterLevel;                //4.1.1.1.1.2 水位 m 實數
        
        //4.1.1.1.2 亞臨界流
        public int subBoundaryConditionNumber;        //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入
        public TwoInOne subFlowQuantity;              //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 

        public bool verticalVelocityDistribution;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)
        public double[,] verticalVelocityDistributionArray;     //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0) 
        
        //4.1.2 下游 二選一
        public FlowConditionType downFlowCondition;         //4.1.2 下游 二選一
        public TwoInOne downSuperWaterLevel;                //4.1.2.2.1 水位 實數 同 4.1.1.1.1.2，T 與前同(4.1.1.1.1.0 或4.1.1.1.2.0)
        
        //4.1.3 側壁
        public bool sidewallBoundarySlip;               //4.1.3.1 側壁邊界滑移 -- 0 整數(>0) 整數 8 格 0：非滑移、1：滑移，check box

        //4.1.4 水面 三維 only。(”即時互動處”不放圖示)
        public double mainstreamWindShear;              //4.1.4.1 主流方向風剪 單一數值 N/m2 0 實數 實數 8 格
        public double sideWindShear;                    //4.1.4.2 側方向風剪 單一數值 N/m2 0 實數 實數 8 格
        public double coriolisForce;                    //4.1.4.3 科氏力 單一數值 N/m2 0 實數 實數 8 格

        //4.1.5 底床 實數 三維 only。(”即時互動處”不放圖示)
        public int boundaryLayerThickness;              //4.1.5.1 邊界層厚度 三選一 3 整數(>0) 整數 8 格 1、2、3，三維 only，下拉選單。
        public enum SeabedBoundarySlipType
        {
            NonSlip,
            Slip,
            WallFunction,
        }
        public SeabedBoundarySlipType seabedBoundarySlip;   //4.1.5.2 底床邊界滑移 三選一 -- 0 整數(>0) 整數 8 格 a. 三維 only，下拉選單 b. 0：非滑移、1：滑移、2：壁函數

        
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

        //public void SetModuleType1(DimensionType t) { dimensionType = t; }
        //public DimensionType GetModuleType1() { return dimensionType; }
        //public void SetModuleType2(ModelingType t) { modelingType = t; }
        //public ModelingType GetModuleType2() { return modelingType; }
        
        ////Setting for special functions
        //public bool diffusionEffectFunction { get; set; }
        //public bool secFlowEffectFunction { get; set; }
        //public bool structureSetFunction { get; set; }
        ////public bool dryBedEffectFunction { get; set; }
        ////public bool immersedBoundaryFunction { get; set; }
        //public bool sideInOutFlowFunction { get; set; }
        //public bool highSandContentEffectFunction { get; set; }


        //public bool HasMovableBedMode() { return modelingType == ModelingType.MovableBed; }


        //WaterModeling 數值參數
        //public double convergenceCriteria2d;    //二維水裡收斂標準 
        //public double convergenceCriteria3d;    //三維水裡收斂標準
        public int maxIterationsNum = 10000;        //水理最大疊代次數。1.1.2.3

        //結構物設置
        //public bool tBarCheck = false;
        //public bool bridgePierCheck = false;
        //public bool groundsillWorkCheck = false;
        //public bool sedimentationWeirCheck = false;
        //public int tBarNum = 0;
        //public int bridgePierNum = 0;
        //public int groundsillWorkNum = 0;
        //public int sedimentationWeirNum = 0;

        // private int _dryBedNum = 0;
        //private List<Point>[] _tBarPts = null;
        //private List<Point>[] _bridgePierPts = null;
        //private List<Point>[] _groundsillWorkPts = null;
        //private List<Point>[] _sedimentationWeirPts = null;


        //public List<Point>[] TBarSets
        //{
        //    get { return _tBarSets; }
        //    set { _tBarSets = (List<Point>[])value.Clone(); }
        //}


        //浸沒邊界資訊
        //private int _immersedBoundaryNum = 0;
        //private List<Point>[] _immersedBoundaryPts = null;
        //public bool sidewallBoundarySlip = false;      //4.1.3.1

  
        private void Initialization()
        {
            //模組特殊功能高程
            dimensionType = DimensionType.None;   //維度選擇
            modelingType = ModelingType.None;      //模組選擇

            //Special Functions
            //水理
            closeDiffusionEffectFunction = false;              //關閉移流擴散效應
            secondFlowEffectFunction = false;                  //二次流效應
            structureSetFunction = false;                      //結構物設置
            sideInOutFlowFunction = false;                     //側出入流
            waterHighSandContentEffectFunction = false;        //水理高含砂效應

            //動床
            bedrockFunction = false;                           //岩床
            quayStableAnalysisFunction = false;                //岩壁穩定分析
            movableBedHighSandContentEffectFunction = false;   //動床高含砂效應

            //全域參數
            inputGrid = null;
            verticalLevelNumber = 19;      //0.1.1 垂向格網分層數目
            levelProportion = null;       //0.1.1.1 分層比例 陣列大小_verticalLevelNumber

            //水理參數
            flowType = FlowType.None;               //1.0 定/變量流
            //1.1 數值參數 =========================================
            //1.1.1 時間
            totalSimulationTime = 0;         //1.1.1.1 總模擬時間
            timeSpan2d = 0;                  //1.1.1.2 二維時間間距
            outputFrequency = 0;              //1.1.1.3 輸出頻率
            steppingTimesInVertVslcTime = 10;  //1.1.1.4 垂直方向計算時間步進次數
            //1.1.2 收斂條件
            waterModelingConvergenceCriteria2d = 0.0001;          //1.1.2.1 二維水理收斂標準
            waterModelingConvergenceCriteria3d = 0.0001;          //1.1.2.2 三維水理收斂標準
            waterModelingMaxIterationTimes = 10000;               //1.1.2.3 水理最大疊代次數

            //1.1.3 輸出控制
            //2D
            outputControlInitialBottomElevation = false;   //1.1.3 輸出控制 初始底床高程
            outputControlLevel = false;                    //1.1.3 輸出控制 水位
            outputControlDepth = false;                    //1.1.3 輸出控制 水深
            outputControlAverageDepthFlowRate = false;     //1.1.3 輸出控制 水深平均流速
            outputControlFlow = false;                     //1.1.3 輸出控制 流量
            outputControlBottomShearingStress = false;     //1.1.3 輸出控制 底床剪應力
            //3D
            outputControlVelocityInformation3D = false;    //1.1.3 輸出控制 三維流速資訊

            minWaterDeoth = 0.0001;                               //1.1.4 最小水深 單一數值 m 0.0001 實數(>0) 實數 8 格 (隱藏版功能)
            viscosityFactorAdditionInMainstream = 1;         //1.1.5 主流方向黏滯係數加成比例 單一數值 1 實數(>=0) 實數 8 格 (隱藏版功能)
            viscosityFactorAdditionInSideDirection = 1;      //1.1.6 側方向黏滯係數加成比例 單一數值 1 實數(>=0) 實數 8 格 (隱藏版功能)
            //1.2 物理參數 =========================================
            roughnessType = RoughnessType.None;        //1.2.1 糙度係數 二選一 整數 8 格
            manningN = 0;                    //1.2.1.1 Manning n 二選一 -- 均一值
            manningNArray = null;             //1.2.1.1 Manning n 二選一 -- 矩陣[I,J]
            chezy = 0;                       //1.2.1.2 Chezy 二選一 -- 均一值
            chezyArray = null;               //1.2.1.2 Chezy 二選一 -- 矩陣[I,J]
            roughnessHeightKs = 0;           //1.2.1.3 粗糙高度 ks mm -- 均一值
            roughnessHeightKsArray = null;   //1.2.1.3 粗糙高度 ks mm -- 矩陣[I,J]

            turbulenceViscosityType = TurbulenceViscosityType.None;    //1.2.2 紊流黏滯係數 四選一 整數 8 格 
            //1.2.2.1 使用者輸入 模擬功能為二維或三維都可選擇此項輸入
            //1.2.2.1.1 紊流黏滯係數 Ns/m2 實數(>0) 實數 8 格
            tvInMainstreamDirection = 0;     //需確認
            tvInSideDirection = 0;           //需確認
            zeroEquationType = ZeroEquationType.None;  //1.2.2.2 零方程 五選一 總共 5 種選項
            //1.2.2.3 單方程 --
            //1.2.2.4 雙方程(k-ε) 三維 only，僅一項，不用下拉選單。

            //1.2.3 其他
            gravityConstant = 9.81;             //1.2.3.1 重力常數 單一數值 m/s2 9.81 實數 Free
            waterDensity = 1000;                //1.2.3.2 水密度 單一數值 kg/m3 1000 實數(>0) Free

            //1.3 二次流效應 二維 only
            curvatureRadiusType = 0;      //1.3.1 曲率半徑 是否自動計算
            curvatureRadius = null;      //1.3.1 曲率半徑 矩陣(I,J) m 0 實數 Free

            //1.4 結構物設置 四種結構物：丁壩、橋墩、固床工、攔河堰。
            tBarSet = false;                   //丁壩設置
            bridgePierSet = false;             //橋墩設置
            groundsillWorkSet = false;         //固床工設置
            sedimentationWeirSet = false;      //攔河堰設置
            //1.4.1 結構物數量
            tBarNumber = 0;               //丁壩數量
            bridgePierNumber = 0;         //橋墩數量
            groundsillWorkNumber = 0;     //固床工數量
            sedimentationWeirNumber = 0;  //攔河堰數量 
            //1.4.1.1 格網位置
            tBarSets = null;                 //丁壩位置集合
            bridgePierSets = null;           //橋墩位置集合
            groundsillWorkSets = null;       //固床工位置集合
            sedimentationWeirSets = null;    //攔河堰位置集合

            //1.6 高含砂效應 供使用者輸入 6 個常數：α1、β1、c 1、α2、β2、c 2
            highSandEffectAlpha1 = 0;
            highSandEffectBeta1 = 0;
            highSandEffectC1 = 0;
            highSandEffectAlpha2 = 0;
            highSandEffectBeta2 = 0;          
            highSandEffectC2 = 0;

            //動床參數
            //2.1 數值參數 =========================================
            waterTimeSpan = 0;                //2.1.1 時間間距
            waterOutputFrequency = 0;          //2.1.2 輸出頻率

            //2.1.3 輸出控制
            //2D
            outputControlBottomElevation = false;       //2.1.3 輸出控制 初始底床高程
            outputControlAverageDepthDensity = false;   //2.1.3 輸出控制 水深平均流速
            outputControlErosionDepth = false;          //2.1.3 沖淤深度

            //3D
            outputControlDensityInformation3D = false;   //2.1.3 輸出控制 三維流速資訊

            //2.1.4 選擇擴散公式
            diffusionFormulaUse = false;;                //2.1.4 擴散公式
            diffusionFormula = DiffusionFormulaType.None;   //2.1.4 擴散公式

            diffusionBonusProportionalInMainstream = 1.0;   //2.1.5 主流方向擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
            diffusionBonusProportionalInSideflow = 1.0;     //2.1.6 側方向擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
            diffusionBonusProportionalInSurface = 1.0;      //2.1.7 水面擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)
            diffusionBonusProportionalInBottom = 1.0;       //2.1.8 底床擴散係數加成比例單一數值 1 實數(>=0) 實數8 格三維 only (隱藏版功能)


            //2.2 物理參數
            kinematicViscosityCoefficient = 1.12e-6;    //2.2.1 動力黏滯係數單一數值 秒 1.12e-6 實數(>=0) 實數16 格
            sedimentPoreRatio = 0.4;                //2.2.2 泥砂孔隙比單一數值 -- 0.4 實數(>=0) 實數8 格
            sedimentDensity = 2700;                  //2.2.3 泥砂密度單一數值 Kg/m3 2700 實數(>=0) 實數8 格
            sedimentParticlesNumber = 3;           //2.2.4 泥砂顆粒數目單一數值K 3 整數(>2) 最優先設定
            sedimentParticleSize = null;          //2.2.4.1 泥砂粒徑矩陣(K) m 實數(>0) 實數16 格矩陣(K)為泥砂顆粒數目
            //2.3 底床組成
            bottomLevelNumber = 6;                 //2.3.1 底床分層數目單一數值 整數(>0) a. 使用者輸入底床分層數目後
            bottomLevelArray = null;               //2.3.1.1 底床分層厚度矩陣(L) m 實數(>0) 矩陣(L)為底床分層數目
            sedimentCompositionArray = null;      //2.3.1.2 泥砂組成比例矩陣(K,L) 實數(>0) 矩陣(K,L)為(泥砂顆粒數目, 底床分層數目)

            shenCohesiveSediment = false;  //2.3.2 凝聚性沉滓option

            surfaceErosion = false;                 //2.3.2.1 表層沖刷 -- 實數(>0) 供者用者輸入係數及臨界剪應力(N/m2)兩個值
            surfaceErosionCoefficient = 0;          //2.3.2.1 表層沖刷 -- 實數(>0) 供者用者輸入係數及臨界剪應力(N/m2)兩個值
            surfaceErosionCriticalShearStress = 0;  //2.3.2.1 表層沖刷 -- 實數(>0) 供者用者輸入係數及臨界剪應力(N/m2)兩個值
        
            massiveErosion = false;                 //2.3.2.2 塊狀沖蝕 單一數值 N/m2。 -- 實數(>0) 供者用者輸入臨界剪應力(N/m2)
            massiveErosionCriticalShearStress = 0;  //2.3.2.2 塊狀沖蝕 單一數值 N/m2。 -- 實數(>0) 供者用者輸入臨界剪應力(N/m2)
            noErosionElevation = false;             //2.3.3 不可沖刷高程 二選一 m 實數 a. option 用 check box
            noErosionElevationValue = 0;            //b. 0：均一值，逐點給：-1
            noErosionElevationArray = null;         //若為逐點給，則參數形式為矩陣(I,J)

            //2.4.2 高含砂輸砂公式 多選一 -- -- 整數 8 格 共 3 種選項
            sandTransportEquation = SandTransportEquationType.None; 
            waterJettingAlpha = 0;    //2.5.1 水力沖刷 實數 供使用者輸入α及β兩個常數。
            waterJettingBeta = 0;     //2.5.1 水力沖刷 實數 供使用者輸入α及β兩個常數。
       
            sedimentErosion = false;                            //2.5.2 泥砂磨蝕
            sedimentErosionElasticModulusValue = 0;       //2.5.2.1 彈性模數 二選一 pa 實數(>=0) a. 0：均一值，逐點給：-1
            sedimentErosionElasticModulusArray = null;    //2.5.2.1 彈性模數 二選一 pa 實數(>=0) a. 若為逐點給，則參數形式為矩陣(I,J)
            sedimentErosionTensileStrengthValue = 0;      //2.5.2.2 張力強度 二選一 pa 實數(>=0) a. 0：均一值，逐點給：-1
            sedimentErosionTensileStrengthArray = null;   //2.5.2.2 張力強度 二選一 pa 實數(>=0) a. 若為逐點給，則參數形式為矩陣(I,J)

            bedrockElevation = false;           //2.5.3 岩床高程
            bedrockElevationValue = 0;;    //2.5.3 岩床高程 二選一 m 實數 a. 0：均一值，逐點給：-1。
            bedrockElevationArray = null; //2.5.3 岩床高程 二選一 m 實數 a. 為逐點給，則參數形式為矩陣(I,J)

            //2.6 岸壁穩定分析 option
            //2.6.1 分析位置
            positionAnalysis = false;   //2.6.1 分析位置
            positionAnalysisType = PositionAnalysisType.None;   //2.6.1 分析位置二選一 -- a. 僅供介面用，不用輸入到input 檔。此選項為提供全部模擬與局部模擬兩個選項
            localBlockNumber = 0;                        //2.6.1.1 數值格網數目的表格供使用者填入欲分析位置數目IB，其中IB 的數目不可超過格網數目I。
            localBlockArray = null;                         //2.6.1.1 局部區塊數目矩陣(2, IB) -- a. 矩陣(2, IB)，1 代表左岸，2 代表右岸。僅為0、1 兩個數目字可供選擇，若1 為計算，若0 為不計算。

            //2.6.2 入滲效應
            infiltrationEffect = false;        //2.6.2 入滲效應 option
            infiltrationEffectTimeFormat = InfiltrationEffectTimeFormat.Minute;   //2.6.2.1 時間格式二選一 小時/分鐘，二選一。
            infiltrationEffectTimeSpacing = 0;                        //2.6.2.2 間距單一數值 實數(>0) 使用者自行輸入數值ex：1.5 小時or 90 分鐘
            rainfall = null;                                           //2.6.2.2.1 降雨量矩陣 mm 實數(>0) Free a. 矩陣大小需計算：首先將間距換為秒，然後“總模擬時間” (秒)除於間距(秒)，即為矩陣大小

            //2.6.3 岸壁幾何條件
            quayGeometry = false;      //2.6.3 岸壁幾何條件
            soilStratificationNumber = 0;        //2.6.3.1 岸壁土壤分層數目單一數值 整數(>0) option
            layerThicknessArray = null;       //2.6.3.1.1 分層厚度矩陣(LBK)m 實數(>0) 矩陣(LBK)為岸壁土壤分層數目
            quayHeightArray = null;           //2.6.3.2 岸壁高度矩陣(2, IB) m 實數
            dikeToWharfLengthArray = null;    //2.6.3.3 堤防到岸壁的長度矩陣(2, IB)m 實數(>0)

            //2.6.4 岸壁土壤性質
            quaySoilProperties = false;   //2.6.4 岸壁土壤性質
            cohesion = 0;                         //2.6.4.1 凝聚力 二選一 pa 實數(>0) a. 0：均一值，逐點給：-1
            cohesionArray = null;                //2.6.4.1 凝聚力 若為逐點給，則參數形式為矩陣(2,IB,LBK)

            reposeAngle = 0;                      //2.6.4.2 安息角 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
            reposeAngleArray = null;             //2.6.4.2 安息角 若為逐點給，則參數形式為矩陣(2,IB,LBK)

            frictionAngle = 0;                    //2.6.4.3 內摩擦角 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
            frictionAngleArray = null;           //2.6.4.3 內摩擦角 若為逐點給，則參數形式為矩陣(2,IB,LBK)

            flowRateRatio = 0;                    //2.6.4.3 比流率 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
            flowRateRatioArray = null;          //2.6.4.3 比流率 若為逐點給，則參數形式為矩陣(2,IB,LBK)

            porosityRatio = 0;                    //2.6.4.5 孔隙率二選一 -- 實數(>0) a. 0：均一值，逐點給：-1
            porosityRatioArray = null;           //若為逐點給，則參數形式為矩陣(2,IB,LBK)

            soilProportion = 0;                  //2.6.4.6 土壤比重二選一 -- 實數(>0) a. 0：均一值，逐點給：-1
            soilProportionArray = null;         //若為逐點給，則參數形式為矩陣(2,IB,LBK)

            ShearStrengthAngle = 0;              //2.6.4.7 岸壁未飽和基值吸力造成剪力強度增加所對應角度 二選一 deg 實數(>0) a. 0：均一值，逐點給：-1
            ShearStrengthAngleArray = null;      //2.6.4.7 岸壁未飽和基值吸力造成剪力強度增加所對應角度 若為逐點給，則參數形式為矩陣(2,IB,LBK)

            //3. 初始條件
            //3.1 水理模組 =========================================
            //depthAverageFlowSpeedU = -1;           //3.1.1 水深平均流速-U 二選一m/s 實數 實數 8 格a. 0：均一值，逐點給：-1
            //depthAverageFlowSpeedUArray = null;      //3.1.1 水深平均流速-U 二選一m/s 實數 實數 8 格a. 逐點給，參數 形式為矩陣(I,J)
            depthAverageFlowSpeedU = new TwoInOne();

            //depthAverageFlowSpeedV = -1;           //3.1.2 水深平均流速-V 二選一m/s 實數 實數 8 格a. 0：均一值，逐點給：-1
            //depthAverageFlowSpeedVArray = null;      //3.1.2 水深平均流速-V 二選一m/s 實數 實數 8 格a. 逐點給，參數 形式為矩陣(I,J)
            depthAverageFlowSpeedV = new TwoInOne();

            waterLevel = -1;             //3.1.3 水位 二選一 m 實數 實數 8 格a. 0：均一值，逐點給：-1
            waterLevelArray = null;      //3.1.4 水位 二選一 m 實數 實數 8 格a. 若為逐 點給，則參數形式為矩陣(I,J)

            verticalVelocitySlice = VerticalVelocitySliceType.None;         //3.1.4 垂向流速剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

            //3.2 動床模組
            depthAverageConcentration = null;              //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入
            depthAverageConcentrationList = null;           //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入。

            verticalConcentrationSlice = VerticalConcentrationSliceType.None;         //3.2.2 垂向濃度剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

            //4. 邊界條件
            //4.1 水理模組
            //4.1.1 上游
            upFlowCondition = FlowConditionType.None;         //4.1.1.1 流況設定 二選一
            
            //4.1.1.1.1 超臨界流
            superBoundaryConditionNumber = 0;                   //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入
            superFlowQuantity = new TwoInOne();                 //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 
            superWaterLevel = new TwoInOne();                   //4.1.1.1.1.2 水位 m 實數
        
            //4.1.1.1.2 亞臨界流
            subBoundaryConditionNumber = 0;                 //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入
            subFlowQuantity = new TwoInOne();               //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 

            verticalVelocityDistribution = false;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)
            verticalVelocityDistributionArray = null;     //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0) 
        
            //4.1.2 下游 二選一
            downFlowCondition = FlowConditionType.None;         //4.1.2 下游 二選一
            downSuperWaterLevel = new TwoInOne();                   //4.1.2.2.1 水位 實數 同 4.1.1.1.1.2，T 與前同(4.1.1.1.1.0 或4.1.1.1.2.0)
        
            //4.1.3 側壁
            sidewallBoundarySlip = false;               //4.1.3.1 側壁邊界滑移 -- 0 整數(>0) 整數 8 格 0：非滑移、1：滑移，check box

            //4.1.4 水面 三維 only。(”即時互動處”不放圖示)
            mainstreamWindShear = 0;              //4.1.4.1 主流方向風剪 單一數值 N/m2 0 實數 實數 8 格
            sideWindShear = 0;                    //4.1.4.2 側方向風剪 單一數值 N/m2 0 實數 實數 8 格
            coriolisForce = 0;                    //4.1.4.3 科氏力 單一數值 N/m2 0 實數 實數 8 格

            //4.1.5 底床 實數 三維 only。(”即時互動處”不放圖示)
            boundaryLayerThickness = 3;              //4.1.5.1 邊界層厚度 三選一 3 整數(>0) 整數 8 格 1、2、3，三維 only，下拉選單。
            seabedBoundarySlip = SeabedBoundarySlipType.NonSlip;   //4.1.5.2 底床邊界滑移 三選一 -- 0 整數(>0) 整數 8 格 a. 三維 only，下拉選單 b. 0：非滑移、1：滑移、2：壁函數
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
        //public int sedimentParticlesNum = 3;            //2.2.4 泥砂顆粒數目
        //public int seabedLevelNum = 6;
        //public double[] seabedLevelArray = null;
        //public double[,] sedimentCompositionRatioArray = null;


        public bool GenerateInputFile(string file)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("2011m4.i       m4.dat         \n");

            sb.AppendFormat("{0,8}", inputGrid.GetI.ToString());
            sb.AppendFormat("{0,8}", inputGrid.GetJ.ToString());
            sb.AppendFormat("{0,8}", sedimentParticlesNumber.ToString());
            sb.AppendFormat("{0,8}", 10.ToString());    //模式預設值
            sb.AppendFormat("{0,8}", 5.ToString());     //模式預設值
            sb.AppendFormat("{0,8}", 0.ToString());     //模式預設值
            sb.AppendFormat("{0,8}", verticalLevelNumber.ToString());     //垂向格網分層數目0.1.1
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
