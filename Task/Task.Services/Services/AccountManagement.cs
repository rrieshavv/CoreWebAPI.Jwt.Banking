using Microsoft.AspNetCore.Identity;
using Task.Data;
using Task.Data.Modals;
using Task.Services.Modals;
using Task.Services.Modals.User.Account;

namespace Task.Services.Services
{
    public class AccountManagement : IAccountManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        public AccountManagement(UserManager<ApplicationUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<Response<BalanceInquiryResponse>> DisplayBalanceAsync(string username)
        {
            // check if the user is present
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                double balance = user.Balance;
                return new Response<BalanceInquiryResponse> { Res = new BalanceInquiryResponse { Balance = balance }, IsSuccess = true, Message = "Balance inquiry successful!", StatusCode = 200 };
            }

            return new Response<BalanceInquiryResponse> { IsSuccess = false, Message = "Invalid credentials provided.", StatusCode = 401 };
        }

        public async Task<Response<Deposit>> DepositAsync(Deposit deposit)
        {
            if (deposit.Amount <= 0)
            {
                return new Response<Deposit> { IsSuccess = false, Message = "Invalid request. The amount must be greater than 0.", StatusCode = 401 };
            }
            // check if the user is present
            var user = await _userManager.FindByNameAsync(deposit.Username!);

            if (user != null)
            {
                // create a deposit 
                var userDeposit = new UserDeposit
                {
                    UserId = user.Id,
                    DepositAmount = deposit.Amount,
                    UserName = user.UserName,
                    DepositedOn = DateTime.Now
                };

                // update the balance
                user.Balance += userDeposit.DepositAmount;
                await _db.UserDeposits.AddAsync(userDeposit);

                //save changes in the db
                await _db.SaveChangesAsync();

                return new Response<Deposit> { IsSuccess = true, Message = $"Deposit successful. Rs. {deposit.Amount} deposited to your account.", StatusCode = 200, Res = new Deposit { Username = deposit.Username, Amount = deposit.Amount } };
            }

            return new Response<Deposit> { IsSuccess = false, Message = "Invalid credentials provided.", StatusCode = 401 };

        }

        public async Task<Response<WithdrawResponse>> WithdrawAsync(Withdraw withdraw)
        {

            if (withdraw.Amount <= 0)
            {
                return new Response<WithdrawResponse> { IsSuccess = false, Message = "Invalid request. The amount is not valid.", StatusCode = 400, Res = new WithdrawResponse { IsSuccess = false, Amount = withdraw.Amount } };
            }
            // check if the user is valid
            var user = await _userManager.FindByNameAsync(withdraw.Username!);

            if (user != null && await _userManager.CheckPasswordAsync(user, withdraw.Password!))
            {
                // check the balance
                if (user.Balance < withdraw.Amount)
                {
                    // balance is less than withdrawal amount
                    return new Response<WithdrawResponse> { IsSuccess = false, Message = "Not enough balance.", StatusCode = 400, Res = new WithdrawResponse { IsSuccess = false, Amount = withdraw.Amount } };
                }

                // create the withdrawal instance
                var userWithdrawal = new UserWithdraws
                {
                    UserId = user.Id,
                    WithdrawnAmount = withdraw.Amount,
                    UserName = user.UserName,
                    WithdrawnOn = DateTime.Now,
                };

                user.Balance -= withdraw.Amount;

                await _db.UserWithdraws.AddAsync(userWithdrawal);
                await _db.SaveChangesAsync();

                return new Response<WithdrawResponse> { IsSuccess = true, Message = $"Withdrawal successful. Rs. {withdraw.Amount} was withdrawn from your account.", StatusCode = 200, Res = new WithdrawResponse { IsSuccess = true, Amount = withdraw.Amount } };
            }

            return new Response<WithdrawResponse> { IsSuccess = false, Message = "Invalid credentials.", StatusCode = 401, Res = new WithdrawResponse { IsSuccess = false, Amount = withdraw.Amount } };
        }
    }
}
