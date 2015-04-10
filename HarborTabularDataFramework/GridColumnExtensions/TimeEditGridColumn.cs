using System;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.TabularPoperties;

namespace Termine.HarborTabularData.GridColumnExtensions
{
    public class TimeEditGridColumn: GridColumn
    {
        public TimeEditGridColumn()
        {
            Init();
        }

        public TimeEditGridColumn(TimeEditType editType)
        {
            Init();
            Populate(editType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemTimeEdit();
        }

        private void Populate(TimeEditType dateEditType)
        {
            Name = $"GridColumn_{dateEditType.Name}";
            FieldName = dateEditType.Name;
            Caption = dateEditType.Caption;
            Visible = true;

            var repEditor = ColumnEdit as RepositoryItemTimeEdit;
            if (repEditor == null) return;
   
			//if (dateEditType.MinValue != default(DateTime)) repEditor. = dateEditType.MinValue;
			//if (dateEditType.MaxValue != default(DateTime)) repEditor.MaxValue = dateEditType.MaxValue;
		}
    }
}