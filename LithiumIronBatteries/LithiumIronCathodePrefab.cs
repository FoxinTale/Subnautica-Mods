using Common.Helpers;
using HarmonyLib;
using QModManager.API.ModLoading;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace LithiumIronBatteries{
    internal class LithiumIronCathodePrefab : CraftableModItem
  {
    public static TechType TechTypeID { get; private set; }
 internal LithiumIronCathodePrefab() : base(
          techTypeName: "LithiumIronCathode", 
          friendlyName:"LiFePo4 Cathode", 
          description: "The Cathode (positive end) of a Lithium Iron Phosphate battery.", 
          requiredAnalysis: TechType.BloodOil,
          groupForPDA: TechGroup.Resources,
          categoryForPDA: TechCategory.Electronics,
          equipmentType: EquipmentType.None,
          quickSlotType: QuickslotType.None,
          backgroundType: CraftData.BackgroundType.Normal,
          itemSize: new Vector2int(1,1),
          fragment: null

 );

    protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.Benzene,2),
                    new Ingredient(TechType.Lithium, 2),
                },
            };
        }
  }
}