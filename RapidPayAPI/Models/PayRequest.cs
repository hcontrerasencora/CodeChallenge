namespace RapidPayAPI.Models
{
    public class PayRequest
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
