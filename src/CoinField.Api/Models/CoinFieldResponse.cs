using Newtonsoft.Json;
using System;

namespace CoinField.Api.Models
{
    public abstract class CoinFieldResponse
    {
        /// <summary>
        /// ISO 8601 representation of the current time markets are fetched.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Number of milliseconds taken for the API to respond.
        /// </summary>
        /// <remarks>
        /// Useful to debug latency vs processing delays.
        /// </remarks>
        [JsonProperty("took")]
        public string Took { get; set; }
    }
}
