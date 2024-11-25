using RapidPayAPI.Models;

namespace RapidPayAPI.Services.Interfaces
{
    public interface ICardService
    {
        public int CreateCard(CreateCardRequest createCardRequest);

        public int Pay(PayRequest payRequest);

        public decimal GetBalance(int cardId);
    }
}
