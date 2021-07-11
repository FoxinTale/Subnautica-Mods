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
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "lifepo_battery_icon.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lifepo_battery.png")),
                    CustomNormalMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lifepo_battery_msn.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lifepo_battery_s.png")),
                    UseIonModelsAsBase = true
                }
            };
            lifepoBattery.Patch();

            var lifepoPowercell = new CbPowerCell
            {
                EnergyCapacity = lifepoBattery.EnergyCapacity * 2,
                ID = "LithiumIronPowercell",
                Name = "LiFePO Power Cell",
                FlavorText = "A Lithium Iron Phosphate Power Cell. Less powerful than Lithium-Ion cells, but safer and more stable in extreme conditions.",
                CraftingMaterials = { lifepoBattery.TechType, lifepoBattery.TechType, TechType.Silicone, TechType.Copper },
                UnlocksWith = lifepoBattery.TechType,
                CustomIcon = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, "lifepo_powercell_icon.png")),
                CBModelData = new CBModelData
                {
                    CustomTexture = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lifepo_powercell.png")),
                    CustomNormalMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lifepo_powercell_msn.png")),
                    CustomSpecMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder,"lifepo_powercell_s.png")),
                    CustomIllumMap = ImageUtils.LoadTextureFromFile(Path.Combine(AssetsFolder, "lifepo_powercell_sk.png")),
                    // I'm a gal coming from skyrim modding. That's why there's the name suffixes...
                    UseIonModelsAsBase = true
                }

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
