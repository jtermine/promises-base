using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tabular.TabModels;
using Tabular.Types;
using Termine.Promises.Base.Generics;

namespace Tabular.Workloads
{
    public class DataTableWorkload: GenericWorkload
    {
        public ObservableCollection<StudentHarborModel> StudentHarborModels { get; set; }
        public ConcurrentQueue<Action> FormActions { get; set; }
        public List<IColumnDefinitionType> List { get; set; }
    }
}
