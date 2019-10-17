using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/deposits/{optional:currency}' endpoint.
    /// </summary>
    public class DepositsResponse : CoinFieldResponse
    {
        /// <summary>
        /// Collection of objects containing deposits.
        /// </summary>
        [JsonProperty("deposits")]
        public IEnumerable<Deposit> Deposits { get; set; }

        public class Deposit
        {
            /// <summary>
            /// ID of the specific deposit record.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Currency of the deposit item.
            /// </summary>
            [JsonProperty("currency")]
            public string Currency { get; set; }

            /// <summary>
            /// Amount of deposit.
            /// </summary>
            [JsonProperty("amount")]
            public decimal Amount { get; set; }

            /// <summary>
            /// Fee assosiated with the transaction.
            /// </summary>
            [JsonProperty("fee")]
            public decimal Fee { get; set; }

            /// <summary>
            /// Transaction ID from Blockchain.
            /// </summary>
            [JsonProperty("txid")]
            public string Txid { get; set; }

            /// <summary>
            /// Time of when the deposit was initiated.
            /// </summary>
            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// Number of confirmations on the Blockchain.
            /// </summary>
            [JsonProperty("confirmations")]
            public int Confirmations { get; set; }

            /// <summary>
            /// Time of when the deposit was completed.
            /// </summary>
            [JsonProperty("completed_at")]
            public DateTime CompletedAt { get; set; }

            /// <summary>
            /// The state of the deposit.
            /// </summary>
            [JsonProperty("state")]
            public string State { get; set; }
        }
    }
}
