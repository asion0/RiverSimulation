using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverSimulationApplication
{
    class RiverSimulationProfile
    {
        public static RiverSimulationProfile profile = new RiverSimulationProfile();

        public bool IsImportFinished() { return importFinished; }
        public bool IsImportReady() { return true; }

        public bool IsSimulationModuleFinished() { return moduleType1 != ModuleType1.NoSelect && moduleType2 != ModuleType2.NoSelect; }
        public bool IsSimulationModuleReady() { return importFinished; }

        public bool IsWaterModelingFinished() { return waterModelingFinished; }
        public bool IsWaterModelingReady() { return IsSimulationModuleFinished(); }

        public bool IsMovableBedFinished() { return movableBedFinished; }
        public bool IsMovableBedReady() { return IsSimulationModuleFinished() && moduleType2 == ModuleType2.MovableBed; }

        public bool IsInitialConditionsFinished() { return initialConditionsFinished; }
        public bool IsInitialConditionsReady() 
        {
            if (moduleType2 == ModuleType2.MovableBed)
                return IsWaterModelingFinished() && IsMovableBedFinished();
            else
                return IsWaterModelingFinished();
        }

        public bool IsBoundaryConditionsFinished() { return boundaryConditionsFinished; }
        public bool IsBoundaryConditionsReady() 
        {
            if (moduleType2 == ModuleType2.MovableBed)
                return IsWaterModelingFinished() && IsMovableBedFinished(); 
            else
                return IsWaterModelingFinished();
        }

        public bool IsRunSimulationFinished() { return runSimulationFinished; }
        public bool IsRunSimulationReady() { return IsBoundaryConditionsFinished() && IsInitialConditionsFinished(); }

        public bool IsSimulationResultFinished() { return true; }
        public bool IsSimulationResultReady() { return IsRunSimulationFinished(); }
        
        public bool importFinished = false;
        public bool simulationModuleFinished = false;
        public bool waterModelingFinished = false;
        public bool movableBedFinished = false;
        public bool initialConditionsFinished = false;
        public bool boundaryConditionsFinished = false;
        public bool runSimulationFinished = false;

        public bool ModuleSelectUsability() { return importFinished; }

        public enum ModuleType1 { NoSelect, Type2D, Type3D }
        public enum ModuleType2 { NoSelect, WaterModeling, MovableBed }

        public ModuleType1 moduleType1 = ModuleType1.NoSelect;
        public ModuleType2 moduleType2 = ModuleType2.NoSelect;
        public bool Is3DMode() { return moduleType1 == ModuleType1.Type3D; }
        public bool HasMovableBedMode() { return moduleType2 == ModuleType2.MovableBed; }


    }
}
