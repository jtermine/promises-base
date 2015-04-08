using System;
using System.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Tabular.GridColumnExtensions;
using Tabular.Promises;
using Tabular.TabModels;

namespace Tabular
{
    public partial class TableForm : XtraForm
    {
	    private readonly StudentTabModel _studentTabModel = new StudentTabModel();
        private readonly DataSet _dataSet = new DataSet("columns");
        private readonly EventSink _timer = new EventSink();

        public TableForm()
        {
            InitializeComponent();

            _dataSet.Tables.Add("tableColumns");

            var dataTable = _dataSet.Tables["tableColumns"];

            dataTable.SyncColumns(new StudentTabModel());

            bindingSource1.DataSource = dataTable;

            gridControl1.DataSource = bindingSource1.DataSource;
            gridView1.Columns.Clear();

            _studentTabModel.ForEach(type => gridView1.Columns.Add(type.AsGridColumn()));

            _timer.Tick += timer1_Tick;
        }

        private void barAddColumn_ItemClick(object sender, ItemClickEventArgs e)
        {

	        var addColumnPromise = AddColumnPromise.Get()
		        .WithWorkloadCtor("workloadCtor", (actions, config, workload, req, res) =>
		        {
			        workload.FormActions = _timer.Queue;
			        workload.DataTable = _dataSet.Tables["tableColumns"];
			        workload.List = _studentTabModel;
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
		        .WithWorkloadCtor("workloadCtor", (actions, config, workload, req, res) =>
		        {
			        workload.DataTable = (bindingSource1.DataSource as DataTable);
			        workload.GridCells = selectedCells;
			        workload.NewValue = "newValue";
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