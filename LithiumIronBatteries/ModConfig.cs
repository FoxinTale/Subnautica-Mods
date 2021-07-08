using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace LithiumIronBatteries 
{
    [Menu("Lithium Iron Batteries")]
    public class ModConfig : ConfigFile
    {
        [Choice(Tooltip = "This lets you change the battery capacity to your liking. This does require a game restart, and will not apply to pre-existing LiFePO batteries.")]
        public BatteryMode Mode { get; set; } = BatteryMode.Balanced;
        public enum BatteryMode
        {
            Midrange = 500,
            Realistic = 750 ,  // Sub ion battery.
            Balanced = 1250, // Just above ion battery.
            Unrealistic = 2000, // Double ion battery.
            Absurd = 2500, // On par with nuclear batteries. The value makes sense for them, but not LiFePo4 batteries.
            Wtf = 5000
        }
    }
}