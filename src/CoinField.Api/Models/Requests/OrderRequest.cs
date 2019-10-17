namespace CoinField.Api.Models.Requests
{
    /// <summary>
    /// Represents the request to the '/v1/order' endpoint.
    /// </summary>
    public class OrderRequest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRequest"/> class.
        /// </summary>
        /// <param name="market">Market in which to place an order.</param>
        /// <param name="strategy">The strategy which defines how to match and execute orders. Either <see cref="Strategy.Limit"/> or <see cref="Strategy.Market"/>.</param>
        /// <param name="volume">The amount you are willing to buy or sell.</param>
        /// <param name="price">The price for each unit.</param>
        public OrderRequest(string market, OrderType type, Strategy strategy, decimal volume, decimal price)
        {
            Market = market;
            Type = type;
            Strategy = Strategy;
            Volume = volume;
            Price = price;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRequest"/> class for a <see cref="OrderType.Bid"/> <see cref="Strategy.Market"/> order.
        /// </summary>
        /// <param name="market">Market in which to place an order.</param>
        /// <param name="price">The price for each unit.</param>
        /// <param name="funds">The amount of money you are willing to spend for purchase.</param>
        public OrderRequest(string market, decimal price, decimal funds)
        {
            Market = market;
            Type = OrderType.Bid;
            Strategy = Strategy.Market;
            Volume = null;
            Price = price;
            Funds = funds;
        }

        #endregion

        /// <summary>
        /// Market in which to place an order.
        /// </summary>
        public string Market { get; private set; }

        /// <summary>
        /// <see cref="OrderType.Bid"/>  if you want to create buy order or <see cref="OrderType.Ask"/> if you want to create sell order.
        /// </summary>
        public OrderType Type { get; private set; }

        /// <summary>
        /// The strategy which defines how to match and execute orders. Either <see cref="Strategy.Limit"/>or <see cref="Strategy.Market"/>.
        /// </summary>
        public Strategy Strategy { get; private set; }

        /// <summary>
        /// The amount you are willing to buy or sell.
        /// </summary>
        public decimal? Volume { get; private set; }

        /// <summary>
        /// The price for each unit.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// The amount of money you are willing to spend for purchase. Applicable only to bid market orders.
        /// </summary>
        /// <remarks>
        /// The system will automatically calculate the <see cref="Volume"/> based on the current exchange rates and the value of parameter.
        /// </remarks>
        public decimal? Funds { get; private set; }
    }
}
