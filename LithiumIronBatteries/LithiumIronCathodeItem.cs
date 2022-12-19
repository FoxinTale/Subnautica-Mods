using System.IO;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using UnityEngine;


namespace LithiumIronBatteries{
    internal class LithiumIronCathodeItem : Craftable
  {
      public LithiumIronCathodeItem() : base(
          "LithiumIronCathode", 
          "LiFePO4 Cathode", 
          "The Cathode (positive end) of a Lithium Iron Phosphate battery.")
 {}

    public override Vector2int SizeInInventory => new Vector2int(1, 1); 
    public override TechCategory CategoryForPDA => TechCategory.Electronics;
    public override TechGroup GroupForPDA => TechGroup.Resources;
    public override TechType RequiredForUnlock => TechType.BloodOil;
    public override float CraftingTime => 5f;
    public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
    public override string[] StepsToFabricatorTab => new string[] { "Resources", "Electronics" };
    public override string AssetsFolder => Main.AssetsFolder;
    public override string IconFileName => "lifepo_cathode_icon.png";

    public override GameObject GetGameObject()
    {
        var cathodeObj = Object.Instantiate(CraftData.GetPrefabForTechType(TechType.Silicone));
        var cathodeMaterial = cathodeObj.GetComponentInChildren<Renderer>().material;
        cathodeMaterial.mainTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_cathode.png"));
        cathodeMaterial.SetTexture(ShaderPropertyID._BumpMap, ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_cathode_n.png")));
        cathodeMaterial.SetTexture(ShaderPropertyID._SpecTex, ImageUtils.LoadTextureFromFile(Path.Combine(Main.AssetsFolder, "lifepo_cathode_s.png")));

        ModPrefabCache.AddPrefab(cathodeObj);
        return cathodeObj;
    }

    protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.GasPod,2),
                    new Ingredient(TechType.Lithium, 1),
                    new Ingredient(TechType.Magnetite, 1)
                },
            };
        }
  }
}