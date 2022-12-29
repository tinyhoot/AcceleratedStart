using System.Collections.Generic;
using SMLHelper.V2.Json;

namespace AcceleratedStart
{
    internal class Config : ConfigFile
    {
        public bool bFixLifepod = true;
        public bool bFixRadio = true;
        public bool bStartHealed = true;
        public bool bUseDefaultLoadout = true;
        public string sCurrentLoadout = "default";
        public string sLifepodInventorySize = "large";
        // All valid options for lifepod inventory sizes.
        internal readonly Dictionary<string, int[]> _inventorySizes = new Dictionary<string, int[]>
        {
            { "tiny", new[] { 4, 4 } },
            { "small", new[] { 4, 6 } },
            { "default", new[] { 4, 8 } },
            { "large", new[] { 6, 10 } },
            { "huge", new[] { 8, 10 } },
        };
    }
}