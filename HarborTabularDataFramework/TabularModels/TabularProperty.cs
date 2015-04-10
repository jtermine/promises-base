using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Models;
using Termine.HarborData.Promises;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularPoperties;

namespace Termine.HarborTabularData.TabularModels
{
	public sealed class TabularProperty
	{
		public TabularProperty(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public EnumDataType DataType  => HarborProperty.DataType;
		public HarborModel HarborModel => HarborProperty.HarborModel;
		public string Name => HarborProperty.Name;
		public HarborPropertyValue PropertyValue => HarborProperty.PropertyValue;
		public string Caption => HarborProperty.Caption;
		public EnumColumnValueType ValueType { get; private set; }
		public bool IsBlockMultiChange { get; private set; }
		public HarborProperty HarborProperty { get; }

		public IAmATabularPropertyEditor PropertyEditor { get; set; }


		public TabularProperty AllowNull_OmitStoreAsNull()
		{
			HarborProperty.AllowNull_OmitStoreAsNull();

			return this;
		}

		public TabularProperty AllowNull_StoreAsNull()
		{
			HarborProperty.AllowNull_StoreAsNull();

			return this;
		}

		public TabularProperty BlockNull()
		{
			HarborProperty.BlockNull();

			return this;
		}

		public TabularProperty IndexAllowDuplicates()
		{
			HarborProperty.IndexAllowDuplicates();

			return this;
		}

		public TabularProperty IndexWithNoDuplicates_UsingFIFO()
		{
			HarborProperty.IndexWithNoDuplicates_UsingFIFO();

			return this;
		}

		public TabularProperty IndexWithNoDuplicates_UsingLIFO()
		{
			HarborProperty.IndexWithNoDuplicates_UsingLIFO();

			return this;
		}

		public TabularProperty IsImmutable()
		{
			HarborProperty.IsImmutable();

			return this;
		}

		public TabularProperty IsNotImmutable()
		{
			HarborProperty.IsNotImmutable();

			return this;
		}

		public TabularProperty MakePrivateCalculatedProperty()
		{
			HarborProperty.MakePrivateCalculatedProperty();

			return this;
		}

		public TabularProperty MakePrivateProperty()
		{
			HarborProperty.MakePrivateProperty();

			return this;
		}

		public TabularProperty MakePrivateSensitiveProperty()
		{
			HarborProperty.MakePrivateSensitiveProperty();

			return this;
		}

		public TabularProperty MakePublicCalculatedProperty()
		{
			HarborProperty.MakePublicCalculatedProperty();

			return this;
		}

		public TabularProperty MakePublicProperty()
		{
			HarborProperty.MakePublicProperty();

			return this;
		}

		public TabularProperty MakeSensitiveProperty()
		{
			HarborProperty.MakeSensitiveProperty();
			
			return this;
		}

		public TabularProperty MarkClean()
		{
			HarborProperty.MarkClean();
			
			return this;
			
		}

		public TabularProperty MarkDirty()
		{
			HarborProperty.MarkDirty();	
			
			return this;
		}

		public TabularProperty NotIndexed()
		{
			HarborProperty.NotIndexed();
			
			return this;
		}

		public TabularProperty SetCaption(string caption)
		{
			HarborProperty.SetCaption(caption);
						
			return this;
		}

		public TabularProperty TypeIsBinary()
		{
			HarborProperty.TypeIsBinary();
						
			return this;
		}

		public TabularProperty TypeIsBoolean(bool defaultValue = false)
		{
			HarborProperty.TypeIsBoolean(defaultValue);

			UseCheckBox();
			
			return this;
		}

		public TabularProperty TypeIsComputedBool(Action<HarborModel, BoolResponse> modelAction, bool defaultValue = false, string actionName = null)
		{
			HarborProperty.TypeIsComputedBool(modelAction, defaultValue, actionName);

			UseCheckBox();

			return this;
		}

		public TabularProperty TypeIsComputedDateTimeUTC(Action<HarborModel, DateTimeResponse> modelAction, DateTime defaultValue = new DateTime(),
			string actionName = null)
		{
			HarborProperty.TypeIsComputedDateTimeUTC(modelAction, defaultValue);

			UseTextEditType();

			return this;
		}

		public TabularProperty TypeIsComputedDecimal(Action<HarborModel, DecimalResponse> modelAction, decimal defaultValue = 0, string actionName = null)
		{
			HarborProperty.TypeIsComputedDecimal(modelAction, defaultValue, actionName);

			UseDecimalSpinEditType();

			return this;
		}

		public TabularProperty TypeIsComputedInt(Action<HarborModel, IntResponse> modelAction, int defaultValue = 0, string actionName = null)
		{
			HarborProperty.TypeIsComputedInt(modelAction, defaultValue, actionName);

			UseIntSpinEditType();
			
			return this;
		}

		public TabularProperty TypeIsComputedString(Action<HarborModel, StringResponse> modelAction, string defaultValue = null, string actionName = null)
		{
			HarborProperty.TypeIsComputedString(modelAction, defaultValue, actionName);

			UseTextEditType();
			
			return this;
		}

		public TabularProperty TypeIsDate(DateTime defaultValue = new DateTime())
		{
			HarborProperty.TypeIsDate(defaultValue);

			UseTextEditType();

			return this;
		}

		public TabularProperty TypeIsDateTime(DateTime defaultValue = new DateTime())
		{
			HarborProperty.TypeIsDateTime(defaultValue);

			UseTextEditType();

			return this;
		}

		public TabularProperty TypeIsDecimalNotMoney(decimal defaultValue = 0)
		{
			HarborProperty.TypeIsDecimalNotMoney(defaultValue);

			UseDecimalSpinEditType();

			return this;
		}

		public TabularProperty TypeIsFixedEnumerable()
		{
			HarborProperty.TypeIsFixedEnumerable();

			UseComboBox();

			return this;
		}

		public TabularProperty TypeIsInteger(int defaultValue = 0)
		{
			HarborProperty.TypeIsInteger(defaultValue);

			UseIntSpinEditType();

			return this;
		}

		public TabularProperty TypeIsMoney(decimal defaultValue = 0)
		{
			HarborProperty.TypeIsMoney(defaultValue);

			UseDecimalSpinEditType();

			return this;
		}

		public TabularProperty TypeIsString(string defaultValue = "")
		{
			HarborProperty.TypeIsString(defaultValue);

			UseTextEditType();

			return this;
		}

		public TabularProperty Update(string name, string caption = "", string description = "")
		{
			HarborProperty.Update(name, caption, description);
			
			return this;
		}

		public TabularProperty ValidateWithRegexAndBlockFalseMatch(string regex)
		{
			HarborProperty.ValidateWithRegexAndBlockFalseMatch(regex);
			
			return this;
		}

		public TabularProperty ValidateWithRegexAndIgnoreFalseMatch(string regex)
		{
			HarborProperty.ValidateWithRegexAndIgnoreFalseMatch(regex);
			
			return this;
		}

		public TabularProperty WhenErrorBlockChange()
		{
			HarborProperty.WhenErrorBlockChange();
			
			return this;
		}

		public TabularProperty WhenErrorIgnore()
		{
			HarborProperty.WhenErrorIgnore();
			
			return this;
		}

		public TabularProperty WhenNullBlockChange()
		{
			HarborProperty.WhenNullBlockChange();
			
			return this;
		}

		public TabularProperty WhenNullStoreAsNull()
		{
			HarborProperty.WhenNullStoreAsNull();
			
			return this;
		}

		public TabularProperty WheNullOmitValue()
		{
			HarborProperty.WheNullOmitValue();
			
			return this;			
		}

		public TabularProperty BlockMultiChange()
		{
			IsBlockMultiChange = true;
			return this;
		}

		public TabularProperty AllowMultiChange()
		{
			IsBlockMultiChange = false;
			return this;
		}

		public CheckBoxType UseCheckBox()
		{
			PropertyEditor = new CheckBoxType(this);

			return (CheckBoxType)PropertyEditor;
		}

		public ComboBoxType UseComboBox()
		{
			PropertyEditor = new ComboBoxType(this);

			return (ComboBoxType) PropertyEditor;
		}

		public DecimalSpinEditType UseDecimalSpinEditType()
		{
			PropertyEditor = new DecimalSpinEditType(this);

			return (DecimalSpinEditType) PropertyEditor;
		}

		public IdentityColumnType UseIdentityColumnType()
		{
			PropertyEditor = new IdentityColumnType(this);

			return (IdentityColumnType)PropertyEditor;
		}

		public IntSpinEditType UseIntSpinEditType()
		{
			PropertyEditor = new IntSpinEditType(this);

			return (IntSpinEditType)PropertyEditor;
		}

		public TextEditType UseTextEditType()
		{
			PropertyEditor = new TextEditType(this);

			return (TextEditType)PropertyEditor;
		}

		public DateEditType UseDateEditType()
		{
			PropertyEditor = new DateEditType(this);

			return (DateEditType)PropertyEditor;
		}

		public TimeEditType UseTimeEditType()
		{
			PropertyEditor = new TimeEditType(this);

			return (TimeEditType)PropertyEditor;
		}
	}
}
