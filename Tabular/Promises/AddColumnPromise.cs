using NLog;
using Tabular.TabModels;
using Tabular.Workloads;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace Tabular.Promises
{
    public static class AddColumnPromise
    {
		public static Promise<GenericConfig, DataTableWorkload, GenericRequest, GenericResponse> Get()
	    {
		    var promise = new Promise<GenericConfig, DataTableWorkload, GenericRequest, GenericResponse>();

		    promise.WithValidator("dataTableNotNull", DataTableNotNull);
		    promise.WithExecutor("addColumn", AddColumn);

		    return promise;
	    }

	    private static void DataTableNotNull(IHandlePromiseActions actions, GenericConfig genericConfig, DataTableWorkload dataTableWorkload, GenericRequest genericRequest, GenericResponse genericResponse)
        {
	        if (dataTableWorkload.StudentHarborModels == null)
		        actions.Abort(new GenericEventMessage(0, 1, "The datatable is null blocking the promise from executing."));
        }

        private static void AddColumn(IHandlePromiseActions actions, GenericConfig genericConfig, DataTableWorkload dataTableWorkload, GenericRequest genericRequest, GenericResponse genericResponse)
        {
			var harborModel = new StudentHarborModel();

	        //harborModel.PropertyChanged += (sender, args) =>
	        //{
		       // LogManager.GetCurrentClassLogger().Trace($"{args.PropertyName} changed to {harborModel[args.PropertyName]}");
	        //};

			dataTableWorkload.FormActions.Enqueue(() => dataTableWorkload.StudentHarborModels.Add(harborModel));
        }
    }
}