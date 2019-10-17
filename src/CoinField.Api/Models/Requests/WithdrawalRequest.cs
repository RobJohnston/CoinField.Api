using System;
using System.Collections.Generic;
using System.Text;

namespace CoinField.Api.Models.Requests
{
    public class WithdrawalRequest
    {
        #region Constructors

        public WithdrawalRequest(string currency)
        {
            Currency = currency;
        }

        #endregion

        /// <summary>
        /// Currency (Cryptocurrency only) identifier e.g. btc or xrp. 
        /// </summary>
        /// <remarks>
        /// A list of all currencies are available via <see cref="CoinFieldClient.GetCurrenciesAsync"/>.
        /// </remarks>
        public string Currency { get; private set; }

        /// <summary>
        /// Amount to be withdrawn 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Destination ID.
        /// </summary>
        /// <remarks>
        /// Use <see cref="CoinFieldClient.PostWithdrawalAddressAsync(string, string, string, WithdrawalWireDetails)"/> to create a new destination.
        /// </remarks>
        public string Destination { get; set; }

        /// <summary>
        /// Filter by method, e.g. wire.
        /// </summary>
        public string Method { get; set; }
    }
}
