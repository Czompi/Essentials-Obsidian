﻿//using Essentials.Commands;
using Essentials.Configs;
using Essentials.Settings;
using Obsidian.API;
using Obsidian.API.Events;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using System.Threading.Tasks;

namespace Essentials.Plugin
{
    [Plugin(Name = "Essentials", Version = "0.0.1",
            Authors = "Czompi", Description = "Remake of the famous Essentials(X) Bukkit/Spigot plugin for Obsidian.",
            ProjectUrl = "https://github.com/Czompi/Essentials-Obsidian")]
    public class EssentialsPlugin : PluginBase
    {
        // Any interface from Obsidian.Plugins.Services can be injected into properties
        [Inject] public ILogger Logger { get; set; }
        [Inject] public IFileReader IFileReader { get; set; }
        [Inject] public IPluginInfo IPluginInfo { get; set; }
        [Inject] public IFileWriter IFileWriter { get; set; }
        // One of server messages, called when an event occurs
        public async Task OnLoad(IServer server)
        {
            Globals.PluginInfo = Info;
            Logger.Log($"Essentials §6{Globals.VersionFull}§r loading...");

            Logger.Log($"§7[Global]§r §6Loading§r global things...");
            Globals.Logger = Logger;
            Globals.FileReader = IFileReader;
            Globals.FileWriter = IFileWriter;
            Globals.Configs = new ConfigManager();
            Logger.Log($"§7[Global]§r Global things §asuccessfully§r assigned.");

            Logger.Log($"§7[Commands]§r Registering §6commands§r...");
            Logger.Log($"§7[Commands]§r Skipping due to missing §7ChatMessage§r.");
            //server.RegisterCommandClass<EssentialsCommandModule>();
            //server.RegisterCommandClass<HomeCommandModule>();
            Logger.Log($"§7[Commands]§r Commands §asuccessfully§r registered...");

            Logger.Log($"Essentials §a{Globals.VersionFull}§r loaded!");
            await Task.CompletedTask;
        }

        public async Task OnPlayerJoin(PlayerJoinEventArgs e)
        {
            var player = e.Player;
            var server = e.Server;

            await player.SendMessageAsync(
                Globals.Configs.Motd
                    .Replace("{PLAYER}", player.Username)
                    .Replace("{WORLDTIME12}", "worldtime12:null")
                    .Replace("{WORLDTIME24}","worldtime24:null")
                    .Replace("{ONLINE}","onlinetime:null")
            );
        }
    }
}
