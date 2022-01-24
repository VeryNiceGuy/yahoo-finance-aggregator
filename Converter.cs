using System;
using System.Collections.Generic;
using Aggregator.Models;

namespace Aggregator
{
    static class Converter
    {
        private static readonly int[] _tillFriday = new int[] { 5, 4, 3, 2, 1, 0 };

        public static List<Record> ConvertTo(string period, Root root)
        {
            List<Record> records = null;
            switch (period)
            {
                case "Day":
                    records = Converter.ConvertToDaily(root);
                    break;
                case "Week":
                    records = Converter.ConvertToWeekly(root);
                    break;
                case "Month":
                    records = Converter.ConvertToMonthly(root);
                    break;
                case "Year":
                    records = Converter.ConvertToYearly(root);
                    break;
            }
            return records;
        }

        private static DateTime ConvertToDateTime(long timestamp)
        {
            //return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }

        private static List<Record> ConvertToRecords(Root root)
        {
            var result = root.Chart.Results[0];
            var quote = result.Indicators.Quotes[0];
            var timestamps = result.Timestamps;
            var records = new List<Record>();

            for (var i = 0; i < timestamps.Count; ++i)
            {
                records.Add(new Record
                {
                    DateTime = ConvertToDateTime(timestamps[i]),
                    Open = quote.Open[i],
                    Low = quote.Low[i],
                    High = quote.High[i],
                    Close = quote.Close[i]
                });
            }

            return records;
        }

        private static DateTime CalculateFridayDateTime(Record record)
        {
            var today = record.DateTime;
            return today.AddDays(_tillFriday[(int)today.DayOfWeek]);
        }

        private static List<Record> ConvertToDaily(Root root)
        {
            return ConvertToRecords(root);
        }

        private static List<Record> ConvertToWeekly(Root root)
        {
            var daily = ConvertToRecords(root);
            DateTime? friday = null;
            var weeklyRecords = new List<Record>();
            double open = 0;
            double low = 0;
            double high = 0;

            for (var i = 0; i < daily.Count; ++i)
            {
                var record = daily[i];
                var last = daily.Count - 1;

                if (friday == null)
                {
                    friday = CalculateFridayDateTime(record);
                    open = record.Open;
                    low = record.Low;
                    high = record.High;
                }
                else
                {
                    low = Math.Min(low, record.Low);
                    high = Math.Max(high, record.High);
                }

                if (i == last || daily[i + 1].DateTime.CompareTo(friday) > 0)
                {
                    record.Type = RecordType.Week;
                    weeklyRecords.Add(new Record {
                        Type = RecordType.Week,
                        DateTime = record.DateTime,
                        Open = open,
                        Low = low,
                        High = high,
                        Close = record.Close
                    });

                    friday = null;
                }
            }

            return weeklyRecords;
        }

        private static List<Record> ConvertToMonthly(Root root)
        {
            var daily = ConvertToRecords(root);
            int? month = null;
            var monthlyRecords = new List<Record>();
            double open = 0;
            double low = 0;
            double high = 0;

            for (var i = 0; i < daily.Count; ++i)
            {
                var record = daily[i];
                var last = daily.Count - 1;

                if (month == null)
                {
                    month = record.DateTime.Month;
                    open = record.Open;
                    low = record.Low;
                    high = record.High;
                }
                else
                {
                    low = Math.Min(low, record.Low);
                    high = Math.Max(high, record.High);
                }

                if (i == last || record.DateTime.Month != daily[i + 1].DateTime.Month)
                {
                    monthlyRecords.Add(new Record
                    {
                        Type = RecordType.Month,
                        DateTime = record.DateTime,
                        Open = open,
                        Low = low,
                        High = high,
                        Close = record.Close
                    });

                    month = null;
                }
            }

            return monthlyRecords;
        }

        private static List<Record> ConvertToYearly(Root root)
        {
            var daily = ConvertToRecords(root);
            int? year = null;
            var yearlyRecords = new List<Record>();
            double open = 0;
            double low = 0;
            double high = 0;

            for (var i = 0; i < daily.Count; ++i)
            {
                var record = daily[i];
                var last = daily.Count - 1;

                if (year == null)
                {
                    year = record.DateTime.Year;
                    open = record.Open;
                    low = record.Low;
                    high = record.High;
                }
                else
                {
                    low = Math.Min(low, record.Low);
                    high = Math.Max(high, record.High);
                }

                if(i == last || record.DateTime.Year != daily[i + 1].DateTime.Year)
                {
                    yearlyRecords.Add(new Record {
                        Type = RecordType.Year,
                        DateTime = record.DateTime,
                        Open = open,
                        Low = low,
                        High = high,
                        Close = record.Close
                    });

                    year = null;
                }
            }

            return yearlyRecords;
        }
    }
}
