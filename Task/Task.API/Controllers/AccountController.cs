using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.API.Modals;
using Task.Services.Modals.User.Account;
using Task.Services.Services;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {

        private readonly IAccountManagement _account;
        public AccountController(IAccountManagement account)
        {
            _account = account;
        }


        [HttpGet]
        [Route("Balance-Inquiry")]
        public async Task<IActionResult> GetBalance()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // if user is authenticated with correct credentials and username is not null, fetch and return the account balance
                var response = await _account.DisplayBalanceAsync(User.Identity.Name!);

                if (response.IsSuccess)
                {
                    // balance fetched successfully
                    return StatusCode(StatusCodes.Status200OK, new InquiryResponse { Balance = response.Res!.Balance, IsSuccess = true });
                }
                else
                {
                    // failed to retrieve balance
                    return StatusCode(StatusCodes.Status401Unauthorized, new InquiryResponse { IsSuccess = false });
                }
            }
            // user is not authenticated
            return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = "You are not logged in!", IsSuccess = false });
        }

        [HttpPost]
        [Route("Deposit")]
        public async Task<IActionResult> Deposit(double amount)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var deposit = new Deposit
                {
                    Username = User.Identity.Name,
                    Amount = amount
                };

                // initiate the deposit 
                var response = await _account.DepositAsync(deposit);
                if (response.IsSuccess)
                {
                    // deposit successful
                    return StatusCode(StatusCodes.Status200OK, new Response { Message = response.Message, IsSuccess = response.IsSuccess });
                }
                else
                {
                    // deposit failed
                    return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = response.Message, IsSuccess = response.IsSuccess });
                }
            }
            // user not authenticated
            return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = "You are not logged in!", IsSuccess = false });
        }


        [HttpPost]
        [Route("Withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawRequest withdrawReq)
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var withdraw = new Withdraw
                {
                    Amount = withdrawReq.Amount,
                    Password = withdrawReq.Password,
                    Username = User.Identity.Name
                };

                // initiate the withdraw
                var response = await _account.WithdrawAsync(withdraw);

                if (response.IsSuccess)
                {
                    // withdraw successful
                    return StatusCode(StatusCodes.Status200OK, new Response { Message = response.Message, IsSuccess = response.IsSuccess });
                }
                else
                {
                    // withdraw failed
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Message = response.Message, IsSuccess = false });
                }
            }
            // use is not authenticated
            return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = "You are not logged in!", IsSuccess = false });
        }
    }
}
