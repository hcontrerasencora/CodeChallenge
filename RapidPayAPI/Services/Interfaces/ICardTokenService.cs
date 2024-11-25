using RapidPayAPI.Models;

namespace RapidPayAPI.Services.Interfaces
{
    public interface ICardTokenService
    {
        string CreateToken(CreateCardRequest createCardRequest);
    }
}
