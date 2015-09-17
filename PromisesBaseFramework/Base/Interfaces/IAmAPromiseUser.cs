using Nancy.Security;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseUser: IUserIdentity
    {
        string Email { get; set; }
        string DisplayName { get; set; }

        IAmAPromiseUser AddClaim(string claim);

    }
}
