namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyTemporalRelationship: ICanExtendAnyRelationship
    {
        IAmAHarborTemporalRelationship T { get; }
    }
}