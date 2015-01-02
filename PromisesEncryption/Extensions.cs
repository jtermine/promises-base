using Sodium;

namespace Termine.Promises.Encryption
{
    public static class Extensions
    {
        public static EncryptedRequest Encrypt(this byte[] message, byte[] key)
        {
            var nonce = StreamEncryption.GenerateNonce();

            var payloadBytes = StreamEncryption.Encrypt(message, nonce, key);

            return new EncryptedRequest(message, payloadBytes, nonce, key);
        }

        public static byte[] Decrypt(this EncryptedRequest request, byte[] key)
        {
            var response = StreamEncryption.Decrypt(request.EncryptedPayloadBytes, request.NonceBytes, key);

            return response;
        }

        public static EncryptedRequest Pack(this EncryptedRequest request, string clientId, byte[] publicKeyBytes)
        {
            if (request == null) return null;

            request.ClientId = clientId;

            request.SharedKeyEncryptedBytes =  PublicKeyAuth.Sign(request.SharedKeyBytes, publicKeyBytes);

            return request;
        }

        public static EncryptedRequest Unpack(this EncryptedRequest request, byte[] privateKeyBytes)
        {
            if (request == null) return null;

            request.SharedKeyBytes = PublicKeyAuth.Verify(request.SharedKeyEncryptedBytes, privateKeyBytes);

            request.PayloadBytes = request.Decrypt(request.SharedKeyBytes);

            return request;
        }

        public static KeyPair GetKeyPair()
        {
            return PublicKeyAuth.GenerateKeyPair();
        }

    }
}
