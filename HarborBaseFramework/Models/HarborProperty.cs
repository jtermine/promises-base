using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Promises;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;

namespace Termine.HarborData.Models
{
    public sealed class HarborProperty : IExposeHarborProperty
	{
	    public HarborProperty(HarborModel harborModel)
	    {
		    _harborPropertyInstance.SetHarborModel(harborModel);
	    }

	    public string Name => _harborPropertyInstance.Name;
	    public string Caption => _harborPropertyInstance.Caption;
		public EnumDataType DataType => _harborPropertyInstance.DataType;
	    public HarborModel HarborModel => _harborPropertyInstance.HarborModel;
	    public HarborPropertyValue PropertyValue => _harborPropertyInstance.PropertyValue;

	    public IAmAHarborProperty Instance => _harborPropertyInstance;

	    private class HarborPropertyInstance : IAmAHarborProperty
	    {
		    public HarborModel HarborModel { get; private set; }
		    private int Version { get; set; }

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
			
		    public void IncrementVersion()
		    {
			    Version++;
		    }

		    public HarborPropertyValue PropertyValue { get; set; }

		    public void SetHarborModel(HarborModel harborModel)
		    {
			    HarborModel = harborModel;
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
			return this;
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
			_harborPropertyInstance.AllowNull = EnumAllowNull.AllowNullAndStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty AllowNull_OmitStoreAsNull()
		{
			_harborPropertyInstance.AllowNull = EnumAllowNull.AllowNullAndOmitStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty BlockNull()
		{
			_harborPropertyInstance.AllowNull = EnumAllowNull.BlockNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePublicProperty()
		{
			_harborPropertyInstance.Visibility = EnumVisibility.Public;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePublicCalculatedProperty()
		{
			_harborPropertyInstance.Visibility = EnumVisibility.PublicCalculated;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakeSensitiveProperty()
		{
			_harborPropertyInstance.Visibility = EnumVisibility.Sensitive;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePrivateProperty()
		{
			_harborPropertyInstance.Visibility = EnumVisibility.Private;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePrivateCalculatedProperty()
		{
			_harborPropertyInstance.Visibility = EnumVisibility.PrivateCalculated;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty MakePrivateSensitiveProperty()
		{
			_harborPropertyInstance.Visibility = EnumVisibility.PrivateSensitive;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IndexWithNoDuplicates_UsingLIFO()
		{
			_harborPropertyInstance.IndexType = EnumIndexType.IndexNoDuplicates_LIFO;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IndexWithNoDuplicates_UsingFIFO()
		{
			_harborPropertyInstance.IndexType = EnumIndexType.IndexNoDuplicates_FIFO;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty NotIndexed()
		{
			_harborPropertyInstance.IndexType = EnumIndexType.NotIndexed;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty IndexAllowDuplicates()
		{
			_harborPropertyInstance.IndexType = EnumIndexType.IndexAllowDuplicates;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

	    public HarborProperty TypeIsString(string defaultValue = "")
		{
			_harborPropertyInstance.DataType = EnumDataType.StringType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsInteger(int defaultValue = 0)
		{
			_harborPropertyInstance.DataType = EnumDataType.IntegerType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsMoney(decimal defaultValue = decimal.Zero)
		{
			_harborPropertyInstance.DataType = EnumDataType.MoneyType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsDecimalNotMoney(decimal defaultValue = decimal.Zero)
		{
			_harborPropertyInstance.DataType = EnumDataType.DecimalType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

	    public HarborProperty TypeIsComputedDecimal(Action<HarborModel, DecimalResponse> modelAction, decimal defaultValue = decimal.Zero, string actionName = default(string))
	    {
			if (string.IsNullOrEmpty(actionName)) actionName = $"decimalEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedDecimal;

			var i = new HarborPropertyValue(this);

			i.Set(defaultValue, EnumPropertyValueState.Default);
			

			i.ComputeAction += (instance =>
			{
				var promise = new Promise<GenericConfig, GenericUserIdentity, HarborModel, GenericRequest, DecimalResponse>();

				promise.WithExecutor(actionName, (actions, u, uconfig, model, request, response) =>
				{
					modelAction.Invoke(_harborPropertyInstance.HarborModel, response);
				});

				promise.Run();

				instance.Set(promise.Response.Value);
			});


			_harborPropertyInstance.PropertyValue = i;

			return this;
		}

		public HarborProperty TypeIsComputedString(Action<HarborModel, StringResponse> modelAction, string defaultValue = default(string), string actionName = default(string))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"stringEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedString;

			var i = new HarborPropertyValue(this);

			i.Set(defaultValue, EnumPropertyValueState.Default);

			i.ComputeAction += (instance =>
			{
				var promise = new Promise<GenericConfig, GenericUserIdentity, HarborModel, GenericRequest, StringResponse>();

				promise.WithExecutor(actionName, (actions, config, u, model, request, response) =>
				{
					modelAction.Invoke(_harborPropertyInstance.HarborModel, response);
				});

				promise.Run();

				instance.Set(promise.Response.Value);
			});

			_harborPropertyInstance.PropertyValue = i;

			return this;
		}
		
		public HarborProperty TypeIsComputedInt(Action<HarborModel, IntResponse> modelAction, int defaultValue = default(int), string actionName = default(string))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"intEvaluator.{Name}";
            
			_harborPropertyInstance.DataType = EnumDataType.ComputedInt;

			var i = new HarborPropertyValue(this);

			i.Set(defaultValue, EnumPropertyValueState.Default);

			i.ComputeAction += (instance =>
			{
				var promise = new Promise<GenericConfig, GenericUserIdentity, HarborModel, GenericRequest, IntResponse>();

				promise.WithExecutor(actionName, (actions, config, u, model, request, response) =>
				{
					modelAction.Invoke(_harborPropertyInstance.HarborModel, response);
				});

				promise.Run();

				instance.Set(promise.Response.Value);
			});

			_harborPropertyInstance.PropertyValue = i;

			return this;
		}

		public HarborProperty TypeIsComputedBool(Action<HarborModel, BoolResponse> modelAction, bool defaultValue = default(bool), string actionName = default(string))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"boolEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedBool;

			var i = new HarborPropertyValue(this);

			i.Set(defaultValue, EnumPropertyValueState.Default);

			i.ComputeAction += (instance =>
			{
				var promise = new Promise<GenericConfig, GenericUserIdentity, HarborModel, GenericRequest, BoolResponse>();

				promise.WithExecutor(actionName, (actions, config, u, model, request, response) =>
				{
					modelAction.Invoke(_harborPropertyInstance.HarborModel, response);
				});

				promise.Run();

				instance.Set(promise.Response.Value);
			});

			_harborPropertyInstance.PropertyValue = i;

			return this;
		}

		public HarborProperty TypeIsComputedDateTimeUTC(Action<HarborModel, DateTimeResponse> modelAction, DateTime defaultValue = default(DateTime), string actionName = default(string))
		{
			if (string.IsNullOrEmpty(actionName)) actionName = $"dateTimeEvaluator.{Name}";

			_harborPropertyInstance.DataType = EnumDataType.ComputedDateTimeUTC;

			var i = new HarborPropertyValue(this);

			i.Set(defaultValue, EnumPropertyValueState.Default);

			i.ComputeAction += (instance =>
			{
				var promise = new Promise<GenericConfig, GenericUserIdentity, HarborModel, GenericRequest, DateTimeResponse>();

				promise.WithExecutor(actionName, (actions, config, u, model, request, response) =>
				{
					modelAction.Invoke(_harborPropertyInstance.HarborModel, response);
				});

				promise.Run();

				instance.Set(promise.Response.Value);
			});

			_harborPropertyInstance.PropertyValue = i;

			return this;
		}

		public HarborProperty TypeIsDate(DateTime defaultValue = default(DateTime))
		{
			_harborPropertyInstance.DataType = EnumDataType.DateType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsDateTime(DateTime defaultValue = default(DateTime))
		{
			_harborPropertyInstance.DataType = EnumDataType.DateTimeUTCType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

			return this;
		}

		public HarborProperty TypeIsBinary()
		{
			_harborPropertyInstance.DataType = EnumDataType.BinaryType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			
			return this;
		}

	    public HarborProperty TypeIsId()
	    {
			_harborPropertyInstance.DataType = EnumDataType.StringType;

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(Guid.NewGuid().ToString("N"), EnumPropertyValueState.Default);

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

			_harborPropertyInstance.PropertyValue = new HarborPropertyValue(this);
			_harborPropertyInstance.PropertyValue.Set(defaultValue, EnumPropertyValueState.Default);

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
			_harborPropertyInstance.AllowNull = EnumAllowNull.AllowNullAndOmitStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WhenNullStoreAsNull()
		{
			_harborPropertyInstance.AllowNull = EnumAllowNull.AllowNullAndStoreAsNull;

			_harborPropertyInstance.IncrementVersion();

			return this;
		}

		public HarborProperty WhenNullBlockChange()
		{
			_harborPropertyInstance.AllowNull = EnumAllowNull.BlockNull;

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
