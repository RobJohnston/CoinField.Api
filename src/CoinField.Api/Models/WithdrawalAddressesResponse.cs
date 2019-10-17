using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/withdrawal-addresses/{currency}' endpoint.
    /// </summary>
    public class WithdrawalAddressesResponse : CoinFieldResponse
    {
        /// <summary>
        /// Collection of Address objects.
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }

        public class Address
        {
            /// <summary>
            /// ID of the withdrawal address.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Currency of the withdrawal address.
            /// </summary>
            public string Currency { get; set; }

            /// <summary>
            /// Method of the withdrawal address e.g wire, crypto.
            /// </summary>
            public string Method { get; set; }

            /// <summary>
            /// Memorable label e.g My Cold Wallet 123.
            /// </summary>
            public string Label { get; set; }

            /// <summary>
            /// Type of the withdrawal address crypto|fiat.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// Object containing details for this address.
            /// </summary>
            public WithdrawalDetails Details { get; set; }

            public class WithdrawalDetails
            {
                #region When method is "crypto"

                /// <summary>
                /// Destination wallet address.
                /// </summary>
                /// <remarks>
                /// <list type="bullet">
                /// <item>XRP withdrawal, if destination tag is used, you must append it to the end of the wallet_address in the following format YOUR_XRP_WALLET_ADDRESS?dt=YOUR_DESTINATION_TAG.</item>
                /// <item>BCH withdrawal addresses must use legacy type.</item>
                /// <item>LTC wallets utilize the new litecoin address format. The new address format begins in an M.</item>
                /// <itm>LTC wallets utilize the new litecoin address format. The new address format begins in an M.</itm>
                /// </list>
                /// </remarks>
                public string WalletAddress { get; set; }

                #endregion

                #region When method is "wire"

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

                #endregion  
            }
        }
    }
}
