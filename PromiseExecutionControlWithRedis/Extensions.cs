using System;
using Termine.Promises.Interfaces;
using StackExchange.Redis;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static TX WithDuplicatePrevention<TX, TW>(this TX promise)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            /*
            var duplicationValidator = new PromiseActionInstance<TW>("request.duplicationPrevention", workload =>
            {
                var requestId = workload.PromiseId;

                using (var redisClient = new RedisClient(Config.RedisUri))
                {
                    if (redisClient.Exists(Request.RequestId.RGuidHash()) == 1)
                    {
                        BlockedOnDuplicateRequest.Invoke(this, EventArgs.Empty);
                        return true;
                    }

                    redisClient.Add(Request.RequestId.RGuidHash(), DateTime.UtcNow, TimeSpan.FromDays(1));
                }
            });

            return promise.WithValidator(duplicationValidator
             */
            return promise;
        }
    }
}
