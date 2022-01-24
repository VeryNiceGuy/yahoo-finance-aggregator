using System;

namespace Aggregator
{
    enum RecordType
    {
        Day, Week, Month, Year
    }

    class Record
    {
        public RecordType Type { get; set; }
        public DateTime DateTime { get; set; }
        public double Open { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Close { get; set; }
        public override string ToString()
        {
            string dateInfo = "";

            switch(Type)
            {
                case RecordType.Day:
                case RecordType.Week:
                    dateInfo = DateTime.ToString("MM/dd/yyyy");
                    break;
                case RecordType.Month:
                    dateInfo = DateTime.ToString("MM/yyyy");
                    break;
                case RecordType.Year:
                    dateInfo = DateTime.ToString("yyyy");
                    break;
            }
            return $"{dateInfo} - Open: {Open}, Low: {Low}, High: {High}, Close: {Close}";
        }
    }
}
