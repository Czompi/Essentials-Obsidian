using System.Collections;
using System.Text.Json.Serialization;

namespace Essentials.Configs
{

        public class GlobalConfig
        {

            [JsonPropertyName("ops-name-color")]
            public string OpsNameColor { get; set; }

            [JsonPropertyName("nickname-prefix")]
            public string NicknamePrefix { get; set; }

            [JsonPropertyName("max-nick-length")]
            public int MaxNickLength { get; set; }

            [JsonPropertyName("nick-blacklist")]
            public IList<string> NickBlacklist { get; set; }

            [JsonPropertyName("ignore-colors-in-max-nick-length")]
            public bool IgnoreColorsInMaxNickLength { get; set; }

            [JsonPropertyName("hide-displayname-in-vanish")]
            public bool HideDisplaynameInVanish { get; set; }

            [JsonPropertyName("change-displayname")]
            public bool ChangeDisplayname { get; set; }

            [JsonPropertyName("teleport-safety")]
            public bool TeleportSafety { get; set; }

            [JsonPropertyName("force-disable-teleport-safety")]
            public bool ForceDisableTeleportSafety { get; set; }

            [JsonPropertyName("force-safe-teleport-location")]
            public bool ForceSafeTeleportLocation { get; set; }

            [JsonPropertyName("teleport-passenger-dismount")]
            public bool TeleportPassengerDismount { get; set; }

            [JsonPropertyName("teleport-cooldown")]
            public int TeleportCooldown { get; set; }

            [JsonPropertyName("teleport-delay")]
            public int TeleportDelay { get; set; }

            [JsonPropertyName("teleport-invulnerability")]
            public int TeleportInvulnerability { get; set; }

            [JsonPropertyName("teleport-to-center")]
            public bool TeleportToCenter { get; set; }

            [JsonPropertyName("heal-cooldown")]
            public int HealCooldown { get; set; }

            [JsonPropertyName("remove-effects-on-heal")]
            public bool RemoveEffectsOnHeal { get; set; }

            [JsonPropertyName("near-radius")]
            public int NearRadius { get; set; }

            [JsonPropertyName("item-spawn-blacklist")]
            public object ItemSpawnBlacklist { get; set; }

            [JsonPropertyName("permission-based-item-spawn")]
            public bool PermissionBasedItemSpawn { get; set; }

            [JsonPropertyName("spawnmob-limit")]
            public int SpawnmobLimit { get; set; }

            [JsonPropertyName("warn-on-smite")]
            public bool WarnOnSmite { get; set; }

            [JsonPropertyName("drop-items-if-full")]
            public bool DropItemsIfFull { get; set; }

            [JsonPropertyName("notify-no-new-mail")]
            public bool NotifyNoNewMail { get; set; }

            [JsonPropertyName("notify-player-of-mail-cooldown")]
            public int NotifyPlayerOfMailCooldown { get; set; }

            [JsonPropertyName("overridden-commands")]
            public IList<object> OverriddenCommands { get; set; }

            [JsonPropertyName("disabled-commands")]
            public IList<object> DisabledCommands { get; set; }

            [JsonPropertyName("socialspy-commands")]
            public IList<string> SocialspyCommands { get; set; }

            [JsonPropertyName("socialspy-listen-muted-players")]
            public bool SocialspyListenMutedPlayers { get; set; }

            [JsonPropertyName("world-change-fly-reset")]
            public bool WorldChangeFlyReset { get; set; }

            [JsonPropertyName("world-change-speed-reset")]
            public bool WorldChangeSpeedReset { get; set; }

            [JsonPropertyName("mute-commands")]
            public IList<object> MuteCommands { get; set; }

            [JsonPropertyName("player-commands")]
            public IList<string> PlayerCommands { get; set; }

            [JsonPropertyName("use-bukkit-permissions")]
            public bool UseBukkitPermissions { get; set; }

            [JsonPropertyName("skip-used-one-time-kits-from-kit-list")]
            public bool SkipUsedOneTimeKitsFromKitList { get; set; }

            [JsonPropertyName("pastebin-createkit")]
            public bool PastebinCreatekit { get; set; }

            [JsonPropertyName("enabledSigns")]
            public IList<object> EnabledSigns { get; set; }

            [JsonPropertyName("sign-use-per-second")]
            public int SignUsePerSecond { get; set; }

            [JsonPropertyName("allow-old-id-signs")]
            public bool AllowOldIdSigns { get; set; }

            [JsonPropertyName("unprotected-sign-names")]
            public IList<object> UnprotectedSignNames { get; set; }

            [JsonPropertyName("backup")]
            public object Backup { get; set; }

            [JsonPropertyName("interval")]
            public int Interval { get; set; }

            [JsonPropertyName("always-run")]
            public bool AlwaysRun { get; set; }

            [JsonPropertyName("per-warp-permission")]
            public bool PerWarpPermission { get; set; }

            [JsonPropertyName("list")]
            public List List { get; set; }

            [JsonPropertyName("real-names-on-list")]
            public bool RealNamesOnList { get; set; }

            [JsonPropertyName("debug")]
            public bool Debug { get; set; }

            [JsonPropertyName("remove-god-on-disconnect")]
            public bool RemoveGodOnDisconnect { get; set; }

            [JsonPropertyName("auto-afk")]
            public int AutoAfk { get; set; }

            [JsonPropertyName("auto-afk-kick")]
            public int AutoAfkKick { get; set; }

            [JsonPropertyName("freeze-afk-players")]
            public bool FreezeAfkPlayers { get; set; }

            [JsonPropertyName("disable-item-pickup-while-afk")]
            public bool DisableItemPickupWhileAfk { get; set; }

            [JsonPropertyName("cancel-afk-on-interact")]
            public bool CancelAfkOnInteract { get; set; }

            [JsonPropertyName("cancel-afk-on-move")]
            public bool CancelAfkOnMove { get; set; }

            [JsonPropertyName("sleep-ignores-afk-players")]
            public bool SleepIgnoresAfkPlayers { get; set; }

            [JsonPropertyName("afk-list-name")]
            public string AfkListName { get; set; }

            [JsonPropertyName("broadcast-afk-message")]
            public bool BroadcastAfkMessage { get; set; }

            [JsonPropertyName("death-messages")]
            public bool DeathMessages { get; set; }

            [JsonPropertyName("vanishing-items-policy")]
            public string VanishingItemsPolicy { get; set; }

            [JsonPropertyName("binding-items-policy")]
            public string BindingItemsPolicy { get; set; }

            [JsonPropertyName("send-info-after-death")]
            public bool SendInfoAfterDeath { get; set; }

            [JsonPropertyName("allow-silent-join-quit")]
            public bool AllowSilentJoinQuit { get; set; }

            [JsonPropertyName("custom-join-message")]
            public string CustomJoinMessage { get; set; }

            [JsonPropertyName("custom-quit-message")]
            public string CustomQuitMessage { get; set; }

            [JsonPropertyName("hide-join-quit-messages-above")]
            public int HideJoinQuitMessagesAbove { get; set; }

            [JsonPropertyName("no-god-in-worlds")]
            public IList<object> NoGodInWorlds { get; set; }

            [JsonPropertyName("world-teleport-permissions")]
            public bool WorldTeleportPermissions { get; set; }

            [JsonPropertyName("default-stack-size")]
            public int DefaultStackSize { get; set; }

            [JsonPropertyName("oversized-stacksize")]
            public int OversizedStacksize { get; set; }

            [JsonPropertyName("repair-enchanted")]
            public bool RepairEnchanted { get; set; }

            [JsonPropertyName("unsafe-enchantments")]
            public bool UnsafeEnchantments { get; set; }

            [JsonPropertyName("register-back-in-listener")]
            public bool RegisterBackInListener { get; set; }

            [JsonPropertyName("login-attack-delay")]
            public int LoginAttackDelay { get; set; }

            [JsonPropertyName("max-fly-speed")]
            public double MaxFlySpeed { get; set; }

            [JsonPropertyName("max-walk-speed")]
            public double MaxWalkSpeed { get; set; }

            [JsonPropertyName("mails-per-minute")]
            public int MailsPerMinute { get; set; }

            [JsonPropertyName("max-mute-time")]
            public int MaxMuteTime { get; set; }

            [JsonPropertyName("max-tempban-time")]
            public int MaxTempbanTime { get; set; }

            [JsonPropertyName("last-message-reply-recipient")]
            public bool LastMessageReplyRecipient { get; set; }

            [JsonPropertyName("last-message-reply-recipient-timeout")]
            public int LastMessageReplyRecipientTimeout { get; set; }

            [JsonPropertyName("milk-bucket-easter-egg")]
            public bool MilkBucketEasterEgg { get; set; }

            [JsonPropertyName("send-fly-enable-on-join")]
            public bool SendFlyEnableOnJoin { get; set; }

            [JsonPropertyName("world-time-permissions")]
            public bool WorldTimePermissions { get; set; }

            [JsonPropertyName("command-cooldowns")]
            public CommandCooldowns CommandCooldowns { get; set; }

            [JsonPropertyName("command-cooldown-persistence")]
            public bool CommandCooldownPersistence { get; set; }

            [JsonPropertyName("npcs-in-balance-ranking")]
            public bool NpcsInBalanceRanking { get; set; }

            [JsonPropertyName("allow-bulk-buy-sell")]
            public bool AllowBulkBuySell { get; set; }

            [JsonPropertyName("allow-selling-named-items")]
            public bool AllowSellingNamedItems { get; set; }

            [JsonPropertyName("delay-motd")]
            public int DelayMotd { get; set; }

            [JsonPropertyName("default-enabled-confirm-commands")]
            public IList<object> DefaultEnabledConfirmCommands { get; set; }

            [JsonPropertyName("teleport-back-when-freed-from-jail")]
            public bool TeleportBackWhenFreedFromJail { get; set; }

            [JsonPropertyName("tpa-accept-cancellation")]
            public int TpaAcceptCancellation { get; set; }

            [JsonPropertyName("allow-direct-hat")]
            public bool AllowDirectHat { get; set; }

            [JsonPropertyName("allow-world-in-broadcastworld")]
            public bool AllowWorldInBroadcastworld { get; set; }

            [JsonPropertyName("is-water-safe")]
            public bool IsWaterSafe { get; set; }

            [JsonPropertyName("safe-usermap-names")]
            public bool SafeUsermapNames { get; set; }

            [JsonPropertyName("log-command-block-commands")]
            public bool LogCommandBlockCommands { get; set; }

            [JsonPropertyName("max-projectile-speed")]
            public int MaxProjectileSpeed { get; set; }
        }
}