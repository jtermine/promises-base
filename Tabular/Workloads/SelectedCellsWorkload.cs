using System.Collections.Generic;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using Termine.Promises.Base.Generics;

namespace Tabular.Workloads
{
    public class SelectedCellsWorkload: GenericWorkload
    {
        public IEnumerable<GridCell> GridCells { get; set; }

        public string NewValue { get; set; }

        public DataTable DataTable { get; set; }
    }
}
