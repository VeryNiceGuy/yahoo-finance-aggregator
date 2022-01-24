using System.Text.Json.Serialization;

namespace Aggregator.Models
{
    public class Root
    {
        [JsonPropertyName("chart")]
        public Chart Chart { get; set; }
    }
}
