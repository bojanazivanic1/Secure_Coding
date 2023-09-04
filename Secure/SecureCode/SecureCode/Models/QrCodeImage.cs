namespace SecureCode.Models
{
    public class QrCodeImage
    {
        public QrCodeImage(byte[] bytes)
        {
            Bytes = bytes;
        }

        public string DataUri => @"data:image/png;base64," + Convert.ToBase64String(Bytes);
        public byte[] Bytes { get; }
    }
}
