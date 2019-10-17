namespace CoinField.Api.Models.Requests
{
    /// <summary>
    /// Represents the request to the '/v1/deposits' endpoint.
    /// </summary>
    public class DepositsRequest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DepositsRequest"/> class.
        /// </summary>
        public DepositsRequest()
        {
            Limit = 50;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DepositsRequest"/> class.
        /// </summary>
        /// <param name="limit">Limit the number of returned deposits.</param>
        /// <param name="state">Filter results based on the state of the deposit.</param>
        public DepositsRequest(int limit, DepositState state)
        {
            Limit = limit;
            State = state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DepositsRequest"/> class.
        /// </summary>
        /// <param name="txid">Filter based on a specific transaction ID on the Blockchain.</param>
        public DepositsRequest(string txid)
        {
            Txid = txid;
        }

        #endregion

        /// <summary>
        /// Currency identifier e.g. btc or cad. 
        /// </summary>
        /// <remarks>
        /// If not set all currencies will be returned.
        /// </remarks>
        public string Currency { get; set; }

        /// <summary>
        /// Limit the number of returned deposits.
        /// </summary>
        /// <remarks>
        /// Default value: 50.
        /// </remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// Filter results based on the state of the deposit.
        /// </summary>
        public DepositState? State { get; set; }

        /// <summary>
        /// Filter based on a specific transaction ID on the Blockchain. If this value is set, limit and state fields are ignored.
        /// </summary>
        public string Txid { get; set; }
    }
}
