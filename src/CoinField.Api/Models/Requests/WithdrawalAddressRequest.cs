namespace CoinField.Api.Models.Requests
{
    public class WithdrawalAddressRequest
    {
        #region  Constructors

        public WithdrawalAddressRequest(string label, string currency, string method)
        {
            Label = label;
            Currency = currency;
            Method = method;
        }

        #endregion

        /// <summary>
        /// Label for this address
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Currency identifier e.g. btc or cad. 
        /// </summary>
        /// <remarks>
        /// A list of all currencies are available via <see cref="CoinFieldClient.GetCurrenciesAsync"/>.
        /// </remarks>
        public string Currency { get; set; }

        /// <summary>
        /// crypto|wire|interac 
        /// (For cryptocurrency withdrawals "crypto" must be set and for fiat withdrawals, we currently support "interac" and "wire".)
        /// </summary>
        public string Method { get; set; }

        public Payload Details { get; set; }

        /// <summary>
        /// Payload depends on the <code>Method</code>. See <see cref="CryptoPayload"/> or <see cref="WirePayload"/>.
        /// </summary>
        public abstract class Payload { }

        /// <summary>
        /// The payload used for crypto withdrawals.
        /// </summary>
        public class CryptoPayload : Payload
        {
            /// <summary>
            /// Destination wallet address.
            /// </summary>
            /// <remarks>
            /// <list type="bullet">
            /// <item>XRP withdrawal, if destination tag is used, you must append it to the end of the wallet_address in the following format YOUR_XRP_WALLET_ADDRESS?dt=YOUR_DESTINATION_TAG</item>
            /// <item>BCH withdrawal addresses must use legacy type</item>
            /// <item>LTC wallets utilize the new litecoin address format. The new address format begins in an M</item>
            /// <item>ERC20 wallets addresses are the same as your ETH address in the destination wallet</item>
            /// </list>
            /// </remarks>
            public string WalletAddress { get; set; }
        }

        /// <summary>
        /// The payload used for wire withdrawals.
        /// </summary>
        public class WirePayload : Payload
        {
            public string BeneficiaryName { get; set; }

            public string BeneficiaryAddressLine1 { get; set; }

            public string BeneficiaryAddressLine2 { get; set; }

            public string BeneficiaryAddressLine3 { get; set; }

            public string BeneficiaryCity { get; set; }

            public string BeneficiaryZip { get; set; }

            public string BeneficiaryCountry { get; set; }

            public string BeneficiaryTelephone { get; set; }

            public string BankName { get; set; }

            public string BankAddressLine1 { get; set; }

            public string BankAddressLine2 { get; set; }

            public string BankAddressLine3 { get; set; }

            public string BankCity { get; set; }

            public string BankZip { get; set; }

            public string BankCountry { get; set; }

            public string Swift { get; set; }

            public string AccountIban { get; set; }

            public string Aba { get; set; }
        }
    }
}
