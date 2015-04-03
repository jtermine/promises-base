using System;
using System.Text;
using Termine.HarborData.Enumerables;

namespace Termine.HarborData.Models
{
	public class HarborPropertyValue
	{
		public HarborPropertyValue(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public string Name { get; set; }
		public byte[] Bytes { get; set; }
		public EnumPropertyValueState ValueState { get; set; } = EnumPropertyValueState.None;
		public int PropertyVersion { get; set; } = -1;
		public HarborProperty HarborProperty { get; }

		public enum EnumPropertyValueState
		{
			None = 0,
			Default = 1,
			Loaded = 2,
			Changed = 3
		}

		public string GetLuaVariableExpression()
		{
			string expressionVariable;

			switch (HarborProperty.DataType)
			{
				case EnumDataType.StringType:
					expressionVariable = (Bytes == null) ? "\"\"" : $"\"{Encoding.UTF8.GetString(Bytes)}\"";
					break;
				case EnumDataType.ComputedDecimal:
				case EnumDataType.MoneyType:
				case EnumDataType.DecimalType:

					if (Bytes == null)
					{
						expressionVariable = "0";
						break;
					}

					const int offset = 0;

					var i1 = BitConverter.ToInt32(Bytes, offset);
					var i2 = BitConverter.ToInt32(Bytes, offset + 4);
					var i3 = BitConverter.ToInt32(Bytes, offset + 8);
					var i4 = BitConverter.ToInt32(Bytes, offset + 12);

					var dec = new decimal(new[] {i1, i2, i3, i4});

					expressionVariable = $"{dec}";
					break;
				case EnumDataType.IntegerType:

					if (Bytes == null)
					{
						expressionVariable = "0";
						break;
					}

					expressionVariable = $"{BitConverter.ToInt32(Bytes, 0)}";
					break;
				default:
					return "";

			}

			return $"{HarborProperty.Name}={expressionVariable}";
		}

	}
}
