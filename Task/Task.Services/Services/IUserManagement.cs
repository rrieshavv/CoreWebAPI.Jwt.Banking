using Task.Data.Modals;
using Task.Services.Modals;
using Task.Services.Modals.User;
using Task.Services.Modals.User.Account;

namespace Task.Services.Services
{
    public interface IUserManagement
    {
        /// <summary>
        /// Asynchronously creates the user account.
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns>A task containing the token and the user.</returns>
        Task<Response<UserRegistrationResponse>> CreateUserAsync(UserRegistration userRegistration);

        /// <summary>
        /// Asynchronously logs in the user into their account.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>A task object containing the token, user and the expiration time of the token.</returns>
        Task<Response<UserLoginResponse>> LoginUserAsync(UserLogin userLogin);

        /// <summary>
        /// Asynchronously assigns roles to the user.
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="user"></param>
        /// <returns>A list of roles assigned to the user.</returns>
        Task<Response<List<string>>> AssignRoleToUserAsync(List<string> roles, ApplicationUser user);

 
    }
}
