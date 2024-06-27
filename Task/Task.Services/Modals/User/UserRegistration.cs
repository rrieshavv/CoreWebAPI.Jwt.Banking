using System.ComponentModel.DataAnnotations;

namespace Task.Services.Modals.User
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "User Name is required.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Initial deposit is required.")]
        public double InitialDeposit { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public List<string>? Roles { get; set; }
    }
}
