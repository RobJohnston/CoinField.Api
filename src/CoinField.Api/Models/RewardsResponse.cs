using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/rewards/{optional:currency}' endpoint.
    /// </summary>
    public class RewardsResponse : CoinFieldResponse
    {
        /// <summary>
        /// Collection of Reward objects.
        /// </summary>
        public IEnumerable<Reward> Rewards { get; set; }

        public class Reward
        {
            /// <summary>
            /// ID of the reward.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// trading|deposit.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// ID of the user you referred. Not available if you are not referrer in context of current reward.
            /// </summary>
            public string ReferredUid { get; set; }

            /// <summary>
            /// Email of the user you referred. Not available if you are not referrer in context of current reward.
            /// </summary>
            public string ReferredEmail { get; set; }

            /// <summary>
            /// Currency of reward.
            /// </summary>
            public string Currency { get; set; }

            /// <summary>
            /// Reward amount.
            /// </summary>
            public decimal Amount { get; set; }

            /// <summary>
            /// ISO 8601 date it was created.
            /// </summary>
            public DateTime CreatedAt { get; set; }

            /// <summary>
            /// ISO 8601 date it was executed.
            /// </summary>
            public DateTime ExecutedAt { get; set; }
        }
    }
}
