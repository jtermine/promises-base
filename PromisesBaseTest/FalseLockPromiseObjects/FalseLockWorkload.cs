using Termine.Promises.ExectionControlWithRedis.Interfaces;

namespace Termine.Promises.Base.Test.FalseLockPromiseObjects
{
    public class FalseLockWorkload: ISupportRedis
    {
        public string RequestId { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsBlocked { get; set; }
        public string RedisConnectionString { get; set; }

        public FalseLockWorkload()
        {
            RedisConnectionString = ConfigHelpers.GetRedisConnString();
        }
    }
}
