using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/order/{id}' endpoint.
    /// </summary>
    public class OrderResponse : CoinFieldResponse
    {
        /// <summary>
        /// An objects containing order details.
        /// </summary>
        [JsonProperty("order")]
        public ExistingOrder Order { get; set; }

        public class ExistingOrder
        {
            /// <summary>
            /// ID of the order.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Order side - bid|ask.
            /// </summary>
            [JsonProperty("side")]
            public Side Side { get; set; }

            /// <summary>
            /// Order strategy: limit|market.
            /// </summary>
            [JsonProperty("strategy")]
            public Strategy Strategy { get; set; }

            /// <summary>
            /// Request price.
            /// </summary>
            [JsonProperty("price")]
            public decimal Price { get; set; }

            /// <summary>
            /// Average prices of executed trades.
            /// </summary>
            [JsonProperty("avg_price")]
            public decimal AvgPrice { get; set; }

            /// <summary>
            /// The status of the order: open|closed|canceled.
            /// </summary>
            [JsonProperty("state")]
            public OrderState State { get; set; }

            /// <summary>
            /// Market identifier of this order.
            /// </summary>
            [JsonProperty("market")]
            public string Market { get; set; }

            /// <summary>
            /// Timestamp of when the order was created.
            /// </summary>
            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// Total volume for the order.
            /// </summary>
            [JsonProperty("volume")]
            public decimal Volume { get; set; }

            /// <summary>
            /// Total volume that's remaining to be filled.
            /// </summary>
            [JsonProperty("remaining_volume")]
            public decimal RemainingVolume { get; set; }

            /// <summary>
            /// Total volume that's been executed so far.
            /// </summary>
            [JsonProperty("executed_volume")]
            public decimal ExecutedVolume { get; set; }

            /// <summary>
            /// Number of trades executed to fulfill the order.
            /// </summary>
            [JsonProperty("trades_count")]
            public int TradesCount { get; set; }

            /// <summary>
            /// List of trades that were matched with this trade.
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

                [JsonProperty("funds")]
                public string Funds { get; set; }

                [JsonProperty("market")]
                public string Market { get; set; }

                [JsonProperty("created_at")]
                public DateTime CreatedAt { get; set; }

                [JsonProperty("side")]
                public Side Side { get; set; }
            }
        }
    }
}
