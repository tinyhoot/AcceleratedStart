using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HootLib;

namespace AcceleratedStart
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.snmodding.nautilus", "1.0")]
    public class Initialiser : BaseUnityPlugin
    {
        public const string GUID = "com.github.tinyhoot.AcceleratedStart";
        public const string NAME = "Accelerated Start";
        public const string VERSION = "2.0.1";
        internal static ManualLogSource _log;
        internal static Config _config;
        internal static Dictionary<string, List<TechType>> _loadouts;

        public void Awake()
        {
            _log = Logger;
            // Parse all loadouts from their directory.
            _loadouts = LoadoutParser.ParseLoadouts(GetLoadoutDirectory());
            _log.LogInfo($"Loaded {_loadouts.Count} loadouts.");
            // Build the in-game mod menu.
            _config = new Config(Hootils.GetConfigFileName(NAME), Info.Metadata);
            _config.RegisterModOptions(NAME);

            var harmony = new Harmony(GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Get the selected loadout from the config.
        /// </summary>
        /// <returns>The currently selected loadout or null if something goes wrong.</returns>
        internal static List<TechType> GetActiveLoadout()
        {
            if (_loadouts is null || _loadouts.Count == 0)
                return null;
            return _loadouts.GetOrDefault(_config.CurrentLoadout.Value, null);
        }

        internal static string GetLoadoutDirectory()
        {
            return Path.Combine(Hootils.GetModDirectory(), "Loadouts");
        }
    }
}
