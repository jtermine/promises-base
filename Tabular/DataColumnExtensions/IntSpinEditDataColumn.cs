using System.Data;
using Tabular.Types;

namespace Tabular.DataColumnExtensions
{
    public class IntSpinEditDataColumn: DataColumn
    {
        public IntSpinEditDataColumn()
        {
            Init();
        }

        public IntSpinEditDataColumn(IntSpinEditType type)
        {
            Init();
            Populate(type);
        }

        private void Init()
        {
            DataType = typeof (int);
        }

        private void Populate(IntSpinEditType type)
        {
            ColumnName = type.Name;
            Caption = type.Caption;
            DefaultValue = type.DefaultValue;
        }
    }
}
