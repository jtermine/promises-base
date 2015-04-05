using System;
using System.Globalization;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class ComputedDecimalPVType : IAmAHarborPropertyValueType
	{
		public ComputedDecimalPVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		private decimal Value { get; set; }

		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;
		public Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ConvertToDecimal(), valueState);
		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value ? 1m : 0m, valueState);
		}

		[Obsolete("Cannot reliably convert a DateTime to a decimal, so this function will perform no action.")]
		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}
		
		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Value = value;
			ValueState = valueState;
			HarborProperty.MarkDirty();
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{

			Set(Convert.ToDecimal(value), valueState);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			try
			{
				decimal parsed;
				if (decimal.TryParse(value, out parsed)) Set(parsed, valueState);
			}
			catch 
			{
				Set(default(decimal));
			}
		}

		[Obsolete("Cannot reliably convert an untyped object to a decimal, so this function will perform no action.  Convert it first to a supported type.")]
		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		public byte[] GetBinary()
		{
			return GetDecimal().ConvertToBytes();
		}

		public bool GetBool()
		{
			return (GetDecimal() != 0m);
		}

		[Obsolete("Cannot reliably convert a decimal to a DateTime value, so this will always return a default(DateTime)")]
		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			return default(DateTime);
		}

		public decimal GetDecimal()
		{
			ComputeAction?.Invoke(this);
			return Value;
		}

		public int GetInt()
		{
			return Convert.ToInt32(GetDecimal());
		}

		public string GetString()
		{
			return GetDecimal().ToString(CultureInfo.InvariantCulture);
		}

		public object Get()
		{
			return GetDecimal();
		}
	}
}
