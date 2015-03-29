using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Termine.HarborData.Enumerables;
using Termine.HarborData.Properties;

namespace Termine.HarborData.Models
{
	public sealed class HarborFixedRelationship
	{
		public string Name => _instance.Name;
		public string Caption => _instance.Caption;
		public bool IsActive => _instance.IsActive;

		private sealed class HarborFixedRelationshipInstance : IDisposable, INotifyPropertyChanged
		{
			private bool _isActive;
			private long _maxCapacity = long.MaxValue;
			private bool _canWaitlist;
			private EnumRelationship_SingletonMode _singletonMode;
			private string _name;
			private string _caption;

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

			public bool IsActive
			{
				get { return _isActive; }
				set
				{
					if (value.Equals(_isActive)) return;
					_isActive = value;
					OnPropertyChanged();
				}
			}

			public long MaxCapacity
			{
				get { return _maxCapacity; }
				set
				{
					if (value == _maxCapacity) return;
					_maxCapacity = value;
					OnPropertyChanged();
				}
			}

			public bool CanWaitlist
			{
				get { return _canWaitlist; }
				set
				{
					if (value.Equals(_canWaitlist)) return;
					_canWaitlist = value;
					OnPropertyChanged();
				}
			}

			public EnumRelationship_SingletonMode SingletonMode
			{
				get { return _singletonMode; }
				set
				{
					if (value == _singletonMode) return;
					_singletonMode = value;
					OnPropertyChanged();
				}
			}

			public Dictionary<string, HarborProperty> Properties { get; set; } = new Dictionary<string, HarborProperty>();
			public List<string> Models { get; set; } = new List<string>();

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

		private readonly HarborFixedRelationshipInstance _instance =
			new HarborFixedRelationshipInstance();

		public HarborFixedRelationship Update(string name, string caption = "")
		{
			if (!string.IsNullOrEmpty(name)) _instance.Name = name;
			if (!string.IsNullOrEmpty(caption)) _instance.Caption = caption;

			return this;
		}

		public HarborFixedRelationship HasMaxCapacity(long maxCapacity)
		{
			_instance.MaxCapacity = maxCapacity;

			return this;
		}

		public HarborFixedRelationship HasInfiniteCapacity()
		{
			_instance.MaxCapacity = long.MaxValue;

			return this;
		}

		public HarborFixedRelationship CanWaitlist()
		{
			_instance.CanWaitlist = true;

			return this;
		}

		public HarborFixedRelationship CannotWaitlist()
		{
			_instance.CanWaitlist = false;

			return this;
		}

		public HarborFixedRelationship MakeSingleton_Always()
		{
			_instance.SingletonMode = EnumRelationship_SingletonMode.IsASingleton_Always;

			return this;
		}

		public HarborFixedRelationship MakeSingleton_WhenActive()
		{
			_instance.SingletonMode = EnumRelationship_SingletonMode.IsASingleton_WhenActive;

			return this;
		}

		public HarborFixedRelationship MakeNotSingleton()
		{
			_instance.SingletonMode = EnumRelationship_SingletonMode.NotASingleton;

			return this;
		}

		public HarborFixedRelationship LinkModels(params string[] modelNames)
		{
			if (modelNames == default(string[]) || modelNames.Length < 1) return this;

			var modelsToAdd = modelNames.Where(modelName => !_instance.Models.Contains(modelName)).ToArray();

			_instance.Models.AddRange(modelsToAdd);

			return this;
		}
	}
}
