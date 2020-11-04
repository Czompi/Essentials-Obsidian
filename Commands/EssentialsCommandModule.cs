using Essentials.Settings;
using Obsidian.API;
using Obsidian.CommandFramework.Attributes;
using Obsidian.CommandFramework.Entities;
using Obsidian.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Essentials.Commands
{
    public class EssentialsCommandModule : BaseCommandClass
    {

        #region essentials
        [Command("essentials", "ess")]
        [CommandInfo("Essentials available commands.", "/essentials <reload|debug|commands>")]
        public async Task EssentialsAsync(ObsidianContext Context) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/essentials <reload/debug/commands>"));

        [CommandOverload]
        public async Task EssentialsAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            var chatMessage = IChatMessage.Simple("");
            switch (args[0].ToLower())
            {
                #region commands
                case "commands":
                    // List all of the commands of this plugin
                    //It will be necessary when the plugin system works fully and I does not really need to hard code this into the server to work properly.
                    chatMessage = IChatMessage.Simple("");
                    var cmds_prefix = IChatMessage.CreateNew();
                    cmds_prefix.Text = $"{ChatColor.Gray}Essentials {ChatColor.Red}{Globals.VersionFull}{ChatColor.Gray} commands:";
                    chatMessage.AddExtra(cmds_prefix);
                    var cmds_list = IChatMessage.Simple("");
                    foreach (var cmd in Globals.Commands)
                    {
                        var commandName = IChatMessage.Simple("\n");
                        commandName.AddExtra(Globals.RenderClickableCommand(cmd.Key));

                        var commandInfo = IChatMessage.CreateNew();
                        commandInfo.Text = $"{ChatColor.Gray}:{ChatColor.Reset} {cmd.Value}{ChatColor.Reset}";

                        cmds_list.AddExtra(commandName);
                        cmds_list.AddExtra(commandInfo);
                    }
                    chatMessage.AddExtra(cmds_list);
                    break;
                #endregion

                #region debug
                case "debug":
                    // What the h*ll does this arg does?!
                    break;
                #endregion

                #region reload
                case "reload":
                    //It will be necessary when the plugin system works fully and I does not really need to hard code this into the server to work properly.
                    chatMessage = IChatMessage.Simple("");
                    var message = IChatMessage.CreateNew();
                    message.Text = $"{ChatColor.Gray}Essentials {ChatColor.Red}{Globals.VersionFull}{ChatColor.Gray} successfully reloaded.";

                    chatMessage.AddExtra(message);
                    break;
                #endregion

                #region Not valid args[0]
                default:
                    chatMessage = Globals.RenderCommandUsage("/essentials <reload/debug/commands>");
                    break;
                #endregion
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

        #region gm
        [Command("gm")]
        [CommandInfo("Switch gamemode.", "/gm <0|1|2|3>")]
        public async Task GamemodeAsync(ObsidianContext Context) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/gm <0|1|2|3>"));

        [CommandOverload]
        public async Task GamemodeAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var chatMessage = IChatMessage.Simple("");
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            Gamemode? gamemode = null;
            if (args.Count == 1)
            {
                if (args[0].IsInteger())
                {
                    var gmInt = Int32.Parse(args[0]);
                    if (gmInt > 3 && gmInt < 0)
                    {
                        chatMessage = Globals.RenderCommandUsage("/gm <0|1|2|3>");
                    }
                    else
                    {
                        gamemode = (Gamemode)gmInt;
                    }
                }
                else if (args[0].ToLower() == "creative" || args[0].ToLower() == "survival" || args[0].ToLower() == "spectator" || args[0].ToLower() == "adventure")
                {
                    gamemode = (Gamemode)Enum.Parse(typeof(Gamemode), args[0], true);
                }
                if (gamemode != null)
                {
                    await Context.Player.SetGamemodeAsync(gamemode.Value);
                    chatMessage = IChatMessage.Simple($"{ChatColor.Reset}Your game mode set to {ChatColor.Red}{gamemode.Value}{ChatColor.Reset}.");
                }
            }
            else
            {
                chatMessage = Globals.RenderCommandUsage("/gm <0|1|2|3>");
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

    }
}
