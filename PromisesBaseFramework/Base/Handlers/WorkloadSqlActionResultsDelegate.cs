using System.Collections;
using System.Data.SqlClient;
using Dapper;

namespace Termine.Promises.Base.Handlers
{
    public delegate IEnumerable WorkloadSqlActionResultsDelegate(SqlConnection conn, CommandDefinition cmd);
}
