using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseUser: IIdentity
    {
        string Email { get; }
        string DisplayName { get; }
        List<Claim> Claims { get; }

        void Authenticate(string userName, string displayName = "", string email = "", string authenticationType = "None",
            IEnumerable<Claim> claims = default(IEnumerable<Claim>));
    }
}