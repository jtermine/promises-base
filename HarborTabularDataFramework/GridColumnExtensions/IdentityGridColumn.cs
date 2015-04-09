using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.TabularPoperties;

namespace Termine.HarborTabularData.GridColumnExtensions
{
    public class IdentityGridColumn: GridColumn
    {
        public IdentityGridColumn()
        {
            Init();
        }

        public IdentityGridColumn(IdentityColumnType textEditType)
        {
            Init();
            Populate(textEditType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemTextEdit();
        }

        private void Populate(IdentityColumnType textEditType)
        {
            Name = $"GridColumn_{textEditType.Name}";
            FieldName = textEditType.Name;
            Caption = textEditType.Caption;
            Visible = false;
            
            var repEditor = ColumnEdit as RepositoryItemTextEdit;
            if (repEditor == null) return;

            repEditor.ReadOnly = true;
        }
    }
}
