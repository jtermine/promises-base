using System.Data;
using Tabular.Types;

namespace Tabular.DataColumnExtensions
{
    public class IdentityDataColumn: DataColumn
    {
         public IdentityDataColumn()
        {
            Init();
        }

        public IdentityDataColumn(IdentityColumnType type)
        {
            Init();
            Populate(type);
        }

        private void Init()
        {
            DataType = typeof (string);
        }

        private void Populate(IdentityColumnType type)
        {
            ColumnName = type.Name;
            Caption = type.Caption;
            Unique = true;
        }
    }
}
