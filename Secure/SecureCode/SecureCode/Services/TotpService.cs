using SecureCode.Exceptions;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;
using SecureCode.Services.Helpers;

namespace SecureCode.Services
{
    public class TotpService : ITotpService
    {
        public TotpSetup Generate(string issuer, string accountIdentity, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true)
        {
            if(issuer == null) { throw new InternalServerErrorException("Issuer not found."); }
            if(accountIdentity == null) { throw new InternalServerErrorException("Account not found."); }
            if(accountSecretKey == null) { throw new InternalServerErrorException("Key not found."); }

            accountIdentity = accountIdentity.Replace(" ", "");
            var encodedSecretKey = Base32.Encode(accountSecretKey);
            var provisionUrl = Helpers.UrlEncoder.Encode($"otpauth://totp/{accountIdentity}?secret={encodedSecretKey}&issuer={Helpers.UrlEncoder.Encode(issuer)}");
            var protocol = useHttps ? "https" : "http";
            var url = $"{protocol}://chart.googleapis.com/chart?cht=qr&chs={qrCodeWidth}x{qrCodeHeight}&chl={provisionUrl}";

            return new TotpSetup(encodedSecretKey, GetQrImage(url));
        }

        private static byte[] GetQrImage(string url, int timeoutInSeconds = 30)
        {
            try
            {
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) };
                var res = client.GetAsync(url).Result;

                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new InternalServerErrorException("Unexpected result from the Google QR web site.");

                return res.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception exception)
            {
                throw new InternalServerErrorException("Unexpected result from the Google QR web site.\n" + exception);
            }
        }

        public bool Validate(string accountSecretKey, int clientTotp, int timeToleranceInSeconds = 60)
        {
            var codes = GetValidTotps(accountSecretKey, TimeSpan.FromSeconds(timeToleranceInSeconds));
            return codes.Any(c => c == clientTotp);
        }
        public IEnumerable<int> GetValidTotps(string accountSecretKey, TimeSpan timeTolerance)
        {
            var codes = new List<int>();
            var iterationCounter = GetCurrentCounter();
            var iterationOffset = 0;

            if (timeTolerance.TotalSeconds > 30)
            {
                iterationOffset = Convert.ToInt32(timeTolerance.TotalSeconds / 30.00);
            }

            var iterationStart = iterationCounter - iterationOffset;
            var iterationEnd = iterationCounter + iterationOffset;

            for (var counter = iterationStart; counter <= iterationEnd; counter++)
            {
                codes.Add(Generate(accountSecretKey, counter));
            }

            return codes.ToArray();
        }
        private long GetCurrentCounter()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / 30;
        }
        private int Generate(string accountSecretKey, long counter, int digits = 6)
        {
            return TotpHasher.Hash(accountSecretKey, counter, digits);
        }
    }
}
