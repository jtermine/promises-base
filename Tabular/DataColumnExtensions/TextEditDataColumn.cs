using System.Data;
using Tabular.Types;

namespace Tabular.DataColumnExtensions
{
    public class TextEditDataColumn: DataColumn
    {
        public TextEditDataColumn()
        {
            Init();
        }

        public TextEditDataColumn(TextEditType type)
        {
            Init();
            Populate(type);
        }

        private void Init()
        {
            DataType = typeof (string);
        }

        private void Populate(TextEditType type)
        {
            ColumnName = type.Name;
            Caption = type.Caption;
            MaxLength = type.MaxLength;
            DefaultValue = type.DefaultValue;
        }
    }
}