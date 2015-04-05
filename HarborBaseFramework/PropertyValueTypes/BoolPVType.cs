using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class BoolPVType : IAmAHarborPropertyValueType
	{
		public BoolPVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		private bool Value { get; set; }
		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;
		public Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			try
			{
				Set(value.ConvertToBool());
			}
			catch
			{
				Set(false);
			}

		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Value = value;
			ValueState = valueState;
			HarborProperty.MarkDirty();
		}

		[Obsolete("Cannot reliably convert a DateTime to a bool, so this function will perform no action.")]
		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value != 0m);
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			Set(value != 0);
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			try
			{
				bool parsedBool;
				if (bool.TryParse(value, out parsedBool)) Set(parsedBool);
			}
			catch
			{
				Set(false);
			}
		}

		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			try
			{
				Set((bool)value);
			}
			catch
			{
				Set(false);
			}
		}

		public byte[] GetBinary()
		{
			return GetBool().ConvertToBytes();
		}

		public bool GetBool()
		{
			return Value;
		}

		[Obsolete("Cannot reliably convert a bool to a DateTime value, so this will always return a default(DateTime)")]
		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			return default(DateTime);
		}

		/// <summary>
		/// Returns the computed value as a decimal
		/// </summary>
		/// <returns>returns a 1.0m when true, 0.0m when false</returns>
		public decimal GetDecimal()
		{
			return (GetBool()) ? 1.0m : 0m;
		}

		/// <summary>
		/// Returns the computed value as an int
		/// </summary>
		/// <returns>returns a 1 when true, 0 when false</returns>
		public int GetInt()
		{
			return (GetBool()) ? 1 : 0;
		}

		/// <summary>
		/// Returns the computed value as string
		/// </summary>
		/// <returns>returns "true" and "false"</returns>
		public string GetString()
		{
			return (GetBool()) ? "true" : "false";
		}

		public object Get()
		{
			return GetBool();
		}
	}
}
