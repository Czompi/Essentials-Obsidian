using Obsidian.API;
using System;
using System.Linq;

namespace Essentials.Extensions
{
    internal static class ObsidianExtensions
    {
        #region IPlayer Extensions
        public static Boolean HasPermission(this IPlayer player, String permission) => player.Permissions.ToList().Select(x => x.ToLower()).Contains(permission.ToLower());
        #endregion
    }
}
