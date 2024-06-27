using Task.Data.Modals;
using Task.Services.Modals;
using Task.Services.Modals.User;
using Task.Services.Modals.User.Account;

namespace Task.Services.Services
{
    public interface IUserManagement
    {
        Task<Response<UserRegistrationResponse>> CreateUserAsync(UserRegistration userRegistration);

        Task<Response<UserLoginResponse>> LoginUserAsync(UserLogin userLogin);

        Task<Response<List<string>>> AssignRoleToUserAsync(List<string> roles, ApplicationUser user);

 
    }
}
