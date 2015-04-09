using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.TabularPoperties;

namespace Termine.HarborTabularData.GridColumnExtensions
{
    public class ComboBoxGridColumn: GridColumn
    {
        public ComboBoxGridColumn()
        {
            Init();
        }

        public ComboBoxGridColumn(ComboBoxType comboBoxType)
        {
            Init();
            Populate(comboBoxType);
        }

        private void Init()
        {
            ColumnEdit = new RepositoryItemLookUpEdit();
        }

        private void Populate(ComboBoxType comboBoxType)
        {
            Name = $"GridColumn_{comboBoxType.Name}";
            FieldName = comboBoxType.Name;
            Caption = comboBoxType.Caption;
            Visible = true;

            var repEditor = ColumnEdit as RepositoryItemLookUpEdit;
            if (repEditor == null) return;

            repEditor.DataSource = comboBoxType.ItemsDictionary;
            repEditor.ShowHeader = false;
            repEditor.DisplayMember = "Value";
            repEditor.ValueMember = "Key";
        }
    }
}

