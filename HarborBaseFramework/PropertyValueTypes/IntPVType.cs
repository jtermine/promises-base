using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class IntPVType : IAmAHarborPropertyValueType
	{
		public IntPVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		private int Value { get; set; }
		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;
		public Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ConvertToInt(), valueState);
		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value ? 1 : 0, valueState);
		}

		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			switch (kind)
			{
				case DateTimeKind.Local:
				case DateTimeKind.Unspecified:
					Set(value.ToUniversalTime().ToUnixTime(), valueState);
					break;
				default:
					Set(value.ToUnixTime(), valueState);
					break;
			}
		}

		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(decimal.ToInt32(value), valueState);
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Value = value;
			ValueState = valueState;
			HarborProperty.MarkDirty();
			HarborProperty.HarborModel.OnPropertyChanged(HarborProperty.Name);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			try
			{
				int parsed;
				if (int.TryParse(value, out parsed)) Set(parsed, valueState);
			}
			catch 
			{
				Set(default(int), valueState);
			}
		}

		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			try
			{
				Set(Convert.ToInt32(value), valueState);
			}
			catch
			{
				Set(default(int), valueState);
			}
		}

		public byte[] GetBinary()
		{
			return GetInt().ConvertToBytes();
		}

		public bool GetBool()
		{
			return (GetInt() != 0);
		}

		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			switch (kind)
			{
				case DateTimeKind.Local:
				case DateTimeKind.Unspecified:
					return GetInt().FromUnixTime().ToLocalTime();
				default:
					return GetInt().FromUnixTime();
			}
		}

		public decimal GetDecimal()
		{
			return GetInt();
		}

		public int GetInt()
		{
			return Value;
		}

		public string GetString()
		{
			return GetInt().ToString();
		}

		public object Get()
		{
			return GetInt();
		}
	}
}
