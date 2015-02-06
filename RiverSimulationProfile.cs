using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using PictureBoxCtrl;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace RiverSimulationApplication
{
    [Serializable]
    public class RiverSimulationProfile
    {
        //public static RiverSimulationProfile profile = new RiverSimulationProfile();
        public static RiverSimulationProfile profile = null;
 
        #region Constructor
        public RiverSimulationProfile()
        {
            Initialization();
        }
        #endregion
        public static void SerializeBinary(RiverSimulationProfile p, string file)
        {
            FileStream oFileStream = new FileStream(file, FileMode.Create);
            BinaryFormatter myBinaryFormatter = new BinaryFormatter();
            myBinaryFormatter.Serialize(oFileStream, p);
            oFileStream.Flush();
            oFileStream.Close();
            oFileStream.Dispose();
        }

        public static RiverSimulationProfile DeSerialize(string file)
        {
            RiverSimulationProfile o = null;
            FileStream oFileStream = new FileStream(file, FileMode.Open);
            BinaryFormatter myBinaryFormatter = new BinaryFormatter();
            o = (RiverSimulationProfile)myBinaryFormatter.Deserialize(oFileStream);
            oFileStream.Close();
            oFileStream.Dispose();
            return o;
        }

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

        [Serializable]
        public class TwoInOne                //二選一資料類別
        {
            public TwoInOne(ValueType vt, ArrayType at, Type tp = Type.None)
            {
                type = tp;
                //valueType = ValueType.Double;
                //arrayType = ArrayType.TwoDim;
                valueType = vt;
                arrayType = at;
                dataValue = null;
                dataArray = null;
            }

            public TwoInOne(TwoInOne o)
            {
                type = o.type;
                valueType = o.valueType;
                arrayType = o.arrayType;
                check = o.check;
                if (o.dataArray == null)
                {
                    dataArray = null;
                }
                else
                {
                    if (arrayType == ArrayType.TwoDim)
                    {
                        dataArray = (double[,])(o.dataArray as double[,]).Clone();
                    }
                    else if (arrayType == ArrayType.ThreeDim)
                    {
                        dataArray = (double[, ,])(o.dataArray as double[, ,]).Clone();
                    }
                }

                if (o.dataValue == null)
                {
                    dataValue = null;
                }
                else
                {
                    if (valueType == ValueType.Double)
                    {
                        dataValue = (double[])(o.dataValue as double[]).Clone();
                    }
                    else if (valueType == ValueType.TwoDim)
                    {
                        dataValue = (double[,])(o.dataValue as double[,]).Clone();
                    }
                    else if (valueType == ValueType.ThreeDim)
                    {
                        dataValue = (double[, ,])(o.dataValue as double[, ,]).Clone();
                    }
                }
            }

            public void CreateDouble2D(int i, int j)
            {
                Debug.Assert(valueType == ValueType.Double);
                Debug.Assert(arrayType == ArrayType.TwoDim);
                dataValue = new double[1];
                if (i > 0 && j > 0)
                {
                    dataArray = new double[i, j];
                }
            }

            public void Create2D(int i, int j)
            {
                Debug.Assert(valueType == ValueType.TwoDim);
                Debug.Assert(arrayType == ArrayType.TwoDim);
                dataValue = new double[i, j];
                dataArray = new double[i, j];
            }

            public void Create3D(int i, int j, int k)
            {
                Debug.Assert(valueType == ValueType.ThreeDim);
                Debug.Assert(arrayType == ArrayType.ThreeDim);
                dataValue = new double[i, j, k];
                dataArray = new double[i, j, k];
            }         

            public void Clear()
            {
                type = Type.None;
                dataValue = null;
                dataArray = null;
                check = false;
            }

            public double[,] Array2D()
            {
                Debug.Assert(arrayType == ArrayType.TwoDim);
                return dataArray as double[,];
            }

            public double[,,] Array3D()
            {
                Debug.Assert(arrayType == ArrayType.ThreeDim);
                return dataArray as double[, ,];
            }

            public void SetArrayObject(object o)
            {
                dataArray = o;
            }

            public double[] ValueDouble()
            {
                Debug.Assert(valueType == ValueType.Double);
                return dataValue as double[];
            }

            public double[,] Value2D()
            {
                Debug.Assert(valueType == ValueType.TwoDim);
                return dataValue as double[,];
            }

            public double[,,] Value3D()
            {
                Debug.Assert(valueType == ValueType.ThreeDim);
                return dataValue as double[, ,];
            }

            public bool ValueNull()
            {
                return dataValue == null; ;
            }

            public bool ArrayNull()
            {
                return dataArray == null; ;
            }

            public enum ArrayType
            {
                TwoDim,
                ThreeDim,
            };

            public enum ValueType
            {
                Double,
                TwoDim,
                ThreeDim,
            };

            public enum Type
            {
                None,
                UseValue,
                UseArray,
            };

            public Type type;
            public ArrayType arrayType = ArrayType.TwoDim;
            public ValueType valueType = ValueType.Double;
            public bool check = false;
            private object dataValue;
            private object dataArray;
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
        public TwoInOne manningN;                               //1.2.1.1 Manning n 二選一 -- 均一值 矩陣[I,J]
        public TwoInOne chezy;                                  //1.2.1.2 Chezy n 二選一 -- 均一值 矩陣[I,J]
        public TwoInOne roughnessHeightKs;                      //1.2.1.3 粗糙高度 n 二選一 -- 均一值 矩陣[I,J]

        //public double manningN { get; set; }                    //1.2.1.1 Manning n 二選一 -- 均一值
        //public double[,] manningNArray { get; set; }             //1.2.1.1 Manning n 二選一 -- 
        //public double chezy { get; set; }                       //1.2.1.2 Chezy 二選一 -- 均一值
        //public double[,] chezyArray { get; set; }               //1.2.1.2 Chezy 二選一 -- 矩陣[I,J]
        //public double roughnessHeightKs { get; set; }           //1.2.1.3 粗糙高度 ks mm -- 均一值
        //public double[,] roughnessHeightKsArray { get; set; }   //1.2.1.3 粗糙高度 ks mm -- 矩陣[I,J]

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
        public double tvInMainstreamDirection;     //需確認
        public double tvInSideDirection;           //需確認

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

        public bool noErosionElevationUse;                     //2.3.3 不可沖刷高程 二選一 m 實數 a. option 用 check box
        public TwoInOne noErosionElevation;

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
        public TwoInOne depthAverageFlowSpeedV;           //3.1.2 水深平均流速-V 二選一m/s 實數 實數 8 格a. 0：均一值，逐點給：-1
        public TwoInOne waterLevel;      //3.1.3 水位 二選一 m 實數 實數 8 格a. 若為逐 點給，則參數形式為矩陣(I,J)

        public enum VerticalVelocitySliceType
        {
            None,
            Open,
            Close,
        }
        public VerticalVelocitySliceType verticalVelocitySlice;         //3.1.4 垂向流速剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

        //3.2 動床模組 =========================================
        //public List<double> depthAverageConcentration;              //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入
        //public List<double[,]> depthAverageConcentrationList;       //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入。
        public TwoInOne depthAverageConcentration;      //3.2.1 水深平均濃 二選一  總共有K 個粒徑種類，每種粒徑都要輸入。若為均一值，則每種粒徑分別輸入單一值；若為逐點給，則每種粒徑都要將矩陣(I,J)輸入完整，即有K 個矩陣(I,J)要輸入。

        public enum VerticalConcentrationSliceType
        {
            None,
            Open,
            Close,
        }
        public VerticalConcentrationSliceType verticalConcentrationSlice;         //3.2.2 垂向濃度剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

        //4. 邊界條件
        //4.1 水理模組
        public int boundaryTimeNumber;                     //4.1.0 邊界時間數目
        public double[] boundaryTime;                      //4.1.0 邊界時間

        //4.1.1 上游
        public enum FlowConditionType
        {
            None,
            SuperCriticalFlow,
            SubCriticalFlow,
        }
        public FlowConditionType upFlowCondition;         //4.1.1.1 流況設定 二選一
        //public int boundaryConditionNumber;               //4.1.1.1.1.0 邊界條件數目 T 整數(>1) 定量流不輸入
        //4.1.1.1.1 超臨界流
        //public int superBoundaryConditionNumber;        //4.1.1.1.1.0 邊界條件數目 T 整數(>1) 定量流不輸入
        public TwoInOne superMainFlowQuantity;              //4.1.1.1.1.1 流量 m3/s 實數(>=0) a. 圖 5，“即時互動處”呈現流量歷線圖，根
        public TwoInOne superSideFlowQuantity;              //4.1.1.1.1.1 流量 m3/s 實數(>=0) a. 圖 5，“即時互動處”呈現流量歷線圖，根
        public TwoInOne superWaterLevel;                //4.1.1.1.1.2 水位 m 實數
        
        //4.1.1.1.2 亞臨界流
        //public int subBoundaryConditionNumber;        //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入
        public TwoInOne subMainFlowQuantity;              //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 
        public TwoInOne subSideFlowQuantity;              //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 

        public bool verticalVelocityDistribution;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)
        public int verticalVelocityDistributionNumber;  ////4.1.1.2 垂向流速分布(3D) 分層數目P 整數(>=3) 
        public double[,] verticalVelocityDistributionArray;     //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0) 
        
        //4.1.2 下游 二選一
        public FlowConditionType downFlowCondition;         //4.1.2 下游 二選一
        public TwoInOne downSubWaterLevel;                //4.1.2.2.1 水位 實數 同 4.1.1.1.1.2，T 與前同(4.1.1.1.1.0 或4.1.1.1.2.0)
        
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

        //4.2 動床模組
        //4.2.1 上游
        //4.2.1.1 入流泥砂設定
        public enum BottomBedLoadFluxType
        {
            Auto,
            Input,
        }
        public BottomBedLoadFluxType bottomBedLoadFluxType; //4.2.1.1.1 底床載通量 實數(>=0)“模式自動計算”
        public TwoInOne bottomBedLoadFluxArray;            //4.2.1.1.1 底床載通量 實數(>=0)“自行輸入”，如果為“自行輸入”，則如圖4.2.1.1.1 所示
        public TwoInOne suspendedLoadDepthAvgConcentration;            //4.2.1.1.2 懸浮載水深平均濃度實數(>=0) 如圖 4.2.1.1.1 所示

        public int boundaryUpVerticalDistributionNum;  //4.2.1.1.3 垂向濃度分布(3D) 矩陣(2,PP) 均一值 實數(>=0) a. 可為均一值或自行輸入。
        public TwoInOne boundaryUpVerticalDistribution;     //4.2.1.1.3 垂向濃度分布(3D) 矩陣(2,PP) 均一值 實數(>=0) a. 可為均一值或自行輸入。

        //4.2.1.2 上游邊界底床
        public BottomBedLoadFluxType upBoundaryElevationType;   //4.2.1.2.1 可採用初始上游邊界底床高程或自行輸入
        public double[,] upBoundaryElevationArray;              //4.2.1.2.1 高程矩陣(T,J) m 初始實數 可採用初始上游邊界底床高程或自行輸入，
        public double[,] bottomBedParticleSizeRatio;            //4.2.1.2.2 底床粒徑比例實數(>=0) 如圖 4.2.1.1.3 所示

        //4.2.2 下游 圖5，“即時互動處”不放圖示
        public BottomBedLoadFluxType movableBedDownType;        //4.2.2.1 通量實數(>=0) 如圖 2.2.2.1 所示
        public double[,] movableBedDownConcentration;           //4.2.2.2 濃度 實數(>=0) 如圖 2.2.2.1 所示

        public int boundaryDownVerticalDistributionNum;  //4.2.2.3 垂向濃度分布(3D) 矩陣(2,PP) 均一值 實數(>=0) a. 可為均一值或自行輸入。
        public TwoInOne boundaryDownVerticalDistribution;     //4.2.2.3 垂向濃度分布(3D) 矩陣(2,PP) 均一值 實數(>=0) a. 可為均一值或自行輸入。

        public enum NearBedBoundaryType
        {
            None,
            ConcentrationCalculation,
            Input,
        }
        public NearBedBoundaryType nearBedBoundaryType;         //4.2.3 近底床濃度邊界二選一 實數 a. 三維only

        public enum ConcentrationCalculationType
        {
            None,
            Type1,
            Type2,
            Type3,
        }
        public ConcentrationCalculationType concentrationCalculation;   //4.2.3.1 濃度計算公式多選一 整數 8 格下拉式選單(總共2~3 種選項)
        public TwoInOne inputConcentration;                             //4.2.3.2 通量/給定濃度二選一 a. 先令使用者選擇是通量或者是給定濃
        
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
            manningN = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);                    //1.2.1.1 Manning n 二選一 -- 均一值
            chezy = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);                       //1.2.1.2 Chezy 二選一 -- 均一值
            roughnessHeightKs = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);           //1.2.1.3 粗糙高度 ks mm -- 均一值

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
            noErosionElevationUse = false;             //2.3.3 不可沖刷高程 二選一 m 實數 a. option 用 check box
            noErosionElevation = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);            //b. 0：均一值，逐點給：-1

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
            depthAverageFlowSpeedU = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);
            depthAverageFlowSpeedV = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);
            waterLevel = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim); ;      //3.1.4 水位 二選一 m 實數 實數 8 格a. 若為逐 點給，則參數形式為矩陣(I,J)
            verticalVelocitySlice = VerticalVelocitySliceType.None;         //3.1.4 垂向流速剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

            //3.2 動床模組
            depthAverageConcentration = new TwoInOne(TwoInOne.ValueType.ThreeDim, TwoInOne.ArrayType.ThreeDim);      //3.2.1 水深平均濃度二選一 ppm -- 實數(>=0) 實數8 格a. 總共有K 個粒徑種類，每種粒徑都要輸入。
            verticalConcentrationSlice = VerticalConcentrationSliceType.None;         //3.2.2 垂向濃度剖面二選一 -- -- 整數8 格a. 三維only b. 0：關；1：開

            //4. 邊界條件
            //4.1 水理模組
            boundaryTimeNumber = 0;         //4.1.0 邊界時間數目
            boundaryTime = null;            //4.1.0 邊界時間    

            //4.1.1 上游
            upFlowCondition = FlowConditionType.None;         //4.1.1.1 流況設定 二選一
            
            //4.1.1.1.1 超臨界流
            //boundaryConditionNumber = 0;                   //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入
            superMainFlowQuantity = new TwoInOne(TwoInOne.ValueType.TwoDim, TwoInOne.ArrayType.TwoDim);                 //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 
            superSideFlowQuantity = new TwoInOne(TwoInOne.ValueType.TwoDim, TwoInOne.ArrayType.TwoDim);                 //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 
            superWaterLevel = new TwoInOne(TwoInOne.ValueType.TwoDim, TwoInOne.ArrayType.TwoDim);                   //4.1.1.1.1.2 水位 m 實數
        
            //4.1.1.1.2 亞臨界流
            //subBoundaryConditionNumber = 0;                 //4.1.1.1.2.0 邊界條件數目 T 整數(>1) 定量流不輸入
            subMainFlowQuantity = new TwoInOne(TwoInOne.ValueType.TwoDim, TwoInOne.ArrayType.TwoDim);               //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 
            subSideFlowQuantity = new TwoInOne(TwoInOne.ValueType.TwoDim, TwoInOne.ArrayType.TwoDim);               //4.1.1.1.2.1 流量 實數(>=0) 同 4.1.1.1.1.1 

            verticalVelocityDistribution = false;       //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0)
            verticalVelocityDistributionArray = null;     //4.1.1.2 垂向流速分布(3D) 矩陣(2,P) 實數(>=0) 
        
            //4.1.2 下游 二選一
            downFlowCondition = FlowConditionType.None;         //4.1.2 下游 二選一
            downSubWaterLevel = new TwoInOne(TwoInOne.ValueType.TwoDim, TwoInOne.ArrayType.TwoDim);                   //4.1.2.2.1 水位 實數 同 4.1.1.1.1.2，T 與前同(4.1.1.1.1.0 或4.1.1.1.2.0)
        
            //4.1.3 側壁
            sidewallBoundarySlip = false;               //4.1.3.1 側壁邊界滑移 -- 0 整數(>0) 整數 8 格 0：非滑移、1：滑移，check box

            //4.1.4 水面 三維 only。(”即時互動處”不放圖示)
            mainstreamWindShear = 0;              //4.1.4.1 主流方向風剪 單一數值 N/m2 0 實數 實數 8 格
            sideWindShear = 0;                    //4.1.4.2 側方向風剪 單一數值 N/m2 0 實數 實數 8 格
            coriolisForce = 0;                    //4.1.4.3 科氏力 單一數值 N/m2 0 實數 實數 8 格

            //4.1.5 底床 實數 三維 only。(”即時互動處”不放圖示)
            boundaryLayerThickness = 3;              //4.1.5.1 邊界層厚度 三選一 3 整數(>0) 整數 8 格 1、2、3，三維 only，下拉選單。
            seabedBoundarySlip = SeabedBoundarySlipType.NonSlip;   //4.1.5.2 底床邊界滑移 三選一 -- 0 整數(>0) 整數 8 格 a. 三維 only，下拉選單 b. 0：非滑移、1：滑移、2：壁函數

            //4.2 動床模組
            //4.2.1 上游
            //4.2.1.1 入流泥砂設定
            bottomBedLoadFluxType = BottomBedLoadFluxType.Auto; //4.2.1.1.1 底床載通量 實數(>=0)“模式自動計算”
            bottomBedLoadFluxArray = new TwoInOne(TwoInOne.ValueType.ThreeDim, TwoInOne.ArrayType.ThreeDim); ;            //4.2.1.1.1 底床載通量 實數(>=0)“自行輸入”，如果為“自行輸入”，則如圖4.2.1.1.1 所示
            suspendedLoadDepthAvgConcentration = new TwoInOne(TwoInOne.ValueType.ThreeDim, TwoInOne.ArrayType.ThreeDim);  //4.2.1.1.2 懸浮載水深平均濃度實數(>=0) 如圖 4.2.1.1.1 所示
            boundaryUpVerticalDistributionNum = 0;
            boundaryUpVerticalDistribution = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);    //4.2.1.1.3 垂向濃度分布(3D) 矩陣(2,PP) 均一值 實數(>=0) a. 可為均一值或自行輸入。

            //4.2.1.2 上游邊界底床
            upBoundaryElevationType = BottomBedLoadFluxType.Auto;   //4.2.1.2.1 可採用初始上游邊界底床高程或自行輸入
            upBoundaryElevationArray = null;              //4.2.1.2.1 高程矩陣(T,J) m 初始實數 可採用初始上游邊界底床高程或自行輸入，
            bottomBedParticleSizeRatio = null;            //4.2.1.2.2 底床粒徑比例實數(>=0) 如圖 4.2.1.1.3 所示

            //4.2.2 下游 圖5，“即時互動處”不放圖示
            movableBedDownType = BottomBedLoadFluxType.Auto;        //4.2.2.1 通量實數(>=0) 如圖 2.2.2.1 所示
            movableBedDownConcentration = null;           //4.2.2.2 濃度 實數(>=0) 如圖 2.2.2.1 所示

            boundaryDownVerticalDistributionNum = 0;
            boundaryDownVerticalDistribution = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);    //4.2.1.1.3 垂向濃度分布(3D) 矩陣(2,PP) 均一值 實數(>=0) a. 可為均一值或自行輸入。
            nearBedBoundaryType = NearBedBoundaryType.None;         //4.2.3 近底床濃度邊界二選一 實數 a. 三維only
            concentrationCalculation = ConcentrationCalculationType.None;   //4.2.3.1 濃度計算公式多選一 整數 8 格下拉式選單(總共2~3 種選項)
            inputConcentration = new TwoInOne(TwoInOne.ValueType.Double, TwoInOne.ArrayType.TwoDim);    //4.2.3.2 通量/給定濃度二選一 a. 先令使用者選擇是通量或者是給定濃
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
            return bkImgType; 
        }

        public void ClearBackgroundMapType()
        {
            bkImgType = BackgroundMapType.None;
        }

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

        //const int LineMaxCount = 10;
        public bool GenerateInputFile(string file)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("2011m4.i       m4.dat         \n");

            sb.AppendFormat("{0,8}", inputGrid.GetI.ToString());
            sb.AppendFormat("{0,8}", inputGrid.GetJ.ToString());
            sb.AppendFormat("{0,8}", sedimentParticlesNumber.ToString());
            sb.AppendFormat("{0,8}", 10.ToString());    //模式預設值
            sb.AppendFormat("{0,8}", 50.ToString());     //模式預設值
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

            //註3-1(水理2D 輸出控制開關，1：開、0：關。)：
            sb.AppendFormat("{0,8}", (outputControlInitialBottomElevation? 1 : 0).ToString());    //1.1.3 輸出控制 水深平均流速
            sb.AppendFormat("{0,8}", (outputControlAverageDepthFlowRate ? 1 : 0).ToString());    //1.1.3 輸出控制 初始底床高程
            sb.AppendFormat("{0,8}", (outputControlBottomShearingStress ? 1 : 0).ToString());    //1.1.3 輸出控制 底床剪應力
            sb.AppendFormat("{0,8}", (outputControlLevel ? 1 : 0).ToString());    //1.1.3 輸出控制 水位
            sb.AppendFormat("{0,8}", (outputControlDepth ? 1 : 0).ToString());    //1.1.3 輸出控制 水深
            sb.AppendFormat("{0,8}", (outputControlFlow ? 1 : 0).ToString());    //1.1.3 輸出控制 流量
            sb.AppendFormat("{0,8}", (outputControlVelocityInformation3D ? 1 : 0).ToString());    //1.1.3 輸出控制 三維流速資訊
            sb.Append("\n");

            //註3-2(動床2D 輸出控制開關，1：開、0：關。)：
            sb.AppendFormat("{0,8}", (outputControlBottomElevation ? 1 : 0).ToString());    //2.1.3 輸出控制 初始底床高程
            sb.AppendFormat("{0,8}", (outputControlAverageDepthDensity ? 1 : 0).ToString());    //2.1.3 輸出控制 水深平均流速
            sb.AppendFormat("{0,8}", (outputControlErosionDepth ? 1 : 0).ToString());    //2.1.3 輸出控制 沖淤深度
            sb.Append("\n");

            //註4：
            sb.AppendFormat("{0,8}", (secondFlowEffectFunction ? 1 : 0).ToString());     //是否計算二次流效應
            sb.AppendFormat("{0,8}", (closeDiffusionEffectFunction ? 0 : 1).ToString());     //是否關閉移流擴散效應
            sb.AppendFormat("{0,8}", (1).ToString());     //是否計算傳輸(propogation)效應 不讓使用者更改。
            sb.AppendFormat("{0,8}", (sidewallBoundarySlip ? 1 : 0).ToString());     //1:滑移；0:非滑移。4.1.3.1
            sb.AppendFormat("{0,8}", (modelingType == ModelingType.MovableBed ? 1 : 0).ToString());     //對照“模擬功能”-“模組選擇”。若執行動床計算需產生SED.dat檔案。
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.AppendFormat("{0,8}", (quayStableAnalysisFunction ? 1 : 0).ToString());     //是否計算岸壁崩塌。1:是；0:否。參考介面“模擬功能”-“特殊功能”的“岸壁穩定分析”。
            sb.AppendFormat("{0,8}", maxIterationsNum.ToString());     //水理最大疊代次數。1.1.2.3
            sb.Append("\n");

            //註5：
            sb.AppendFormat("{0,8}", (9999999).ToString());     //模式預設值
            sb.AppendFormat("{0,8}", (9999999).ToString());     //模式預設值
            sb.AppendFormat("{0,8}", waterOutputFrequency.ToString());     //2.1.2 動床輸出頻率
            sb.AppendFormat("{0,8}", outputFrequency.ToString());     //1.1.1.3 水理輸出頻率
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.AppendFormat("{0,8}", (1).ToString());     //是否計算泥砂懸浮載(suspending load)。1:是；0:否。預設值：1。不供使用者更改
            sb.AppendFormat("{0,8}", (1).ToString());     //是否計算泥砂底床載(bedload)。1:是；0:否。預設值：1。不供使用者更改
            sb.AppendFormat("{0,8}", (0).ToString());     //模式預設值
            sb.Append("\n");

            //註6：
            sb.AppendFormat("{0,8}", (1).ToString());     //水理計算之權重(建議採預設值)
            sb.AppendFormat("{0,8}", minWaterDeoth.ToString());     //1.1.4 最小水深
            sb.AppendFormat("{0,8}", minWaterDeoth.ToString());     //1.1.4 最小水深
            sb.AppendFormat("{0,8}", minWaterDeoth.ToString());     //1.1.4 最小水深
            sb.AppendFormat("{0,8}", (0.02).ToString());     //模式預設值
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.AppendFormat("{0,8}", (0).ToString());     //模式內部設定值
            sb.Append("\n");

            //註7：
            sb.AppendFormat(" {0,15}", "0.0");                               //初始計算時間(sec)。格式為實數16 格。值為0.0。
            sb.AppendFormat(" {0,15}", waterTimeSpan.ToString());            //2.1.1 時間間距
            sb.AppendFormat(" {0,15}", timeSpan2d.ToString());               //1.1.1.2 二維時間間距
            sb.AppendFormat(" {0,15}", totalSimulationTime.ToString());      //1.1.1.1 總模擬時間
            sb.AppendFormat(" {0,15}", waterModelingConvergenceCriteria2d.ToString());     //1.1.2.1 二維水理收斂標準
            sb.Append("\n");

            //註8：各點之X 座標值。由第一個斷面依序排列，側方向一行最多10 個值
            int count = 0;
            for (int i = 0; i < inputGrid.GetI; ++i)
            {
                for (int j = 0; j < inputGrid.GetJ; ++j)
                {
                    if (count == 10)
                    {
                        sb.Append("\n");
                        count = 0;
                    }
                    sb.AppendFormat(" {0,15}", inputGrid.inputCoor[i, j].x.ToString());     //各點之X 座標值。由第一個斷面依序排列，側方向一行最多10 個值
                    ++count;
                }
                sb.Append("\n");
                count = 0;
            }

            //註9：各點之Y 座標值。由第一個斷面依序排列，側方向一行最多10 個值，若斷面超過10 個點則需跳行
            count = 0;
            for (int i = 0; i < inputGrid.GetI; ++i)
            {
                for (int j = 0; j < inputGrid.GetJ; ++j)
                {
                    if (count == 10)
                    {
                        sb.Append("\n");
                        count = 0;
                    }
                    sb.AppendFormat(" {0,15}", inputGrid.inputCoor[i, j].y.ToString("#######.#######"));
                    ++count;
                }
                sb.Append("\n");
                count = 0;
            }

            //註10：各點之曲率半徑值RS。1.3.1
            count = 0;
            for (int i = 0; i < inputGrid.GetI; ++i)
            {
                for (int j = 0; j < inputGrid.GetJ; ++j)
                {
                    if (count == 10)
                    {
                        sb.Append("\n");
                        count = 0;
                    } 
                    if (curvatureRadius == null)
                    {
                        sb.AppendFormat("{0,8}", (0).ToString());
                    }
                    else
                    {
                        sb.AppendFormat("{0,8}", curvatureRadius[j, i].ToString());
                    }
                    ++count;
                }
                sb.Append("\n");
                count = 0;
            }

            //註11：
            DumpTwoInOne(waterLevel, ref sb);

            //註12：初始底床高程。對照介面“計算格網”-“計算網格來源”-“由檔案匯入水平格網”及“線上輸入水平格
            sb.AppendFormat("{0,8}\n", (-1).ToString());
            count = 0;
            for (int i = 0; i < inputGrid.GetI; ++i)
            {
                for (int j = 0; j < inputGrid.GetJ; ++j)
                {
                    if (count == 10)
                    {
                        sb.Append("\n");
                        count = 0;
                    }
                    sb.AppendFormat(" {0,7}", inputGrid.inputCoor[i, j].z.ToString());
                    ++count;
                }
                sb.Append("\n");
                count = 0;
            }

            //註13：
            DumpTwoInOne(depthAverageFlowSpeedU, ref sb);       //3.1.1 水深平均流速-U
            DumpTwoInOne(depthAverageFlowSpeedV, ref sb);       //3.1.2 水深平均流速-V

            //註14：
            if (roughnessType== RoughnessType.ManningN)
            {
                DumpTwoInOne(manningN, ref sb, DumpTwoInOneType.OnlyType, true);       //1.2.1.1 Manning n 二選一
            }
            else if (roughnessType == RoughnessType.Chezy)
            {
                DumpTwoInOne(manningN, ref sb, DumpTwoInOneType.OnlyType, true);       //1.2.1.2 Chezy 二選一 
            }
            else
            {
                DumpTwoInOne(null, ref sb, DumpTwoInOneType.OnlyType, true);       
            }
            sb.AppendFormat("{0,8}\n", ((int)roughnessType).ToString());      //糙度係數之類型。輸入1 為Manning 糙度係數(n)；輸入2 為Chezy 糙度係數(C)。1.2.1
            if (roughnessType== RoughnessType.ManningN)
            {
                DumpTwoInOne(manningN, ref sb, DumpTwoInOneType.OnlyValueOrArray);       //1.2.1.1 Manning n 二選一
            }
            else if (roughnessType == RoughnessType.Chezy)
            {
                DumpTwoInOne(manningN, ref sb, DumpTwoInOneType.OnlyValueOrArray);       //1.2.1.2 Chezy 二選一 
            }
            else
            {
                DumpTwoInOne(null, ref sb, DumpTwoInOneType.OnlyValueOrArray);       
            }

            //註15：上游邊界條件設定
            for (int j = 0; j < inputGrid.GetJ; ++j)
            {
                sb.AppendFormat("{0,8}", (1).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.AppendFormat("{0,8}", (upFlowCondition == FlowConditionType.SubCriticalFlow ? 1 : 5).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.Append("\n");
            }

            //註16：下游邊界條件設定
            for (int j = 0; j < inputGrid.GetJ; ++j)
            {
                if (j == inputGrid.GetJ - 1)
                {
                    sb.AppendFormat("{0,8}", (inputGrid.GetI * -1).ToString());
                }
                else
                {
                    sb.AppendFormat("{0,8}", (inputGrid.GetI).ToString());
                }
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.AppendFormat("{0,8}", (downFlowCondition == FlowConditionType.SubCriticalFlow ? 3 : 6).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.AppendFormat("{0,8}", (j + 1).ToString());
                sb.Append("\n");
            }

            //註17~20：邊界條件設定值
            count = 0;
            for (int t = 0; t < boundaryTime.Length; ++t)
            {
                GenerateBoundaryConditions(t, ref sb, count);
            }
            //註21：邊界條件設定值
            GenerateBoundaryConditions(boundaryTime.Length - 1, ref sb, count, true);
            sb.Append("\n");

            using (StreamWriter outfile = new StreamWriter(file))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }
            return true;
        }

        private void GenerateBoundaryConditions(int t, ref StringBuilder sb, int count, bool external = false)
        {
            if (count == 8)
            {
                sb.AppendFormat("\n");
            }

            //模擬時間(sec)。實數16 格
            if (external)
            {
                sb.AppendFormat("{0,16}", (boundaryTime[t] + 1).ToString());    
            }
            else
            {
                sb.AppendFormat("{0,16}", (boundaryTime[t]).ToString());        
            }
            count = 0;

            for (int jw = 0; jw < inputGrid.GetJ; ++jw)
            {   //主流方向流量 4.1.1.1.1.1
                if (count == 8)
                {
                    sb.AppendFormat("\n{0,16}", " ");
                    count = 0;
                }
                double flowQ = CalcFlowQ(t, jw, (upFlowCondition == FlowConditionType.SubCriticalFlow) ? subMainFlowQuantity : superMainFlowQuantity);
                sb.AppendFormat("{0,8}", flowQ);
                ++count;
            }
            count = 8;

            for (int jw = 0; jw < inputGrid.GetJ; ++jw)
            {   //側流方向流量
                if (count == 8)
                {
                    sb.AppendFormat("\n{0,16}", " ");
                    count = 0;
                }
                double flowQ = CalcFlowQ(t, jw, (upFlowCondition == FlowConditionType.SubCriticalFlow) ? subSideFlowQuantity : superSideFlowQuantity);
                sb.AppendFormat("{0,8}", flowQ);
                ++count;
            }
            count = 8;

            if (upFlowCondition == FlowConditionType.SuperCriticalFlow)
            {
                for (int jw = 0; jw < inputGrid.GetJ; ++jw)
                {   //上游水位
                    if (count == 8)
                    {
                        sb.AppendFormat("\n{0,16}", " ");
                        count = 0;
                    }
                    double level = CalcWaterLevel(t, jw, superWaterLevel);
                    sb.AppendFormat("{0,8}", level);
                    ++count;
                }
                count = 8;
            }

            if (downFlowCondition == FlowConditionType.SubCriticalFlow)
            {
                for (int jw = 0; jw < inputGrid.GetJ; ++jw)
                {   //上游水位
                    if (count == 8)
                    {
                        sb.AppendFormat("\n{0,16}", " ");
                        count = 0;
                    }
                    double level = CalcWaterLevel(t, jw, downSubWaterLevel);
                    sb.AppendFormat("{0,8}", level);
                    ++count;
                }
                count = 8;
            }

            //註18：上游底床高程
            if (upBoundaryElevationType == BottomBedLoadFluxType.Input && this.IsMovableBedMode())
            {
                for (int jw = 0; jw < inputGrid.GetJ; ++jw)
                {   //上游水位
                    if (count == 8)
                    {
                        sb.AppendFormat("\n{0,16}", " ");
                        count = 0;
                    }
                    double level = upBoundaryElevationArray[jw, t];
                    sb.AppendFormat("{0,8}", level);
                    ++count;
                }
                count = 8;
            }

            //註19：邊界條件設定值
            if (count == 8)
            {
                sb.AppendFormat("\n");
                count = 0;
            }
            if (IsMovableBedMode())
            {
                sb.AppendFormat("{0,16}", 1);
                for (int k = 0; k < sedimentParticlesNumber; ++k)
                {
                    sb.AppendFormat("{0,8}", bottomBedParticleSizeRatio[k, t]);
                }
                sb.AppendFormat("\n");

                for (int j = 0; j < inputGrid.GetJ; ++j)
                {
                    sb.AppendFormat("{0,16}", j + 1);
                    for (int k = 0; k < sedimentParticlesNumber; ++k)
                    {
                        if (suspendedLoadDepthAvgConcentration.type == TwoInOne.Type.UseArray)
                        {
                            sb.AppendFormat("{0,8}", suspendedLoadDepthAvgConcentration.Array3D()[k, j, t]);
                        }
                        else
                        {
                            sb.AppendFormat("{0,8}", suspendedLoadDepthAvgConcentration.Value3D()[k, 0, t]);
                        }
                    }
                    sb.AppendFormat("\n");
                }
            }

        }

        public double CalcFlowQ(int t, int j, TwoInOne o)
        {
            double q = 0;
            if(!o.check)
            {   //均一流量，採用Value欄位直接回傳
                q = o.Value2D()[0, t];
            }
            else
            {   //逐點輸入，採用Value欄位 * Array欄位百分比，取前後位中位數。
                double first = (j == 0) ? 0 : (o.Value2D()[0, t] * o.Array2D()[j - 1, t] / 100);
                double second = o.Value2D()[0, t] * o.Array2D()[j, t] / 100;
                q = (first + second) / 2;
            }
            return q;
        }

        public double CalcWaterLevel(int t, int j, TwoInOne o)
        {
            double l = 0;
            if(o.type == TwoInOne.Type.UseValue)
            {   //均一水位，採用Value欄位直接回傳
                l = o.Value2D()[0, t];
            }
            else
            {   //逐點輸入，採用Value欄位 
                l = o.Array2D()[j, t];
            }
            return l;
        }

        public bool GenerateSedFile(string file)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            //註22 2.2.4.1 sedimentParticleSize[1, k]
            for (int k = 0; k < sedimentParticlesNumber; ++k)
            {
                if (count == 5)
                {   
                    sb.AppendFormat("\n");
                    count = 0;
                } 
                sb.AppendFormat(" {0,15}", sedimentParticleSize[0, k]);
                ++count;
            }
            sb.AppendFormat("\n");
            count = 0;
            //註23 2.2.4.1 kinematicViscosityCoefficient
            sb.AppendFormat(" {0,15}\n", kinematicViscosityCoefficient.ToString("e"));

            //註23 2.2.3 sedimentDensity, 2.2.2 sedimentPoreRatio
            sb.AppendFormat(" {0,7} {1,7}\n", sedimentDensity, sedimentPoreRatio);

            //註24：模式內部設定值
            sb.AppendFormat("     0.4    10.0     0.1\n");
            sb.AppendFormat("        .0000001        .0000001        .0000001\n");
            sb.AppendFormat("     200       0       1\n");

            //註25：初始水深平均濃度。3.2.1 depthAverageConcentration[k, j, i]
            sb.AppendFormat(" {0,7}\n", 0);
            for (int k = 0; k < sedimentParticlesNumber; ++k)
            {
                if (count == 10)
                {
                    sb.AppendFormat("\n");
                    count = 0;
                } 
                sb.AppendFormat(" {0,7}", depthAverageConcentration.Value3D()[k, 0, 0]);
                ++count;
            }
            sb.AppendFormat("\n");

            //註26
            sb.AppendFormat(" {0,7}\n", 0);
            sb.AppendFormat(" {0,7}", bottomLevelNumber);   //作用層代號。目前當做底床分層數目2.3.1。
            sb.AppendFormat(" {0,7}", bottomLevelNumber - 1);   //作用層下底床分層層數。目前當做底床分層數目扣1，即本案例是6-1=5。
            count = 3;
            for (int l = bottomLevelNumber - 1; l > 0; --l)
            {
                if (count == 10)
                {
                    sb.AppendFormat("\n");
                    count = 0;
                }
                sb.AppendFormat(" {0,7}", l);   //作用層下底床分層層數。目前當做底床分層數目扣1，即本案例是6-1=5。
                ++count;
            }
            sb.AppendFormat("\n");
            count = 0;
            for (int l = 0; l < bottomLevelNumber; ++l)
            {
                if (count == 10)
                {
                    sb.AppendFormat("\n");
                    count = 0;
                }
                sb.AppendFormat(" {0,7}", bottomLevelArray[l]);   //底床分層厚度。2.3.1.1 bottomLevelArray[L]
                ++count;
            }
            sb.AppendFormat("\n");
            count = 0;

            //註27：泥砂組成比例。2.3.1.2 sedimentCompositionArray[K, L]
            for (int l = bottomLevelNumber - 1; l >= 0; --l)
            {
                if (count != 0)
                {
                    sb.AppendFormat("\n");
                    count = 0;
                }
                if (l == 0)
                {
                    sb.AppendFormat(" {0,7}", -1);
                }
                else
                {
                    sb.AppendFormat(" {0,7}", l + 1);
                }
                count = 1;
                for (int k = 0; k < sedimentParticlesNumber; ++k)
                {
                    if (count == 10)
                    {
                        sb.AppendFormat("\n");
                        count = 0;
                    }
                    sb.AppendFormat(" {0,7}", sedimentCompositionArray[k, l] / 100);   //泥砂組成比例。2.3.1.2 sedimentCompositionArray[K, L]
                    ++count;
                }
            }
            sb.AppendFormat("\n");

            //註28：模式內部設定
            for (int i = 0; i < inputGrid.GetI; ++i)
            {
                for (int j = 0; j < inputGrid.GetJ; ++j)
                {
                    if (i != inputGrid.GetI - 1)
                    {
                        sb.AppendFormat(" {0,7}", i + 1);   //上游入流斷面編號I=1
                        sb.AppendFormat(" {0,7}", j + 1);   //側方向斷面編號J=1~Jtotal
                        sb.AppendFormat(" {0,7}", 1);       //上游輸砂邊界型態。預設值1
                        sb.AppendFormat(" {0,7}", j + 1);   //模式內部設定值。
                        sb.AppendFormat(" {0,7}", 1);       //模式內部設定值。預設值1
                        sb.AppendFormat(" {0,7}\n", 1);       //模式內部設定值。預設值1
                    }
                    else
                    {
                        if (j == inputGrid.GetJ - 1)
                        {
                            sb.AppendFormat(" {0,7}", -(i + 1));   //下游入流斷面編號I=1
                        }
                        else
                        {
                            sb.AppendFormat(" {0,7}", i + 1);   //下游入流斷面編號I=1
                        }
                        sb.AppendFormat(" {0,7}", j + 1);   //側方向斷面編號J=1~Jtotal
                        sb.AppendFormat(" {0,7}", -1);      //下游輸砂邊界型態。預設值-1。
                        sb.AppendFormat(" {0,7}", 1);       //模式內部設定值。預設值1
                        sb.AppendFormat(" {0,7}", 1);       //模式內部設定值。預設值1
                        sb.AppendFormat(" {0,7}\n", 1);     //模式內部設定值。預設值1
                    }
                }
            }
           
            using (StreamWriter outfile = new StreamWriter(file))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }
            return true;
        }

        public bool Generate3dFile(string file)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            //註30 0.1.1.1 - 分層比例 levelProportion[verticalLevelNumber]
            for (int p = 0; p < verticalLevelNumber; ++p)
            {
                if (count == 10)
                {   
                    sb.AppendFormat("\n");
                    count = 0;
                }
                sb.AppendFormat(" {0,7}", levelProportion[p]);
                ++count;
            }
            sb.AppendFormat("\n");
            count = 0;

            //註31
            sb.AppendFormat(" {0,7}", steppingTimesInVertVslcTime); //垂直方向計算時間步進次數。1.1.1.4
            sb.AppendFormat(" {0,7}", mainstreamWindShear); //主流方向風剪。4.1.4.1
            sb.AppendFormat(" {0,7}", sideWindShear); //側方向風剪。4.1.4.2
            sb.AppendFormat(" {0,7}", coriolisForce); //科氏力。4.1.4.3

            if (((int)turbulenceViscosityType - 1) == 0)
            {
                sb.AppendFormat(" {0,7}", tvInMainstreamDirection); //主流方向黏滯係數加成比例。1.1.5 ???
                sb.AppendFormat(" {0,7}", tvInSideDirection);       //側方向黏滯係數加成比例。1.1.6 ???
            }
            else
            {
                sb.AppendFormat(" {0,7}", viscosityFactorAdditionInMainstream); //主流方向黏滯係數加成比例。1.1.5 ???
                sb.AppendFormat(" {0,7}", viscosityFactorAdditionInSideDirection); //側方向黏滯係數加成比例。1.1.6 ???
            }
            sb.AppendFormat(" {0,7}", waterModelingConvergenceCriteria3d); //三維水理收斂標準。1.1.2.2 ???
            sb.AppendFormat(" {0,7}", waterModelingConvergenceCriteria3d); //三維水理收斂標準。1.1.2.2 ???
            sb.AppendFormat("\n");

            //註32
            if (roughnessHeightKs.type == TwoInOne.Type.UseArray)
            {
                sb.AppendFormat(" {0,7}\n", -1); //粗糙高度Ks。1.2.1.3。
                count = 0;
                for (int i = 0; i < inputGrid.GetI; ++i)
                {
                    for (int j = 0; j < inputGrid.GetJ; ++j)
                    {
                        if (count == 10)
                        {
                            sb.Append("\n");
                            count = 0;
                        }
                        sb.AppendFormat(" {0,7}", roughnessHeightKs.Array2D()[j, i].ToString());
                        ++count;
                    }
                    sb.Append("\n");
                    count = 0;
                }
            }
            else
            {
                sb.AppendFormat(" {0,7}\n", 0); //粗糙高度Ks。1.2.1.3。本案例為均一值
                sb.AppendFormat(" {0,7}\n", roughnessHeightKs.ValueDouble()[0]); //粗糙高度Ks。1.2.1.3
            }

            //註33：
            sb.AppendFormat(" {0,7}", boundaryLayerThickness); //邊界層厚度。4.1.5.1
            sb.AppendFormat(" {0,7}", (int)seabedBoundarySlip); //底床邊界滑移。4.1.5.2
            sb.AppendFormat(" {0,7}", (int)turbulenceViscosityType - 1);       //紊流黏滯係數。1.2.2
            sb.AppendFormat(" {0,7}", (int)turbulenceViscosityType - 1);       //紊流黏滯係數。1.2.2
            sb.AppendFormat(" {0,7}", 0);       //模式預設值
            sb.AppendFormat(" {0,7}", (verticalVelocitySlice== VerticalVelocitySliceType.Open) ? 1 : 0);       //垂向流速剖面。3.1.4
            sb.AppendFormat(" {0,7}", 0);       //模式預設值。
            sb.AppendFormat("\n");

            //註34：模式預設值
            sb.AppendFormat("     0.5     0.1     0.1       1       1       1       1       1       0       0\n"); 
            sb.AppendFormat("     0\n");
            sb.AppendFormat("       1       1       1       1       0       0\n");

            //註35：模式預設值
            //水理輸出控制3D 三維流速資訊。1：開、0：關。
            //動床輸出控制3D 三維濃度資訊。1：開、0：關。
            //sb.AppendFormat(" {0,7} {1,7}\n", (IsWaterModelingMode()) ? 1 : 0, (IsMovableBedMode()) ? 1 : 0);
            sb.AppendFormat(" {0,7} {1,7}\n", 1, 1);

            //註36：垂向濃度分布(3D)
            if(boundaryUpVerticalDistribution.type != TwoInOne.Type.UseArray)
            {   //均一值
                sb.AppendFormat(" {0,7}\n", 0);
            }
            else
            {   //個別輸入
                sb.AppendFormat(" {0,7}\n", boundaryUpVerticalDistributionNum);
                for(int pp = 0; pp < boundaryUpVerticalDistributionNum; ++pp)
                {
                    count = 0;
                    if (count == 10)
                    {
                        sb.AppendFormat("\n");
                        count = 0;
                    }
                    sb.AppendFormat(" {0,7}", boundaryUpVerticalDistribution.Array2D()[0, boundaryUpVerticalDistributionNum - pp - 1]);
                    ++count;
                }
                sb.AppendFormat("\n");
                count = 0;
                for (int pp = 0; pp < boundaryUpVerticalDistributionNum; ++pp)
                {
                    count = 0;
                    if (count == 10)
                    {
                        sb.AppendFormat("\n");
                        count = 0;
                    }
                    sb.AppendFormat(" {0,7}", boundaryUpVerticalDistribution.Array2D()[1, boundaryUpVerticalDistributionNum - pp - 1] / 100);
                    ++count;
                }
                sb.AppendFormat("\n");
            }

            //註37：垂向流速分布(3D)
            if (verticalVelocityDistribution)
            {
                //if (boundaryUpVerticalDistribution.type != TwoInOne.Type.UseArray)
                //{   //均一值
                //    sb.AppendFormat(" {0,7}\n", 0);
                //}
                //else
                {   //個別輸入
                    sb.AppendFormat(" {0,7}\n", verticalVelocityDistributionNumber);
                    for (int pp = 0; pp < verticalVelocityDistributionNumber; ++pp)
                    {
                        count = 0;
                        if (count == 10)
                        {
                            sb.AppendFormat("\n");
                            count = 0;
                        }
                        sb.AppendFormat(" {0,7}", verticalVelocityDistributionArray[1, pp] / 100);
                        ++count;
                    }
                    sb.AppendFormat("\n");
                    for (int pp = 0; pp < verticalVelocityDistributionNumber; ++pp)
                    {
                        count = 0;
                        if (count == 10)
                        {
                            sb.AppendFormat("\n");
                            count = 0;
                        }
                        sb.AppendFormat(" {0,7}", verticalVelocityDistributionArray[0, pp]);
                        ++count;
                    }
                    sb.AppendFormat("\n");
                    count = 0;
                }
            }
            //註38：模式預設值
            sb.AppendFormat("       0\n");
            sb.AppendFormat("     500\n"); 

            using (StreamWriter outfile = new StreamWriter(file))
            {
                outfile.Write(sb.ToString());
                outfile.Close();
            }
            return true;
        }

        public enum DumpTwoInOneType
        {
            Normal,
            OnlyType,
            OnlyValueOrArray,
        }

        void DumpTwoInOne(TwoInOne o, ref StringBuilder sb, DumpTwoInOneType t = DumpTwoInOneType.Normal, bool noNewLine = false)
        {
            if(o == null || o.type == TwoInOne.Type.None)
            {
                if (t != DumpTwoInOneType.OnlyValueOrArray)
                {
                    sb.AppendFormat("{0,8}", (0).ToString());
                    if (!noNewLine)
                    {
                        sb.Append("\n");
                    }
                }
                if (t != DumpTwoInOneType.OnlyType)
                {
                    sb.AppendFormat("{0,8}\n", (0).ToString());
                }
                return;
            }
            if(o.type == TwoInOne.Type.UseValue)
            {
                if (t != DumpTwoInOneType.OnlyValueOrArray)
                {
                    sb.AppendFormat("{0,8}", (0).ToString());
                    if (!noNewLine)
                    {
                        sb.Append("\n");
                    }
                }

                if (t != DumpTwoInOneType.OnlyType)
                {
                    sb.AppendFormat("{0,8}\n", o.ValueDouble()[0].ToString());
                }
                return;
            }
            
            if (t != DumpTwoInOneType.OnlyValueOrArray)
            {
                sb.AppendFormat("{0,8}", (-1).ToString());
                if (!noNewLine)
                {
                    sb.Append("\n");
                }
            }
            if (t == DumpTwoInOneType.OnlyType)
            {
                return;
            }

            int count = 0;
            for (int i = 0; i < o.Array2D().GetLength(0); ++i)
            {
                for (int j = 0; j < o.Array2D().GetLength(1); ++j)
                {
                    if (count == 10)
                    {
                        sb.Append("\n");
                        count = 0;
                    }
                    sb.AppendFormat("{0,8}", o.Array2D()[i, j].ToString());
                    ++count;
                }
                sb.Append("\n");
                count = 0;
            }
        }
    }
}
