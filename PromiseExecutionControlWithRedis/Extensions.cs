using System;
using ServiceStack.Redis;
using Termine.Promises.ExectionControlWithRedis;
using Termine.Promises.ExectionControlWithRedis.Interfaces;
using Termine.Promises.Interfaces;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static TX WithDuplicatePrevention<TX, TW>(this TX promise)
            where TX : IAmAPromise<TW>
            where TW : class, ISupportRedis, new()
        {
            
            var duplicationValidator = new PromiseActionInstance<TW>("request.duplicationPrevention", workload =>
            {
                var requestId = workload.RequestId;

                using (var redisClient = new RedisClient(workload.RedisConnectionString))
                {
                    if (redisClient.Exists(requestId.RGuidHash()) == 1)
                    {
                        promise.Block(ExecutionControlMessages.PromiseIsADuplicate(requestId));
                    }

                    redisClient.Add(requestId.RGuidHash(), DateTime.UtcNow, TimeSpan.FromDays(1));
                }
            });

            return promise.WithValidator(duplicationValidator);
        }

        private static string RGuidHash(this string requestId)
        {
            return string.IsNullOrEmpty(requestId) ? string.Empty : string.Format("urn:requestId:{0}", requestId.Replace("-", "").Replace("{", "").Replace("}", "").ToLowerInvariant());
        }
        
    }
}
