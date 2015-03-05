using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public sealed class HarborModel<TT> : IAmAHarborModel, IDisposable, INotifyPropertyChanged
        where TT : IAmAHarborProperty
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

        IAmAHarborBaseType ICanExtendAnyHarborBaseType<IAmAHarborProperty>.H
        {
            get { return H; }
        }

        public Dictionary<string, IAmAHarborProperty> Properties { get; private set; }

        public HarborModel()
        {
            Properties = new Dictionary<string, IAmAHarborProperty>();
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

        public IAmAHarborModel H { get {return this;} }
        
    }
}
