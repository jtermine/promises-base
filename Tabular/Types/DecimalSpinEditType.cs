namespace Tabular.Types
{
    public class DecimalSpinEditType: AbstractColumnDefinitionType
    {
        public DecimalSpinEditType()
        {
            ValueType = ColumnValueType.DecimalSpinEdit;
        }

        public DecimalSpinEditType(string name, string caption, bool blockMultiChange = false)
        {
            Name = name;
            Caption = caption;
            ValueType = ColumnValueType.DecimalSpinEdit;
            BlockMultiChange = blockMultiChange;
        }

        public DecimalSpinEditType(string name, string caption, decimal defaultValue, decimal minValue, decimal maxValue, bool blockMultiChange = false)
        {
            Name = name;    
            Caption = caption;
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;
            ValueType = ColumnValueType.DecimalSpinEdit;
            BlockMultiChange = blockMultiChange;
        }

        public decimal DefaultValue { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

    }
}