using Task.Data.Modals;
using Task.Services.Modals;
using Task.Services.Modals.User.Account;

namespace Task.Services.Services
{
    public interface IAccountManagement
    {

        Task<Response<BalanceInquiryResponse>> DisplayBalanceAsync(string username);

        Task<Response<Deposit>> DepositAsync(Deposit deposit);

        Task<Response<WithdrawResponse>> WithdrawAsync(Withdraw withdraw);
    }
}
