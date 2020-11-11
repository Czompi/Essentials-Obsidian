using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Essentials.Configs.GlobalModel
{
        public class Homes
        {

            [JsonPropertyName("update-bed-at-daytime")]
            public bool UpdateBedAtDaytime { get; set; }

            [JsonPropertyName("world-home-permissions")]
            public bool WorldHomePermissions { get; set; }

            [JsonPropertyName("sethome-multiple")]
            public Dictionary<string, short> SetHomeMultiple { get; set; }

            [JsonPropertyName("compass-towards-home-perm")]
            public bool CompassTowardsHomePerm { get; set; }

            [JsonPropertyName("spawn-if-no-home")]
            public bool SpawnIfNoHome { get; set; }

            [JsonPropertyName("confirm-home-overwrite")]
            public bool ConfirmHomeOverwrite { get; set; }
        }
}