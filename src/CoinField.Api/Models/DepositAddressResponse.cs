using Newtonsoft.Json;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/deposit-addresses/{currency}' endpoint.
    /// </summary>
    public class DepositAddressResponse : CoinFieldResponse
    {
        /// <summary>
        /// Requested currency code.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// The address of the wallet. If the address is null it means it is being created and therefore 
        /// allowing a few seconds and then trying again will result in getting the address successfully.
        /// </summary>
        /// <remarks>
        /// Please note that ERC20 token addresses are the same as the ETH address.
        /// </remarks>
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
