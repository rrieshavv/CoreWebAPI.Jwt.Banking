

using System.ComponentModel.DataAnnotations;

namespace Task.Services.Modals.User.Account
{
    public class Withdraw
    {
        [Required(ErrorMessage ="Username is required. Please login first.")]
        public string? Username { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        public string? Password { get; set; }

        [Required(ErrorMessage ="Withdrawal amount is required.")]
        public double Amount { get; set; }
    }
}
