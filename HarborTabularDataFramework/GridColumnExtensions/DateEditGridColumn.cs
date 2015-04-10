using System;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.TabularPoperties;

namespace Termine.HarborTabularData.GridColumnExtensions
{
    public class DateEditGridColumn: GridColumn
    {
        public DateEditGridColumn()
        {
            Init();
        }

        public DateEditGridColumn(DateEditType dateEditType)
        {
            Init();
            Populate(dateEditType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemDateEdit();
        }

        private void Populate(DateEditType dateEditType)
        {
            Name = $"GridColumn_{dateEditType.Name}";
            FieldName = dateEditType.Name;
            Caption = dateEditType.Caption;
            Visible = true;

            var repEditor = ColumnEdit as RepositoryItemDateEdit;
            if (repEditor == null) return;

            if (dateEditType.MinValue != default(DateTime)) repEditor.MinValue = dateEditType.MinValue;
			if (dateEditType.MaxValue != default(DateTime)) repEditor.MaxValue = dateEditType.MaxValue;
		}
    }
}