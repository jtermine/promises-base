using System.Collections.Generic;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface IAmAHarborModel: IAmAHarborBaseType, ICanExtendAnyModel, ICanExtendAHarborContainer
	{
        string Name { get; set; }
        string Caption { get; set; }
		string Description { get; set; }
        bool IsPublic { get; set; }

		IAmAHarborContainer C {get; set; }
    }
}
