using System.Data;
using Tabular.Types;

namespace Tabular.DataColumnExtensions
{
    public class DecimalSpinEditDataColumn: DataColumn
    {
        public DecimalSpinEditDataColumn()
        {
            Init();
        }

        public DecimalSpinEditDataColumn(DecimalSpinEditType type)
        {
            Init();
            Populate(type);
        }

        private void Init()
        {
            DataType = typeof (decimal);
        }

        private void Populate(DecimalSpinEditType type)
        {
            ColumnName = type.Name;
            Caption = type.Caption;
            DefaultValue = type.DefaultValue;
        }
    }
}
