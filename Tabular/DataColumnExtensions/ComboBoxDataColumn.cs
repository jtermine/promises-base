using System.Data;
using Tabular.Types;

namespace Tabular.DataColumnExtensions
{
    public class ComboBoxDataColumn: DataColumn
    {
        public ComboBoxDataColumn()
        {
            Init();
        }

        public ComboBoxDataColumn(ComboBoxType type)
        {
            Init();
            Populate(type);
        }

        private void Init()
        {
            DataType = typeof (string);
        }

        private void Populate(ComboBoxType comboBoxType)
        {
            ColumnName = comboBoxType.Name;
            Caption = comboBoxType.Caption;
            DefaultValue = comboBoxType.DefaultValue;
        }
    }
}
