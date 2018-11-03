using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class TradesResponse : CoinFieldResponse
    {
        /// <summary>
        /// Market of the requested order book in the format of "basequote" e.g. btcxrp.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Checksum hash of trades, may be used in order to see if the trades has been updated since last request.
        /// </summary>
        [JsonProperty("trades_hash")]
        public string TradesHash { get; set; }

        /// <summary>
        /// Array of objects containing trades.
        /// </summary>
        [JsonProperty("trades")]
        public IEnumerable<Trade> Trades { get; set; }

        public class Trade
        {
            /// <summary>
            /// ID of the specific trade.
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// Price of the executed trade.
            /// </summary>
            [JsonProperty("price")]
            public string Price { get; set; }

            /// <summary>
            /// Volume of the trade.
            /// </summary>
            [JsonProperty("volume")]
            public string Volume { get; set; }

            /// <summary>
            /// Total value of the trade in base currency.
            /// </summary>
            [JsonProperty("total_value")]
            public string TotalValue { get; set; }

            /// <summary>
            /// ISO 8601 time of when the trade was executed.
            /// </summary>
            [JsonProperty("timestamp")]
            public DateTime Timestamp { get; set; }
        }
    }
}
