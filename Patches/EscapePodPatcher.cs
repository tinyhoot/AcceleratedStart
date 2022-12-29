using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace AcceleratedStart.Patches
{
    [HarmonyPatch]
    internal class EscapePodPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(EscapePod), nameof(EscapePod.DamageRadio))]
        public static bool FixRadio()
        {
            // If the radio should start fixed, skip the method and never damage it in the first place.
            return !Initialiser._config.bFixRadio;
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(IntroLifepodDirector), nameof(IntroLifepodDirector.ConcludeIntroSequence))]
        public static void FixLifepodAfterCinematic()
        {
            // Ensure the pod shows as fixed even if the intro cinematic is not skipped.
            // This method executes after the PDA is opened at the end of the intro.
            EscapePod.main.damageEffectsShowing = true;
            EscapePod.main.UpdateDamagedEffects();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(EscapePod), nameof(EscapePod.StopIntroCinematic))]
        public static void PatchIntro(ref EscapePod __instance)
        {
            if (Initialiser._config.bFixLifepod)
            {
                __instance.GetComponent<LiveMixin>().ResetHealth();
                __instance.UpdateDamagedEffects();
            }
            // Start with no health or food lost.
            if (Initialiser._config.bStartHealed)
            {
                Player.main.GetComponent<LiveMixin>().ResetHealth();
                Survival survival = Player.main.GetComponent<Survival>();
                if (survival != null)
                {
                    survival.food = 100f;
                    survival.water = 100f;
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EscapePodFirstUseCinematicsController), nameof(EscapePodFirstUseCinematicsController.Initialize))]
        public static bool SkipExitCinematic()
        {
            // Skip lifepod exit cinematics.
            EscapePod.main.bottomHatchUsed = true;
            EscapePod.main.topHatchUsed = true;

            return true;
        }
    }
}
