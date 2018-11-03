using Newtonsoft.Json;

namespace CoinField.Api.Models
{
    public class TimestampResponse
    {
        /// <summary>
        /// ISO 8601 representation of the current time of the server.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
