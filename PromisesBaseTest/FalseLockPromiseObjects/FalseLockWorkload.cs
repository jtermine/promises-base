using Termine.Promises.ExectionControlWithRedis.Interfaces;
using Termine.Promises.Generics;

namespace Termine.Promises.Base.Test.FalseLockPromiseObjects
{
    public class FalseLockWorkload: GenericWorkload, ISupportRedis
    {
        public string RedisConnectionString { get; set; }

        public FalseLockWorkload()
        {
            RedisConnectionString = ConfigHelpers.GetRedisConnString();
        }

    }
}
