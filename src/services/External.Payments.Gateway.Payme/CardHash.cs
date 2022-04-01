using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace External.Payments.Gateway.Payme
{
    public class CardHash
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }

        private readonly PaymeService _paymeService;

        public CardHash(PaymeService paymeService)
        {
            _paymeService = paymeService;
        }

        public string Generate()
        {
            using var aesAlg = Aes.Create();

            aesAlg.IV = Encoding.Default.GetBytes(_paymeService.EncryptionKey);
            aesAlg.Key = Encoding.Default.GetBytes(_paymeService.ApiKey);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(CardHolderName + CardNumber + CardExpirationDate + CardCvv);
            }

            return Encoding.ASCII.GetString(msEncrypt.ToArray());
        }
    }
}
