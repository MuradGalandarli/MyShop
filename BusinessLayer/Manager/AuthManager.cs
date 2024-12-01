using BusinessLayer.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SahredLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class AuthManager:IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthManager(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;

        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return (0, "User already exists");

            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
              
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

          await userManager.AddToRoleAsync(user, role);
            
            return (1, "User created successfully!");
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
           
            var user = await userManager.FindByEmailAsync(model.Email );
           
            if (user == null)
                return (0, "Invalid email");
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<(int, string)> ForgotPassword(string email)
        {
            /*  var user =await userManager.FindByEmailAsync(email);
              if (user == null)
              {
                  return (0, "There is not email address");
              }
              if (user != null)
              {
                  var createToken = await userManager.GeneratePasswordResetTokenAsync(user);
                  return (1,createToken.ToString());
              }
              return (0, "null");*/

            var Message = new List<string>();

            if (string.IsNullOrEmpty(email))
            {               
                return (0, "Email cannot be null");
            }

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
              
                return (0, "Internal server error: Auth service is not initialized");
            }


            var token = await userManager.GeneratePasswordResetTokenAsync(user);
          
            return (1, token);


        }

        public async Task<(int, string)> ResetPassword(Resetmodel resetmodel)
        {

           /* var user = await userManager.FindByEmailAsync(resetmodel.Email);
            if(user == null)
            {
                return (0, "There is not user");
            }

            var resetPassword = await userManager.ResetPasswordAsync(user,resetmodel.Token,resetmodel.Password);
            if (resetPassword.Succeeded)
            {
                return (1, "Successed");
            }
            return (1, "Fail");*/


           
            if (string.IsNullOrEmpty(resetmodel.Email))
            {
                return (0, "Email can not empty or null");
            }

            var user = await userManager.FindByEmailAsync(resetmodel.Email);

         
            if (user == null && resetmodel.Password != resetmodel.ConfirmPassword)
            {
               
                return (0, "Invalid email address.");
            }

            var result = await userManager.ResetPasswordAsync(user, resetmodel.Token, resetmodel.Password);
            if (result.Succeeded)
            {
                return (1, "Password has been reset successfully.");
            }

            return (0,"Error");
        }
    }
}
