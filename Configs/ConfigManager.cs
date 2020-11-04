using Essentials.Settings;
using Newtonsoft.Json;
using Obsidian.API;
using Obsidian.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace Essentials.Configs
{
    public class ConfigManager
    {
        public string Motd { get; private set; }
        public Config Config { get; private set; }
        public Dictionary<Guid, HomeConfig> PlayerHomes { get; set; } = new Dictionary<Guid, HomeConfig>();
        public Dictionary<String, WarpConfig> Warps { get; set; } = new Dictionary<String, WarpConfig>();

        public ConfigManager()
        {
            ReloadConfig();
        }

        #region MultiConfig

        #region LoadConfig
        public void LoadConfig()
        {
            #region Motd.txt
            LoadSingleConfig(EConfigs.Motd, Globals.Files.Motd, Globals.Defaults.Motd, out String motd);
            Motd = Globals.RenderColoredChatMessage(motd);
            #endregion

            #region Config.json
            LoadSingleConfig(EConfigs.Config, Globals.Files.Config, JsonConvert.SerializeObject(Globals.Defaults.Config), out String config);
            Config = JsonConvert.DeserializeObject<Config>(config);
            #endregion

            #region Homes
            var homes = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < homes.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(homes[i]), "N");
                LoadSingleConfig(EConfigs.Homes, homes[i], JsonConvert.SerializeObject(Globals.Defaults.Config), out String playerHomeData);
                PlayerHomes ??= new Dictionary<Guid, HomeConfig>();
                PlayerHomes.Add(uuid, JsonConvert.DeserializeObject<HomeConfig>(playerHomeData));
            }
            #endregion

            #region Warps
            var warps = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < warps.Count; i++)
            {
                var name = Globals.FileReader.GetFileNameWithoutExtension(warps[i]);
                LoadSingleConfig(EConfigs.Homes, warps[i], JsonConvert.SerializeObject(Globals.Defaults.Config), out String warpData);
                Warps ??= new Dictionary<String, WarpConfig>();
                Warps.Add(name, JsonConvert.DeserializeObject<WarpConfig>(warpData));
            }
            #endregion
        }
        #endregion

        #region ReloadConfig
        public void ReloadConfig()
        {
            SaveConfig();
            LoadConfig();
        }
        #endregion

        #region SaveConfig
        public void SaveConfig()
        {
            #region Motd.txt
            SaveSingleConfig(EConfigs.Motd, Globals.Files.Motd, Globals.Defaults.Motd, Motd);
            #endregion

            #region Config.json
            SaveSingleConfig(EConfigs.Config, Globals.Files.Config, JsonConvert.SerializeObject(Globals.Defaults.Config), Config);
            #endregion

            #region Homes
            var homes = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < homes.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(homes[i]), "N");
                SaveSingleConfig(EConfigs.Homes, homes[i], JsonConvert.SerializeObject(Globals.Defaults.Config), PlayerHomes);
            }
            #endregion

            #region Warps
            var warps = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < warps.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(warps[i]), "N");
                SaveSingleConfig(EConfigs.Homes, warps[i], JsonConvert.SerializeObject(Globals.Defaults.Config), Warps);
            }
            #endregion
        }
        #endregion

        #endregion

        #region SingleConfig

        #region LoadSingleConfig
        private void LoadSingleConfig(EConfigs type, string location, string defaultConfig, out String config)
        {
            config = null;
            try
            {
                config = Globals.FileReader.ReadAllText(location);
                Globals.Logger.LogWarning($"§7[Config]§r Config file §a{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r loaded.");
            }
            catch (Exception)
            {
                if (!Globals.FileReader.FileExists(location))
                {
                    Globals.Logger.LogWarning($"§7[Config]§r Config file §e{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r doesn't exists. Creating a new one.");
                    Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(defaultConfig));
                    config = Globals.FileReader.ReadAllText(location);
                }
                Globals.Logger.LogWarning($"§7[Config]§r Config file §c{location.Replace(Globals.Files.WorkingDirectory, "")}§r can't be loaded.");
            }
        }
        #endregion

        #region ReloadSingleConfig
        private void ReloadSingleConfig(EConfigs type, string location, string defaultConfig, out String config)
        {
            SaveSingleConfig(type, location, defaultConfig, null);
            LoadSingleConfig(type, location, defaultConfig, out config);
        }
        #endregion

        #region SaveSingleConfig
        private void SaveSingleConfig(EConfigs type, string location, string defaultConfig, object? config)
        {
            var configString = config != null ? (config is String ? (String)config : JsonConvert.SerializeObject(config, Formatting.Indented)) : defaultConfig;
            try
            {
                if (!Globals.FileReader.FileExists(location))
                {
                    Globals.Logger.LogWarning($"§7[Config]§r Config file §e{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r doesn't exists. Creating a new one.");
                    Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(configString));
                }
                else if (config != null)
                {
                    if (configString == defaultConfig)
                    {
                        Globals.Logger.Log($"§7[Config]§r Config file save §e{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r skipped. No changes were made.");
                    }
                    else
                    {
                        Globals.Logger.LogWarning($"§7[Config]§r Config file §e{type.ToString().ToLower()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r doesn't exists. Creating a new one.");
                        Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(configString));
                    }
                }
            }
            catch (Exception)
            {
                Globals.Logger.LogError($"§7[Config]§r Config file §c{location.Replace(Globals.Files.WorkingDirectory, "")}§r cannot be created.");
            }
        }
        #endregion

        #endregion

        #region AddWarp
        public bool AddWarp(String name, WarpConfig config, IPlayer executor)
        {
            var originalName = name;
            name = name.ReplaceSpecialCharactersWith("_");
            var warpLoc = Globals.Files.Warp($"{name}");
            String logMessage = null;
            try
            {
                Globals.Logger.Log($"§7[Config]§r Creating warp named §9{name.ToLower()}§r...");
                if (!Globals.FileWriter.FileExists(warpLoc))
                {
                    Globals.FileWriter.WriteAllText(warpLoc, JsonConvert.SerializeObject(config));
                    logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully created";
#if DEBUG || SNAPSHOT
                    logMessage += $" on location §a{warpLoc}";
#endif
                    logMessage += ".";
                }
                else //if (executor.HasPermission("essentials.who.knows.what.the.perm.for.it"))
                {
                    Globals.FileWriter.WriteAllText(warpLoc, JsonConvert.SerializeObject(config));
                    logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully overwritten by §e{executor.Username}§r.";
#if DEBUG || SNAPSHOT
                    logMessage += $" Warp location: §a{warpLoc}§r";
#endif
                    Globals.Logger.Log(logMessage);
#if DEBUG || SNAPSHOT
                    logMessage = $"Warps list previously had §b{Warps.Count}§r warps, ";
#endif
                    Warps.Add(name, config);
#if DEBUG || SNAPSHOT
                    logMessage += $"now it has §e{Warps.Count}§r.";
                    Globals.Logger.Log(logMessage);
#endif
                }
                //else if (!executor.HasPermission("essentials.who.knows.what.the.perm.for.it")) { await executor.SendMessageAsync(noperm); return false; }
            }
            catch (Exception ex)
            {
                logMessage = $"§7[Config]§r Creating warp named §c{name.ToLower()}§r";
#if DEBUG || SNAPSHOT
                logMessage += $", location: §c{warpLoc}§r";
#endif
                logMessage += $" thrown an unexpected exception during execution. The exception is the following:\r\n§c{ex}";
                
                Globals.Logger.LogError(logMessage);

                return false;
            }
            return true;
        }
        #endregion

        #region AddHome
        public void AddHome(IPlayer player, HomeConfig config)
        {
            var uuid = player.Uuid;
            var name = uuid.ToString().Replace("-", "");
            var homeLoc = Globals.Files.PlayerHome(uuid);

            Globals.Logger.Log($"§7[Config]§r Creating warp named §9{name.ToLower()}§r...");
            String logMessage = null;
            if (!Globals.FileWriter.FileExists(homeLoc))
            {
                Globals.FileWriter.WriteAllText(homeLoc, JsonConvert.SerializeObject(config));
                logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully created";
#if DEBUG || SNAPSHOT
                logMessage += $" on location §a{homeLoc}";
#endif
                logMessage += ".";
            }
            else //if (executor.HasPermission("essentials.who.knows.what.the.perm.for.it"))
            {
                Globals.FileWriter.WriteAllText(homeLoc, JsonConvert.SerializeObject(config));
                logMessage = $"§7[Config]§r Successfully overwrote §a{name.ToLower()}§r's home list.";
#if DEBUG || SNAPSHOT
                logMessage += $" File location: §a{homeLoc}";
#endif
                logMessage += ".";
                PlayerHomes.Add(uuid, config);
            }
        }
        #endregion
    }
}