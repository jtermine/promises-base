namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyModel : ICanExtendAnyHarborBaseType
    {
        IAmAHarborModel H { get; }
    }
}