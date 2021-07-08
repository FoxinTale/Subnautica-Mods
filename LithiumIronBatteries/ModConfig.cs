using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace LithiumIronBatteries 
{
    [Menu("Lithium Iron Batteries")]
    public class ModConfig : ConfigFile
    {
        [Choice]
        public BatteryMode Mode { get; set; } = BatteryMode.Balanced;
        public enum BatteryMode
        {
            Realistic,  // Sub ion battery, 750 or less
            Balanced, // Just above ion battery, roughly 1250.
            Unrealistic, // Double ion battery, 2000
            Absurd // 2500, on par with nuclear batteries. The value makes sense for them, but not LiFePo4 batteries.
        }
    }
}