namespace Termine.Promises.Interfaces
{
    public interface IHavePromiseMethods
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
