using System.Collections.Generic;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface ICanExtendAnyHarborBaseType<TT> where TT : IAmAHarborProperty
    {
        IAmAHarborBaseType H { get; }
        Dictionary<string, TT> Properties { get; }
    }
}