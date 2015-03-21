using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;

namespace Termine.Promises.WithRedis.Harbor
{
	public sealed class HarborModel : IDisposable
	{
		public bool IsPublic => _harborModelInstance.IsPublic;
		public string Name => _harborModelInstance.Name;
		public string Caption => _harborModelInstance.Caption;
		public string Description => _harborModelInstance.Description;
		public IDictionary<string, HarborProperty> Properties => _harborModelInstance.Properties;

		private sealed class HarborModelInstance : IDisposable, INotifyPropertyChanged
		{
			private bool _isPublic;
			private string _name;
			private string _caption;
			private string _description;

			public string Name
			{
				get { return _name; }
				set
				{
					if (value == _name) return;
					_name = value;
					OnPropertyChanged();
				}
			}

			public string Caption
			{
				get { return _caption; }
				set
				{
					if (value == _caption) return;
					_caption = value;
					OnPropertyChanged();
				}
			}

			public string Description
			{
				get { return _description; }
				set
				{
					if (value == _description) return;
					_description = value;
					OnPropertyChanged();
				}
			}

			public bool IsPublic
			{
				get { return _isPublic; }
				set
				{
					if (value.Equals(_isPublic)) return;
					_isPublic = value;
					OnPropertyChanged();
				}
			}

			public Dictionary<string, HarborProperty> Properties { get; set; } = new Dictionary<string, HarborProperty>();

			public void Dispose()
			{
			}

			public event PropertyChangedEventHandler PropertyChanged;

			[NotifyPropertyChangedInvocator]
			private void OnPropertyChanged([CallerMemberName] string propertyName = null)
			{
				var handler = PropertyChanged;
				handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private readonly HarborModelInstance _harborModelInstance = new HarborModelInstance();

		public HarborModel SetCollectionAsPublic()
		{
			_harborModelInstance.IsPublic = true;

			return this;
		}

		public HarborProperty UpdateProperty(string name, string caption = "", string description = "")
		{
			if (_harborModelInstance.Properties.ContainsKey(name)) return _harborModelInstance.Properties[name];

			var property = new HarborPropertyInstance { Name = name, Caption = caption, Description = description };

			_harborModelInstance.Properties.Add(name, property);

			return property;
		}

		public void Dispose()
		{
		} 
	}
}
