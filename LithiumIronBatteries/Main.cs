using QModManager.API.ModLoading;
using SMLHelper.V2.Utility;
using System.Reflection;
using System.IO;
using CustomBatteries.API;

namespace LithiumIronBatteries
{
    [QModCore]
    public static class Main
    {
        
        static string AssetsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
        
        public const string Version = "1.0.0.0";
        public const string ModName = "[LithiumIronBatteries] ";

        public static ModConfig Config { get;} = new ModConfig();
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
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "lithiumironbatteryicon.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lithiumironbattery.png")),
                    CustomNormalMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lithiumironbattery_msn.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lithiumironbattery_s.png"))
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
