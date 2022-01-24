namespace Aggregator.Models
{
    public class CurrentTradingPeriod
    {
        public MarketSession Pre { get; set; }
        public MarketSession Regular { get; set; }
        public MarketSession Post { get; set; }
    }
}
