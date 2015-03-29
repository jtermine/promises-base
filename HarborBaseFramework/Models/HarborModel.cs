using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Termine.HarborData.Properties;

namespace Termine.HarborData.Models
{
	public sealed class HarborModel : IDisposable
	{
		public bool IsPublic => _harborModelInstance.IsPublic;
		public string Name => _harborModelInstance.Name;
		public string Caption => _harborModelInstance.Caption;
		public string Description => _harborModelInstance.Description;
		public IDictionary<string, HarborProperty> Properties => _harborModelInstance.Properties;
		public IDictionary<string, HarborPropertyInstance> Instances => _harborModelInstance.Instances;

		private sealed class HarborModelInstance
		{
			public string Name { get; set; }

			public string Caption { get; set; }

			public string Description { get; set; }

			public bool IsPublic { get; set; }

			public Dictionary<string, HarborProperty> Properties { get; } = new Dictionary<string, HarborProperty>();

			public Dictionary<string, HarborPropertyInstance> Instances { get; } = new Dictionary<string, HarborPropertyInstance>();

			public void SyncInstances()
			{
				foreach (var harborProperty in Properties)
				{
					SyncInstance(harborProperty.Key);
				}
			}

			public bool MustSyncInstance(string name)
			{
				if (!Properties.ContainsKey(name)) return false;
				if (!Instances.ContainsKey(name)) return true;
				return Instances[name].PropertyVersion != Properties[name].Version;
			}

			public void SyncInstance(string name)
			{
				if (!Properties.ContainsKey(name)) return;

				var property = Properties[name];
				
				if (!Instances.ContainsKey(name))
				{
					Instances.Add(name,
						new HarborPropertyInstance
						{
							Name = property.Name,
							PropertyInstanceState = HarborPropertyInstance.EnumPropertyInstanceState.Default,
							Bytes = property.GetDefaultInBytes(),
							PropertyVersion = property.Version
						});
					return;
				}

				var instance = Instances[name];

				if (instance == null || instance.PropertyVersion == property.Version || instance.PropertyInstanceState == HarborPropertyInstance.EnumPropertyInstanceState.Loaded ||
				    instance.PropertyInstanceState == HarborPropertyInstance.EnumPropertyInstanceState.Changed) return;

				instance.PropertyInstanceState = HarborPropertyInstance.EnumPropertyInstanceState.Default;
				instance.Bytes = property.GetDefaultInBytes();
				instance.PropertyVersion = property.Version;
			}
		}

		private readonly HarborModelInstance _harborModelInstance = new HarborModelInstance();

		public HarborModel Update(string name, string caption = "", string description = "")
		{
			if (!string.IsNullOrEmpty(name)) _harborModelInstance.Name = name;

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

			var property = new HarborProperty();

			property.Update(name, caption, description);

			_harborModelInstance.Properties.Add(name, property);

			_harborModelInstance.SyncInstance(name);

			return property;
		}

		public void Dispose()
		{
		}

		public bool GetBool(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetBool, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) && BitConverter.ToBoolean(instance.Bytes, 0);
		}

		public string GetString(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetString, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) ? Encoding.UTF8.GetString(instance.Bytes) : default(string);
		}

		public int GetInt(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetInt, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) ? BitConverter.ToInt32(instance.Bytes, 0) : default(int);
		}

		public decimal GetMoney(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetMoney, no instance was found in the collection");

			var instance = Instances[propertyName];
			var bytes = instance.Bytes;

			if (bytes == default(byte[])) return default(decimal);

			var offset = 0;

			var i1 = BitConverter.ToInt32(bytes, offset);
			var i2 = BitConverter.ToInt32(bytes, offset + 4);
			var i3 = BitConverter.ToInt32(bytes, offset + 8);
			var i4 = BitConverter.ToInt32(bytes, offset + 12);

			return new decimal(new int[] { i1, i2, i3, i4 });

		}

		public decimal GetDecimal(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetDecimal, no instance was found in the collection");

			var instance = Instances[propertyName];
			var bytes = instance.Bytes;

			if (bytes == default(byte[])) return default(decimal);

			var offset = 0;

			var i1 = BitConverter.ToInt32(bytes, offset);
			var i2 = BitConverter.ToInt32(bytes, offset + 4);
			var i3 = BitConverter.ToInt32(bytes, offset + 8);
			var i4 = BitConverter.ToInt32(bytes, offset + 12);

			return new decimal(new[] { i1, i2, i3, i4 });
		}
		public DateTime GetDate(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetDate, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) ? DateTime.Parse(Encoding.UTF8.GetString(instance.Bytes)) : default(DateTime);
		}

		public DateTime GetDateTime(string propertyName)
		{
			if (_harborModelInstance.MustSyncInstance(propertyName)) _harborModelInstance.SyncInstance(propertyName);
			if (!Instances.ContainsKey(propertyName)) throw new InvalidOperationException("On HarborModel.GetDateTime, no instance was found in the collection");

			var instance = Instances[propertyName];

			return (instance.Bytes != default(byte[])) ? DateTime.Parse(Encoding.UTF8.GetString(instance.Bytes)) : default(DateTime);
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

			var stringBytes = string.IsNullOrEmpty(value) ? default(byte[]) : Encoding.UTF8.GetBytes(value);

			Instances[propertyName].Bytes = stringBytes;
			Instances[propertyName].PropertyInstanceState = HarborPropertyInstance.EnumPropertyInstanceState.Changed;
		}

		public void Set(string propertyName, int value)
		{
		}

		public void SetDecimal(string propertyName, decimal value)
		{
		}

		public void SetMoney(string propertyName, decimal value)
		{
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
	}
}
