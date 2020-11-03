using Essentials.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Essentials.Configs
{
    public class ConfigManager
    {
        public string Motd { get; private set; }
        public Config Config { get; private set; }
        public Dictionary<Guid, HomeConfig> PlayerHomes { get; set; } = new Dictionary<Guid, HomeConfig>();
        public Dictionary<Guid, WarpConfig> Warps { get; set; } = new Dictionary<Guid, WarpConfig>();

        public ConfigManager()
        {
            #region Motd.txt
            LoadConfig(EConfigs.Motd, Globals.Files.Motd, Globals.Defaults.Motd, out String motd);
            Motd = Globals.RenderColoredChatMessage(motd);
            #endregion

            #region Config.json
            LoadConfig(EConfigs.Config, Globals.Files.Config, JsonConvert.SerializeObject(Globals.Defaults.Config), out String config);
            Config = JsonConvert.DeserializeObject<Config>(config);
            #endregion

            #region Homes
            var homes = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < homes.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(homes[i]), "N");
                LoadConfig(EConfigs.Homes, homes[i], JsonConvert.SerializeObject(Globals.Defaults.Config), out String playerHomeData);
                PlayerHomes ??= new Dictionary<Guid, HomeConfig>();
                PlayerHomes.Add(uuid, JsonConvert.DeserializeObject<HomeConfig>(playerHomeData));
            }
            #endregion

            #region Warps
            var warps = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < warps.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(warps[i]), "N");
                LoadConfig(EConfigs.Homes, warps[i], JsonConvert.SerializeObject(Globals.Defaults.Config), out String warpData);
                Warps ??= new Dictionary<Guid, WarpConfig>();
                Warps.Add(uuid, JsonConvert.DeserializeObject<WarpConfig>(warpData));
            }
            #endregion
        }

        #region LoadConfig
        private void LoadConfig(EConfigs type, string location, string defaultConfig, out String config)
        {
            config = null;
            try
            {
                if (!Globals.FileReader.FileExists(location))
                {
                    Globals.Logger.LogWarning($"§7[Config]§r Config file §e{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r doesn't exists. Creating a new one.");
                    Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(defaultConfig));
                }
                config = Globals.FileReader.ReadAllText(location);
                Globals.Logger.LogWarning($"§7[Config]§r Config file §a{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r loaded.");
            }
            catch (Exception)
            {
                if (!Globals.FileReader.FileExists(location))
                {
                    Globals.Logger.LogWarning($"§7[Config]§r Config file §e{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r doesn't exists. Creating a new one.");
                    Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(defaultConfig));
                }
                Globals.Logger.LogWarning($"§7[Config]§r Config file §c{location.Replace(Globals.Files.WorkingDirectory, "")}§r can't be loaded.");
            }
        }
        #endregion
    }
}