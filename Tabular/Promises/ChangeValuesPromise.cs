using System.Collections.Generic;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using Tabular.Workloads;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace Tabular.Promises
{
	public static class ChangeValuesPromise
    {

		public static Promise<GenericConfig, SelectedCellsWorkload, GenericRequest, GenericResponse> Get()
		{
			var promise = new Promise<GenericConfig, SelectedCellsWorkload, GenericRequest, GenericResponse>();

			promise.WithValidator("hasSelectedCells", HasSelectedCells);
			promise.WithValidator("hasDataTable", HasDataTable);
			promise.WithExecutor("changeValue", ChangeValue);

			return promise;
		}
		
        private static void HasDataTable(IHandlePromiseActions actions, GenericConfig genericConfig, SelectedCellsWorkload selectedCellsWorkload, GenericRequest genericRequest, GenericResponse genericResponse)
        {
            var rowCount = selectedCellsWorkload.DataTable.Rows.Count;

            if (rowCount < 1) actions.Abort(new DataSourceHasNoRows());
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

        private static void ChangeValue(IHandlePromiseActions actions, GenericConfig genericConfig, SelectedCellsWorkload selectedCellsWorkload, GenericRequest genericRequest, GenericResponse genericResponse)
        {
            foreach (var gridCell in selectedCellsWorkload.GridCells)
            {
                actions.Trace(new GenericEventMessage(gridCell.Column.FieldName));

                if (gridCell.RowHandle < 0) continue;

                var getRow = selectedCellsWorkload.DataTable.Rows[gridCell.RowHandle];

                if (gridCell.Column.ColumnType == typeof(string))
                    getRow[gridCell.Column.FieldName] = selectedCellsWorkload.NewValue;
            }
        }

        private static void HasSelectedCells(IHandlePromiseActions actions, GenericConfig genericConfig, SelectedCellsWorkload selectedCellsWorkload, GenericRequest genericRequest, GenericResponse genericResponse)
        {
            if (selectedCellsWorkload.GridCells.Equals(default(IEnumerable<GridCell>))) actions.Abort(new SelectedCellsDoNotExist());
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