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
        static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(AssetsFolder, "LithiumIronBatteries"));
        
        public const string version = "1.0.0.0";
        public const string modName = "[LithiumIronBatteries] ";
        internal static TechType LithiumIronCathode{ get; private set;}
        internal static TechType LithiumIronAnode{ get; private set;}
        internal static TechType LithiumIronElectrolyte{ get; private set;} 
        [QModPatch]
        public static void Load()
        {
            LithiumIronBatteries_Init();
            CreateAndPatchPacks();
            
        }
        #region Create And Patch Packs
        private static void CreateAndPatchPacks()
        { 
            var lifepoBattery = new CbBattery
            {
                EnergyCapacity = 1250,
                ID = "LithiumIronBattery",
                Name = "LiFePO Battery",
                FlavorText = "Lithium Iron Phosphate batteries. Less powerful than Lithium-Ion batteries, but safer and more stable in extreme conditions.",
                CraftingMaterials = { LithiumIronCathode, LithiumIronAnode, LithiumIronElectrolyte,TechType.Copper, TechType.Silicone, TechType.Titanium},
                UnlocksWith = TechType.BloodOil,
                CustomIcon = new Atlas.Sprite(assetBundle.LoadAsset<Sprite>("lithiumironbatteryicon")),
                CBModelData = new CBModelData
                {
                    CustomTexture = assetBundle.LoadAsset<Texture2D>("lithiumironbattery"),
                    CustomNormalMap = assetBundle.LoadAsset<Texture2D>("lithiumironbattery_msn"),
                    CustomSpecMap = assetBundle.LoadAsset<Texture2D>("lithiumironbattery_s")
                }
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

        private static void LithiumIronBatteries_Init()
        {
            var Cathode = new LithiumIronCathodeItem();
            Cathode.Patch();
            LithiumIronCathode = Cathode.TechType;
       
            var Anode = new LithiumIronAnodeItem(); 
            Anode.Patch();
            LithiumIronAnode = Anode.TechType;

            var Electrolyte = new LithiumIronElectrolyteItem();
            Electrolyte.Patch();
            LithiumIronElectrolyte = Electrolyte.TechType;
        }
        #endregion
    }
}
