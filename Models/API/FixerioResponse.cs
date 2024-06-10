using System.Text.Json.Serialization;

namespace eBanking.Models.API
{
    public class FixerioResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, double>? Rates { get; set; }
    }
}
