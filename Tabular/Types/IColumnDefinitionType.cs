namespace Tabular.Types
{
    public interface IColumnDefinitionType
    {
        string Name { get; set; }
        string Caption { get; set; }
        ColumnValueType ValueType { get; set; }
        bool BlockMultiChange { get; set; }
    }
}