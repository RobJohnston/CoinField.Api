using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class MarketsResponse : CoinFieldResponse
    {
        /// <summary>
        /// Array of objects containing markets pairs.
        /// </summary>
        [JsonProperty("markets")]
        public IEnumerable<Market> Markets { get; set; }

        public class Market
        {
            /// <summary>
            /// "Basequote" id of the market.
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// BASE/QUOTE name of the market.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Precision for base.
            /// </summary>
            [JsonProperty("ask_precision")]
            public int AskPrecision { get; set; }

            /// <summary>
            /// Precision for quote.
            /// </summary>
            [JsonProperty("bid_precision")]
            public int BidPrecision { get; set; }

            [JsonProperty("minimum_volume")]
            public string MinimumVolume { get; set; }

            [JsonProperty("maximum_volume")]
            public string MaximumVolume { get; set; }

            [JsonProperty("minimum_funds")]
            public string MinimumFunds { get; set; }

            [JsonProperty("maximum_funds")]
            public string MaximumFunds { get; set; }
        }
    }
}
