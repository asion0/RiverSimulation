using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverSimulationApplication
{
    class RiverSimulationProfile
    {
        public static RiverSimulationProfile profile;


        public bool importFinished = false;
        public bool simulationModuleFinished = false;
        public bool waterModelingFinished = false;
        public bool movableBedFinished = false;
        public bool InitialConditionsFinished = false;
        public bool BoundaryConditionsFinished = false;

        public bool ModuleSelectUsability() { return importFinished; }

        public enum ModuleType1 { NoSelect, Type2D, Type3D }
        public enum ModuleType2 { NoSelect, WaterModeling, MovableBed }

        public ModuleType1 moduleType1 = ModuleType1.NoSelect;
        public ModuleType2 moduleType2 = ModuleType2.NoSelect;
        public bool Is3DMode() { return moduleType1 == ModuleType1.Type3D; }
        public bool HasMovableBedMode() { return moduleType2 == ModuleType2.MovableBed; }


    }
}
