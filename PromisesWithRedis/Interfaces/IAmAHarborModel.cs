using System.Collections.Generic;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface IAmAHarborModel: IAmAHarborBaseType, ICanExtendAnyModel
    {
        string Name { get; set; }
        string Caption { get; set; }
        bool IsPublic { get; set; }
    }
}
