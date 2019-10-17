namespace CoinField.Api.Models
{
    public enum WithdrawalState
    {
        Submitted,
        Auditing,
        Audited,
        Authorizing,
        Authorized,
        Canceled,
        Suspected,
        Rejected,
        Enqueued,
        Processing,
        Confirming,
        Succeeded,
        Failed
    }
}
