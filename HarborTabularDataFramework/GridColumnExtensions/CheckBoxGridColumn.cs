using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.TabularPoperties;

namespace Termine.HarborTabularData.GridColumnExtensions
{
    public class CheckBoxGridColumn: GridColumn
    {
        public CheckBoxGridColumn()
        {
            Init();
        }

        public CheckBoxGridColumn(CheckBoxType checkBoxType)
        {
            Init();
            Populate(checkBoxType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemCheckEdit();
        }

        private void Populate(CheckBoxType checkBoxType)
        {
            Name = $"GridColumn_{checkBoxType.Name}";
            FieldName = checkBoxType.Name;
            Caption = checkBoxType.Caption;
            Visible = true;

            //var repEditor = ColumnEdit as RepositoryItemCheckEdit;
            //if (repEditor == null) return;
        }
    }
}

