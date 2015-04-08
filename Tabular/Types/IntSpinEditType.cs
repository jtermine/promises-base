namespace Tabular.Types
{
    public class IntSpinEditType: AbstractColumnDefinitionType
    {
        public IntSpinEditType()
        {
            ValueType = ColumnValueType.IntSpinEdit;
        }

        public IntSpinEditType(string name, string caption, bool blockMultiChange = false)
        {
            Name = name;
            Caption = caption;
            ValueType = ColumnValueType.IntSpinEdit;
            BlockMultiChange = blockMultiChange;
        }

        public IntSpinEditType(string name, string caption, int defaultValue, int minValue, int maxValue, bool blockMultiChange = false)
        {
            Name = name;
            Caption = caption;
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;
            ValueType = ColumnValueType.IntSpinEdit;
            BlockMultiChange = blockMultiChange;
        }

        public int DefaultValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

    }
}
