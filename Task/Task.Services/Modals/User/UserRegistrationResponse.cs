using Microsoft.AspNetCore.Identity;
using Task.Data.Modals;

namespace Task.Services.Modals.User
{
    public class UserRegistrationResponse
    {
        public ApplicationUser User { get; set; } = null!;
        
    }
}
