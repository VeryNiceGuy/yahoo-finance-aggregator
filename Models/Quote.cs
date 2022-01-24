using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregator.Models
{
    public class Quote
    {
        public List<double> Low { get; set; }
        public List<double> Close { get; set; }
        public List<double> High { get; set; }

        [JsonPropertyName("volume")]
        public List<int> Volumes { get; set; }
        public List<double> Open { get; set; }
    }
}
