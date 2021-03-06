﻿using System;
using System.Text;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class BinaryPVType : IAmAHarborPropertyValueType
	{
		public BinaryPVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		private byte[] Value { get; set; }

		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;
		public Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			ValueState = valueState;
			Value = value;
			HarborProperty.MarkDirty();
			HarborProperty.HarborModel.OnPropertyChanged(HarborProperty.Name);
		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(BitConverter.GetBytes(value), valueState);
		}

		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			switch (kind)
			{
					case DateTimeKind.Local:
					case DateTimeKind.Unspecified:
					value = value.ToUniversalTime();
					break;
			}

			var strValue = value.ToString("O");
			Set(Encoding.UTF8.GetBytes(strValue), valueState);
		}
		
		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ConvertToBytes(), valueState);
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(BitConverter.GetBytes(value), valueState);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(Encoding.UTF8.GetBytes(value), valueState);
		}

		[Obsolete("One cannot reliably set an generic object to a byte array unless the object.ToString() method has been implemented.  Convert it first and choose Set(bytes[] value).")]
		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value.ToString(), valueState);
		}

		public byte[] GetBinary()
		{
			return Value;
		}

		public bool GetBool()
		{
			return BitConverter.ToBoolean(GetBinary(), 0);
		}

		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			var strDate = Encoding.UTF8.GetString(GetBinary());

			DateTime result;
			var tryParse = DateTime.TryParse(strDate, out result);

			if (!tryParse) return default(DateTime);

			switch (kind)
			{
				case DateTimeKind.Local:
				case DateTimeKind.Unspecified:
					return result.ToLocalTime();
				default:
					return result;
			}
		}
		
		public decimal GetDecimal()
		{
			return GetBinary().ConvertToDecimal();
		}

		public int GetInt()
		{
			return GetBinary().ConvertToInt();
		}

		public string GetString()
		{
			return GetBinary().ConvertToString();
		}

		public object Get()
		{
			return GetBinary();
		}
	}
}
