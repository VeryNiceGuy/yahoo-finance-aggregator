using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregator.Models
{
    public class Result
    {
        public Meta Meta { get; set; }

        [JsonPropertyName("timestamp")]
        public List<long> Timestamps { get; set; }
        public Indicators Indicators { get; set; }
    }
}
