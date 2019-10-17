using System.Collections.Generic;

namespace CoinField.Api.Models
{
    /// <summary>
    /// Represents the response from the '/v1/rewards-summary' endpoint.
    /// </summary>
    public class RewardsSummaryResponse : CoinFieldResponse
    {
        public IEnumerable<Reward> Rewards { get; set; }

        public class Reward
        {
            /// <summary>
            /// Currency of the reward.
            /// </summary>
            public decimal Currency { get; set; }

            /// <summary>
            /// Total amount earned for this currency.
            /// </summary>
            public decimal Amount { get; set; }
        }
    }
}
