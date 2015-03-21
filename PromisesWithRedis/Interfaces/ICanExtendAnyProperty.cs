
namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyProperty : ICanExtendAnyHarborBaseType<IAmAHarborProperty>
	{
        IAmAHarborProperty Property { get; }
    }
}
