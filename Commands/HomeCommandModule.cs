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
        #region /home <name>
        [Command("home")]
        [CommandInfo("Switch gamemode.", "/home <name>")]
        public async Task HomeAsync(ObsidianContext Context) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/home <name>"));

        [CommandOverload]
        public async Task HomeAsync(ObsidianContext Context, [Remaining] string args_)
        {
            var chatMessage = IChatMessage.Simple("");
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
                        await Context.Player.TeleportAsync(new Obsidian.API.Position(home.Position.X, home.Position.Y, home.Position.Z));

                        chatMessage.AddExtra(IChatMessage.Simple($"Successfully teleported to {ChatColor.BrightGreen}{home.Name}{ChatColor.Reset}."));
                    }
                    catch (Exception ex)
                    {
                        chatMessage.AddExtra(IChatMessage.Simple($"Cannot teleport to {ChatColor.Red}{home.Name}{ChatColor.Reset}!"));
                        if (Context.Player.IsOperator) chatMessage.AddExtra(IChatMessage.Simple($" For more information, see console."));
                        Globals.Logger.LogError($"{ChatColor.Red}{Context.Player.Username}{ChatColor.Reset} cannot teleport to {ChatColor.Red}{home.Name}{ChatColor.Reset}.\n{ex.ToString()}");
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

        #region /homes
        [Command("homes")]
        [CommandInfo("List all homes.")]
        public async Task HomesAsync(ObsidianContext Context)
        {
            var chatMessage = IChatMessage.Simple("{ChatColor.Gray}Your homes: ");
            if (EssentialsConfigs.PlayerHomes.ContainsKey(Context.Player.Uuid))
            {
                var homes = IChatMessage.Simple("");
                var homeList = EssentialsConfigs.PlayerHomes[Context.Player.Uuid].Homes;
                int i = 0;
                foreach (var home in homeList)
                {
                    homes.AddExtra(Globals.RenderClickableCommand(home.Name, "Click to navigate to home", suggestionPrefix: "/home"));
                    if(i+1 <= homeList.Count) homes.AddExtra(IChatMessage.Simple($"{ChatColor.Gray}, "));
                }
                chatMessage.AddExtra(homes);
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }

        [CommandOverload]
        public async Task HomesAsync(ObsidianContext Context, [Remaining] string args_) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/homes"));
        #endregion

    }
}
