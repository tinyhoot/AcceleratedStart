using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using HootLib.Configuration;
using UnityEngine;

namespace AcceleratedStart
{
    internal class Config : HootConfig
    {
        public const string SectionLoadouts = "Loadouts";
        public const string SectionPatches = "Patches";
        
        public ConfigEntryWrapper<bool> FixLifepod;
        public ConfigEntryWrapper<bool> FixRadio;
        public ConfigEntryWrapper<bool> StartHealed;
        public ConfigEntryWrapper<bool> UseLoadouts;
        public ConfigEntryWrapper<string> CurrentLoadout;

        public ConfigEntryWrapper<InventorySize> LifepodInventorySize;

        public Config(ConfigFile configFile) : base(configFile) { }
        public Config(string path, BepInPlugin metadata) : base(path, metadata) { }
        
        protected override void RegisterOptions()
        {
            FixLifepod = RegisterEntry(
                SectionPatches,
                nameof(FixLifepod),
                true,
                "If enabled, start with the lifepod already repaired."
            );
            FixRadio = RegisterEntry(
                SectionPatches,
                nameof(FixRadio),
                true,
                "If enabled, start with the radio already repaired."
            );
            StartHealed = RegisterEntry(
                SectionPatches,
                nameof(StartHealed),
                true,
                "If enabled, start at full health."
            );
            LifepodInventorySize = RegisterEntry(
                SectionPatches,
                nameof(LifepodInventorySize),
                InventorySize.Vanilla,
                "With loadouts, the required inventory size can quickly spiral out of control. This setting "
                + "controls how large the inventory of the small lifepod storage is."
            ).WithChoiceOptionStringsOverride(GetLifepodSizeDescription());

            UseLoadouts = RegisterEntry(
                SectionLoadouts,
                nameof(UseLoadouts),
                false,
                "Loadouts are only used if this option is enabled."
            );

            // Dynamically generate the available options for loadouts based on the files in the directory.
            string[] choices = Initialiser._loadouts.Keys.ToArray();
            if (choices.Length == 0)
            {
                Initialiser._log.LogError("No loadouts were parsed, cannot build mod option!");
                choices = new[] { "No loadouts in directory!" };
            }

            CurrentLoadout = RegisterEntry(
                SectionLoadouts,
                nameof(CurrentLoadout),
                "default",
                "The filename of the loadout you will start the game with. "
                + "Do not include extensions in this name.\n"
                + "Example:\n"
                + "default.txt --> default",
                new AcceptableValueList<string>(choices)
            ).WithDescription(
                "Current Loadout",
                "The name of the loadout you'll start the game with."
            );
        }

        protected override void RegisterControllingOptions() { }

        protected override void RegisterModOptions(HootModOptions modOptions)
        {
            modOptions.AddItem(FixLifepod.ToModToggleOption());
            modOptions.AddItem(FixRadio.ToModToggleOption());
            modOptions.AddItem(StartHealed.ToModToggleOption());
            modOptions.AddItem(LifepodInventorySize.ToModChoiceOption());
            modOptions.AddItem(UseLoadouts.ToModToggleOption());
            modOptions.AddItem(CurrentLoadout.ToModChoiceOption());
        }

        private string[] GetLifepodSizeDescription()
        {
            List<string> descriptions = new List<string>();
            foreach (var value in Enum.GetValues(typeof(InventorySize)))
            {
                Vector2 size = ((InventorySize)value).GetSize();
                descriptions.Add($"{value} ({size.x} x {size.y})");
            }

            return descriptions.ToArray();
        }
    }
}