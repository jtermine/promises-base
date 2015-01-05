using System;
using ServiceStack.Redis;
using Termine.Promises.ExectionControlWithRedis.Interfaces;

namespace Termine.Promises.ExectionControlWithRedis
{
    public static class Extensions
    {
        public static Promise<TW> WithDuplicatePrevention<TW>(this Promise<TW> promise)
            where TW : class, ISupportRedis, new()
        {
            
            var duplicationValidator = new Action<TW>(workload =>
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

            return promise.WithValidator("request.duplicationPrevention", duplicationValidator);
        }

        private static string RGuidHash(this string requestId)
        {
            return string.IsNullOrEmpty(requestId) ? string.Empty : string.Format("urn:requestId:{0}", requestId.Replace("-", "").Replace("{", "").Replace("}", "").ToLowerInvariant());
        }
        
    }
}
