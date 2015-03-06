using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public sealed class HarborModel : IAmAHarborModel, IDisposable, INotifyPropertyChanged
    {
        private bool _isPublic;
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

        IAmAHarborBaseType ICanExtendAnyHarborBaseType<IAmAHarborProperty>.HarborBaseInstance => HarborModelInstance;

	    public IAmAHarborModel HarborModelInstance => this;

	    public Dictionary<string, IAmAHarborProperty> Properties { get; }

        public IDictionary<string, IAmAHarborRelationship> Relationships { get; } 

        public HarborModel()
        {
            Properties = new Dictionary<string, IAmAHarborProperty>();
            Relationships = new Dictionary<string, IAmAHarborRelationship>();
        }

        public void Dispose()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
