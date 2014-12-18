// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public class EncryptedRequest
    {
        public byte[] PayloadBytes { get; set; }
        public byte[] NonceBytes { get; set; }

        public EncryptedRequest ()
        {
            
        }

        public EncryptedRequest(byte[] payloadBytes, byte[] nonceBytes)
        {
            PayloadBytes = payloadBytes;
            NonceBytes = nonceBytes;
        }
    }
}
