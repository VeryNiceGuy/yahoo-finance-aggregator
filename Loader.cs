using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aggregator.Models;

namespace Aggregator
{
    class Loader
    {
        private static readonly HttpClient client = new();

        public static async Task<Root> Load(string symbol, DateTime from, DateTime to)
        {
            var period1 = new DateTimeOffset(from).ToUnixTimeSeconds();
            var period2 = new DateTimeOffset(to).ToUnixTimeSeconds();
            return await LoadHistoryAsync($"https://query1.finance.yahoo.com/v7/finance/chart/{symbol}?period1={period1}&period2={period2}&interval=1d&indicators=quote&includeTimestamps=true");
        }

        private static async Task<Root> LoadHistoryAsync(string uri)
        {
            Root root = null;
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                root = await JsonSerializer.DeserializeAsync<Root>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return root;
        }
    }
}
