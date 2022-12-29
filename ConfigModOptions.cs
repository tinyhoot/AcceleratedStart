using System;
using System.Linq;
using SMLHelper.V2.Options;

namespace AcceleratedStart
{
    /// <summary>
    /// At present, SMLHelper offers no way to dynamically generate config options. Thus, the entire options menu must
    /// be built manually.
    /// </summary>
    internal class ConfigModOptions : ModOptions
    {
        public readonly Config Config;
        // IDs for each option in the mod menu.
        private const string FixLifepod = "FixLifepod";
        private const string FixRadio = "FixRadio";
        private const string UseLoadout = "UseLoadout";
        private const string CurrentLoadout = "CurrentLoadout";
        private const string PodInventorySize = "LifepodInventorySize";

        public ConfigModOptions(string name) : base(name)
        {
            Config = new Config();
            Config.Load();

            // Register events.
            ChoiceChanged += OnChoiceChanged;
            ToggleChanged += OnToggleChanged;
        }

        public override void BuildModOptions()
        {
            AddToggleOption(FixLifepod, "Fix Lifepod", Config.bFixLifepod);
            AddToggleOption(FixRadio, "Fix Radio", Config.bFixRadio);
            AddToggleOption(UseLoadout, "Use loadouts", Config.bUseDefaultLoadout);

            // Add dynamic choices based on the files in the loadout directory.
            string[] choices = Initialiser._loadouts.Keys.ToArray();
            int index = Math.Max(0, Array.IndexOf(choices, Config.sCurrentLoadout));
            if (choices.Length == 0)
            {
                Initialiser._log.LogError("No loadouts were parsed, cannot build mod option!");
                choices = new[] { "No loadouts in directory!" };
                index = 0;
            }
            AddChoiceOption(CurrentLoadout, "Loadout", choices, index);
            AddChoiceOption(PodInventorySize, "Lifepod Inventory Size", Config._inventorySizes.Keys.ToArray(),
                Config.sLifepodInventorySize);
        }

        public void OnChoiceChanged(object sender, ChoiceChangedEventArgs args)
        {
            switch (args.Id)
            {
                case CurrentLoadout:
                    Config.sCurrentLoadout = args.Value;
                    break;
                case PodInventorySize:
                    Config.sLifepodInventorySize = args.Value;
                    break;
                default:
                    throw new ArgumentException($"Unrecognised choice id {args.Id}");
            }

            Config.Save();
        }

        public void OnToggleChanged(object sender, ToggleChangedEventArgs args)
        {
            switch (args.Id)
            {
                case FixLifepod:
                    Config.bFixLifepod = args.Value;
                    break;
                case FixRadio:
                    Config.bFixRadio = args.Value;
                    break;
                case UseLoadout:
                    Config.bUseDefaultLoadout = args.Value;
                    break;
                default:
                    throw new ArgumentException($"Unrecognised toggle id {args.Id}");
            }

            Config.Save();
        }
    }
}