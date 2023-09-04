using SecureCode.Models;

namespace SecureCode.Interfaces.IServices
{
    public interface ITotpService
    {
        TotpSetup Generate(string issuer, string accountIdentity, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true);
        bool Validate(string accountSecretKey, int clientTotp, int timeToleranceInSeconds = 60);
    }
}
