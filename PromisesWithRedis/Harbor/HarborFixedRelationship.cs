using System.Collections.Generic;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public class HarborFixedRelationship: IAmAHarborFixedRelationship
    {
        public IAmAHarborBaseType H { get { return this; } }
        public Dictionary<string, IAmAHarborProperty> Properties { get; private set; }

        public HarborFixedRelationship()
        {
            Properties = new Dictionary<string, IAmAHarborProperty>();
        }
    }
}
