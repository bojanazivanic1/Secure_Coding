namespace SecureCode.Models
{
    public class TotpSetup
    {
        private readonly QrCodeImage _qrCodeImage;

        public TotpSetup(string manualSetupKey, byte[] imageBytes)
        {
            ManualSetupKey = manualSetupKey;
            _qrCodeImage = new QrCodeImage(imageBytes);
        }

        public string ManualSetupKey { get; }
        public string QrCodeImage => _qrCodeImage.DataUri;
        public byte[] QrCodeImageBytes => _qrCodeImage.Bytes;
    }
}
