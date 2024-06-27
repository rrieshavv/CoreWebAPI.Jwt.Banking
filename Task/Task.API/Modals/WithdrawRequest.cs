using System.ComponentModel.DataAnnotations;

namespace Task.API.Modals
{
    public class WithdrawRequest
    {
        [Required(ErrorMessage ="Amount is required.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
