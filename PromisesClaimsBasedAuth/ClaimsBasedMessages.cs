using Termine.Promises.Generics;

namespace Termine.Promises.ClaimsBasedAuth.Base
{
    public static class ClaimsBasedMessages
    {
        public static readonly GenericEventMessage JwtTokenIsNull = new GenericEventMessage(100,
            "The request did not provide an authentication token.");
    
    }
}
