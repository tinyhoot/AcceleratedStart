using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace AcceleratedStart
{
    internal class Config
    {
        public ConfigEntry<bool> FixLifepod;
        public ConfigEntry<bool> FixRadio;
        public ConfigEntry<bool> StartHealed;
        public ConfigEntry<bool> UseLoadouts;
        public ConfigEntry<string> CurrentLoadout;

        public ConfigEntry<string> LifepodInventorySize;

        // All valid options for lifepod inventory sizes.
        internal readonly Dictionary<string, int[]> _inventorySizes = new Dictionary<string, int[]>
        {
            { "tiny", new[] { 4, 4 } },
            { "small", new[] { 4, 6 } },
            { "default", new[] { 4, 8 } },
            { "large", new[] { 6, 10 } },
            { "huge", new[] { 8, 10 } },
        };

        public void RegisterOptions(ConfigFile config)
        {
            FixLifepod = config.Bind("Patches", "FixLifepod", true,
                "If enabled, start with the lifepod already repaired.");
            FixRadio = config.Bind("Patches", "FixRadio", true, "If enabled, start with the radio already repaired.");
            StartHealed = config.Bind("Patches", "StartHealed", true, "If enabled, start at full health.");
            var invDesc = new ConfigDescription("Change the size of the lifepod inventory.",
                new AcceptableValueList<string>(_inventorySizes.Keys.ToArray()));
            LifepodInventorySize = config.Bind("Patches", "LifepodInventorySize", "large",
                invDesc);

            UseLoadouts = config.Bind("Loadout", "UseLoadouts", false,
                "Loadouts are only used if this option is enabled.");

            // Dynamically generate the available options for loadouts based on the files in the directory.
            string[] choices = Initialiser._loadouts.Keys.ToArray();
            if (choices.Length == 0)
            {
                Initialiser._log.LogError("No loadouts were parsed, cannot build mod option!");
                choices = new[] { "No loadouts in directory!" };
            }

            var loadoutDesc = new ConfigDescription("The filename of the loadout you will start the game with."
                                                    + "Do not include extensions in this name. Example:"
                                                    + "default.txt --> default",
                new AcceptableValueList<string>(choices));
            CurrentLoadout = config.Bind("Loadout", "CurrentLoadout", "default", loadoutDesc);
        }

        public int GetLoadoutIndex()
        {
            string[] choices = Initialiser._loadouts.Keys.ToArray();
            int index = Math.Max(0, Array.IndexOf(choices, CurrentLoadout));
            if (choices.Length == 0)
                index = 0;

            return index;
        }
    }
}