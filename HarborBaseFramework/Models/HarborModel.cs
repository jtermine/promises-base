using System;
using System.Collections.Generic;
using System.ComponentModel;
using Termine.HarborData.Properties;
using Termine.Promises.Base.Generics;
using static System.String;

namespace Termine.HarborData.Models
{
	public sealed class HarborModel : GenericWorkload, IDisposable, INotifyPropertyChanged
	{
		public string Name => _harborModelInstance.Name;
		public string Caption => _harborModelInstance.Caption;
		public string Description => _harborModelInstance.Description;
		public bool IsPublic => _harborModelInstance.IsPublic;
		public object DirtyLock => _harborModelInstance.DirtyLock;
		
		public IDictionary<string, HarborProperty> Properties => _harborModelInstance.Properties;

		private sealed class HarborModelInstance
		{
			public string Name { get; set; }

			public string Caption { get; set; }

			public string Description { get; set; }

			public bool IsPublic { get; set; }

			public Dictionary<string, HarborProperty> Properties { get; } = new Dictionary<string, HarborProperty>();

			public bool IsDirty { get; set; } = false;

			public readonly object DirtyLock = new object();
		}

		public void MarkDirty()
		{
			lock (_harborModelInstance.DirtyLock)
			{
				_harborModelInstance.IsDirty = true;
			}
		}

		public void MarkClean()
		{
			lock (_harborModelInstance.DirtyLock)
			{
				_harborModelInstance.IsDirty = false;
			}
		}

		private readonly HarborModelInstance _harborModelInstance = new HarborModelInstance();

		public HarborModel Update(string name, string caption = "", string description = "")
		{
			if (!IsNullOrEmpty(name)) _harborModelInstance.Name = name;

			_harborModelInstance.Description = description;
			_harborModelInstance.Caption = caption;

			return this;
		}

		public HarborModel SetCollectionAsPublic()
		{
			_harborModelInstance.IsPublic = true;

			return this;
		}

		public HarborProperty AddProperty(string name, string caption = "", string description = "")
		{
			if (_harborModelInstance.Properties.ContainsKey(name)) return _harborModelInstance.Properties[name];

			var property = new HarborProperty(this);

			property.Update(name, caption, description);

			_harborModelInstance.Properties.Add(name, property);

			return property;
		}

		public void Dispose()
		{
		}

		/*
		public bool GetBool(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (Properties.ContainsKey(propertyName) && Properties[propertyName].DataType == EnumDataType.ComputedBool)
			{
				return Properties[propertyName].ComputeBool();
			}

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetBool, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) && BitConverter.ToBoolean(instance.Bytes, 0);
		}

		public string GetString(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (Properties.ContainsKey(propertyName) && Properties[propertyName].DataType == EnumDataType.ComputedString)
			{
				return Properties[propertyName].ComputeString();
			}

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetString, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) ? Encoding.UTF8.GetString(instance.Bytes) : default(string);
		}

		public int GetInt(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (Properties.ContainsKey(propertyName) && Properties[propertyName].DataType == EnumDataType.ComputedInt)
			{
				return Properties[propertyName].ComputeInt();
			}

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetInt, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) ? BitConverter.ToInt32(instance.Bytes, 0) : default(int);
		}

		public decimal GetMoney(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetMoney, no instance was found in the collection");

			if (Properties.ContainsKey(propertyName) && Properties[propertyName].DataType == EnumDataType.ComputedDecimal)
			{
				return Properties[propertyName].ComputeDecimal();
			}

			var instance = Instances[propertyName];
			var bytes = instance.Bytes;

			if (bytes == default(byte[])) return default(decimal);

			var offset = 0;

			var i1 = BitConverter.ToInt32(bytes, offset);
			var i2 = BitConverter.ToInt32(bytes, offset + 4);
			var i3 = BitConverter.ToInt32(bytes, offset + 8);
			var i4 = BitConverter.ToInt32(bytes, offset + 12);

			return new decimal(new[] {i1, i2, i3, i4});

		}

		public decimal GetDecimal(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetDecimal, no instance was found in the collection");

			if (Properties.ContainsKey(propertyName) && Properties[propertyName].DataType == EnumDataType.ComputedDecimal)
			{
				return Properties[propertyName].ComputeDecimal();
			}

			var instance = Instances[propertyName];
			var bytes = instance.Bytes;

			if (bytes == default(byte[])) return default(decimal);

			var offset = 0;

			var i1 = BitConverter.ToInt32(bytes, offset);
			var i2 = BitConverter.ToInt32(bytes, offset + 4);
			var i3 = BitConverter.ToInt32(bytes, offset + 8);
			var i4 = BitConverter.ToInt32(bytes, offset + 12);

			return new decimal(new[] {i1, i2, i3, i4});
		}

		public DateTime GetDate(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetDate, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[]))
				? DateTime.Parse(Encoding.UTF8.GetString(instance.Bytes))
				: default(DateTime);
		}

		public DateTime GetDateTime(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.GetDateTime, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[]))
				? DateTime.Parse(Encoding.UTF8.GetString(instance.Bytes))
				: default(DateTime);
		}

		public byte[] GetBinary(string propertyName)
		{
			return default(byte[]);
		}

		public void Set(string propertyName, bool value)
		{

		}

		public void SetString(string propertyName, string value)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.SetString, no instance was found in the collection");

			var stringBytes = IsNullOrEmpty(value) ? default(byte[]) : Encoding.UTF8.GetBytes(value);

			Instances[propertyName].Bytes = stringBytes;
			Instances[propertyName].ValueState = HarborPropertyValue.EnumPropertyValueState.Changed;

			OnPropertyChanged(propertyName);
		}

		public void SetInt(string propertyName, int value)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.SetInt, no instance was found in the collection");

			var intBytes = BitConverter.GetBytes(value);

			Instances[propertyName].Bytes = intBytes;
			Instances[propertyName].ValueState = HarborPropertyValue.EnumPropertyValueState.Changed;

			OnPropertyChanged(propertyName);
		}

		public void SetDecimal(string propertyName, decimal value)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			
			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.SetDecimal, no instance was found in the collection");

			var intArray = decimal.GetBits(value);
			var result = new byte[intArray.Length*sizeof (int)];
			Buffer.BlockCopy(intArray, 0, result, 0, result.Length);

			Instances[propertyName].Bytes = result;

			OnPropertyChanged(propertyName);
		}

		public void SetMoney(string propertyName, decimal value)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);

			if (!Instances.ContainsKey(propertyName))
				throw new InvalidOperationException("On HarborModel.SetMoney, no instance was found in the collection");

			var intArray = decimal.GetBits(value);
			var result = new byte[intArray.Length*sizeof (int)];
			Buffer.BlockCopy(intArray, 0, result, 0, result.Length);

			Instances[propertyName].Bytes = result;

			OnPropertyChanged(propertyName);
		}

		public void Set(string propertyName, DateTime value)
		{
		}

		public void SetDate(string propertyName, DateTime value)
		{
		}

		public void Set(string propertyName, byte[] value)
		{
		}
		*/

		public HarborPropertyValue this[string propertyName] => Get(propertyName);

		public HarborPropertyValue Get(string propertyName)
		{
			return Properties.ContainsKey(propertyName) ? Properties[propertyName].PropertyValue : default(HarborPropertyValue);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		
	}
}
