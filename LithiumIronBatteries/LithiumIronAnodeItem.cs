using System.IO;
using System.Reflection;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;

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
        public override string AssetsFolder =>  Path.Combine(new DirectoryInfo(Path.Combine(Assembly.GetExecutingAssembly().Location, "..")).Name, "Assets");
        public override string IconFileName => "Anode.png";

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