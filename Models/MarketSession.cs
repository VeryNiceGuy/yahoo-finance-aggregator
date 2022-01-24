namespace Aggregator.Models
{
    public class MarketSession
    {
        public string Timezone { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
        public int GmtOffset { get; set; }
    }
}
