using Essentials.Extensions;
using Essentials.Settings;
using Obsidian.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Essentials.Configs
{
    public class ConfigManager
    {

        #region Properties
        public string Motd { get; private set; }
        //public GlobalConfig Config { get; private set; }
        public Dictionary<Guid, HomeConfig> PlayerHomes { get; set; } = new Dictionary<Guid, HomeConfig>();
        public Dictionary<string, WarpConfig> Warps { get; set; } = new Dictionary<string, WarpConfig>();
        #endregion

        public ConfigManager()
        {
            ReloadConfig();
        }

        #region MultiConfig

        #region LoadConfig
        public void LoadConfig()
        {
            #region Motd.txt
            LoadSingleConfig(EConfigs.Motd, Globals.Files.Motd, Globals.Defaults.Motd, out string motd);
            Motd = Globals.RenderColoredChatMessage(motd);
            #endregion

            #region Config.json
            /*LoadSingleConfig(EConfigs.Config, Globals.Files.Config, JsonSerializer.Serialize(Globals.Defaults.Config), out string config);
            Config = JsonSerializer.Deserialize<Config>(config);*/
            #endregion

            #region Homes
            var homes = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < homes.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(homes[i]), "N");
                LoadSingleConfig(EConfigs.Homes, homes[i], JsonSerializer.Serialize(Globals.Defaults.Config, Globals.JsonSerializerOptions), out string playerHomeData);
                PlayerHomes ??= new Dictionary<Guid, HomeConfig>();
                PlayerHomes.Add(uuid, JsonSerializer.Deserialize<HomeConfig>(playerHomeData));
            }
            #endregion

            #region Warps
            var warps = Globals.FileReader.GetDirectoryFiles(Globals.Files.WarpsDir).ToList().Where(x => x.ToLower().EndsWith(".json")).ToList();
            //warps = warps.Where(x => x.ToLower().EndsWith(".json")).ToList();
            for (int i = 0; i < warps.Count; i++)
            {
                var name = Globals.FileReader.GetFileNameWithoutExtension(warps[i]);
                LoadSingleConfig(EConfigs.Warps, warps[i], JsonSerializer.Serialize(Globals.Defaults.Config), out string warpData);
                Warps ??= new Dictionary<String, WarpConfig>();
                Warps.Add(name, JsonSerializer.Deserialize<WarpConfig>(warpData));
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
            //SaveSingleConfig(EConfigs.Config, Globals.Files.Config, JsonSerializer.Serialize(Globals.Defaults.Config, Globals.JsonSerializerOptions), Config);
            #endregion

            #region Homes
            var homes = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < homes.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(homes[i]), "N");
                SaveSingleConfig(EConfigs.Homes, homes[i], JsonSerializer.Serialize(Globals.Defaults.Config, Globals.JsonSerializerOptions), PlayerHomes);
            }
            #endregion

            #region Warps
            var warps = Globals.FileReader.GetDirectoryFiles(Globals.Files.HomesDir).Where(x => x.EndsWith(".json") && Guid.TryParseExact(Globals.FileReader.GetFileNameWithoutExtension(x), "N", out Guid guid)).ToList();
            for (int i = 0; i < warps.Count; i++)
            {
                var uuid = Guid.ParseExact(Globals.FileReader.GetFileNameWithoutExtension(warps[i]), "N");
                SaveSingleConfig(EConfigs.Homes, warps[i], JsonSerializer.Serialize(Globals.Defaults.Config, Globals.JsonSerializerOptions), Warps);
            }
            #endregion
        }
        #endregion

        #endregion

        #region SingleConfig

        #region LoadSingleConfig
        private void LoadSingleConfig(EConfigs type, string location, string defaultConfig, out string config)
        {
            config = null;
            try
            {
                config = Globals.FileReader.ReadAllText(location);
                Globals.Logger.Log($"§7[Config/{type.ToString()}]§r Config file §a{Globals.FileReader.GetFileName(location)}§r loaded.");
            }
            catch (Exception ex)
            {
                /*if (!Globals.FileReader.FileExists(location))
                {
                    Globals.Logger.LogWarning($"§7[Config]§r Config file §e{type.ToString()}.{Globals.FileReader.GetExtension(location).ToLower().Replace(".", "")}§r doesn't exists. Creating a new one.");
                    Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(defaultConfig));
                    try
                    {
                        config = Globals.FileReader.ReadAllText(location);
                    }
                    catch (Exception exInt)
                    {
                        Globals.Logger.LogWarning($"§7[Config]§r Config file §c{location.Replace(Globals.Files.WorkingDirectory, "")}§r can't be loaded.");
#if DEBUG || SNAPSHOT
                        Globals.Logger.LogWarning($"§7[Config]§r Error: §c{exInt}");
#endif
                    }
                }*/
                Globals.Logger.LogWarning($"§7[Config/{type.ToString()}]§r Config file §c{Globals.FileReader.GetFileName(location)}§r can't be loaded.");
#if DEBUG || SNAPSHOT
                Globals.Logger.LogDebug($"§7[Config/{type.ToString()}]§r Error: §c{ex}");
#endif
            }
        }
        #endregion

        #region ReloadSingleConfig
        private void ReloadSingleConfig(EConfigs type, string location, string defaultConfig, out string config)
        {
            SaveSingleConfig(type, location, defaultConfig, null);
            LoadSingleConfig(type, location, defaultConfig, out config);
        }
        #endregion

        #region SaveSingleConfig
        private void SaveSingleConfig(EConfigs type, string location, string defaultConfig, object? config)
        {
            var configString = config != null ? (config is string ? (String)config : JsonSerializer.Serialize(config, Globals.JsonSerializerOptions)) : defaultConfig;
            try
            {
                if (!Globals.FileReader.FileExists(location))
                {
                    Globals.Logger.Log($"§7[Config/{type.ToString()}]§r Config file §e{Globals.FileReader.GetFileName(location)}§r doesn't exists. Creating a new one.");
                    Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(configString));
                }
                else if (config != null)
                {
                    if (configString == defaultConfig)
                    {
                        Globals.Logger.LogWarning($"§7[Config/{type.ToString()}]§r Config file save §e{Globals.FileReader.GetFileName(location)}§r skipped. No changes were made.");
                    }
                    else
                    {
                        Globals.Logger.Log($"§7[Config/{type.ToString()}]§r Config file §a{Globals.FileReader.GetFileName(location)}§r saved!");
                        Globals.FileWriter.WriteAllText(location, Globals.RenderColoredChatMessage(configString));
                    }
                }
            }
            catch (Exception)
            {
                Globals.Logger.LogError($"§7[Config/{type.ToString()}]§r Config file §c{location.Replace(Globals.Files.WorkingDirectory, "")}§r cannot be created.");
            }
        }
        #endregion

        #endregion

        #region AddWarp
        public bool AddWarp(string name, WarpConfig config, IPlayer executor)
        {
            var originalName = name;
            name = name.ReplaceSpecialCharactersWith("_");
            var warpLoc = Globals.Files.Warp($"{name}");
            string logMessage = null;
            try
            {
                Globals.Logger.Log($"§7[Config]§r Creating warp named §9{name.ToLower()}§r...");
                if (!Globals.FileWriter.FileExists(warpLoc))
                {
                    Globals.FileWriter.WriteAllText(warpLoc, JsonSerializer.Serialize(config, Globals.JsonSerializerOptions));
                    logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully created";
#if DEBUG || SNAPSHOT
                    logMessage += $" on location §a{warpLoc}";
#endif
                    logMessage += ".";
                }
                else //if (executor.HasPermission("essentials.who.knows.what.the.perm.for.it"))
                {
                    Globals.FileWriter.WriteAllText(warpLoc, JsonSerializer.Serialize(config, Globals.JsonSerializerOptions));
                    logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully overwritten by §a{executor.Username}§r.";
#if DEBUG || SNAPSHOT
                    logMessage += $" Warp location: §a{warpLoc}§r";
#endif
                    Globals.Logger.Log(logMessage);
#if DEBUG || SNAPSHOT
                    logMessage = $"Warps list previously had §b{Warps.Count}§r warp{(Warps.Count > 1 ?"s":"")}, ";
#endif
                    Warps.Add(name, config);
#if DEBUG || SNAPSHOT
                    logMessage += $"now it has §e{Warps.Count}§r.";
                    Globals.Logger.LogDebug(logMessage);
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
        public bool AddHome(IPlayer player, Home home)
        {
            var uuid = player.Uuid;
            var name = uuid.ToString().Replace("-", "");
            var homeLoc = Globals.Files.PlayerHome(uuid);
            string logMessage = null;

            try
            {
                Globals.Logger.Log($"§7[Config]§r Creating home named §9{name.ToLower()}§r...");
                if (!Globals.FileWriter.FileExists(homeLoc))
                {
                    logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully created";
#if DEBUG || SNAPSHOT
                    logMessage += $" on location §a{homeLoc}";
#endif
                    logMessage += ".";
                    var homes = new HomeConfig
                    {
                        home
                    };
                    PlayerHomes.Add(uuid, homes);
                    Globals.FileWriter.WriteAllText(homeLoc, JsonSerializer.Serialize(PlayerHomes[uuid], Globals.JsonSerializerOptions));
                    Globals.Logger.Log(logMessage);
                }
                else //if (executor.HasPermission("essentials.who.knows.what.the.perm.for.it"))
                {
                    logMessage = $"§7[Config]§r Warp named §a{name.ToLower()}§r successfully overwritten by §a{player.Username}§r.";
#if DEBUG || SNAPSHOT
                    logMessage += $" home location: §a{homeLoc}§r";
#endif
                    Globals.Logger.Log(logMessage);
#if DEBUG || SNAPSHOT
                    logMessage = $"§a{player.Username}§r's home list previously had §b{PlayerHomes[uuid].Count}§r home{(PlayerHomes[uuid].Count > 1 ? "s" : "")}, ";
#endif
                    PlayerHomes[uuid].Add(home);
                    Globals.FileWriter.WriteAllText(homeLoc, JsonSerializer.Serialize(PlayerHomes[uuid], Globals.JsonSerializerOptions));
#if DEBUG || SNAPSHOT
                    logMessage += $"now it has §e{PlayerHomes[uuid].Count}§r.";
                    Globals.Logger.LogDebug(logMessage);
#endif
                }
            }
            catch (Exception ex)
            {
                logMessage = $"§7[Config]§r Creating warp named §c{name.ToLower()}§r";
#if DEBUG || SNAPSHOT
                logMessage += $", location: §c{homeLoc}§r";
#endif
                logMessage += $" thrown an unexpected exception during execution. The exception is the following:\r\n§c{ex}";

                Globals.Logger.LogError(logMessage);

                return false;
            }
            return true;
        }
        #endregion

    }
}