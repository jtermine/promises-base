using PromisesBaseFrameworkTest.Interfaces;
using Termine.Promises.Base;
using Termine.Promises.Base.Interfaces;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
	public static class ClaimsPromiseFactory
	{
		public static Promise<TC, TU, TW, TR, TE> Get<TC, TU, TW, TR, TE>()
			where TC : class, IHandlePromiseConfig, new()
            where TU: class, IAmAPromiseUser, new()
			where TW : class, IAmAPromiseWorkload, new()
			where TR : class, IAmAPromiseRequest, new()
			where TE : class, IAmAPromiseResponse, new()
		{
			var promise = new Promise<TC, TU, TW, TR, TE>(true);

			promise.WithAuthChallenger("claims-authChallenger",
                (func => string.IsNullOrEmpty(func.Rq.RequestId)
                    ? Resp.AbortOnAccessDenied("Claim is null or empty.")
                    : Resp.Success()));

			return promise;
		}
	}
}