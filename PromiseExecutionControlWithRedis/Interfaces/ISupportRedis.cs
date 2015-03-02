using Termine.Promises.Interfaces;

namespace Termine.Promises.ExectionControlWithRedis.Interfaces
{
    public interface ISupportRedis: IHandlePromiseConfig
    {
        string RedisConnectionString { get; set; }
    }
}
