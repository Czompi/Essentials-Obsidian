using Essentials.Extensions;
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
    public class GamemodeCommandModule : BaseCommandClass
    {

        #region /gm <0|1|2|3>
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
