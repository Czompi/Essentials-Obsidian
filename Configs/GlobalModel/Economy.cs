using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Essentials.Configs.GlobalModel
{
        public class Economy
        {

            [JsonPropertyName("starting-balance")]
            public int StartingBalance { get; set; }

            [JsonPropertyName("command-costs")]
            public Dictionary<string, long> CommandCosts { get; set; }

            [JsonPropertyName("currency-symbol")]
            public string CurrencySymbol { get; set; }

            [JsonPropertyName("currency-symbol-suffix")]
            public bool CurrencySymbolSuffix { get; set; }

            [JsonPropertyName("max-money")]
            public long MaxMoney { get; set; }

            [JsonPropertyName("min-money")]
            public int MinMoney { get; set; }

            [JsonPropertyName("economy-log-enabled")]
            public bool EconomyLogEnabled { get; set; }

            [JsonPropertyName("economy-log-update-enabled")]
            public bool EconomyLogUpdateEnabled { get; set; }

            [JsonPropertyName("minimum-pay-amount")]
            public double MinimumPayAmount { get; set; }

            [JsonPropertyName("pay-excludes-ignore-list")]
            public bool PayExcludesIgnoreList { get; set; }
        }
}