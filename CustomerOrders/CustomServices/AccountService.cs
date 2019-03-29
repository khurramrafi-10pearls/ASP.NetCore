using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomerOrders.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CustomerOrders.CustomServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginDTO form)
        {
            //var user = _users.Where(x => x.Username == loginDto.Username && x.Password == loginDto.Password).SingleOrDefault();

            var user = await _userManager.FindByNameAsync(form.Username);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, form.Password, false);

            if (result.Succeeded)
            {
                var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);
                var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = null,              // Not required as no third-party is involved
                    Audience = null,            // Not required as no third-party is involved
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid", user.Id)
                        //,new Claim("role", user.Role)
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                return token;
            }

            return null;
        }
    }
}
