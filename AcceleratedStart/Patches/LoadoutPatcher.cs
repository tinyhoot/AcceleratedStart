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
            var podContainer = __instance.storageContainer.container;
            Vector2 vanilla = InventorySize.Vanilla.GetSize();
            if (podContainer.sizeX != (int)vanilla.x || podContainer.sizeY != (int)vanilla.y)
            {
                Initialiser._log.LogInfo($"Modified lifepod inventory size detected "
                                         + $"({podContainer.sizeX} x {podContainer.sizeY}), probably from another mod. "
                                         + "Skipping storage size changes for mod compatibility.");
                return;
            }
            Vector2 size = Initialiser._config.LifepodInventorySize.Value.GetSize();
            podContainer.Resize((int)size.x, (int)size.y);
        }
    }
}
