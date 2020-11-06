using Obsidian.API;
using System.Linq;

namespace Essentials.Extensions
{
    internal static class ObsidianExtensions
    {
        #region IPlayer Extensions
        public static bool HasPermission(this IPlayer player, string permission) => player.Permissions.ToList().Select(x => x.ToLower()).Contains(permission.ToLower());

        //public static bool HasPermissions(this IPlayer player, List<String> permissions) => HasPermission(player, permission);
        #endregion
    }
}
