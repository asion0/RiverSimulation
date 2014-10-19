using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace RiverSimulationApplication
{
    class RiverSimulationProfile
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
        //WaterModeling 數值參數
        public double convergenceCriteria2d;    //二維水裡收斂標準 
        public double convergenceCriteria3d;    //三維水裡收斂標準


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
            if (bkImgType == BackgroundMapType.ImportImage && importBmp==null)
            {
                return BackgroundMapType.None;
            }
            return bkImgType; 
        }

        private Bitmap gridBmp = new Bitmap(640 * 2, 640 * 2);
        private Bitmap importBmp;
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

        public bool DownloadGoogleStaticMap()
        {
            string tl = Environment.CurrentDirectory + "\\tl.jpg";
            string tr = Environment.CurrentDirectory + "\\tr.jpg";
            string bl = Environment.CurrentDirectory + "\\bl.jpg";
            string br = Environment.CurrentDirectory + "\\br.jpg";

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
            FreeStaticMaps();
            inputGrid.DownloadGridMap(tl, tr, bl, br);
            tlBmp = new Bitmap(tl);
            trBmp = new Bitmap(tr);
            blBmp = new Bitmap(bl);
            brBmp = new Bitmap(br);
            bkImgType = BackgroundMapType.GoogleStaticMap;

            if (tlBmp != null && trBmp != null && blBmp != null && brBmp != null)
            {
                Graphics g = Graphics.FromImage(gridBmp);
                g.DrawImage(tlBmp, 640, 640);
                g.DrawImage(trBmp, 0, 640);
                g.DrawImage(blBmp, 640, 0);
                g.DrawImage(brBmp, 0, 0);
                g.Dispose();
                tlBmp.Dispose();
                trBmp.Dispose();
                blBmp.Dispose();
                brBmp.Dispose();
                //gridBmp.Save(Environment.CurrentDirectory + "Big.jpg");
            }
            return true;
        }

        public void SetImportImageMode()
        {
            bkImgType = BackgroundMapType.ImportImage;
        }

        private CoorPoint bottomRight = new CoorPoint();
        private CoorPoint topLeft = new CoorPoint();       
        public void SetImportImage(string s, double e, double n, double w, double h)
        {
            importBmp = new Bitmap(s);

            topLeft = new CoorPoint(e, n + h);
            bottomRight = new CoorPoint(e + w, n);
        }
    }
}
