using System.ComponentModel.DataAnnotations;

namespace Task.Services.Modals.User.Account
{
    public class Deposit
    {
        [Required(ErrorMessage ="Username is required. Please login first.")]
        public  string? Username {  get; set; }

        [Required(ErrorMessage = "Deposit amount is required.")]
        public double Amount { get; set; }
    }
}
