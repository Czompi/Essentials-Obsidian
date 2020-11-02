using Essentials.Configs;
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
        private readonly string Version = "0.0.1-DEV";

        #region essentials
        [Command("essentials", "ess")]
        [CommandInfo("Essentials available commands.")]
        public async Task EssentialsAsync(ObsidianContext Context)
        {
            var commands = ChatMessage.Simple("");
            var usage = new ChatMessage
            {
                Text = $"{ChatColor.Red}/essentials <reload/debug/commands>",
                ClickEvent = new TextComponent
                {
                    Action = ETextAction.SuggestCommand,
                    Value = $"/essentials "
                },
                HoverEvent = new TextComponent
                {
                    Action = ETextAction.ShowText,
                    Value = $"Click to suggest the command"
                }
            };

            var prefix = new ChatMessage
            {
                Text = $"{ChatColor.Red}Usage: "
            };

            commands.AddExtra(prefix);
            commands.AddExtra(usage);

            await Context.Player.SendMessageAsync(commands);
        }

        [CommandOverload]
        public async Task EssentialsAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            var chatMessage = ChatMessage.Simple("");
            switch (args[0].ToLower())
            {
                #region commands
                case "commands":
                    // List all of the commands of this plugin
                    //It will be necessary when the plugin system works fully and I does not really need to hard code this into the server to work properly.
                    chatMessage = ChatMessage.Simple("");
                    var cmds_prefix = new ChatMessage
                    {
                        Text = $"{ChatColor.Gray}Essentials {ChatColor.Red}{Version}{ChatColor.Gray} commands:"
                    };
                    chatMessage.AddExtra(cmds_prefix);
                    var cmds_list = ChatMessage.Simple("");
                    foreach (var cmd in Commands)
                    {
                        var command = cmd.Key.Contains(" ") ? string.Join(" ", cmd.Key.Split(" ").Where(x => !x.StartsWith("<"))) + " " : cmd.Key;
                        var commandName = new ChatMessage
                        {
                            Text = $"\n{ChatColor.Red}/{cmd.Key}",
                            ClickEvent = new TextComponent
                            {
                                Action = ETextAction.SuggestCommand,
                                Value = $"/{command}"
                            },
                            HoverEvent = new TextComponent
                            {
                                Action = ETextAction.ShowText,
                                Value = $"Click to suggest the command"
                            }
                        };

                        var commandInfo = new ChatMessage
                        {
                            Text = $"{ChatColor.Gray}:{ChatColor.Reset} {cmd.Value}{ChatColor.Reset}"
                        };

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
                    chatMessage = ChatMessage.Simple("");
                    var message = new ChatMessage
                    {
                        Text = $"{ChatColor.Gray}Essentials {ChatColor.Red}{Version}{ChatColor.Gray} successfully reloaded."
                    };

                    chatMessage.AddExtra(message);
                    break;
                #endregion

                #region Not valid args[0]
                default:
                    chatMessage = ChatMessage.Simple($"{args[0]}");
                    var prefix = new ChatMessage
                    {
                        Text = $"{ChatColor.Red}Usage: "
                    };

                    var usage = new ChatMessage
                    {
                        Text = $"{ChatColor.Red}/essentials <reload/debug/commands>",
                        ClickEvent = new TextComponent
                        {
                            Action = ETextAction.SuggestCommand,
                            Value = $"/essentials "
                        },
                        HoverEvent = new TextComponent
                        {
                            Action = ETextAction.ShowText,
                            Value = $"Click to suggest the command"
                        }
                    };

                    chatMessage.AddExtra(prefix);
                    chatMessage.AddExtra(usage);
                    break;
                    #endregion
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

        #region gm
        [Command("gm")]
        [CommandInfo("Switch gamemode.")]
        public async Task GamemodeAsync(ObsidianContext Context)
        {
            var chatMessage = SendCommandUsage("gm");
            await Context.Player.SendMessageAsync(chatMessage);
        }

        [CommandOverload]
        public async Task GamemodeAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var chatMessage = ChatMessage.Simple("");
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            Gamemode? gamemode = null;
            if (args.Count == 1)
            {
                if (args[0].IsInteger())
                {
                    var gmInt = Int32.Parse(args[0]);
                    if (gmInt > 3 && gmInt < 0)
                    {
                        chatMessage = SendCommandUsage("gm");
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
                    Context.Player.Gamemode = gamemode.Value;
                    chatMessage = ChatMessage.Simple($"{ChatColor.Reset}Your game mode set to {ChatColor.Red}{gamemode.Value}{ChatColor.Reset}.");
                }
            }
            else
            {
                chatMessage = SendCommandUsage("gm");
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

    }
}
