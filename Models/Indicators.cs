using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregator.Models
{
    public class Indicators
    {
        [JsonPropertyName("quote")]
        public List<Quote> Quotes { get; set; }

        [JsonPropertyName("adjclose")]
        public List<AdjustedClosingPrices> AdjustedClosingPrices { get; set; }
    }
}
