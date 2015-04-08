namespace Tabular.Types
{
    public abstract class AbstractColumnDefinitionType : IColumnDefinitionType
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public ColumnValueType ValueType { get; set; }
        public bool BlockMultiChange { get; set; }
    }
}
