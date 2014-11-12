namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseWorkload<TT, TR>
        where TT : IAmAPromiseRequest, new()
        where TR: IAmAPromiseResponse, new()
    {
        TT Request { get; set; }
        TR Response { get; set; }
       
    }
}
