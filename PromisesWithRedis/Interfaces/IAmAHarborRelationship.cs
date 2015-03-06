using System.Collections.Generic;
using Termine.Promises.WithRedis.Enumerables;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface IAmAHarborRelationship: IAmAHarborBaseType
    {
		string Name { get; set; }
		string Caption { get; set; }
        bool IsActive { get; set; }
        int MaxCapacity { get; set; }
        bool CanWaitlist { get; set; }
        EnumRelationship_SingletonMode SingletonMode { get; set; }
        List<string> Models { get; set; }
    }
}