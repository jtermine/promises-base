using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;

namespace Termine.HarborData.PropertyValueTypes
{
	public class NullPVType : IAmAHarborPropertyValueType
	{
		public NullPVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		public EnumPropertyValueState ValueState { get; } = EnumPropertyValueState.None;

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public byte[] GetBinary()
		{
			return default(byte[]);
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public bool GetBool()
		{
			return default(bool);
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			return default(DateTime);
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public decimal GetDecimal()
		{
			return default(decimal);
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public int GetInt()
		{
			return default(int);
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public string GetString()
		{
			return default(string);
		}

		[Obsolete("Functions on the NullPVType produce null results.  Choose the appropriate PropertyValueType for the task at hand.")]
		public object Get()
		{
			return default(object);
		}
	}
}