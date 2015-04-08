using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Tabular.Types;

namespace Tabular.GridColumnExtensions
{
    public class DecimalSpinEditGridColumn: GridColumn
    {
        public DecimalSpinEditGridColumn()
        {
            Init();
        }

        public DecimalSpinEditGridColumn(DecimalSpinEditType textEditType)
        {
            Init();
            Populate(textEditType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemSpinEdit();
        }

        private void Populate(DecimalSpinEditType textEditType)
        {
            Name = $"GridColumn_{textEditType.Name}";
            FieldName = textEditType.Name;
            Caption = textEditType.Caption;
            Visible = true;

            var repEditor = ColumnEdit as RepositoryItemSpinEdit;
            if (repEditor == null) return;

            repEditor.MinValue = textEditType.MinValue;
            repEditor.MaxValue = textEditType.MaxValue;
            repEditor.IsFloatValue = true;
        }
    }
}
