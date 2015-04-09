using Termine.HarborTabularData.Enumerables;

namespace Tabular.Types
{
    public interface IColumnDefinitionType
    {
        string Name { get; set; }
        string Caption { get; set; }
        EnumColumnValueType ValueType { get; set; }
        bool BlockMultiChange { get; set; }
    }
}