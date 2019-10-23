using CoinField.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinField.Api
{
    public partial class CoinFieldClient
    {
        /// <summary>
        /// Get the status.
        /// </summary>
        public async Task<StatusResponse> GetStatusAsync()
        {
            return await QueryPublicAsync<StatusResponse>(
                "status",
                null
            );
        }

        /// <summary>
        /// Get the timestamp of the server.
        /// </summary>
        public async Task<TimestampResponse> GetTimestampAsync()
        {
            return await QueryPublicAsync<TimestampResponse>(
                "timestamp",
                null
            );
        }

        /// <summary>
        /// Get a list of all available currencies on the platform
        /// </summary>
        public async Task<CurrenciesResponse> GetCurrenciesAsync()
        {
            return await QueryPublicAsync<CurrenciesResponse>(
                "currencies",
                null
            );
        }

        /// <summary>
        /// Get all available markets.
        /// </summary>
        public async Task<MarketsResponse> GetMarketsAsync()
        {
            return await QueryPublicAsync<MarketsResponse>(
                "markets",
                null
            );
        }

        /// <summary>
        /// Get tickers for all markets.
        /// </summary>
        public async Task<TickersResponse> GetTickersAsync()
        {
            return await QueryPublicAsync<TickersResponse>(
                "tickers",
                null
            );
        }

        /// <summary>
        /// Get tickers for a specific market.
        /// </summary>
        /// <param name="market">Specify a market pair in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        public async Task<TickersResponse> GetTickerAsync(string market)
        {
            return await QueryPublicAsync<TickersResponse>(
                $"tickers/{market}",
                null
            );
        }

        /// <summary>
        /// Get orderbook for a specific market.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <param name="limit">Number of asks and bids array.</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        /// <exception cref="CoinFieldException">Bad Request 400 - market or limit field validation failed.</exception>
        /// <exception cref="CoinFieldException">Not Found 404 - market not found.</exception>
        public async Task<OrderBookResponse> GetOrderBookAsync(string market, int limit = 20)
        {
            return await QueryPublicAsync<OrderBookResponse>(
                $"orderbook/{market}",
                new Dictionary<string, string>(1)
                {
                    ["limit"] = Convert.ToString(limit, _culture)
                }
            );
        }

        /// <summary>
        /// Get depth for a specific market.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <param name="limit">Limit the number of returned price levels.</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        /// <exception cref="CoinFieldException">Bad Request 400 - market or limit field validation failed.</exception>
        /// <exception cref="CoinFieldException">Not Found 404 - market not found.</exception>
        public async Task<DepthResponse> GetDepthAsync(string market, int limit = 300)
        {
            return await QueryPublicAsync<DepthResponse>(
                $"depth/{market}",
                new Dictionary<string, string>(1)
                {
                    ["limit"] = Convert.ToString(limit, _culture)
                }
            );
        }

        /// <summary>
        /// OHLC (KLine) of a specific market.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <param name="limit">Limit number of candles</param>
        /// <param name="period">Candle periods. Valid range: 1, 5, 15, 30, 60, 120, 240, 360, 720, 1440, 4320, 10080</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        /// <exception cref="CoinFieldException">Bad Request 400 - market or limit field validation failed.</exception>
        /// <exception cref="CoinFieldException">Not Found 404 - market not found.</exception>
        public async Task<OhlcResponse> GetOhlcAsync(string market, int limit = 30, int period = 5)
        {
            return await GetOhlcAsync(market, long.MinValue, long.MaxValue, limit, period);
        }

        /// <summary>
        /// OHLC (KLine) of a specific market.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <param name="limit">Limit number of candles.</param>
        /// <param name="period">Candle periods. Valid range: 1, 5, 15, 30, 60, 120, 240, 360, 720, 1440, 4320, 10080.</param>
        /// <param name="from">UNIX epoch timestamp of start time.</param>
        /// <param name="to">UNIX epoch timestamp of end time.</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        /// <exception cref="CoinFieldException">Bad Request 400 - market or limit field validation failed.</exception>
        /// <exception cref="CoinFieldException">Not Found 404 - market not found.</exception>
        public async Task<OhlcResponse> GetOhlcAsync(string market, long from, long to, int limit = 30, int period = 5)
        {
            var parameters = new Dictionary<string, string>(1)
            {
                ["limit"] = Convert.ToString(limit, _culture),
                ["period"] = Convert.ToString(period, _culture)
            };

            if (from > long.MinValue)
                parameters.Add("from", Convert.ToString(from, _culture));

            if (to < long.MaxValue)
                parameters.Add("to", Convert.ToString(to, _culture));

            return await QueryPublicAsync<OhlcResponse>(
                $"ohlc/{market}",
                parameters
            );
        }

        /// <summary>
        /// Get trades for a specific market.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <param name="limit">Limit the number of returned trades.</param>
        /// <param name="orderBy">If set, trades will be sorted in specific order (desc, asc)</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        /// <exception cref="CoinFieldException">Bad Request 400 - market or limit field validation failed.</exception>
        /// <exception cref="CoinFieldException">Not Found 404 - market not found.</exception>
        public async Task<TradesResponse> GetTradesAsync(string market, int limit = 50, OrderBy orderBy = OrderBy.desc)
        {
            return await GetTradesAsync(market, long.MaxValue, limit, orderBy);
        }

        /// <summary>
        /// Get trades for a specific market.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.</param>
        /// <param name="timestamp">An integer represents the seconds elapsed since Unix epoch. If set, only trades executed before the time will be returned.</param>
        /// <param name="limit">Limit the number of returned trades.</param>
        /// <param name="orderBy">If set, trades will be sorted in specific order (desc, asc)</param>
        /// <remarks>
        /// All available markets can be found at <see cref="GetMarketsAsync"/>.
        /// </remarks>
        /// <exception cref="CoinFieldException">Bad Request 400 - market or limit field validation failed.</exception>
        /// <exception cref="CoinFieldException">Not Found 404 - market not found.</exception>
        public async Task<TradesResponse> GetTradesAsync(string market, long timestamp, int limit = 50, OrderBy orderBy = OrderBy.desc)
        {
            var parameters = new Dictionary<string, string>(1)
            {
                ["limit"] = Convert.ToString(limit, _culture),
                ["order_by"] = orderBy.ToString()
            };

            if (timestamp < long.MaxValue)
                parameters.Add("timestamp", Convert.ToString(timestamp, _culture));

            return await QueryPublicAsync<TradesResponse>(
                $"trades/{market}",
                parameters
            );
        }
    }
}
