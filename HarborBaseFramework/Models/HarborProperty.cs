﻿using System;
using System.Text;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Promises;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;

namespace Termine.HarborData.Models
{
    public sealed class HarborProperty
    {
	    public HarborProperty(HarborModel harborModel)
	    {
		    _harborPropertyInstance.HarborModel = harborModel;
	    }

	    public string Name => _harborPropertyInstance.Name;
	    public int Version => _harborPropertyInstance.Version;
		public EnumDataType DataType => _harborPropertyInstance.DataType;

	    private class HarborPropertyInstance
	    {
			public HarborModel HarborModel;
			public readonly Promise<GenericConfig, HarborModel, GenericRequest, StringResponse> StringEvaluatorPromise = new Promise<GenericConfig, HarborModel, GenericRequest, StringResponse>();
			public readonly Promise<GenericConfig, HarborModel, GenericRequest, DecimalResponse> DecimalEvaluatorPromise = new Promise<GenericConfig, HarborModel, GenericRequest, DecimalResponse>();
			public readonly Promise<GenericConfig, HarborModel, GenericRequest, IntResponse> IntEvaluatorPromise = new Promise<GenericConfig, HarborModel, GenericRequest, IntResponse>();

			public int Version { get; set; }

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
		    public string LuaExpression { get; set; }
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

	    public byte[] GetDefaultInBytes()
	    {
		    switch (_harborPropertyInstance.DataType)
		    {
			    case EnumDataType.BinaryType:
				    return default(byte[]);
			    case EnumDataType.BooleanType:
				    return BitConverter.GetBytes(_harborPropertyInstance.DefaultBoolValue);
			    case EnumDataType.DateType:
				    return Encoding.UTF8.GetBytes(_harborPropertyInstance.DefaultDateValue.ToLongDateString());
			    case EnumDataType.UTCDateTimeType:
				    return Encoding.UTF8.GetBytes(_harborPropertyInstance.DefaultDateTimeValue.ToString("u"));
			    case EnumDataType.DecimalType:
				    var intArray = decimal.GetBits(_harborPropertyInstance.DefaultDecimalValue);
				    var result = new byte[intArray.Length*sizeof (int)];
				    Buffer.BlockCopy(intArray, 0, result, 0, result.Length);
				    return result;
			    case EnumDataType.MoneyType:
				    var intArray2 = decimal.GetBits(_harborPropertyInstance.DefaultDecimalValue);
				    var result2 = new byte[intArray2.Length*sizeof (int)];
				    Buffer.BlockCopy(intArray2, 0, result2, 0, result2.Length);
				    return result2;
			    case EnumDataType.IntegerType:
				    return BitConverter.GetBytes(_harborPropertyInstance.DefaultIntValue);
			    case EnumDataType.StringType:
				    return (!string.IsNullOrEmpty(_harborPropertyInstance.DefaultStringValue)) ? Encoding.UTF8.GetBytes(_harborPropertyInstance.DefaultStringValue) : default(byte[]);
			    default:
				    return default(byte[]);
		    }
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

	    private void NullDefaultValues()
	    {
		    _harborPropertyInstance.LuaExpression = default(string);

		    _harborPropertyInstance.DefaultStringValue = default(string);
		    _harborPropertyInstance.DefaultBoolValue = default(bool);
		    _harborPropertyInstance.DefaultIntValue = default(int);
		    _harborPropertyInstance.DefaultDateTimeValue = default(DateTime);
		    _harborPropertyInstance.DefaultDateValue = default(DateTime);
		    _harborPropertyInstance.DefaultDecimalValue = default(decimal);
		    _harborPropertyInstance.DefaultMoneyValue = default(decimal);

			_harborPropertyInstance.IncrementVersion();
		}

		public HarborProperty TypeIsString(string defaultValue = "")
		{
			_harborPropertyInstance.DataType = EnumDataType.StringType;

			NullDefaultValues();

			_harborPropertyInstance.DefaultStringValue = defaultValue;

			return this;
		}

		public HarborProperty TypeIsInteger(int defaultValue = 0)
		{
			_harborPropertyInstance.DataType = EnumDataType.IntegerType;

			NullDefaultValues();

			_harborPropertyInstance.DefaultIntValue = defaultValue;

			return this;
		}

		public HarborProperty TypeIsMoney(decimal defaultValue = decimal.Zero)
		{
			_harborPropertyInstance.DataType = EnumDataType.MoneyType;

			NullDefaultValues();

			_harborPropertyInstance.DefaultMoneyValue = defaultValue;

			return this;
		}

		public HarborProperty TypeIsDecimalNotMoney(decimal defaultValue = decimal.Zero)
		{
			_harborPropertyInstance.DataType = EnumDataType.DecimalType;

			NullDefaultValues();

			_harborPropertyInstance.DefaultDecimalValue = defaultValue;

			return this;
		}

	    public HarborProperty TypeIsComputedDecimal(Action<HarborModel, DecimalResponse> modelAction)
	    {
			_harborPropertyInstance.DataType = EnumDataType.ComputedDecimal;

			NullDefaultValues();

			_harborPropertyInstance.DecimalEvaluatorPromise.WithExecutor("decimalEvaluator", (actions, config, model, req, decimalResponse) =>
			{
				modelAction.Invoke(_harborPropertyInstance.HarborModel, decimalResponse);
			});

			return this;
		}

		public HarborProperty TypeIsComputedString(Action<HarborModel, StringResponse> modelAction)
		{
			_harborPropertyInstance.DataType = EnumDataType.ComputedString;

			NullDefaultValues();

			_harborPropertyInstance.StringEvaluatorPromise.WithExecutor("stringEvaluator", (actions, config, model, req, stringResponse) =>
			{
				modelAction.Invoke(_harborPropertyInstance.HarborModel, stringResponse);
			});

			return this;
		}

		public HarborProperty TypeIsDate(DateTime defaultValue = default(DateTime))
		{
			_harborPropertyInstance.DataType = EnumDataType.DateType;

			NullDefaultValues();

			_harborPropertyInstance.DefaultDateValue = defaultValue;

			return this;
		}

		public HarborProperty TypeIsDateTime(DateTime defaultValue = default(DateTime))
		{
			_harborPropertyInstance.DataType = EnumDataType.UTCDateTimeType;

			NullDefaultValues();

			_harborPropertyInstance.DefaultDateTimeValue = defaultValue;

			return this;
		}

		public HarborProperty TypeIsBinary()
		{
			_harborPropertyInstance.DataType = EnumDataType.BinaryType;
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

			NullDefaultValues();

			_harborPropertyInstance.DefaultBoolValue = defaultValue;

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

	}
}
