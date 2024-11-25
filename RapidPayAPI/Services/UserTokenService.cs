using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RapidPayAPI.Models;
using RapidPayAPI.Services.Interfaces;
using System.Security.Claims;
using System.Text;

namespace RapidPayAPI.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IConfiguration configuration;
        public UserTokenService(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public string CreateToken(UserTokenRequest userTokenRequest)
        {
            string secretKey = configuration["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Email, userTokenRequest.Email)
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationTimeInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDesc);

            return token;
        }
    }
}
