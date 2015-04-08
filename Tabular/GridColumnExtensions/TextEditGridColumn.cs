using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Tabular.Types;

namespace Tabular.GridColumnExtensions
{
    public class TextEditGridColumn: GridColumn
    {
        public TextEditGridColumn()
        {
            Init();
        }

        public TextEditGridColumn(TextEditType textEditType)
        {
            Init();
            Populate(textEditType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemTextEdit();
        }

        private void Populate(TextEditType textEditType)
        {
            Name = $"GridColumn_{textEditType.Name}";
            FieldName = textEditType.Name;
            Caption = textEditType.Caption;
            Visible = true;

            var repEditor = ColumnEdit as RepositoryItemTextEdit;
            if (repEditor == null) return;

            repEditor.MaxLength = textEditType.MaxLength;
        }
    }
}
