using Termine.Promises.WithRedis.Harbor;

namespace Termine.Promises.WithRedis.Interfaces
{
    public interface IAmAHarborProperty
    {
        string Name { get; set; }
        string Caption { get; set; }
        HarborProperty.EnumAllowNull AllowNull { get; set; }
        HarborProperty.EnumVisibility Visibility { get; set; }
        HarborProperty.EnumIndexType IndexType { get; set; }
        HarborProperty.EnumDataType DataType { get; set; }
        bool ValidateWithRegex { get; set; }
        string Regex { get; set; }
        bool BlockOnFalseRegexMatch { get; set; }
        bool BlockOnModelError { get; set; }
        bool IsImmutable { get; set; }

    }
}
