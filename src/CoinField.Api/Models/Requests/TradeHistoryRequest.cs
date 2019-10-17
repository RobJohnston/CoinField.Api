namespace CoinField.Api.Models.Requests
{
    /// <summary>
    /// Represents the request to the '/v1/trade-history/{market}' endpoint.
    /// </summary>
    public class TradeHistoryRequest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeHistoryRequest"/> class.
        /// </summary>
        public TradeHistoryRequest(string market)
        {
            Market = market;
            Limit = 50;
            OrderBy = Models.OrderBy.desc;
        }

        #endregion

        /// <summary>
        /// Market identifier in the format of "basequote" e.g. btcbch or btcxrp. 
        /// </summary>
        /// <remarks>
        /// All available markets can be found at <see cref="CoinFieldClient.GetMarketsAsync"/>.
        /// </remarks>
        public string Market { get; private set; }

        /// <summary>
        /// Limit the number of returned trades.
        /// </summary>
        /// <remarks>
        /// Default value: 50.
        /// </remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// An integer represents the seconds elapsed since Unix epoch. If set, only trades executed before the time will be returned.
        /// </summary>
        public long? Timestamp { get; set; }

        ///// <summary>
        ///// Trade id. If set, only trades done after the specified trade id will be returned.
        ///// </summary>
        //public int? From { get; set; }

        ///// <summary>
        ///// Trade id. If set, only trades done before the specified trade id will be returned.
        ///// </summary>
        //public int? To { get; set; }

        /// <summary>
        /// If set, trades will be sorted in specific order (desc, asc).
        /// </summary>
        /// <remarks>
        /// Default value: <see cref="Models.OrderBy.desc"/>.
        /// </remarks>
        public OrderBy? OrderBy { get; set; }
    }
}
