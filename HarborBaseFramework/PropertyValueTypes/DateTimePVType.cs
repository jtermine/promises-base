using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class DateTimePVType : IAmAHarborPropertyValueType
	{
		public DateTimePVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		private DateTime Value { get; set; }
		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;
		public Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			var dateTime = value.ConvertToDateTime(DateTimeKind.Utc);
			Set(dateTime, DateTimeKind.Utc, valueState);
		}

		[Obsolete("Cannot reliably convert a bool to a DateTime, so this function will perform no action.")]
		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			DateTime setValue;

			if (kind != value.Kind && value.Kind != DateTimeKind.Utc)
			{
				value = value.ToUniversalTime();
				kind = DateTimeKind.Utc;
			}

			switch (kind)
			{
				case DateTimeKind.Local:
				case DateTimeKind.Unspecified:
					setValue = value.ToUniversalTime();
					break;
				default:
					setValue = value;
					break;
			}

			Value = setValue;
			ValueState = valueState;
			HarborProperty.MarkDirty();
			HarborProperty.HarborModel.OnPropertyChanged(HarborProperty.Name);
		}

		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set((int) value, valueState);
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.FromUnixTime(), DateTimeKind.Utc, valueState);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			DateTime dt;
			if (DateTime.TryParse(value, out dt)) Set(dt, DateTimeKind.Utc, valueState);
		}

		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ToString(), valueState);
		}

		public byte[] GetBinary()
		{
			return GetString().ConvertToBytes();
		}

		[Obsolete("Cannot reliably convert a bool to a DateTime, so this function will always return default(bool).")]
		public bool GetBool()
		{
			return default(bool);
		}

		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			switch (kind)
			{
				case DateTimeKind.Local:
				case DateTimeKind.Unspecified:
					return Value.ToLocalTime();
				default:
					return Value;
			}
		}

		public decimal GetDecimal()
		{
			return GetDateTime(DateTimeKind.Utc).ToUnixTime();
		}

		public int GetInt()
		{
			return Convert.ToInt32(GetDateTime(DateTimeKind.Utc).ToUnixTime());
		}

		public string GetString()
		{
			return GetDateTime(DateTimeKind.Utc).ToString("O");
		}

		public object Get()
		{
			return GetDateTime(DateTimeKind.Utc);
		}
	}
}
