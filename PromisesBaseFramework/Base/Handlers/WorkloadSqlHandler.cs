using System;
using System.Data.SqlClient;
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
        public Action<WorkloadSqlHandlerConfig<TW>, IHandlePromiseActions, TC, TW, TR, TE> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
        public Action<WorkloadSqlHandlerConfig<TW>> Configurator { get; set; }
        public Action<SqlConnection, CommandDefinition, IHandlePromiseActions, TC, TW, TR, TE> SqlAction { get; set; }

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

                var sql = Functions.GetFileFromResource(gc.SqlFile);

                using (var conn = new SqlConnection(gc.ConnectionString))
                {
                    var cmd = new CommandDefinition(sql, w);
                    SqlAction.Invoke(conn, cmd, p, c, w, rq, rx);
                }
            };
        }
    }
}
