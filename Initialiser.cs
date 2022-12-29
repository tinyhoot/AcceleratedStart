using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace AcceleratedStart
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.ahk1221.smlhelper", "2.15")]
    public class Initialiser : BaseUnityPlugin
    {
        public const string GUID = "com.github.tinyhoot.AcceleratedStart";
        public const string NAME = "Accelerated Start";
        public const string VERSION = "2.0.0";
        internal static readonly ManualLogSource _log = BepInEx.Logging.Logger.CreateLogSource(NAME);
        internal static Config _config;
        internal static Dictionary<string, List<TechType>> _loadouts;
        
        public void Awake()
        {
            // Parse all loadouts from their directory.
            _loadouts = LoadoutParser.ParseLoadouts(GetLoadoutDirectory());
            _log.LogInfo($"Loaded {_loadouts.Count} loadouts.");
            // Build the in-game mod menu.
            ConfigModOptions modOptions = new ConfigModOptions(NAME);
            OptionsPanelHandler.RegisterModOptions(modOptions);
            _config = modOptions.Config;
            
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
            return _loadouts.GetOrDefault(_config.sCurrentLoadout, null);
        }

        internal static string GetLoadoutDirectory()
        {
            return Path.Combine(GetModDirectory(), "Loadouts");
        }
        
        internal static string GetModDirectory()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location).Directory?.FullName;
        }
    }
}
