using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregator.Models
{
    public class AdjustedClosingPrices
    {
        [JsonPropertyName("adjclose")]
        public List<double> Prices { get; set; }
    }
}
