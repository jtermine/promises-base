using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;
using Termine.Promises.WithRedis.Enumerables;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public sealed class HarborTemporalRelationship: IAmAHarborTemporalRelationship, INotifyPropertyChanged
    {
	    private string _name;
	    private string _caption;
	    private bool _isActive;
	    private int _maxCapacity;
	    private bool _canWaitlist;

	    private EnumRelationship_SingletonMode _singletonMode;
	    private EnumTemporalRelationship_ConflictMode _conflictMode;

	    public IAmAHarborBaseType HarborBaseInstance => this;
	    public Dictionary<string, IAmAHarborProperty> Properties { get; }
	    public IDictionary<string, IAmAHarborRelationship> Relationships { get; }

	    public HarborTemporalRelationship()
        {
            Properties = new Dictionary<string, IAmAHarborProperty>();
			Relationships = new Dictionary<string, IAmAHarborRelationship>();
			Models = new List<string>();
		}

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

	    public List<string> Models { get; set; }
	    public IAmAHarborRelationship R => this;
	    public IAmAHarborTemporalRelationship T => this;

	    public EnumTemporalRelationship_ConflictMode ConflictMode
	    {
		    get { return _conflictMode; }
		    set
		    {
			    if (value == _conflictMode) return;
			    _conflictMode = value;
			    OnPropertyChanged();
		    }
	    }

	    public event PropertyChangedEventHandler PropertyChanged;

	    [NotifyPropertyChangedInvocator]
	    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
    }
}
