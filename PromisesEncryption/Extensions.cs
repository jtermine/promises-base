using Sodium;
// ReSharper disable once CheckNamespace


namespace Termine.Promises
{
    public static class Extensions
    {
        public static EncryptedRequest Encrypt(byte[] message, byte[] key)
        {
            var nonce = StreamEncryption.GenerateNonce();

            /*
            var key = new byte[]
                        {
                            163, 131, 167, 58, 217, 15, 129, 240, 162, 46, 222, 202, 176, 227, 143, 77, 230, 61, 52, 10,
                            217, 27, 141, 196, 121, 82, 111, 19, 174, 111, 180, 196
                        };
             */

            var payloadBytes = StreamEncryption.Encrypt(message, nonce, key);

            return new EncryptedRequest(payloadBytes, nonce);
        }

        public static byte[] Decrypt(EncryptedRequest request, byte[] key)
        {
            var response = StreamEncryption.Decrypt(request.PayloadBytes, request.NonceBytes, key);

            return response;
        }

    }
}
