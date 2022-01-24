using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregator.Models
{
    public class Chart
    {
        [JsonPropertyName("result")]
        public List<Result> Results { get; set; }
        public string Error { get; set; }
    }
}
