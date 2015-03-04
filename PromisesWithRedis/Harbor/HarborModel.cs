using System;
using System.Collections.Generic;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public class HarborModel : IAmAHarborModel, IDisposable
    {
        public List<HarborProperty> HarborProperties { get; set; }

        public HarborModel()
        {
            HarborProperties = new List<HarborProperty>();
        }

        public void Dispose()
        {
        }
    }
}
