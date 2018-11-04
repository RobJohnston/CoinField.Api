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
            public decimal Bid { get; set; }

            /// <summary>
            /// Best available ask price.
            /// </summary>
            [JsonProperty("ask")]
            public decimal Ask { get; set; }

            /// <summary>
            /// Lowest traded price in the past 24 hours.
            /// </summary>
            [JsonProperty("low")]
            public decimal Low { get; set; }

            /// <summary>
            /// Highest traded price in the past 24 hours.
            /// </summary>
            [JsonProperty("high")]
            public decimal High { get; set; }

            /// <summary>
            /// Last traded price.
            /// </summary>
            [JsonProperty("last")]
            public decimal Last { get; set; }

            /// <summary>
            /// Opening price 24 hours prior to current time.
            /// </summary>
            [JsonProperty("open")]
            public decimal Open { get; set; }

            /// <summary>
            /// Total volume of the last 24 hours.
            /// </summary>
            [JsonProperty("vol")]
            public decimal Vol { get; set; }
        }
    }
}
