namespace Termine.Promises.ClaimsBasedAuth.Base.Interfaces
{
    public interface IConfigureClaims
    {
        string HmacSigningKey { get; }
        string HmacAudienceUri { get; }
        string HmacIssuer { get; }

    }
}
