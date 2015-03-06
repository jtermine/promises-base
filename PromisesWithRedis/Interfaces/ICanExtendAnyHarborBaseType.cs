using System.Collections.Generic;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyHarborBaseType<TT> where TT : IAmAHarborProperty
    {
        IAmAHarborBaseType HarborBaseInstance { get; }
        Dictionary<string, TT> Properties { get; }
		IDictionary<string, IAmAHarborRelationship> Relationships { get; }
    }
}