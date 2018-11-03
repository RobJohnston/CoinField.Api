using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class TickersResponse : CoinFieldResponse
    {
        /// <summary>
        /// Array of objects containing ticker pairs.
        /// </summary>
        [JsonProperty("markets")]
        public IEnumerable<Ticker> Markets { get; set; }

        public class Ticker
        {
            /// <summary>
            /// "Basequote" id of the market.
            /// </summary>
            [JsonProperty("market")]
            public string Market { get; set; }

            /// <summary>
            /// ISO 8601 representation generated time.
            /// </summary>
            [JsonProperty("timestamp")]
            public DateTime Timestamp { get; set; }

            /// <summary>
            /// Best available bid price.
            /// </summary>
            [JsonProperty("bid")]
            public float Bid { get; set; }

            /// <summary>
            /// Best available ask price.
            /// </summary>
            [JsonProperty("ask")]
            public float Ask { get; set; }

            /// <summary>
            /// Lowest traded price in the past 24 hours.
            /// </summary>
            [JsonProperty("low")]
            public float Low { get; set; }

            /// <summary>
            /// Highest traded price in the past 24 hours.
            /// </summary>
            [JsonProperty("high")]
            public float High { get; set; }

            /// <summary>
            /// Last traded price.
            /// </summary>
            [JsonProperty("last")]
            public float Last { get; set; }

            /// <summary>
            /// Opening price 24 hours prior to current time.
            /// </summary>
            [JsonProperty("open")]
            public float Open { get; set; }

            /// <summary>
            /// Total volume of the last 24 hours.
            /// </summary>
            [JsonProperty("vol")]
            public float Vol { get; set; }
        }
    }
}
