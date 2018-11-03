using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class OrderBookResponse : CoinFieldResponse
    {
        /// <summary>
        /// Market of the requested order book in the format of "basequote" e.g. btcxrp.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Total volume of asks in the requested order book.
        /// </summary>
        /// <remarks>
        /// This is the sum of the current dataset and does not refer to the total order book if the limit lower than the complete order book.
        /// </remarks>
        [JsonProperty("total_asks")]
        public decimal TotalAsks { get; set; }

        /// <summary>
        /// Total volume of bids in the requested order book.
        /// </summary>
        /// <remarks>
        /// This is the sum of the current dataset and does not refer to the total order book if the limit lower than the complete order book.
        /// </remarks>
        [JsonProperty("total_bids")]
        public decimal TotalBids { get; set; }

        /// <summary>
        /// Checksum hash of bids order book, may be used in order to see if the order book has been updated since last request.
        /// </summary>
        [JsonProperty("bids_hash")]
        public string BidsHash { get; set; }

        /// <summary>
        /// Checksum hash of asks order book, may be used in order to see if the order book has been updated since last request.
        /// </summary>
        [JsonProperty("asks_hash")]
        public string AsksHash { get; set; }

        /// <summary>
        /// Array of objects containing bids.
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<BidAsk> Bids { get; set; }

        /// <summary>
        /// Array of objects containing asks.
        /// </summary>
        [JsonProperty("asks")]
        public IEnumerable<BidAsk> Asks { get; set; }

        public class BidAsk
        {
            /// <summary>
            /// ID of the specific order in the orderbook.
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// Price of the order.
            /// </summary>
            [JsonProperty("price")]
            public string Price { get; set; }

            /// <summary>
            /// Available volume of the order.
            /// </summary>
            [JsonProperty("volume")]
            public string Volume { get; set; }

            /// <summary>
            /// ISO 8601 time of when the order was created.
            /// </summary>
            [JsonProperty("timestamp")]
            public DateTime Timestamp { get; set; }
        }
    }
}
