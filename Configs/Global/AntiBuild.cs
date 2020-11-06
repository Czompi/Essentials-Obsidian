namespace Essentials.Configs.Global
{
        public class AntiBuild
        {

            [JsonProperty("build")]
            public bool Build { get; set; }

            [JsonProperty("use")]
            public bool Use { get; set; }

            [JsonProperty("warn-on-build-disallow")]
            public bool WarnOnBuildDisallow { get; set; }

            [JsonProperty("alert")]
            public Alert Alert { get; set; }

            [JsonProperty("blacklist")]
            public Blacklist Blacklist { get; set; }
        }
}