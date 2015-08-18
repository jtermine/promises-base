using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadSqlHandlerConfig<TW>
        where TW : class, IAmAPromiseWorkload, new()
    {
        public string SqlFile { get; set; }
        public string ConnectionString { get; set; }
        
    }
}
