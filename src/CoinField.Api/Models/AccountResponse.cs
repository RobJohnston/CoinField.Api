using Newtonsoft.Json;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/account' endpoint.
    /// </summary>
    public class AccountResponse : CoinFieldResponse
    {
        /// <summary>
        /// Your email address.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// UID represents your unique ID in the system.
        /// </summary>
        [JsonProperty("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// Your level determined by our compliance team.
        /// </summary>
        /// <remarks>
        /// Please check support docs to see what are the limitations of each level and how to upgrade your level.
        /// </remarks>
        [JsonProperty("level")]
        public int Level { get; set; }

        /// <summary>
        /// Your base fiat currency.
        /// </summary>
        [JsonProperty("base_currency")]
        public decimal BaseCurrency { get; set; }

        /// <summary>
        /// Your timezone.
        /// </summary>
        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        /// <summary>
        /// If OTP/2FA is enabled this is true otherwise false.
        /// </summary>
        [JsonProperty("opt")]
        public bool Opt { get; set; }
    }
}
