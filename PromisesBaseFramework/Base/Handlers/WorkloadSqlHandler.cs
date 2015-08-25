using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Termine.Promises.Base.Interfaces;
using Termine.Promises.Helpers;

namespace Termine.Promises.Base.Handlers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WorkloadSqlHandler<TC, TW, TR, TE>
        where TC : class, IHandlePromiseConfig, new()
        where TW : class, IAmAPromiseWorkload, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE : class, IAmAPromiseResponse, new()
    {
        public Action<WorkloadSqlHandlerConfig, IHandlePromiseActions, TC, TW, TR, TE> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
        public WorkloadSqlHandlerConfig WorkloadSqlHandlerConfig { get; set; }
        public WorkloadSqlActionResultsDelegate SqlAction { get; set; }

        public WorkloadSqlHandler()
        {
            ResetToDefaultAction();
        }

        public void ResetToDefaultAction()
        {
            Action = (gc, p, c, w, rq, rx) =>
            {
                if (string.IsNullOrEmpty(gc.SqlFile))
                {
                    p.Abort("No Sql File was provided to this handler.");
                    return;
                }

                var sql = Functions.GetFileFromResource(gc.Assembly, gc.SqlFile);

                if (string.IsNullOrEmpty(sql))
                {
                    p.Abort($"The sql file was not located in the assembly provided > {gc.Assembly?.FullName}.");
                    return;
                }

                using (var conn = new SqlConnection(gc.ConnectionString))
                {
                    var cmd = new CommandDefinition(sql, w);
                    gc.SendResultsTo = new List<object>(SqlAction.Invoke(conn, cmd).Cast<object>());
                }

                if (!gc.DoesAllowNullResponse && !gc.SendResultsTo.Cast<object>().Any())
                {
                    p.Abort("No entries returned from specified values.");
                }
            };
        }
    }
}
