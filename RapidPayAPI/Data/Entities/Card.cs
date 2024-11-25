namespace RapidPayAPI.Data.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string HolderName { get; set; }
        public string CardNumberLastFourDigits { get; set; }
        public string CardToken { get; set; }
        public decimal Balance { get; set; }
    }
}
