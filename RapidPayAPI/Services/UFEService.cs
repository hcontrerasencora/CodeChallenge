namespace RapidPayAPI.Services
{
    public class UFEService
    {
        private DateTime expirationTime;
        private double lastFeeAmount;
        private double feeAmount;
        private static readonly Lazy<UFEService> _instance = new Lazy<UFEService>(() => new UFEService());
        private static readonly Object obj = new Object();


        private UFEService()
        {
            expirationTime = DateTime.UtcNow.AddMinutes(-10);
            lastFeeAmount = GetRandomDecimal();
        }

        public static UFEService Instance => _instance.Value;

        public double GetFeeAmount()
        {
            lock (obj) 
            {
                if (DateTime.UtcNow > expirationTime)
                { 
                    // Set the new expiration date.
                    expirationTime = DateTime.UtcNow.AddMinutes(60);

                    // Set the new fee amount.
                    feeAmount = lastFeeAmount * GetRandomDecimal();

                    // Replace the last fee with the new fee.
                    lastFeeAmount = feeAmount;
                }
            }

            return feeAmount;
        }

        private double GetRandomDecimal() 
        {
            Random rand = new Random();
            return rand.NextDouble() * 2.0;
        }
    }
}
