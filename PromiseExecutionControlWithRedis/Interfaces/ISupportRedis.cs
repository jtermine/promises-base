using Termine.Promises.Interfaces;

namespace Termine.Promises.ExectionControlWithRedis.Interfaces
{
    public interface ISupportRedis: IAmAPromiseWorkload
    {
        string RedisConnectionString { get; set; }
    }
}
