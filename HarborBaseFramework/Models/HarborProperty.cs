using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Promises;
using Termine.HarborData.PropertyValueTypes;

namespace Termine.HarborData.Models
{
    public sealed class HarborProperty
    {
	    public HarborProperty(HarborModel harborModel)
	    {
		    _harborPropertyInstance.HarborModel = harborModel;
	    }

	    public string Name => _harborPropertyInstance.Name;
		public EnumDataType DataType => _harborPropertyInstance.DataType;
	    public HarborModel HarborModel => _harborPropertyInstance.HarborModel;

		public HarborPropertyValue PropertyValue => new HarborPropertyValue(this);

	    private class HarborPropertyInstance
	    {
			public HarborModel HarborModel;

			public int Version { get; private set; }

		    public enum EnumAllowNull
		    {
			    AllowNullAndStoreAsNull = 0,
			    AllowNullAndOmitStoreAsNull = 1,
			    BlockNull = 2,
		    }

		    public enum EnumVisibility
		    {
			    Private = 0,
			    PrivateSensitive = 1,
			    PrivateCalculated = 2,
			    Sensitive = 3,
			    Public = 4,
			    PublicCalculated = 5
		    }

		    public enum EnumIndexType
		    {
			    NotIndexed = 0,
			    IndexNoDuplicates_FIFO = 1,
			    IndexNoDuplicates_LIFO = 2,
			    IndexAllowDuplicates = 3,
		    }

		    public string Name { get; set; }
		    public string Caption { get; set; }
		    public string Description { get; set; }
		    public EnumAllowNull AllowNull { get; set; } = EnumAllowNull.AllowNullAndOmitStoreAsNull;
		    public EnumVisibility Visibility { get; set; } = EnumVisibility.Private;
		    public EnumIndexType IndexType { get; set; } = EnumIndexType.NotIndexed;
		    public EnumDataType DataType { get; set; } = EnumDataType.StringType;
		    public bool ValidateWithRegex { get; set; }
		    public string Regex { get; set; }
		    public bool BlockOnFalseRegexMatch { get; set; }
		    public bool BlockOnModelError { get; set; }
		    public bool IsImmutable { get; set; }
		    public string DefaultStringValue { get; set; }
		    public int DefaultIntValue { get; set; }
		    public bool DefaultBoolValue { get; set; }
		    public decimal DefaultMoneyValue { get; set; }
		    public decimal DefaultDecimalValue { get; set; }
		    public DateTime DefaultDateTimeValue { get; set; }
		    public DateTime DefaultDateValue { get; set; }

		    public void IncrementVersion()
		    {
			    Version++;
		    }
	    }

	    private readonly HarborPropertyInstance _harborPropertyInstance = new HarborPropertyInstance();

	    public HarborProperty MarkDirty()
	    {
		    HarborModel.MarkDirty();
		    return this;
	    }

	    public HarborProperty MarkClean()
	    {
		    HarborModel.MarkClean();
			return this;;
	    }

	    public HarborProperty Update(string name, string caption = "", string description = "")
	    {
		    _harborPropertyInstance.Name = name;
		    _harborPropertyInstance.Caption = caption;
		    _harborPropertyInstance.Description = description;

			_harborPropertyInstance.IncrementVersion();

		    return this;
	    }

		public HarborProperty SetCaption(string caption)
		{
			_harborPropertyInstance.Caption = caption;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty AllowNull_StoreAsNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty AllowNull_OmitStoreAsNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndOmitStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty BlockNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.BlockNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePublicProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.Public;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePublicCalculatedProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.PublicCalculated;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakeSensitiveProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.Sensitive;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePrivateProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.Private;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePrivateCalculatedProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.PrivateCalculated;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePrivateSensitiveProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.PrivateSensitive;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IndexWithNoDuplicates_UsingLIFO()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.IndexNoDuplicates_LIFO;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IndexWithNoDuplicates_UsingFIFO()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.IndexNoDuplicates_FIFO;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty NotIndexed()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.NotIndexed;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IndexAllowDuplicates()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.IndexAllowDuplicates;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

	    public HarborProperty TypeIsString(string defaultValue = "")
		{
			_harborPropertyInstance.DataType = EnumDataType.StringType;

			PropertyValue.I = new StringPVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsInteger(int defaultValue = 0)
		{
			_harborPropertyInstance.DataType = EnumDataType.IntegerType;

			PropertyValue.I = new IntPVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsMoney(decimal defaultValue = decimal.Zero)
		{
			_harborPropertyInstance.DataType = EnumDataType.MoneyType;

			PropertyValue.I = new DecimalPVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsDecimalNotMoney(decimal defaultValue = decimal.Zero)
		{
			_harborPropertyInstance.DataType = EnumDataType.DecimalType;

			PropertyValue.I = new DecimalPVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

	    public HarborProperty TypeIsComputedDecimal(Action<HarborModel, DecimalResponse> modelAction, string actionName = default(string), decimal defaultValue = decimal.Zero)
	    {
			if (string.IsNullOrEmpty(actionName)) actionName = $"decimalEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedDecimal;

			var computedType = new ComputedDecimalPVType(this);

			computedType.Promise.WithExecutor(actionName, (actions, config, model, req, decimalResponse) =>
			{
				modelAction.Invoke(_harborPropertyInstance.HarborModel, decimalResponse);
			});

		    PropertyValue.I = computedType;
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);
			
			return this;
		}

		public HarborProperty TypeIsComputedString(Action<HarborModel, StringResponse> modelAction, string actionName = default(string), string defaultValue = default(string))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"stringEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedString;

			var computedType = new ComputedStringPVType(this);

			computedType.Promise.WithExecutor(actionName, (actions, config, model, req, stringResponse) =>
			{
				modelAction.Invoke(_harborPropertyInstance.HarborModel, stringResponse);
			});

			PropertyValue.I = computedType;

			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}
		
		public HarborProperty TypeIsComputedInt(Action<HarborModel, IntResponse> modelAction, string actionName = default(string), int defaultValue = default (int))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"intEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedInt;

			var computedType = new ComputedIntPVType(this);

			computedType.Promise.WithExecutor(actionName, (actions, config, model, req, intResponse) =>
			{
				modelAction.Invoke(_harborPropertyInstance.HarborModel, intResponse);
			});

			PropertyValue.I = computedType;

			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsComputedBool(Action<HarborModel, BoolResponse> modelAction, string actionName = default(string), bool defaultValue = default(bool))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"boolEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedBool;

			var computedType = new ComputedBoolPVType(this);

			computedType.Promise.WithExecutor(actionName, (actions, config, model, req, boolResponse) =>
			{
				modelAction.Invoke(_harborPropertyInstance.HarborModel, boolResponse);
			});

			PropertyValue.I = computedType;

			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsDate(DateTime defaultValue = default(DateTime))
		{
			_harborPropertyInstance.DataType = EnumDataType.DateType;

			PropertyValue.I = new DateTimePVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsDateTime(DateTime defaultValue = default(DateTime))
		{
			_harborPropertyInstance.DataType = EnumDataType.DateTimeUTCType;

			PropertyValue.I = new DateTimePVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsBinary()
		{
			_harborPropertyInstance.DataType = EnumDataType.BinaryType;

			PropertyValue.I = new BinaryPVType(this);
			
			return this;
		}

		public HarborProperty TypeIsFixedEnumerable()
		{
			_harborPropertyInstance.DataType = EnumDataType.FixedEnumerable;
			return this;
		}

		public HarborProperty TypeIsBoolean(bool defaultValue = false)
		{
			_harborPropertyInstance.DataType = EnumDataType.BooleanType;

			PropertyValue.I = new BoolPVType(this);
			PropertyValue.I.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty ValidateWithRegexAndIgnoreFalseMatch(string regex)
		{
			_harborPropertyInstance.ValidateWithRegex = true;
			_harborPropertyInstance.BlockOnFalseRegexMatch = false;
			_harborPropertyInstance.Regex = regex;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty ValidateWithRegexAndBlockFalseMatch(string regex)
		{
			_harborPropertyInstance.ValidateWithRegex = true;
			_harborPropertyInstance.BlockOnFalseRegexMatch = true;
			_harborPropertyInstance.Regex = regex;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WheNullOmitValue()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndOmitStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WhenNullStoreAsNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WhenNullBlockChange()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.BlockNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IsImmutable()
		{
			_harborPropertyInstance.IsImmutable = true;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IsNotImmutable()
		{
			_harborPropertyInstance.IsImmutable = false;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WhenErrorIgnore()
		{
			_harborPropertyInstance.BlockOnModelError = false;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WhenErrorBlockChange()
		{
			_harborPropertyInstance.BlockOnModelError = true;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		/*
	    public string ComputeString()
	    {
		    switch (_harborPropertyInstance.DataType)
		    {
			    case EnumDataType.ComputedString:
				    var promise = _harborPropertyInstance.StringEvaluatorPromise.Run();
				    return promise.Response.Value;
			    default:
				    return string.Empty;
		    }
	    }

		public decimal ComputeDecimal()
		{
			switch (_harborPropertyInstance.DataType)
			{
				case EnumDataType.ComputedDecimal:
					var promise = _harborPropertyInstance.DecimalEvaluatorPromise.Run();
					return promise.Response.Value;
				default:
					return 0m;
			}
		}

		public bool ComputeBool()
		{
			switch (_harborPropertyInstance.DataType)
			{
				case EnumDataType.ComputedBool:
					var promise = _harborPropertyInstance.BoolEvaluatorPromise.Run();
					return promise.Response.Value;
				default:
					return false;
			}
		}

		public int ComputeInt()
		{
			switch (_harborPropertyInstance.DataType)
			{
				case EnumDataType.ComputedInt:
					var promise = _harborPropertyInstance.IntEvaluatorPromise.Run();
					return promise.Response.Value;
				default:
					return 0;
			}
		}
		*/
	}
}
