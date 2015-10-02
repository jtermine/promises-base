using System.Collections.Generic;
using System.Security.Claims;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericUserIdentity: IAmAPromiseUser
    {
        public string Email { get; private set; }
        public string DisplayName { get;  private set; } = "Anonymous User";
        public List<Claim> Claims { get; } = new List<Claim>();
        public string Name { get; private set; } = "Anonymous";
        public string AuthenticationType { get; private set; } = "None";
        public bool IsAuthenticated => GetAuthenticatedStatus();

        private bool GetAuthenticatedStatus()
        {
            return !string.IsNullOrEmpty(Name) && Name != "Anonymous";
        }

        public void Authenticate(string userName, string displayName = "", string email = "", string authenticationType = "None",
            IEnumerable<Claim> claims = default(IEnumerable<Claim>))
        {
            Name = userName;
            DisplayName = displayName;
            Email = email;
            AuthenticationType = authenticationType;

            Claims.Clear();
            if (claims != null) Claims.AddRange(claims);
        }
    }
}