using Essentials.Commands;
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
        [Inject] public IFileWriter IFileWriter { get; set; }
        // One of server messages, called when an event occurs
        public async Task OnLoad(IServer server)
        {
            Logger.Log($"Essentials §e{Globals.VersionFull}§r loading...");
            Globals.Logger = Logger;
            Globals.FileReader = IFileReader;
            Globals.FileWriter = IFileWriter;
            Globals.Configs = new ConfigManager();
            Logger.Log($"Global things §asuccessfully§r assigned.");
            Logger.Log($"Registering §ecommands§r...");
            server.RegisterCommandClass<EssentialsCommandModule>();
            Logger.Log($"Commands §asuccessfully§r registered...");
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
