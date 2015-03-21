using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;
using Termine.Promises.WithRedis.Enumerables;

namespace Termine.Promises.WithRedis.Harbor
{
    public sealed class HarborFixedRelationship
    {
		private sealed class HarborFixedRelationshipInstance : IDisposable, INotifyPropertyChanged
		{
			private bool _isActive;
			private int _maxCapacity;
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

			public int MaxCapacity
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

	    private readonly HarborFixedRelationshipInstance _harborFixedRelationshipInstance =
		    new HarborFixedRelationshipInstance();


    }
}
