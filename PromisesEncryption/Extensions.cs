using Sodium;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static EncryptedRequest Encrypt(this byte[] message, byte[] key)
        {
            var nonce = StreamEncryption.GenerateNonce();

            var payloadBytes = StreamEncryption.Encrypt(message, nonce, key);

            return new EncryptedRequest(payloadBytes, nonce);
        }

        public static byte[] Decrypt(this EncryptedRequest request, byte[] key)
        {
            var response = StreamEncryption.Decrypt(request.PayloadBytes, request.NonceBytes, key);

            return response;
        }

        public static KeyPair GetKeyPair()
        {
            return PublicKeyAuth.GenerateKeyPair();
        }

    }
}
