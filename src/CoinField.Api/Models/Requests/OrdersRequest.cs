namespace CoinField.Api.Models.Requests
{
    /// <summary>
    /// Represents the GET request to the '/v1/orders/{market}' endpoint.
    /// </summary>
    public class OrdersRequest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRequest"/> class.
        /// </summary>
        /// <param name="market">
        /// Market identifier in the format of "basequote" e.g. btcbch or btcxrp.
        /// All available markets can be found at <see cref="CoinFieldClient.GetMarketsAsync"/>.
        /// </param>
        public OrdersRequest(string market)
        {
            Market = market;
            Limit = 50;
            State = "wait";
            OrderBy = Models.OrderBy.desc;
        }

        #endregion

        /// <summary>
        /// Market identifier in the format of "basequote" e.g. btcbch or btcxrp. 
        /// </summary>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarkets"/>.
        /// </remarks>
        public string Market { get; private set; }

        /// <summary>
        /// Limit the number of returned trades.
        /// </summary>
        /// <remarks>
        /// Default value: <code>50</code>.
        /// </remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// Filter order by state, defaults to <code>wait</code> (active orders).
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Page number of paginated results.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// If set, trades will be sorted in specific order (desc, asc).
        /// </summary>
        /// <remarks>
        /// Default value: <code>desc</code>.
        /// </remarks>
        public OrderBy? OrderBy { get; set; }
    }
}
