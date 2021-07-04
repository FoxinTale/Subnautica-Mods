using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using CustomBatteries.API;
using UnityEngine;

namespace LithiumIronBatteries
{
    [QModCore]
    public static class Main
    {
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        static string AssetsFolder = Path.Combine(ModPath, "Assets");
        static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "lithiumironbatteries"));
        
        public const string version = "1.0.0.0";
        public const string modName = "[LithiumIronBatteries] ";

        [QModPatch]
        public static void Load()
        {
            CreateAndPatchPacks();
            LithiumIronBatteries.LithiumIronBatteries_InitializationMethod();
        }
        #region Create And Patch Packs
        private static void CreateAndPatchPacks()
        { 
            var lifepoBattery = new CbBattery
            {
                EnergyCapacity = 2500,
                ID = "LithiumIronBattery",
                Name = "LiFePO Battery",
                FlavorText = "Lithium Iron Phosphate batteries. Less powerful than Lithium-Ion batteries, but safer and more stable in extreme conditions.",
                CraftingMaterials = { LithiumIronBatteries.LithiumIronCathode, LithiumIronBatteries.LithiumIronAnode, LithiumIronBatteries.LithiumIronElectrolyte, TechType.Silicone, TechType.Titanium},
                UnlocksWith = TechType.BloodOil
            };
            lifepoBattery.Patch();

            var lifepoPowercell = new CbPowerCell
            {
                EnergyCapacity = lifepoBattery.EnergyCapacity * 2,
                ID = "LithiumIronPowercell",
                Name = "LiFePO Power Cell",
                FlavorText = "A Lithium Iron Phosphate Power Cell. Less powerful than Lithium-Ion cells, but safer and more stable in extreme conditions.",
                CraftingMaterials = { lifepoBattery.TechType, lifepoBattery.TechType, TechType.Silicone },
                UnlocksWith = lifepoBattery.TechType

            };
            lifepoPowercell.Patch();
        }
        #endregion
    }
}
