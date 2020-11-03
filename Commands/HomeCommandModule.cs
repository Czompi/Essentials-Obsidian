using Essentials.Configs;
using Essentials.Settings;
using Obsidian.API;
using Obsidian.CommandFramework.Attributes;
using Obsidian.CommandFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Essentials.Commands
{
    public class HomeCommandModule : BaseCommandClass
    {
        #region home
        [Command("home")]
        [CommandInfo("Switch gamemode.")]
        public async Task HomeAsync(ObsidianContext Context)
        {
            var chatMessage = Globals.RenderCommandUsage("home");
            await Context.Player.SendMessageAsync(chatMessage);
        }

        [CommandOverload]
        public async Task HomeAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var chatMessage = ChatMessage.Simple("");
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            if (args.Count == 1)
            {
                var homes = Globals.Configs.PlayerHomes[Context.Player.Uuid].Homes;
                var home_ = homes.Where(x => x.Name.ToLower() == args[0].ToLower());
                if (home_.Count() == 1)
                {
                    var home = home_.FirstOrDefault();
                    try
                    {
                        await Context.Player.TeleportAsync(home.Position);

                        chatMessage.AddExtra(ChatMessage.Simple($"Successfully teleported to {ChatColor.BrightGreen}{home.Name}{ChatColor.Reset}."));
                    }
                    catch (Exception ex)
                    {
                        chatMessage.AddExtra(ChatMessage.Simple($"Cannot teleport to {ChatColor.Red}{home.Name}{ChatColor.Reset}!"));
                        if (Context.Player.IsOperator) chatMessage.AddExtra(ChatMessage.Simple($" For more information, see console."));
                        Globals.Logger.LogError(ex, $"{ChatColor.Red}{Context.Player.Username}{ChatColor.Reset} cannot teleport to {ChatColor.Red}{home.Name}{ChatColor.Reset}.");
                    }
                }
            }
            else
            {
                chatMessage = Globals.RenderCommandUsage("home");
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

        #region homes
        [Command("homes")]
        [CommandInfo("List all homes.")]
        public async Task HomesAsync(ObsidianContext Context)
        {
            var chatMessage = ChatMessage.Simple("");
            var cmds_prefix = new ChatMessage
            {
                Text = $"{ChatColor.Gray}Your homes: "
            };
            chatMessage.AddExtra(cmds_prefix);
            if (EssentialsConfigs.PlayerHomes.ContainsKey(Context.Player.Uuid))
            {
                var home_list = ChatMessage.Simple("");
                foreach (var home in EssentialsConfigs.PlayerHomes[Context.Player.Uuid].Homes)
                {
                    var commandName = new ChatMessage
                    {
                        Text = $"{ChatColor.Red}{home.Name}",
                        ClickEvent = new TextComponent
                        {
                            Action = ETextAction.RunCommand,
                            Value = $"/home {home.Name}"
                        },
                        HoverEvent = new TextComponent
                        {
                            Action = ETextAction.ShowText,
                            Value = $"Click to navigate to home"
                        }
                    };

                    var commandInfo = new ChatMessage
                    {
                        Text = $"{ChatColor.Gray}, "
                    };

                    home_list.AddExtra(commandName);
                }
                chatMessage.AddExtra(home_list);
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }

        [CommandOverload]
        public async Task HomesAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            var chatMessage = Globals.RenderCommandUsage("/homes");
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

    }
}
