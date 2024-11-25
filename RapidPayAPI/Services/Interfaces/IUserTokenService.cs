using RapidPayAPI.Models;

namespace RapidPayAPI.Services.Interfaces
{
    public interface IUserTokenService
    {
        string CreateToken(UserTokenRequest userTokenRequest);
    }
}
