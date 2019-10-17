namespace CoinField.Api.Models.Requests
{
    /// <summary>
    /// Represents the request to the '/v1/rewards' endpoint.
    /// </summary>
    public class RewardsRequest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RewardsRequest"/> class.
        /// </summary>
        public RewardsRequest() { }

        #endregion

        /// <summary>
        /// Currency identifier e.g. btc or cad. 
        /// </summary>
        /// <remarks>
        /// If not set all currencies will be returned.
        /// </remarks>
        public string Currency { get; set; }

        /// <summary>
        /// Return rewards before this id.
        /// </summary>
        public int? BeforeId { get; set; }

        /// <summary>
        /// Return rewards after this id.
        /// </summary>
        public int? AfterId { get; set; }

        /// <summary>
        /// Limit the number of results.
        /// </summary>
        public int? Limit { get; set; }
    }
}
