using Tabular.TabModels;
using Tabular.Workloads;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace Tabular.Promises
{
    public static class AddRowPromise
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
		        actions.Abort(new GenericEventMessage("The datatable is null blocking the promise from executing.", 1));
        }

        private static void AddColumn(IHandlePromiseActions actions, GenericConfig genericConfig, DataTableWorkload workload, GenericRequest genericRequest, GenericResponse genericResponse)
        {
	        for (var i = 0; i < workload.RowsToAdd; i++)
	        {
		        var harborModel = new StudentHarborModel {LastName = $"Termine_{i+workload.RowStart}"};

				/*
				harborModel.PropertyChanged += (sender, args) =>
				{
					LogManager.GetCurrentClassLogger().Trace($"{args.PropertyName} changed to {harborModel[args.PropertyName]}");
				};
				*/

				workload.FormActions.Enqueue(() => workload.StudentHarborModels.Add(harborModel));
			}
        }
    }
}