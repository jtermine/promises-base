namespace Termine.Promises.Interfaces
{
    public interface IPromise
    {
        void Trace();
        void Debug();
        void Info();
        void Warn();
        void Error();
        void Fatal();
        void Abort();
        void AbortOnAccessDenied();
    }
}
