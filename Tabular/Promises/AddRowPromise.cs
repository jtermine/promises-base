using Tabular.TabModels;
using Tabular.Workloads;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;

namespace Tabular.Promises
{
    public static class AddRowPromise
    {
		public static Promise<GenericConfig, GenericUserIdentity, DataTableWorkload, GenericRequest, GenericResponse> Get()
	    {
		    var promise = new Promise<GenericConfig, GenericUserIdentity, DataTableWorkload, GenericRequest, GenericResponse>();

		    promise.WithValidator("dataTableNotNull", DataTableNotNull);
		    promise.WithExecutor("addColumn", AddColumn);

		    return promise;
	    }

        private static Resp DataTableNotNull(
            PromiseFunc<GenericConfig, GenericUserIdentity, DataTableWorkload, GenericRequest, GenericResponse>
                promiseFunc)
        {
            return promiseFunc.W.StudentHarborModels == null
                ? Resp.Abort(new GenericEventMessage("The datatable is null blocking the promise from executing.", 1))
                : Resp.Success();
        }

        private static Resp AddColumn(
            PromiseFunc<GenericConfig, GenericUserIdentity, DataTableWorkload, GenericRequest, GenericResponse> func)
        {
            for (var i = 0; i < func.W.RowsToAdd; i++)
            {
                var harborModel = new StudentHarborModel {LastName = $"Termine_{i + func.W.RowStart}"};

                /*
				harborModel.PropertyChanged += (sender, args) =>
				{
					LogManager.GetCurrentClassLogger().Trace($"{args.PropertyName} changed to {harborModel[args.PropertyName]}");
				};
				*/

                func.W.FormActions.Enqueue(() => func.W.StudentHarborModels.Add(harborModel));
            }
            return Resp.Success();
        }
    }
}