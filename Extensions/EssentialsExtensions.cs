using Obsidian.API;

namespace Essentials.Extensions
{
    public static class EssentialsExtensions
    {

        #region Essentials and Obsidian position conversion
        public static Configs.Position ToEssentialsPosition(this Obsidian.API.Position position) => new Configs.Position { X = position.X, Y = position.Y, Z = position.Z };
        public static Obsidian.API.Position ToObsidianPosition(this Configs.Position position) => new Obsidian.API.Position(position.X, position.Y, position.Z);
        #endregion

        #region Position ToColoredString()
        public static string ToColoredString(this Obsidian.API.Position position) => $"§rX = §9{position.X}§r, Y = §9{position.Y}§r, Z = §9{position.Z}§r";
        public static string ToColoredString(this Obsidian.API.Position position, ChatColor color) => $"§rX = {color}{position.X}§r, Y = {color}{position.Y}§r, Z = {color}{position.Z}§r";

        public static string ToColoredString(this Configs.Position position) => $"§rX = §9{position.X}§r, Y = §9{position.Y}§r, Z = §9{position.Z}§r";
        public static string ToColoredString(this Configs.Position position, ChatColor color) => $"§rX = {color}{position.X}§r, Y = {color}{position.Y}§r, Z = {color}{position.Z}§r";
        #endregion

    }
}
