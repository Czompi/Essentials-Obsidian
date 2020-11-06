namespace Essentials.Configs.Global
{
        public class Protect
        {

            [JsonProperty("prevent")]
            public Prevent Prevent { get; set; }

            [JsonProperty("creeper")]
            public Creeper Creeper { get; set; }

            [JsonProperty("disable")]
            public Disable Disable { get; set; }
        }
}