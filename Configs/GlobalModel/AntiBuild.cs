using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Essentials.Configs.GlobalModel
{
        public class AntiBuild
        {

            [JsonPropertyName("build")]
            public bool Build { get; set; }

            [JsonPropertyName("use")]
            public bool Use { get; set; }

            [JsonPropertyName("warn-on-build-disallow")]
            public bool WarnOnBuildDisallow { get; set; }

            [JsonPropertyName("alert")]
            public Dictionary<string, List<string>> Alert { get; set; }

            [JsonPropertyName("blacklist")]
            public Dictionary<string, List<string>> Blacklist { get; set; }
        }
}