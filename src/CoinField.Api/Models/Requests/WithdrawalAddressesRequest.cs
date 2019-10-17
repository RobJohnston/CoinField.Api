namespace CoinField.Api.Models.Requests
{
    /// <summary>
    /// Represents the GET request to the '/v1/withdrawal-addresses/{currency}' endpoint.
    /// </summary>
    public class WithdrawalAddressesRequest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawalAddressesRequest"/> class.
        /// </summary>
        /// <param name="currency">Specify the currency of.</param>
        public WithdrawalAddressesRequest(string currency)
        {
            Currency = currency;
        }

        #endregion

        /// <summary>
        /// Specify the currency of.
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Number of results per page.
        /// </summary>
        public int? PerPage { get; set; }

        /// <summary>
        /// Page number.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Filter by method, e.g. wire.
        /// </summary>
        public string Method { get; set; }
    }
}
