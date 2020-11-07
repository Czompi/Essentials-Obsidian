using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Essentials.Configs.GlobalModel
{
    public class Chat
    {

        [JsonPropertyName("radius")]
        public int Radius { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("group-formats")]
        public Dictionary<string, string> GroupFormats { get; set; }
    }
}