namespace Essentials.Configs.Global
{
        public class Economy
        {

            [JsonProperty("starting-balance")]
            public int StartingBalance { get; set; }

            [JsonProperty("command-costs")]
            public CommandCosts CommandCosts { get; set; }

            [JsonProperty("currency-symbol")]
            public string CurrencySymbol { get; set; }

            [JsonProperty("currency-symbol-suffix")]
            public bool CurrencySymbolSuffix { get; set; }

            [JsonProperty("max-money")]
            public long MaxMoney { get; set; }

            [JsonProperty("min-money")]
            public int MinMoney { get; set; }

            [JsonProperty("economy-log-enabled")]
            public bool EconomyLogEnabled { get; set; }

            [JsonProperty("economy-log-update-enabled")]
            public bool EconomyLogUpdateEnabled { get; set; }

            [JsonProperty("minimum-pay-amount")]
            public double MinimumPayAmount { get; set; }

            [JsonProperty("pay-excludes-ignore-list")]
            public bool PayExcludesIgnoreList { get; set; }
        }
}