using System.IO;
using System.Reflection;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using UnityEngine;

namespace LithiumIronBatteries
{
    internal class LithiumIronElectrolyteItem : Craftable
    {

        public LithiumIronElectrolyteItem() : base(
            "LithiumIronElectrolyte", 
            "LiFePO4 Electrolyte", 
            "The electrolyte of a Lithium Iron Phosphate battery. This is the stuff that makes it chooch.")
        {}
        public override Vector2int SizeInInventory => new Vector2int(1, 1); 
        public override TechCategory CategoryForPDA => TechCategory.Electronics;
        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechType RequiredForUnlock => TechType.BloodOil;
        public override float CraftingTime => 5f;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new string[] { "Resources", "Electronics" };
        public override string AssetsFolder => Main.AssetsFolder;
        public override string IconFileName => "lifepo_electrolyte_icon.png";
        
        public override GameObject GetGameObject()
        {
            var electrolyteObj = Object.Instantiate(CraftData.GetPrefabForTechType(TechType.Aerogel));
            var electrolyteMaterial = electrolyteObj.GetComponentInChildren<Renderer>().material;
            electrolyteMaterial.mainTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_electrolyte.png"));
            electrolyteMaterial.SetTexture(ShaderPropertyID._BumpMap, ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_electrolyte_n.png")));
            electrolyteMaterial.SetTexture(ShaderPropertyID._SpecTex, ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_electrolyte_s.png")));

            ModPrefabCache.AddPrefab(electrolyteObj);
            return electrolyteObj;
        }
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.HydrochloricAcid,1),
                    new Ingredient(TechType.Benzene,1),
                    new Ingredient(TechType.Lithium, 1)
                }
            };
        }
    }
}