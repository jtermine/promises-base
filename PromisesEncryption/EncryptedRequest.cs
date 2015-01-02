// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public class EncryptedRequest
    {
        public string ClientId { get; set; }
        public byte[] PayloadBytes { get; set; }
        public byte[] EncryptedPayloadBytes { get; set; }
        public byte[] NonceBytes { get; set; }
        public byte[] SharedKeyBytes { get; set; }
        public byte[] SharedKeyEncryptedBytes { get; set; }
        public byte[] SignatureKeyBytes { get; set; }
        public byte[] PayloadSignatureBytes { get; set; }
        
        public EncryptedRequest ()
        {
            
        }

        public EncryptedRequest(byte[] payloadBytes, byte[] encryptedPayloadBytes, byte[] nonceBytes, byte[] sharedKeyBytes)
        {
            PayloadBytes = payloadBytes;
            EncryptedPayloadBytes = encryptedPayloadBytes;
            NonceBytes = nonceBytes;
            SharedKeyBytes = sharedKeyBytes;
        }
    }
}