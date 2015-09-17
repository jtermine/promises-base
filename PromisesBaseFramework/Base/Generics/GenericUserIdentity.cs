using System.Collections.Generic;
using Nancy.Security;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericUserIdentity: IAmAPromiseUser
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; } = new List<string>();
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public IAmAPromiseUser AddClaim(string claim)
        {
            if ((Claims as List<string>) == null) return this;
            ((List<string>) Claims).Add(claim);
            return this;
        }

        public static GenericUserIdentity Init(IUserIdentity userIdentity)
        {
            var genericUserIdentity = new GenericUserIdentity
            {
                UserName = userIdentity.UserName,
                Claims = userIdentity.Claims
            };

            return genericUserIdentity;
        }

    }
}