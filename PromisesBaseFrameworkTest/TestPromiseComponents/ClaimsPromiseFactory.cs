using PromisesBaseFrameworkTest.Interfaces;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
	public static class ClaimsPromiseFactory
	{
		public static Promise<TC, TU, TW, TR, TE> Get<TC, TU, TW, TR, TE>()
			where TC : class, IHandlePromiseConfig, new()
            where TU: class, IAmAPromiseUser, new()
			where TW : class, IAmAPromiseWorkload, new()
			where TR : class, IAmAClaimsRequest, new()
			where TE : class, IAmAPromiseResponse, new()
		{
			var promise = new Promise<TC, TU, TW, TR, TE>();

			promise.WithAuthChallenger("claims-authChallenger",
				(p, c, u, w, rq, rx) =>
				{
					if (string.IsNullOrEmpty(rq.Claim))
						p.AbortOnAccessDenied(new GenericEventMessage("Claim is null or empty"));
				});

			return promise;
		}
	}
}