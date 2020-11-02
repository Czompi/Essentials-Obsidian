using Essentials.Settings;
using System;
using System.Collections.Generic;

namespace Essentials.Configs
{
    public class ConfigManager
    {
        public string Motd { get; private set; }
        public static Dictionary<Guid, HomeConfig> PlayerHomes { get; set; }

        public ConfigManager()
        {
            if (!Globals.FileReader.FileExists(Globals.Files.Motd))
            {
                Globals.Logger.LogWarning("Config file §emotd.txt§r doesn't exists. Creating a new one.");
                Globals.FileWriter.WriteAllText(Globals.Files.Motd, Globals.RenderColoredChatMessage(Globals.Defaults.Motd));
            }
            Motd = Globals.RenderColoredChatMessage(Globals.FileReader.ReadAllText(Globals.Files.Motd));
            Globals.Logger.LogWarning("Config file §amotd.txt§r loaded.");
        }
    }
}