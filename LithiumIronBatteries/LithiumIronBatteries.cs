using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.API;
using QModManager.Utility;
using System.Collections.Generic;

namespace LithiumIronBatteries{
    [QModCore]

    public static class LithiumIronBatteries{
        internal static TechType LithiumIronCathode{ get; private set;}

    }

    [QModPatch]
    public static void LithiumIronBetteries_InitializationMethod(){
        var Cathode = new LithiumIronCathodePrefab();
        Cathode.Patch();
        LithiumIronCathode = Cathode.TechType;

    }
    
}