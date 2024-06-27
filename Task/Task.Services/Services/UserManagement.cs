using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task.Data.Modals;
using Task.Services.Modals;
using Task.Services.Modals.User;

namespace Task.Services.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagement(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public async Task<Response<UserRegistrationResponse>> CreateUserAsync(UserRegistration userRegistration)
        {
            // Check whether the user already exists in the database
            var userExists = await _userManager.FindByEmailAsync(userRegistration.Email!);
            if (userExists != null)
            {
                return new Response<UserRegistrationResponse> { IsSuccess = false, StatusCode = 403, Message = "User already exists" };
            }

            // Add the user in the database
            ApplicationUser user = new()
            {
                Email = userRegistration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegistration.UserName,
                InitialDeposit = userRegistration.InitialDeposit,
                Balance = userRegistration.InitialDeposit
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password!);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return new Response<UserRegistrationResponse> { Res = new UserRegistrationResponse() { User = user, Token = token }, IsSuccess = true, StatusCode = 201, Message = "User Created." };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new Response<UserRegistrationResponse> { IsSuccess = false, StatusCode = 500, Message = "Failed to create the user.", Errors = errors };
            }
        }

        public async Task<Response<List<string>>> AssignRoleToUserAsync(List<string> roles, ApplicationUser user)
        {
            var assignedRole = new List<string>();
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    if (!await _userManager.IsInRoleAsync(user, role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        assignedRole.Add(role);
                    }
                }
            }
            return new Response<List<string>> { IsSuccess = true, StatusCode = 200, Message = "Roles has been assigned successfully.", Res = assignedRole };
        }

        public async Task<Response<UserLoginResponse>> LoginUserAsync(UserLogin login)
        {
            // check the user...
            var user = await _userManager.FindByNameAsync(login.UserName!);

            // check the password

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                // claim list creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // add roles to the list
                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // generate the token with the claims
                var jwtToken = GetToken(authClaims);

                return new Response<UserLoginResponse> { Res = new UserLoginResponse() { Token = new JwtSecurityTokenHandler().WriteToken(jwtToken), Expiration = jwtToken.ValidTo, User=user.UserName}, IsSuccess = true, StatusCode = 200, Message = "Login successful." };
            }

            return new Response<UserLoginResponse> {  IsSuccess = false, StatusCode = 401, Message = "Login failed. Please provide valid credentials." };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
