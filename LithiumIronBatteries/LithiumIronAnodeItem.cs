using System.IO;
using System.Reflection;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using UnityEngine;

namespace LithiumIronBatteries
{
    internal class LithiumIronAnodeItem : Craftable
    {
        public LithiumIronAnodeItem() : base(
            "LithiumIronAnode", 
            "LiFePO4 Anode", 
            "The Anode (negative end) of a Lithium Iron Phosphate battery.")
        {}

        public override Vector2int SizeInInventory => new Vector2int(1, 1); 
        public override TechCategory CategoryForPDA => TechCategory.Electronics;
        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechType RequiredForUnlock => TechType.BloodOil;
        public override float CraftingTime => 5f;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new string[] { "Resources", "Electronics" };
        public override string AssetsFolder => Main.AssetsFolder;
        public override string IconFileName => "lifepo_anode_icon.png";

        public override GameObject GetGameObject()
        {
            var anodeObj = Object.Instantiate(CraftData.GetPrefabForTechType(TechType.Silicone));
            var anodeMaterial = anodeObj.GetComponentInChildren<Renderer>().material;
            anodeMaterial.mainTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_anode.png"));
            anodeMaterial.SetTexture(ShaderPropertyID._BumpMap, ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_anode_msn.png")));
            anodeMaterial.SetTexture(ShaderPropertyID._SpecTex, ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_anode_s.png")));
            
            ModPrefabCache.AddPrefab(anodeObj);
            return anodeObj;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.Lithium,1),
                    new Ingredient(TechType.Diamond,1)
                },
            };
        }
    }
}