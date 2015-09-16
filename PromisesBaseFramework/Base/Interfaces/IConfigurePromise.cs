namespace Termine.Promises.Base.Interfaces
{
	public interface IConfigurePromise<in TC, in TU, in TW, in TR, in TE>
        where TC : IHandlePromiseConfig
        where TU : IAmAPromiseUser
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
		void Configure(IHandlePromiseEvents<TC, TU, TW, TR, TE> promise);
	}
}
