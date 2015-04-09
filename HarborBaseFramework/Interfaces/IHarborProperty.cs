using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Models;
using Termine.HarborData.Promises;

namespace Termine.HarborData.Interfaces
{
	public interface IExposeHarborProperty
	{
		EnumDataType DataType { get; }
		HarborModel HarborModel { get; }
		IAmAHarborProperty Instance { get; }
		string Name { get; }
		HarborPropertyValue PropertyValue { get; }

		HarborProperty AllowNull_OmitStoreAsNull();
		HarborProperty AllowNull_StoreAsNull();
		HarborProperty BlockNull();
		HarborProperty IndexAllowDuplicates();
		HarborProperty IndexWithNoDuplicates_UsingFIFO();
		HarborProperty IndexWithNoDuplicates_UsingLIFO();
		HarborProperty IsImmutable();
		HarborProperty IsNotImmutable();
		HarborProperty MakePrivateCalculatedProperty();
		HarborProperty MakePrivateProperty();
		HarborProperty MakePrivateSensitiveProperty();
		HarborProperty MakePublicCalculatedProperty();
		HarborProperty MakePublicProperty();
		HarborProperty MakeSensitiveProperty();
		HarborProperty MarkClean();
		HarborProperty MarkDirty();
		HarborProperty NotIndexed();
		HarborProperty SetCaption(string caption);
		HarborProperty TypeIsBinary();
		HarborProperty TypeIsBoolean(bool defaultValue = false);
		HarborProperty TypeIsComputedBool(Action<HarborModel, BoolResponse> modelAction, bool defaultValue = false, string actionName = null);
		HarborProperty TypeIsComputedDateTimeUTC(Action<HarborModel, DateTimeResponse> modelAction, DateTime defaultValue = default(DateTime), string actionName = null);
		HarborProperty TypeIsComputedDecimal(Action<HarborModel, DecimalResponse> modelAction, decimal defaultValue = 0, string actionName = null);
		HarborProperty TypeIsComputedInt(Action<HarborModel, IntResponse> modelAction, int defaultValue = 0, string actionName = null);
		HarborProperty TypeIsComputedString(Action<HarborModel, StringResponse> modelAction, string defaultValue = null, string actionName = null);
		HarborProperty TypeIsDate(DateTime defaultValue = default(DateTime));
		HarborProperty TypeIsDateTime(DateTime defaultValue = default(DateTime));
		HarborProperty TypeIsDecimalNotMoney(decimal defaultValue = 0);
		HarborProperty TypeIsFixedEnumerable();
		HarborProperty TypeIsInteger(int defaultValue = 0);
		HarborProperty TypeIsMoney(decimal defaultValue = 0);
		HarborProperty TypeIsString(string defaultValue = "");
		HarborProperty Update(string name, string caption = "", string description = "");
		HarborProperty ValidateWithRegexAndBlockFalseMatch(string regex);
		HarborProperty ValidateWithRegexAndIgnoreFalseMatch(string regex);
		HarborProperty WhenErrorBlockChange();
		HarborProperty WhenErrorIgnore();
		HarborProperty WhenNullBlockChange();
		HarborProperty WhenNullStoreAsNull();
		HarborProperty WheNullOmitValue();
	}
}