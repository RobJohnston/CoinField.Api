using Newtonsoft.Json;
using System;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/order' endpoint.
    /// </summary>
    public class OrderPostResponse : CoinFieldResponse
    {
        /// <summary>
        /// An objects containing order details.
        /// </summary>
        [JsonProperty("order")]
        public NewOrder Order { get; set; }

        public class NewOrder
        {
            /// <summary>
            /// Unique ID of the order.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// The market this order was placed at.
            /// </summary>
            [JsonProperty("market")]
            public string Market { get; set; }

            /// <summary>
            /// Strategy for this order: limit|market.
            /// </summary>
            [JsonProperty("strategy")]
            public Strategy Strategy { get; set; }

            /// <summary>
            /// Price for each unit.
            /// </summary>
            [JsonProperty("price")]
            public decimal Price { get; set; }

            /// <summary>
            /// Order type: bid|ask.
            /// </summary>
            [JsonProperty("type")]
            public OrderType Type { get; set; }

            /// <summary>
            /// Status of the order: open|closed|canceled.
            /// </summary>
            [JsonProperty("state")]
            public OrderState State { get; set; }

            /// <summary>
            /// Number of trades taken place for the order.
            /// </summary>
            [JsonProperty("trades_count")]
            public int TradesCount { get; set; }

            /// <summary>
            /// Timestamp the order is created in the orderbook.
            /// </summary>
            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// Original volume.
            /// </summary>
            [JsonProperty("volume")]
            public decimal Volume { get; set; }

            /// <summary>
            /// Remaining volume.
            /// </summary>
            [JsonProperty("remaining_volume")]
            public decimal RemainingVolume { get; set; }

            /// <summary>
            /// Fee percentage for the trade.
            /// </summary>
            [JsonProperty("fee_percentage")]
            public decimal FeePercentage { get; set; }

            /// <summary>
            /// Calculated total fee.
            /// </summary>
            [JsonProperty("total_fee")]
            public decimal TotalFee { get; set; }

            /// <summary>
            /// Fees to be collected in this currency.
            /// </summary>
            [JsonProperty("fee_currency")]
            public string FeeCurrency { get; set; }

            /// <summary>
            /// Total cost of the trade.
            /// </summary>
            [JsonProperty("cost")]
            public decimal Cost { get; set; }

            /// <summary>
            /// Receive after fees.
            /// </summary>
            [JsonProperty("receive")]
            public decimal Receive { get; set; }

            /// <summary>
            /// Base currency.
            /// </summary>
            [JsonProperty("base")]
            public string Base { get; set; }

            /// <summary>
            /// Quote currency.
            /// </summary>
            [JsonProperty("quote")]
            public string Quote { get; set; }
        }
    }
}
