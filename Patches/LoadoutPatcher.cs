using System.Collections.Generic;
using HarmonyLib;

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
            if (loadout is null || !Initialiser._config.bUseDefaultLoadout)
                return true;
            
            __instance.escapePodTechTypes.Clear();
            __instance.escapePodTechTypes.AddRange(loadout);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(EscapePod), nameof(EscapePod.Awake))]
        public static void ExpandPodInventory(ref EscapePod __instance)
        {
            int[] size = Initialiser._config._inventorySizes
                .GetOrDefault(Initialiser._config.sLifepodInventorySize, new[] { 4, 8 });
            __instance.storageContainer.container.Resize(size[0], size[1]);
        }
    }
}
