using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.PropertyValueTypes;

namespace Termine.HarborData.Models
{
	public class HarborPropertyValue : HarborPropertyValue<IAmAHarborPropertyValueType>
	{
		public HarborPropertyValue(HarborProperty harborProperty) : base(harborProperty)
		{
			switch (harborProperty.DataType)
			{
				case EnumDataType.ComputedBool:
					I = new ComputedBoolPVType(harborProperty);
					break;
				case EnumDataType.BooleanType:
					I = new BoolPVType(harborProperty);
					break;
				case EnumDataType.ComputedDecimal:
					I = new ComputedDecimalPVType(harborProperty);
					break;
				case EnumDataType.DecimalType:
					I = new DecimalPVType(harborProperty);
					break;
				case EnumDataType.ComputedInt:
					I = new ComputedIntPVType(harborProperty);
					break;
				case EnumDataType.IntegerType:
					I = new IntPVType(harborProperty);
					break;
				case EnumDataType.ComputedString:
					I = new ComputedStringPVType(harborProperty);
					break;
				case EnumDataType.StringType:
					I = new StringPVType(harborProperty);
					break;
				case EnumDataType.BinaryType:
					I = new BinaryPVType(harborProperty);
					break;
				case EnumDataType.DateType:
				case EnumDataType.DateTimeUTCType:
					I = new DateTimePVType(harborProperty);
					break;
				case EnumDataType.ComputedDate:
				case EnumDataType.ComputedDateTimeUTC:
					I = new ComputedDateTimePVType(harborProperty);
					break;
				default:
					I = new NullPVType(harborProperty);
					break;
			}
		}
	}


	public class HarborPropertyValue<TT> : IAmAHarborPropertyValueType where TT : IAmAHarborPropertyValueType
	{
		protected HarborPropertyValue(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public TT I;

		public HarborProperty HarborProperty { get; }

		public EnumPropertyValueState ValueState => I.ValueState;

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, valueState);
		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, valueState);
		}

		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, kind, valueState);
		}

		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, valueState);
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, valueState);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, valueState);
		}

		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			I.Set(value, valueState);
		}

		public byte[] GetBinary()
		{
			return I.GetBinary();
		}

		public bool GetBool()
		{
			return I.GetBool();
		}

		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			return I.GetDateTime(kind);
		}

		public decimal GetDecimal()
		{
			return I.GetDecimal();
		}

		public int GetInt()
		{
			return I.GetInt();
		}

		public string GetString()
		{
			return I.GetString();
		}

		public object Get()
		{
			return I.Get();
		}
	}
}