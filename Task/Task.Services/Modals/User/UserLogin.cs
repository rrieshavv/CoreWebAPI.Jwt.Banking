using System.ComponentModel.DataAnnotations;

namespace Task.Services.Modals.User
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Username is a required field.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        public string? Password { get; set; }
    }
}
