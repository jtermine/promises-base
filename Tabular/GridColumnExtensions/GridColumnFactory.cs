using DevExpress.XtraGrid.Columns;
using Tabular.Types;

namespace Tabular.GridColumnExtensions
{
    public static class GridColumnFactory
    {
        public static GridColumn AsGridColumn(this IColumnDefinitionType type)
        {
            switch (type.ValueType)
            {
                    case ColumnValueType.IdentityColumn:
                    return (type as IdentityColumnType).AsGridColumn();
                case ColumnValueType.IntSpinEdit:
                    return (type as IntSpinEditType).AsGridColumn();
                case ColumnValueType.DecimalSpinEdit:
                    return (type as DecimalSpinEditType).AsGridColumn();
                case ColumnValueType.ComboBox:
                    return (type as ComboBoxType).AsGridColumn();
                case ColumnValueType.TextEdit:
                    return (type as TextEditType).AsGridColumn();
                default:
                    return null;
            }
        }

        public static IdentityGridColumn AsGridColumn(this IdentityColumnType type)
        {
            return type == null ? null : new IdentityGridColumn(type);
        }

        public static ComboBoxGridColumn AsGridColumn(this ComboBoxType type)
        {
            return type == null ? null : new ComboBoxGridColumn(type);
        }

        public static DecimalSpinEditGridColumn AsGridColumn(this DecimalSpinEditType type)
        {
            return type == null ? null : new DecimalSpinEditGridColumn(type);
        }

        public static IntSpinEditGridColumn AsGridColumn(this IntSpinEditType type)
        {
            return type == null ? null : new IntSpinEditGridColumn(type);
        }

        public static TextEditGridColumn AsGridColumn(this TextEditType type)
        {
            return type == null ? null : new TextEditGridColumn(type);
        }
    }
}
