namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the POST response from the '/v1/withdrawals/{currency}' endpoint.
    /// </summary>
    public class WithdrawalResponse : CoinFieldResponse
    {
        public WithdrawalDetails Withdrawal { get; set; }

        public class WithdrawalDetails
        {
            /// <summary>
            /// Withdrawal transaction ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Currency of the withdrawal.
            /// </summary>
            public string Currency { get; set; }

            /// <summary>
            /// Amount of withdrawal.
            /// </summary>
            public string Amount { get; set; }

            /// <summary>
            /// Fee charged for this withdrawal.
            /// </summary>
            public string Fee { get; set; }

            /// <summary>
            /// Status of the withdrawal.
            /// </summary>
            public WithdrawalState State { get; set; }
        }
    }
}
