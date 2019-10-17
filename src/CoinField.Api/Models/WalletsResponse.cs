using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/wallets' endpoint.
    /// </summary>
    public class WalletsResponse : CoinFieldResponse
    {
        /// <summary>
        /// Collection of Wallet objects.
        /// </summary>
        [JsonProperty("wallets")]
        public IEnumerable<Wallet> Wallets { get; set; }

        public class Wallet
        {
            /// <summary>
            /// Currency of the specific wallet object.
            /// </summary>
            [JsonProperty("currency")]
            public string Currency { get; set; }

            /// <summary>
            /// Available balance of the specific wallet object.
            /// </summary>
            [JsonProperty("balance")]
            public string Balance { get; set; }

            /// <summary>
            /// Locked balance of the specific wallet object. 
            /// </summary>
            /// <remarks>
            /// This value represents the amount that is unavailable due to either the balance being locked in a pending trade or a pending withdrawal.
            /// </remarks>
            [JsonProperty("locked")]
            public string Locked { get; set; }
        }
    }
}
