using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/fees' endpoint.
    /// </summary>
    public class FeesResponse : CoinFieldResponse
    {
        [JsonProperty("fees")]
        public IEnumerable<Fee> Fees { get; set; }

        public class Fee
        {
            /// <summary>
            /// Fees have 3 types in general: withdrawal, deposit, trading.
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// The currency (e.g. BTC, CAD).
            /// </summary>
            [JsonProperty("currency")]
            public string Currency { get; set; }

            /// <summary>
            /// relative|fixed
            /// </summary>
            [JsonProperty("fee_type")]
            public FeeType FeeType { get; set; }

            /// <summary>
            /// Fee value.
            /// </summary>
            [JsonProperty("fee_value")]
            public decimal FeeValue { get; set; }

            /// <summary>
            /// The market pair (e.g. btccad).
            /// </summary>
            [JsonProperty("market")]
            public string Market { get; set; }

            [JsonProperty("ask_fee")]
            public decimal AskFee { get; set; }

            [JsonProperty("bid_fee")]
            public decimal BidFee { get; set; }
        }
    }
}
