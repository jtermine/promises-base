using Termine.Promises.WithRedis.Enumerables;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface IAmAHarborTemporalRelationship: IAmAHarborRelationship, ICanExtendAnyTemporalRelationship
    {
        EnumTemporalRelationship_ConflictMode ConflictMode { get; set; }
    }
}