﻿using System;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Interfaces;
using Termine.HarborData.Models;
using Termine.HarborData.Promises;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;

namespace Termine.HarborData.PropertyValueTypes
{
	public class ComputedDateTimePVType : IAmAHarborPropertyValueType
	{
		public ComputedDateTimePVType(HarborProperty harborProperty)
		{
			HarborProperty = harborProperty;
		}

		public HarborProperty HarborProperty { get; }
		public DateTime Value { get; set; }
		private readonly Promise<GenericConfig, HarborModel, GenericRequest, DateTimeResponse> _promise = new Promise<GenericConfig, HarborModel, GenericRequest, DateTimeResponse>();
		public EnumPropertyValueState ValueState { get; private set; } = EnumPropertyValueState.None;

		public void Set(byte[] value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public void Set(bool value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public void Set(DateTime value, DateTimeKind kind = DateTimeKind.Local,
			EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public void Set(decimal value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public void Set(int value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public void Set(string value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public void Set(object value, EnumPropertyValueState valueState = EnumPropertyValueState.Changed)
		{
			throw new NotImplementedException();
		}

		public byte[] GetBinary()
		{
			throw new NotImplementedException();
		}

		public bool GetBool()
		{
			throw new NotImplementedException();
		}

		public DateTime GetDateTime(DateTimeKind kind = DateTimeKind.Local)
		{
			throw new NotImplementedException();
		}

		public decimal GetDecimal()
		{
			throw new NotImplementedException();
		}

		public int GetInt()
		{
			throw new NotImplementedException();
		}

		public string GetString()
		{
			throw new NotImplementedException();
		}

		public object Get()
		{
			throw new NotImplementedException();
		}
	}
}