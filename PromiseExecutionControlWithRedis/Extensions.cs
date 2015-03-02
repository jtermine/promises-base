using System;
using ServiceStack.Redis;
using Termine.Promises.ExectionControlWithRedis.Interfaces;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ExectionControlWithRedis
{
    public static class Extensions
    {
        public static Promise<TC, TW, TR> WithDuplicatePrevention<TC,TW,TR>(this Promise<TC, TW, TR> promise)
            where TC : class, ISupportRedis, new()
            where TW : class, IAmAPromiseWorkload, new()
            where TR : class, IAmAPromiseRequest, new()
        {
            
            var duplicationValidator = new Action<IHandlePromiseActions, TC, TW, TR>((promiseActions,config,workload,request) =>
            {
                var requestId = workload.RequestId;

                using (var redisClient = new RedisClient(config.RedisConnectionString))
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
