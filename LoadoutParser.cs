using System;
using System.Collections.Generic;
using System.IO;

namespace AcceleratedStart
{
    internal class LoadoutParser
    {
        /// <summary>
        /// Parse each of the loadouts contained in the specified directory.
        /// </summary>
        public static Dictionary<string, List<TechType>> ParseLoadouts(string directory)
        {
            Dictionary<string, List<TechType>> loadouts = new Dictionary<string, List<TechType>>();
            foreach (string filePath in Directory.GetFiles(directory))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                // Skip the tutorial file.
                if (fileName.StartsWith("HowTo"))
                    continue;
                Initialiser._log.LogDebug($"Parsing loadout {fileName}");
                try
                {
                    List<TechType> loadout = ParseFile(filePath);
                    loadouts.Add(fileName, loadout);
                }
                catch (Exception ex)
                {
                    Initialiser._log.LogError($"Failed to parse loadout from file {Path.GetFileName(filePath)}!");
                    Initialiser._log.LogError(ex);
                }
            }

            return loadouts;
        }

        private static List<TechType> ParseFile(string path)
        {
            List<TechType> loadout = new List<TechType>();
            StreamReader file = new StreamReader(path);
            
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                // Skip comments and empty lines.
                if (line is null || line.Length == 0 || line.StartsWith("#"))
                    continue;
                
                Tuple<TechType, int> item = ParseLine(line);
                // If the parsing failed, skip the line.
                if (item.Item1.Equals(TechType.None) || item.Item2 < 1)
                    continue;
                // Add the item to the loadout as many times as needed.
                for (int i=0; i<item.Item2; i++)
                {
                    loadout.Add(item.Item1);
                }
            }

            return loadout;
        }

        /// <summary>
        /// Parse a single line to a TechType and its specified number.
        /// </summary>
        private static Tuple<TechType, int> ParseLine(string line)
        {
            line = line.Trim();
            // A line may or may not contain a trailing comma with a quantifier.
            string[] elements = line.Split(',');
            TechType techType = StringToTechType(elements[0].Trim());
            int number = 1;
            if (elements.Length > 1)
                number = Int32.Parse(elements[1].Trim());

            return new Tuple<TechType, int>(techType, number);
        }

        private static TechType StringToTechType(string value)
        {
            if (Enum.TryParse(value, true, out TechType result))
                return result;
            
            Initialiser._log.LogError($"Failed to parse item to TechType: {value}");
            return TechType.None;
        }
    }
}