using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class OhlcResponse : CoinFieldResponse
    {
        /// <summary>
        /// Market of the requested OHLC in the format of "basequote" e.g. btcxrp.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Array of objects containing OHLC data.
        /// </summary>
        [JsonProperty("ohlc")]
        public IEnumerable<Ohlc> KLines { get; set; }

        public class Ohlc
        {
            /// <summary>
            /// ISO 8601 time of candle.
            /// </summary>
            [JsonProperty("ts")]
            public DateTime Timestamp { get; set; }

            /// <summary>
            /// Open.
            /// </summary>
            [JsonProperty("o")]
            public string Open { get; set; }

            /// <summary>
            /// High.
            /// </summary>
            [JsonProperty("h")]
            public string High { get; set; }

            /// <summary>
            /// Low.
            /// </summary>
            [JsonProperty("l")]
            public string Low { get; set; }

            /// <summary>
            /// Close.
            /// </summary>
            [JsonProperty("c")]
            public string Close { get; set; }

            /// <summary>
            /// Volume.
            /// </summary>
            [JsonProperty("v")]
            public string Volume { get; set; }
        }
    }
}
