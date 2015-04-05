using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Models;

namespace Termine.HarborData.Interfaces
{
	public interface IAmAHarborPropertyValueType
	{
		HarborProperty HarborProperty { get; }
		EnumPropertyValueState ValueState { get; }
		Action<IAmAHarborPropertyValueType> ComputeAction { get; set; }

		void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);
		void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);
		void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);
		void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);
		void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);
		void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);
		void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed);

		byte[] GetBinary();
		bool GetBool();
		DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local);
		decimal GetDecimal();
		int GetInt();
		string GetString();
		object Get();

	}
}
