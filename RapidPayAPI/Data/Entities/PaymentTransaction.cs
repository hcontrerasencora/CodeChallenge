namespace RapidPayAPI.Data.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string CardToken { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }

    }
}
