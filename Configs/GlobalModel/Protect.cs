using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Essentials.Configs.GlobalModel
{
        public class Protect
        {

            [JsonPropertyName("prevent")]
            public Dictionary<string, object> Prevent { get; set; }

            [JsonPropertyName("creeper")]
            public Dictionary<string, object> Creeper { get; set; }

            [JsonPropertyName("disable")]
            public Dictionary<string, object> Disable { get; set; }
        }
}