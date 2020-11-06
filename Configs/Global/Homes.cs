namespace Essentials.Configs.Global
{
        public class Homes
        {

            [JsonProperty("update-bed-at-daytime")]
            public bool UpdateBedAtDaytime { get; set; }

            [JsonProperty("world-home-permissions")]
            public bool WorldHomePermissions { get; set; }

            [JsonProperty("sethome-multiple")]
            public SethomeMultiple SethomeMultiple { get; set; }

            [JsonProperty("compass-towards-home-perm")]
            public bool CompassTowardsHomePerm { get; set; }

            [JsonProperty("spawn-if-no-home")]
            public bool SpawnIfNoHome { get; set; }

            [JsonProperty("confirm-home-overwrite")]
            public bool ConfirmHomeOverwrite { get; set; }
        }
}