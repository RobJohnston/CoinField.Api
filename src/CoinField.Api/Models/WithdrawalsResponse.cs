using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the GET response from the '/v1/withdrawals/{currency}' endpoint.
    /// </summary>
    public class WithdrawalsResponse : CoinFieldResponse
    {
        /// <summary>
        /// A collection of Withdrawal objects for a specific <code>Currency</code>.
        /// </summary>
        public IEnumerable<Withdrawal> Withdrawals { get; set; }

        public class Withdrawal
        {
            /// <summary>
            /// ID of withdrawal.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Currency of withdrawal.
            /// </summary>
            public string Currency { get; set; }

            /// <summary>
            /// Type of withdrawal.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// Amount of withdrawal.
            /// </summary>
            public decimal Amount { get; set; }

            /// <summary>
            /// Fee of withdrawal.
            /// </summary>
            public decimal Fee { get; set; }

            /// <summary>
            /// If crypto, transaction ID from blockchain.
            /// </summary>
            public string Txix { get; set; }

            /// <summary>
            /// If crypto, this is the wallet address, if any other method, this value is the address ID.
            /// </summary>
            public string Rid { get; set; }

            /// <summary>
            /// Status of the withdrawal.
            /// </summary>
            public WithdrawalState State { get; set; }

            /// <summary>
            /// Time created.
            /// </summary>
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// Time update.
            /// </summary>
            public DateTime UpdatedAt { get; set; }

            /// <summary>
            /// Completed.
            /// </summary>
            public DateTime CompletedAt { get; set; }
        }
    }
}
