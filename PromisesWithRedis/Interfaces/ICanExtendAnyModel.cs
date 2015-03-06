namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyModel : ICanExtendAnyHarborBaseType<IAmAHarborProperty>
    {
        IAmAHarborModel HarborModelInstance { get; }
    }
}