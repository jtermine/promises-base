using System.Collections.Generic;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using Tabular.Workloads;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;

namespace Tabular.Promises
{
	public static class ChangeValuesPromise
    {

		public static Promise<GenericConfig, GenericUserIdentity, SelectedCellsWorkload, GenericRequest, GenericResponse> Get()
		{
			var promise = new Promise<GenericConfig, GenericUserIdentity, SelectedCellsWorkload, GenericRequest, GenericResponse>();

			promise.WithValidator("hasSelectedCells", HasSelectedCells);
			promise.WithValidator("hasDataTable", HasDataTable);
			promise.WithExecutor("changeValue", ChangeValue);

			return promise;
		}
		
        private static Resp HasDataTable(PromiseFunc<GenericConfig, GenericUserIdentity, SelectedCellsWorkload, GenericRequest, GenericResponse> promiseFunc)
        {
            var rowCount = promiseFunc.W.DataTable.Rows.Count;
            return rowCount < 1 ? Resp.Abort(new DataSourceHasNoRows()) : Resp.Success();
        }

        public static SelectedCellsWorkload Prep(IEnumerable<GridCell> gridCells, DataTable dataTable, string newValue )
        {
	        var workload = new SelectedCellsWorkload
	        {
		        GridCells = gridCells,
		        NewValue = newValue,
		        DataTable = dataTable
	        };
			
            return workload;
        }

        private static Resp ChangeValue(PromiseFunc<GenericConfig, GenericUserIdentity, SelectedCellsWorkload, GenericRequest, GenericResponse> promiseFunc)
        {
            foreach (var gridCell in promiseFunc.W.GridCells)
            {
                promiseFunc.P.Trace(new GenericEventMessage(gridCell.Column.FieldName));

                if (gridCell.RowHandle < 0) continue;

                var getRow = promiseFunc.W.DataTable.Rows[gridCell.RowHandle];

                if (gridCell.Column.ColumnType == typeof(string))
                    getRow[gridCell.Column.FieldName] = promiseFunc.W.NewValue;
            }

            return Resp.Success();
        }

        private static Resp HasSelectedCells(PromiseFunc<GenericConfig, GenericUserIdentity, SelectedCellsWorkload, GenericRequest, GenericResponse> promiseFunc)
        {
            return promiseFunc.W.GridCells.Equals(default(IEnumerable<GridCell>)) ? Resp.Abort(new SelectedCellsDoNotExist()) : Resp.Success();
        }

	    private class SelectedCellsDoNotExist : GenericEventMessage
        {
            public SelectedCellsDoNotExist()
            {
                EventPublicMessage = "No selected cells were passed to the promise.";
                IsSensitiveMessage = true;
            }
        }

		private class DataSourceHasNoRows : GenericEventMessage
        {
            public DataSourceHasNoRows()
            {
				EventPublicMessage = "The datasource provided to the promise does not have any rows.";
            }
        }
    }
}