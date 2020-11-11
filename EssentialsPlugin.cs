using Essentials.Commands;
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
            Logger.Log($"Essentials §9{Globals.VersionFull}{ChatColor.Reset} loading...");

            Logger.Log($"§7[Global]{ChatColor.Reset} Global things are §9loading{ChatColor.Reset}...");
            Globals.Server = server;
            Globals.Logger = Logger;
            Globals.FileReader = IFileReader;
            Globals.FileWriter = IFileWriter;
            Logger.Log($"§7[Global]{ChatColor.Reset} Global things {ChatColor.BrightGreen}successfully{ChatColor.Reset} assigned.");

            Logger.Log($"§7[Language]{ChatColor.Reset} §9Detecting{ChatColor.Reset} language...");
            Globals.Language = new LanguageManager();
            Logger.Log($"§7[Language]{ChatColor.Reset} Language loaded {ChatColor.BrightGreen}successfully{ChatColor.Reset}.");

            Logger.Log($"§7[Config]{ChatColor.Reset} Config files are §9loading{ChatColor.Reset}...");
            Globals.Configs = new ConfigManager();
            Logger.Log($"§7[Config]{ChatColor.Reset} Config files are loaded {ChatColor.BrightGreen}successfully{ChatColor.Reset}.");

            Logger.Log($"§7[Commands]{ChatColor.Reset} Registering §9commands{ChatColor.Reset}...");
            server.RegisterCommandClass<EssentialsCommandModule>();
            Logger.Log($"§7[Commands]{ChatColor.Reset} Command module {ChatColor.BrightGreen}EssentialsCommandModule{ChatColor.Reset} registered.");
            server.RegisterCommandClass<GamemodeCommandModule>();
            Logger.Log($"§7[Commands]{ChatColor.Reset} Command module {ChatColor.BrightGreen}GamemodeCommandModule{ChatColor.Reset} registered.");
            server.RegisterCommandClass<StatisticsCommandModule>();
            Logger.Log($"§7[Commands]{ChatColor.Reset} Command module {ChatColor.BrightGreen}StatisticsCommandModule{ChatColor.Reset} registered.");
            server.RegisterCommandClass<HomeCommandModule>();
            Logger.Log($"§7[Commands]{ChatColor.Reset} Command module {ChatColor.BrightGreen}HomeCommandModule{ChatColor.Reset} registered.");
            server.RegisterCommandClass<WarpCommandModule>();
            Logger.Log($"§7[Commands]{ChatColor.Reset} Command module {ChatColor.BrightGreen}WarpCommandModule{ChatColor.Reset} registered.");
            Logger.Log($"§7[Commands]{ChatColor.Reset} Commands {ChatColor.BrightGreen}successfully{ChatColor.Reset} registered...");

            Logger.Log($"Essentials {ChatColor.BrightGreen}{Globals.VersionFull}{ChatColor.Reset} loaded!");
#if DEBUG
            Logger.Log($"{ChatColor.DarkGreen}// Lot of things are loaded here...");
#endif
            await Task.CompletedTask;
        }

        /*public async Task OnIncomingChatMessage(IncomingChatMessageEventArgs e)
        {
            var player = e.Player;
            var message = e.Message;
            await e.Server.BroadcastAsync($"<{player.Username}> {message}");
            e.Handled = true;
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
