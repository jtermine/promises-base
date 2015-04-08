namespace Tabular.Types
{
    public class IdentityColumnType: AbstractColumnDefinitionType
    {
        public IdentityColumnType()
        {
            ValueType = ColumnValueType.TextEdit;
        }

        public IdentityColumnType(string name, string caption)
        {
            Name = name;
            Caption = caption;
            ValueType = ColumnValueType.IdentityColumn;
        }

    }
}
