using QModManager.API.ModLoading;
using SMLHelper.V2.Utility;
using System.Reflection;
using System.IO;
using CustomBatteries.API;
using SMLHelper.V2.Handlers;

namespace LithiumIronBatteries
{
    [QModCore]
    public static class Main
    {
        
        static string AssetsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
        
        public const string Version = "1.0.0.0";
        public const string ModName = "[LithiumIronBatteries] ";

        private static ModConfig Config { get; set; }
        private static TechType LithiumIronCathode { get; set;}
        private static TechType LithiumIronAnode{ get; set;}
        private static TechType LithiumIronElectrolyte{ get; set;} 
        [QModPatch]
        public static void Load()
        {
            LithiumIronBatteries_Init();
            Config = OptionsPanelHandler.Main.RegisterModOptions<ModConfig>();
            CreateAndPatchPacks();
        }
        #region Create And Patch Packs
        private static void CreateAndPatchPacks()
        { 
            var lifepoBattery = new CbBattery
            {  
                EnergyCapacity = (int)Config.Mode,
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
            var cathode = new LithiumIronCathodeItem();
            cathode.Patch();
            LithiumIronCathode = cathode.TechType;
       
            var anode = new LithiumIronAnodeItem(); 
            anode.Patch();
            LithiumIronAnode = anode.TechType;

            var electrolyte = new LithiumIronElectrolyteItem();
            electrolyte.Patch();
            LithiumIronElectrolyte = electrolyte.TechType;
        }
        #endregion
    }
}
