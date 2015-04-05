using System;
using System.Globalization;
using System.Text;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class StringPVType : IAmAHarborPropertyValueType
	{
		public StringPVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		private string Value { get; set; }
		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;
		public Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(Encoding.UTF8.GetString(value), valueState);
		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value ? "true" : "false", valueState);
		}

		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			switch (kind)
			{
					case DateTimeKind.Local:
					case DateTimeKind.Unspecified:
					Set(value.ToUniversalTime().ToString("O"), valueState);
					break;
				default:
					Set(value.ToString("0"), valueState);
					break;
			}
		}

		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ToString(CultureInfo.InvariantCulture), valueState);
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(Convert.ToString(value), valueState);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Value = value;
			ValueState = valueState;
			HarborProperty.MarkDirty();
		}

		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ToString(), valueState);
		}

		public byte[] GetBinary()
		{
			return GetString().ConvertToBytes();
		}

		public bool GetBool()
		{
			try
			{
				bool returnBool;
				return bool.TryParse(GetString(), out returnBool) && returnBool;
			}
			catch
			{
				return default(bool);
			}
		}

		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			try
			{
				DateTime returnDateTime;
				return DateTime.TryParse(GetString(), out returnDateTime) ? returnDateTime : default(DateTime);
			}
			catch
			{
				return default(DateTime);
			}
		}

		public decimal GetDecimal()
		{
			try
			{
				decimal returnDecimal;
				return decimal.TryParse(GetString(), out returnDecimal) ? returnDecimal : default(decimal);
			}
			catch
			{
				return default(decimal);
			}
		}

		public int GetInt()
		{
			try
			{
				int returnInt;
				return int.TryParse(GetString(), out returnInt) ? returnInt : default(int);
			}
			catch
			{
				return default(int);
			}
		}

		public string GetString()
		{
			return Value;
		}

		public object Get()
		{
			return GetString();
		}
	}
}
