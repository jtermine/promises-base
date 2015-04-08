using System.Data;
using Tabular.Types;

namespace Tabular.DataColumnExtensions
{
    public static class DataColumnFactory
    {
        public static DataColumn AsDataColumn(this IColumnDefinitionType type)
        {
            switch (type.ValueType)
            {
                case ColumnValueType.IdentityColumn:
                    return (type as IdentityColumnType).AsDataColumn();
                case ColumnValueType.IntSpinEdit:
                    return (type as IntSpinEditType).AsDataColumn();
                case ColumnValueType.DecimalSpinEdit:
                    return (type as DecimalSpinEditType).AsDataColumn();
                case ColumnValueType.ComboBox:
                    return (type as ComboBoxType).AsDataColumn();
                case ColumnValueType.TextEdit:
                    return (type as TextEditType).AsDataColumn();
                default:
                    return null;
            }
        }

        public static IdentityDataColumn AsDataColumn(this IdentityColumnType type)
        {
            return type == null ? null : new IdentityDataColumn(type);
        }

        public static ComboBoxDataColumn AsDataColumn(this ComboBoxType type)
        {
            return type == null ? null : new ComboBoxDataColumn(type);
        }

        public static DecimalSpinEditDataColumn AsDataColumn(this DecimalSpinEditType type)
        {
            return type == null ? null : new DecimalSpinEditDataColumn(type);
        }

        public static IntSpinEditDataColumn AsDataColumn(this IntSpinEditType type)
        {
            return type == null ? null : new IntSpinEditDataColumn(type);
        }

        public static TextEditDataColumn AsDataColumn(this TextEditType type)
        {
            return type == null ? null : new TextEditDataColumn(type);
        }
    }
}
