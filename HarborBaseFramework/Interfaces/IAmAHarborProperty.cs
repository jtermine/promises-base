using Termine.HarborData.Enumerables;
using Termine.HarborData.Models;

namespace Termine.HarborData.Interfaces
{
	public interface IAmAHarborProperty
	{
		HarborModel HarborModel { get; }
		EnumAllowNull AllowNull { get; }
		bool BlockOnFalseRegexMatch { get; }
		bool BlockOnModelError { get; }
		string Caption { get; }
		EnumDataType DataType { get; }
		string Description { get; }
		EnumIndexType IndexType { get; }
		bool IsImmutable { get; }
		string Name { get; }
		HarborPropertyValue PropertyValue { get; }
		string Regex { get; }
		bool ValidateWithRegex { get; }
		EnumVisibility Visibility { get; }


	}
}