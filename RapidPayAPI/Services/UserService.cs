using RapidPayAPI.Models;
using RapidPayAPI.Services.Interfaces;

namespace RapidPayAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserTokenService tokenService;
        private readonly Dictionary<string, string> moqUsers;
        public UserService(IUserTokenService tokenService) 
        { 
            this.tokenService = tokenService;
            moqUsers = new Dictionary<string, string>
            {
                { "test1@test.com", "123" },
                { "test2@test.com", "123" },
                { "test3@test.com", "123" },
                { "test4@test.com", "123" },
                { "test5@test.com", "123" }
            };
        }
        public string GetToken(UserTokenRequest userTokenRequest)
        {
            if (userTokenRequest != null && moqUsers.TryGetValue(userTokenRequest.Email, out var pwd) && userTokenRequest.Password == pwd)
            { 
                return tokenService.CreateToken(userTokenRequest);
            }

            return string.Empty;
        }
    }
}
