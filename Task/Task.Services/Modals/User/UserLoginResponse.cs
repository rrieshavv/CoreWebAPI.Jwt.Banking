using System.IdentityModel.Tokens.Jwt;

namespace Task.Services.Modals.User
{
    public class UserLoginResponse
    {
        public string? Token {  get; set; }
        public string? User {  get; set; }
        public DateTime Expiration { get; set; }
    }
}
