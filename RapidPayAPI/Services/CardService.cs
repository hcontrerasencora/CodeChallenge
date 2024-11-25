using RapidPayAPI.Data.Database;
using RapidPayAPI.Data.Entities;
using RapidPayAPI.Exceptions;
using RapidPayAPI.Models;
using RapidPayAPI.Services.Interfaces;

namespace RapidPayAPI.Services
{
    public class CardService : ICardService
    {
        private readonly ICardTokenService cardTokenService;
        private readonly DataContext dataContext;

        public CardService(ICardTokenService cardTokenService, DataContext dataContext) 
        {
            this.cardTokenService = cardTokenService;
            this.dataContext = dataContext;
        }

        public int CreateCard(CreateCardRequest createCardRequest)
        {
            // We should never store credit card information, instead we will tokeize the card and store the token.
            // We sill use Stripe, lookup for testing card at https://docs.stripe.com/testing#cards
            var cardToken = cardTokenService.CreateToken(createCardRequest);
            var newCard = new Card
            {
                HolderName = createCardRequest.HolderName,
                CardNumberLastFourDigits = createCardRequest.Number.Substring(createCardRequest.Number.Length - 4),
                CardToken = cardToken,
                Balance = createCardRequest.Balance
            };

            dataContext.Add(newCard);
            dataContext.SaveChanges();

            return newCard.Id;
        }

        public decimal GetBalance(int cardId)
        {
            var card = dataContext.Cards.Where(x => x.Id == cardId).FirstOrDefault();
            return card == null ? throw new NotFoundException("Card not found") : card.Balance;
        }

        public int Pay(PayRequest payRequest)
        {
            int paymentId;
            using (var dbContextTransaction = dataContext.Database.BeginTransaction())
            {
                var card = dataContext.Cards.Where(x => x.Id == payRequest.CardId).FirstOrDefault();

                if (card == null)
                {
                    throw new NotFoundException("Card not found");
                }

                var fees = (payRequest.Amount * (decimal)UFEService.Instance.GetFeeAmount()) / 100;

                var payment = new PaymentTransaction
                {
                    CardToken = card.CardToken,
                    Amount = payRequest.Amount,
                    Fee = fees,
                    TotalAmount = payRequest.Amount + fees,
                    Description = payRequest.Description
                };

                dataContext.Add(payment);
                card.Balance = card.Balance - payment.TotalAmount;

                dataContext.SaveChanges();
                dbContextTransaction.Commit();

                paymentId = payment.Id;
            }

            return paymentId;
        }
    }
}
