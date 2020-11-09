﻿using Essentials.Commands;
using Essentials.Configs;
using Essentials.Extensions;
using Essentials.Settings;
using Essentials.Settings.Lang;
using Obsidian.API;
using Obsidian.API.Events;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using Obsidian.CommandFramework.Entities;
using System.Collections.Generic;
using System.Linq;
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
        [Inject] public IFileWriter IFileWriter { get; set; }
        // One of server messages, called when an event occurs
        public async Task OnLoad(IServer server)
        {
            Globals.PluginInfo = Info;
            Logger.Log($"Essentials §9{Globals.VersionFull}§r loading...");

            Logger.Log($"§7[Global]§r Global things are §9loading§r...");
            Globals.Server = server;
            Globals.Logger = Logger;
            Globals.FileReader = IFileReader;
            Globals.FileWriter = IFileWriter;
            Logger.Log($"§7[Global]§r Global things §asuccessfully§r assigned.");

            Logger.Log($"§7[Language]§r §9Detecting§r language...");
            Globals.Language = new LanguageManager();
            Logger.Log($"§7[Language]§r Language loaded §asuccessfully§r.");

            Logger.Log($"§7[Config]§r Config files are §9loading§r...");
            Globals.Configs = new ConfigManager();
            Logger.Log($"§7[Config]§r Config files are loaded §asuccessfully§r.");

            Logger.Log($"§7[Commands]§r Registering §9commands§r...");
            server.RegisterCommandClass<EssentialsCommandModule>();
            Logger.Log($"§7[Commands]§r Command module §aEssentialsCommandModule§r registered.");
            server.RegisterCommandClass<GamemodeCommandModule>();
            Logger.Log($"§7[Commands]§r Command module §aGamemodeCommandModule§r registered.");
            server.RegisterCommandClass<StatisticsCommandModule>();
            Logger.Log($"§7[Commands]§r Command module §aStatisticsCommandModule§r registered.");
            server.RegisterCommandClass<HomeCommandModule>();
            Logger.Log($"§7[Commands]§r Command module §aHomeCommandModule§r registered.");
            server.RegisterCommandClass<WarpCommandModule>();
            Logger.Log($"§7[Commands]§r Command module §aWarpCommandModule§r registered.");
            Logger.Log($"§7[Commands]§r Commands §asuccessfully§r registered...");

            Logger.Log($"Essentials §a{Globals.VersionFull}§r loaded!");
            await Task.CompletedTask;
        }

        /*public async Task OnIncomingChatMessage(IncomingChatMessageEventArgs e)
        {
            var player = e.Player;
            var message = e.Message;
            
        }*/

        public async Task OnPlayerJoin(PlayerJoinEventArgs e)
        {
            var player = e.Player;
            var server = e.Server;

            await player.SendMessageAsync(
                Globals.Configs.Motd.ReplaceKeywords(player));
        }
    }
}
