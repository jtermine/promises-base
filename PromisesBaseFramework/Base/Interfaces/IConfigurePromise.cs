namespace Termine.Promises.Base.Interfaces
{
	public interface IConfigurePromise<in TC, in TW, in TR, in TE>
        where TC : IHandlePromiseConfig
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
		void Configure(IHandlePromiseEvents<TC, TW, TR, TE> promise);
	}
}
