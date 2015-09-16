using System;
using System.Collections.ObjectModel;
using System.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Tabular.Promises;
using Tabular.TabModels;

namespace Tabular
{
    public partial class TableForm : XtraForm
    {
	    private readonly ObservableCollection<StudentHarborModel> _dataSet = new ObservableCollection<StudentHarborModel>();
        private readonly EventSink _timer = new EventSink();
	    private int _rowStart;

        public TableForm()
        {
	        InitializeComponent();

			var model = new StudentHarborModel();

			bindingSource1.DataSource = _dataSet;

            gridControl1.DataSource = bindingSource1.DataSource;
            gridView1.Columns.Clear();

	        gridView1.Columns.AddRange(model.TabularModel.GetColumns());

            _timer.Tick += timer1_Tick;
        }

        private void barAddRow_ItemClick(object sender, ItemClickEventArgs e)
        {

			var addColumnPromise = AddRowPromise.Get()
				.WithWorkloadCtor("workloadCtor", (p, c, u, w, rq, rx) =>
				{
					w.FormActions = _timer.Queue;
					w.StudentHarborModels = _dataSet;
					w.RowsToAdd = 1000;
					w.RowStart = _rowStart;
				})
				.WithExecutor("updateRowStart", (p, c, u, w, rq, rx) =>
				{
					_rowStart = _rowStart + w.RowsToAdd;
				});


	        addColumnPromise.Run();
        }

        private void barOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
			/*
            var fileDialog = openFileDialog1.ShowDialog(this);

            switch (fileDialog)
            {
                case DialogResult.OK:
                    new OpenTabularFilePromise()
                        .WithFileName(openFileDialog1.FileName)
                        .RunAsync();
                    break;
            }
			*/
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            var view = sender as GridView;

	        (view?.ActiveEditor as LookUpEdit)?.ShowPopup();

            //throw new DevExpress.Utils.HideException();
        }

        private void barChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0) return;

            var selectedCells = gridView1.GetSelectedCells();

	        var promise = ChangeValuesPromise.Get()
		        .WithWorkloadCtor("workloadCtor", (p, c, u, w, rq, rx) =>
		        {
			        w.DataTable = (bindingSource1.DataSource as DataTable);
			        w.GridCells = selectedCells;
			        w.NewValue = "newValue";
		        });

	        promise.Run();
        }

        private void timer1_Tick(object sender, Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            Invoke(action);
        }
    }
}