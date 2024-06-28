using Task.Data.Modals;
using Task.Services.Modals;
using Task.Services.Modals.User.Account;

namespace Task.Services.Services
{
    public interface IAccountManagement
    {
        /// <summary>
        /// Asynchronously retrieves and returns the current balance of the specified user.
        /// </summary>
        /// <param name="username">The username of the user whose balance is to be retrieved.</param>
        /// <returns>The task object containing the balance of the user.</returns>
        Task<Response<BalanceInquiryResponse>> DisplayBalanceAsync(string username);

        /// <summary>
        /// Asynchronously deposits amount into the user account.
        /// </summary>
        /// <param name="deposit"></param>
        /// <returns>A task object containing the username and deposited amount.</returns>
        Task<Response<Deposit>> DepositAsync(Deposit deposit);

        /// <summary>
        /// Asynchronously withdraws amount from the user account.
        /// </summary>
        /// <param name="withdraw"></param>
        /// <returns>A task object containing the withdrawn amount and a success flag. </returns>
        Task<Response<WithdrawResponse>> WithdrawAsync(Withdraw withdraw);
    }
}
