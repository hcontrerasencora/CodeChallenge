using RapidPayAPI.Models;

namespace RapidPayAPI.Services.Interfaces
{
    public interface IUserService
    {
        string GetToken(UserTokenRequest userTokenRequest);
    }
}
