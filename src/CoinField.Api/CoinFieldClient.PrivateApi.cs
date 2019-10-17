using CoinField.Api.Models;
using CoinField.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinField.Api
{
    public partial class CoinFieldClient
    {
        /// <summary>
        /// Get my account details
        /// </summary>
        public async Task<AccountResponse> GetAccountAsync()
        {
            return await QueryPrivateAsync<AccountResponse>(
                "account",
                null
            );
        }

        /// <summary>
        /// Get my wallets balances.
        /// </summary>
        public async Task<WalletsResponse> GetWalletsAsync()
        {
            return await QueryPrivateAsync<WalletsResponse>(
                "wallets",
                null
            );
        }

        /// <summary>
        /// Get trading, withdrawal and deposit fees for your account for different currency and markets.
        /// </summary>
        public async Task<FeesResponse> GetFeesAsync()
        {
            return await QueryPrivateAsync<FeesResponse>(
                "fees",
                null
            );
        }

        /// <summary>
        /// Place a new order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderPostResponse> PostOrderAsync(OrderRequest request)
        {
            var parameters = new Dictionary<string, string>(4)
            {
                ["market"] = request.Market.ToLowerInvariant(),
                ["type"] = request.Type.ToString().ToLowerInvariant(),
                ["strategy"] = request.Strategy.ToString().ToLowerInvariant(),
                ["price"] = Convert.ToString(request.Price),
            };

            if (request.Volume != null)
                parameters.Add("volume", Convert.ToString(request.Volume));

            if (request.Funds != null)
                parameters.Add("funds", Convert.ToString(request.Funds));

            return await PostAsync<OrderPostResponse>(
                "order",
                parameters
            );
        }

        /// <summary>
        /// Get order.
        /// </summary>
        /// <param name="id">Order ID.</param>
        public async Task<OrderResponse> GetOrderAsync(string id)
        {
            return await QueryPrivateAsync<OrderResponse>(
                string.Format("order/{0}", id),
                null
            );
        }

        /// <summary>
        /// Cancel an order.
        /// </summary>
        /// <param name="id">Order ID</param>
        public async Task<OrderResponse> DeleteOrderAsync(string id)
        {
            return await DeleteAsync<OrderResponse>(
                string.Format("orders/{0}", id),
                null
            );
        }

        /// <summary>
        /// Get your orders for a specific market
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrdersResponse> GetOrdersAsync(OrdersRequest request)
        {
            var parameters = new Dictionary<string, string>(0) { };

            if (request.Limit != null)
                parameters.Add("limit", Convert.ToString(request.Limit));

            if (!string.IsNullOrEmpty(request.State))
                parameters.Add("state", request.State.ToLowerInvariant());

            if (request.Page != null)
                parameters.Add("page", Convert.ToString(request.Page));

            if (request.OrderBy != null)
                parameters.Add("order_by", request.OrderBy.ToString().ToLowerInvariant());

            return await QueryPrivateAsync<OrdersResponse>(
                string.Format("orders/{0}", request.Market.ToLowerInvariant()),
                parameters
            );
        }

        /// <summary>
        /// Cancel all your orders for a specific market and specific side (bid|ask).
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp.
        /// All available markets can be found at <see cref="GetMarketsAsync"/></param>
        /// <param name="side">Side of the orders (ask|bid).</param>
        /// <returns></returns>
        public async Task<OrdersResponse> DeleteOrdersAsync(string market, OrderType side)
        {
            return await DeleteAsync<OrdersResponse>(
                string.Format("orders/{0}", market.ToLowerInvariant()),
                new Dictionary<string, string>(1)
                {
                    ["side"] = side.ToString().ToLowerInvariant(),
                }
            );
        }

        /// <summary>
        /// Get your trade history.
        /// </summary>
        /// <param name="market">Market identifier in the format of "basequote" e.g. btcbch or btcxrp. All available markets can be found at <see cref="GetMarketsAsync"/></param>
        /// <param name="limit">Limit the number of returned trades. Default value: 50</param>
        /// <param name="timestamp">An integer represents the seconds elapsed since Unix epoch. If set, only trades executed before the time will be returned.</param>
        /// <param name="orderBy">If set, trades will be sorted in specific order (desc, asc).  Default value: desc</param>
        /// <returns></returns>
        public async Task<TradesResponse> GetTradeHistoryAsync(TradeHistoryRequest request)
        {
            var parameters = new Dictionary<string, string>(0) { };

            if (request.Limit != null)
                parameters.Add("limit", Convert.ToString(request.Limit));

            if (request.Timestamp != null)
                parameters.Add("timestamp", Convert.ToString(request.Timestamp));

            //if (request.From != null)
            //    parameters.Add("from", Convert.ToString(request.From));

            //if (request.To != null)
            //    parameters.Add("to", Convert.ToString(request.To));

            if (request.OrderBy != null)
                parameters.Add("order_by", request.OrderBy.ToString().ToLowerInvariant());

            return await QueryPrivateAsync<TradesResponse>(
                string.Format("trade-history/{0}", request.Market),
                parameters
            );
        }

        /// <summary>
        /// Get wallet address for cryptocurrencies.
        /// </summary>
        /// <param name="currency">Currency (Cryptocurrency only) identifier e.g. btc or xrp. 
        /// A list of all currencies are available via <see cref="GetMarketsAsync"/></param>
        /// <returns></returns>
        public async Task<DepositAddressResponse> GetDepositAddressAsync(string currency)
        {
            return await QueryPrivateAsync<DepositAddressResponse>(
                string.Format("deposit-addresses/{0}", currency),
                null
            );
        }

        /// <summary>
        /// Get your deposit history for cryptocurrencies or fiat.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DepositsResponse> GetDepositsAsync(DepositsRequest request)
        {
            //REVIEW:  Do I want one object as input param or use nullable params?
            string requestUrl = "deposits";

            if (!string.IsNullOrWhiteSpace(request.Currency))
                requestUrl += "/" + request.Currency.ToLowerInvariant();

            var parameters = new Dictionary<string, string>(0) { };

            if (request.Limit.HasValue && request.Limit.Value > 0)
                parameters.Add("limit", Convert.ToString(request.Limit.Value));

            if (request.State.HasValue)
                parameters.Add("state", request.State.Value.ToString().ToLowerInvariant());

            if (!string.IsNullOrWhiteSpace(request.Txid))
                parameters.Add("txid", request.Txid);

            return await QueryPrivateAsync<DepositsResponse>(
                requestUrl,
                parameters
            );
        }

        /// <summary>
        /// Get withdrawal destination addresses for different currencies.
        /// </summary>
        /// <param name="currency">Currency identifier e.g. btc. </param>
        /// <returns></returns>
        public async Task<WithdrawalAddressesResponse> GetWithdrawalAddressesAsync(WithdrawalAddressesRequest request)
        {
            var parameters = new Dictionary<string, string>(0) { };

            if (request.PerPage != null)
                parameters.Add("per_page", Convert.ToString(request.PerPage));

            if (request.Page != null)
                parameters.Add("page", Convert.ToString(request.Page));

            if (!string.IsNullOrEmpty(request.Method))
                parameters.Add("method", request.Method.ToLowerInvariant());

            return await QueryPrivateAsync<WithdrawalAddressesResponse>(
                string.Format("withdrawal-addresses/{0}", request.Currency),
                parameters
            );
        }

        /// <summary>
        /// Create a new withdrawal destination.
        /// </summary>
        /// <param name="label">Label for this address.</param>
        /// <param name="currency">Currency identifier e.g. btc or cad.
        /// A list of all currencies are available via <see cref="CoinFieldClient.GetCurrenciesAsync"/>.</param>
        /// <param name="method">crypto|wire|interac
        /// (For cryptocurrency withdrawals "crypto" must be set and for fiat withdrawals, we currently support "interac" and "wire".)</param>
        /// <param name="details">Payload depends on the <paramref name="method"/>.</param>
        /// <returns></returns>
        public async Task<CoinFieldResponse> PostWithdrawalAddressAsync(WithdrawalAddressRequest details)
        {
            //REVIEW:  Do I want to put the label and method in the function signature?
            var requestUrl = string.Format("withdrawal-addresses/{0}?label={1}&method={2}",
                details.Currency.ToLowerInvariant(), details.Label, details.Method.ToLowerInvariant());

            var parameters = new Dictionary<string, string>(0) { };

            if (details.Method.ToLowerInvariant() == "crypto")
            {
                var cryptoPayload = (WithdrawalAddressRequest.CryptoPayload)details.Details;
                parameters.Add("wallet_address", cryptoPayload.WalletAddress);
            }
            else
            {
                var wirePayload = (WithdrawalAddressRequest.WirePayload)details.Details;
                parameters.Add("beneficiary_name", wirePayload.BeneficiaryName);
                parameters.Add("beneficiary_address_line_1", wirePayload.BeneficiaryAddressLine1);
                parameters.Add("beneficiary_address_line_2", wirePayload.BeneficiaryAddressLine2);
                parameters.Add("beneficiary_address_line_3", wirePayload.BeneficiaryAddressLine3);
                parameters.Add("beneficiary_city", wirePayload.BeneficiaryCity);
                parameters.Add("beneficiary_zip", wirePayload.BeneficiaryZip);
                parameters.Add("beneficiary_country", wirePayload.BeneficiaryCountry);
                parameters.Add("beneficiary_telephone", wirePayload.BeneficiaryTelephone);
                parameters.Add("bank_name", wirePayload.BankName);
                parameters.Add("bank_address_line_1", wirePayload.BankAddressLine1);
                parameters.Add("bank_address_line_2", wirePayload.BankAddressLine2);
                parameters.Add("bank_address_line_3", wirePayload.BankAddressLine3);
                parameters.Add("bank_city", wirePayload.BankCity);
                parameters.Add("bank_zip", wirePayload.BankZip);
                parameters.Add("bank_country", wirePayload.BankCountry);
                parameters.Add("swift", wirePayload.Swift);
                parameters.Add("account_iban", wirePayload.AccountIban);
                parameters.Add("aba", wirePayload.Aba);
            }

            return await PostAsync<CoinFieldResponse>(
                requestUrl,
                parameters
            );
        }

        /// <summary>
        /// List all withdrawals.
        /// </summary>
        /// <param name="currency">Currency identifier e.g. btc or cad.</param>
        /// <param name="limit">Limit the number of returned results.</param>
        /// <param name="page">Page number.</param>
        /// <returns></returns>
        public async Task<WithdrawalsResponse> GetWithdrawalsAsync(string currency, int limit, int page)
        {
            return await QueryPrivateAsync<WithdrawalsResponse>(
                string.Format("withdrawals/{0}", currency),
                new Dictionary<string, string>(2)
                {
                    ["limit"] = Convert.ToString(limit),
                    ["page"] = Convert.ToString(page),
                }
            );
        }

        /// <summary>
        /// Submit a new withdrawal request for fiat or crypto.
        /// </summary>
        /// <param name="currency">Currency (Cryptocurrency only) identifier e.g. btc or xrp. A list of all currencies are available via <see cref="GetCurrenciesAsync"/>./param>
        /// <param name="amount">Amount to be withdrawn.</param>
        /// <param name="destinationId">Destination ID. Use <see cref="PostWithdrawalAddressAsync(WithdrawalAddressRequest)"/> to create a new destination.</param>
        /// <param name="otp">If OTP/2FA (Two Factor Authentication) is turned on, this value is required.</param>
        /// <returns></returns>
        public async Task<WithdrawalResponse> PostWithdrawal(string currency, decimal amount, int destination, string otp = "")
        {
            return await PostAsync<WithdrawalResponse>(
                string.Format("withdrawals/{0}", currency),
                new Dictionary<string, string>(2)
                {
                    ["amount"] = Convert.ToString(amount),
                    ["destination"] = Convert.ToString(destination),
                    ["otp"] = Convert.ToString(otp),
                }
            );
        }

        /// <summary>
        /// Get details of my rewards through referrals.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RewardsResponse> GetRewards(RewardsRequest request)
        {
            var parameters = new Dictionary<string, string>(0) { };

            if (!string.IsNullOrEmpty(request.Currency))
                parameters.Add("currency", Convert.ToString(request.Currency.ToLowerInvariant()));

            if (request.BeforeId.HasValue)
                parameters.Add("before_id", Convert.ToString(request.BeforeId.Value));

            if (request.AfterId.HasValue)
                parameters.Add("after_id", Convert.ToString(request.AfterId.Value));

            if (request.Limit.HasValue && request.Limit.Value > 0)
                parameters.Add("limit", Convert.ToString(request.Limit.Value));

            return await QueryPrivateAsync<RewardsResponse>(
                "rewards",
                parameters
            );
        }

        /// <summary>
        /// View lifetime rewards earnings.
        /// </summary>
        /// <returns></returns>
        public async Task<RewardsSummaryResponse> GetRewardsSummary()
        {
            return await QueryPrivateAsync<RewardsSummaryResponse>(
                "rewards-summary",
                null
            );
        }
    }
}
