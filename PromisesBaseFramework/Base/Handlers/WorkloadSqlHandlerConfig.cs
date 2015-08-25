using System.Collections;
using System.Reflection;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadSqlHandlerConfig
    {
        public string SqlFile { get; set; }
        public string ConnectionString { get; set; }
        public Assembly Assembly { get; set; }
        public bool DoesAllowNullResponse { get; set; }
        public IEnumerable SendResultsTo { get; set; }
    }
}
