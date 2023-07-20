using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace AcceleratedStart.Patches
{
    [HarmonyPatch]
    internal class LoadoutPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(LootSpawner), nameof(LootSpawner.GetEscapePodStorageTechTypes))]
        public static bool ChangeStartingSupplies(ref LootSpawner __instance)
        {
            List<TechType> loadout = Initialiser.GetActiveLoadout();
            if (loadout is null || !Initialiser._config.UseLoadouts.Value)
                return true;
            
            __instance.escapePodTechTypes.Clear();
            __instance.escapePodTechTypes.AddRange(loadout);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(EscapePod), nameof(EscapePod.Awake))]
        public static void ExpandPodInventory(ref EscapePod __instance)
        {
            Vector2 size = Initialiser._config.LifepodInventorySize.Value.GetSize();
            __instance.storageContainer.container.Resize((int)size.x, (int)size.y);
        }
    }
}
