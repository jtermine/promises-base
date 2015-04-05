﻿using System;
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
		
		public HarborPropertyValue this[string propertyName] => Get(propertyName);

		private HarborPropertyValue Get(string propertyName)
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
