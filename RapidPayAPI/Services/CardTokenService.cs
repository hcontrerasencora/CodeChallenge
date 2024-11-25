using RapidPayAPI.Models;
using RapidPayAPI.Services.Interfaces;
using Stripe;

namespace RapidPayAPI.Services
{
    public class CardTokenService : ICardTokenService
    {
        public CardTokenService(IConfiguration configuration) 
        {
            // Using test api key, https://docs.stripe.com/keys#obtain-api-keys
            StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];
        }

        public string CreateToken(CreateCardRequest createCardRequest)
        {
            try
            {
                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = createCardRequest.Number,
                        ExpMonth = createCardRequest.ExpiryMonth,
                        ExpYear = createCardRequest.ExpiryYear,
                        Cvc = createCardRequest.Cvc,
                    }
                };

                var tokenService = new TokenService();
                Token token = tokenService.Create(tokenOptions);

                return token.Id;
            }
            catch (StripeException ex)
            { 
                throw new Exception(ex.Message);
            }
        }
    }
}
