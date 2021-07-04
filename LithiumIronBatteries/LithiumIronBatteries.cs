using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;

namespace LithiumIronBatteries{
    [QModCore]

    public static class LithiumIronBatteries{
        internal static TechType LithiumIronCathode{ get; private set;}
        internal static TechType LithiumIronAnode{ get; private set;}
        internal static TechType LithiumIronElectrolyte{ get; private set;}

        [QModPatch]
  public static void LithiumIronBatteries_InitializationMethod(){
       var Cathode = new LithiumIronCathodeItem();
       Cathode.Patch();
       LithiumIronCathode = Cathode.TechType;
       
       var Anode = new LithiumIronAnodeItem(); 
       Anode.Patch();
       LithiumIronAnode = Anode.TechType;

       var Electrolyte = new LithiumIronElectrolyteItem();
       Electrolyte.Patch();
       LithiumIronElectrolyte = Electrolyte.TechType;
       
       Logger.Log(Logger.Level.Debug, "Lithium Iron Batteries Initialization");
       Harmony harmony = new Harmony("LithiumIronBatteries");
       harmony.PatchAll(Assembly.GetExecutingAssembly()); 
       Logger.Log(Logger.Level.Info, "Lithium Iron Batteries Patched");
  }
    }
}