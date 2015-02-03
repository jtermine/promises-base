using System;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseWorkload
    {
        string RequestId { get; set; }
        string RequestName { get; set; }
        bool IsTerminated { get; set; }
        bool IsBlocked { get; set; }

        void WithRequestId(string requestId);

        IAmAPromiseRequest GetRequest();
    }
}
