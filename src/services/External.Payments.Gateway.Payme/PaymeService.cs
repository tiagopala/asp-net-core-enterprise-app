namespace External.Payments.Gateway.Payme
{
    public class PaymeService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;

        public PaymeService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}
