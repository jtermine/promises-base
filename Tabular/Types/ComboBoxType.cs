using System.Collections.Generic;

namespace Tabular.Types
{
    public class ComboBoxType: AbstractColumnDefinitionType
    {
        public ComboBoxType()
        {
            ItemsDictionary = new Dictionary<string, string>();
            ValueType = ColumnValueType.ComboBox;
        }

        public ComboBoxType(string name, string caption, IDictionary<string, string> items,  bool blockMultiChange = false)
        {
            Name = name;
            Caption = caption;
            ItemsDictionary = new Dictionary<string, string>(items);
            ValueType = ColumnValueType.ComboBox;
            BlockMultiChange = blockMultiChange;
        }

        public ComboBoxType(string name, string caption, string defaultValue, IDictionary<string, string> items, bool blockMultiChange = false)
        {
            Name = name;
            Caption = caption;
            ItemsDictionary = new Dictionary<string, string>(items);
            DefaultValue = defaultValue;
            ValueType = ColumnValueType.ComboBox;
            BlockMultiChange = blockMultiChange;
        }

        public Dictionary<string, string> ItemsDictionary { get; set; }

        public string DefaultValue { get; set; }

    }
}
