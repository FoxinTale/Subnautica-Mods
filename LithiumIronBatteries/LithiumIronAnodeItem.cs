using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;

namespace LithiumIronBatteries
{
    internal class LithiumIronAnodeItem : Craftable
    {
        public static TechType TechTypeID { get; private set; }
    
    
        public LithiumIronAnodeItem() : base(
            "LithiumIronAnode", 
            "LiFePo4 anode", 
            "The Anode (negative end) of a Lithium Iron Phosphate battery.")
        {}

        public override Vector2int SizeInInventory => new Vector2int(1, 1); 
        public override TechCategory CategoryForPDA => TechCategory.Electronics;
        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechType RequiredForUnlock => TechType.BloodOil;
        public override float CraftingTime => 5f;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.Magnetite,2),
                    new Ingredient(TechType.EyesPlantSeed,2)
                },
            };
        }
    }
}