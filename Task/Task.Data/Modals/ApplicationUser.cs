
using Microsoft.AspNetCore.Identity;

namespace Task.Data.Modals
{
    public class ApplicationUser: IdentityUser
    {
        public double Balance { get; set; }
        public double InitialDeposit { get; set; }
    }
}
