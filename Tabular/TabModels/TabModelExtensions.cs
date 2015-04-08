using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tabular.DataColumnExtensions;
using Tabular.Types;

namespace Tabular.TabModels
{
    public static class TabModelExtensions
    {
        public static IEnumerable<DataColumn> GetColumns(this IEnumerable<IColumnDefinitionType> tabModel)
        {
            var columnDefinitionTypes = tabModel as IColumnDefinitionType[] ?? tabModel.ToArray();

            var columns = columnDefinitionTypes.Select(f => f.AsDataColumn()).ToArray();

            return columns;
        }

        public static void SyncColumns(this DataTable dataTable, IEnumerable<IColumnDefinitionType> tabModel)
        {
            var columnDefinitionTypes = tabModel as IColumnDefinitionType[] ?? tabModel.ToArray();

            lock (Program.Lock)
            {
                foreach (var columnDefinitionType in columnDefinitionTypes.Where(columnDefinitionType => !dataTable.Columns.Contains(columnDefinitionType.Name)))
                {
                    dataTable.Columns.Add(columnDefinitionType.AsDataColumn());
                }
            }
        }

        public static DataRow GetDataRow(this DataTable dataTable, IEnumerable<IColumnDefinitionType> tabModel)
        {
            var columnDefinitionTypes = tabModel as IColumnDefinitionType[] ?? tabModel.ToArray();

            lock (Program.Lock)
            {
                var row = dataTable.NewRow();

                foreach (var dc in columnDefinitionTypes.GetColumns().Select(dataColumn => dataColumn as IdentityDataColumn).Where(isDataColumn => isDataColumn != null))
                {
                    row[dc.ColumnName] = Guid.NewGuid().ToString("N");
                }

                dataTable.Rows.Add(row);

                return row;

            }
        }
    }
}