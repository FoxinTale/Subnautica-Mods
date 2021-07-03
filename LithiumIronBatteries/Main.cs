using QModManager.API.ModLoading;
using System.Reflection;
using System.IO;
using CustomBatteries.API;
using SMLHelper.V2.Utility;
using UnityEngine;
using LithiumIronBatteries;

namespace LithiumIronBatteries
{
    [QModCore]
    public static class Main
    {
        static Assembly myAssembly = Assembly.GetExecutingAssembly();
        static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        static string AssetsFolder = Path.Combine(ModPath, "Assets");
        static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "lithiumironbatteries"));

        internal static TechType LithiumIronCathode{ get; private set;}
        public const string version = "1.0.0.0";
        public const string modName = "[LithiumIronBatteries] ";

        [QModPatch]
        public static void Load()
        {
            CreateAndPatchPacks();
        }
        #region Create And Patch Packs
        private static void CreateAndPatchPacks()
        { 
            var lifepoBattery = new CbBattery
            {
                EnergyCapacity = 2500,
                ID = "LithiumIronBattery",
                Name = "LiFePO Battery",
                FlavorText = "Lithium Iron Phosphate batteries. Similar to Lithium-Ion batteries, but safer and more stable in extreme conditions.",
                CraftingMaterials = { TechType.None, LithiumIronBatteries.LithiumIronCathode, TechType.LithiumIronElectrolyte, TechType.Silicone, TechType.Titanium},
                UnlocksWith = TechType.BloodOil
            };
            lifepoBattery.Patch();

            var lifepoPowercell = new CbPowerCell // Calling the CustomBatteries API to patch this item as a Power Cell
            {
                EnergyCapacity = lifepoBattery.EnergyCapacity * 2,
                ID = "LithiumIronPowercell",
                Name = "LiFePO Power Cell",
                FlavorText = "A Power Cell made by the Precursor Technology and its interaction with Nuclear Power.",
                CraftingMaterials = { lifepoBattery.TechType, lifepoBattery.TechType, TechType.Silicone },
                UnlocksWith = lifepoBattery.TechType

            };
            lifepoPowercell.Patch();
        }
        #endregion
    }
}
