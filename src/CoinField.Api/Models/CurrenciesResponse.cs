using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class CurrenciesResponse : CoinFieldResponse
    {
        /// <summary>
        /// Array of objects containing currencies.
        /// </summary>
        public IEnumerable<Currency> Currencies { get; set; }

        public class Currency
        {
            /// <summary>
            /// Id of the currency.
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// Type of the currency [fiat|crypto].
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// Is this an ERC20 token?
            /// </summary>
            [JsonProperty("erc20")]
            public bool Erc20 { get; set; }

            /// <summary>
            /// Full name of the currency.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Symbol of the currency.
            /// </summary>
            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            /// <summary>
            /// ISO 4217 representation of the currency.
            /// </summary>
            [JsonProperty("iso4217")]
            public string Iso4217 { get; set; }

            /// <summary>
            /// Precision of the currency or 0s after delimiter.
            /// </summary>
            [JsonProperty("precision")]
            public int Precision { get; set; }

            /// <summary>
            /// Color of the currency.
            /// </summary>
            [JsonProperty("color")]
            public string Color { get; set; }

            /// <summary>
            /// Base64 logo of the currency.
            /// </summary>
            [JsonProperty("logo")]
            public string Logo { get; set; }
        }
    }
}
