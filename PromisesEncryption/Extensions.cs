using Sodium;

namespace Termine.Promises.Encryption
{
    public static class Extensions
    {
        /// <summary>
        /// Encrypts a stream of bytes using a secret key and boxes this into an EncryptedRequest
        /// </summary>
        /// <param name="message">the stream of bytes to encrypt</param>
        /// <param name="key">the secret key to use</param>
        /// <returns>an EncryptedRequest that contains the message, the encrypted payload, a nonce, and the secret key</returns>
        public static EncryptedRequest Encrypt(this byte[] message, byte[] key)
        {
            var nonce = StreamEncryption.GenerateNonce();

            var payloadBytes = StreamEncryption.Encrypt(message, nonce, key);

            return new EncryptedRequest(message, payloadBytes, nonce, key);
        }

        /// <summary>
        /// Decrypts the byte array in an EncryptedRequest.EncryptedPayloadBytes using a given secret key and nonce
        /// </summary>
        /// <param name="request">the EncryptedRequest that has the property EncryptedPayloadBytes and NonceBytes populated</param>
        /// <param name="key">the secret key used to decrypt the bytes</param>
        /// <returns>a an array of bytes that is the decrypted message</returns>
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
