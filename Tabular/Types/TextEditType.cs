namespace Tabular.Types
{
    public class TextEditType: AbstractColumnDefinitionType
    {
        public TextEditType()
        {
            ValueType = ColumnValueType.TextEdit;
        }

        public TextEditType(string name, string caption, string defaultValue, int minLength, int maxLength, bool blockMultiChange = false)
        {
            Name = name;
            Caption = caption;
            DefaultValue = defaultValue;
            MinLength = minLength;
            MaxLength = maxLength;
            ValueType = ColumnValueType.TextEdit;
            BlockMultiChange = blockMultiChange;
        }

        public string DefaultValue { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        
    }
}
