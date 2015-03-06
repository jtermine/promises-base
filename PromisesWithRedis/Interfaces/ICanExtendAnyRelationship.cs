namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyRelationship : ICanExtendAnyHarborBaseType<IAmAHarborProperty>
    {
        IAmAHarborRelationship R { get; }
    }
}