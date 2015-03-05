using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
    public sealed class HarborProperty: IAmAHarborProperty, ICanExtendAnyProperty, INotifyPropertyChanged
    {
        private string _name;
        private string _caption;
        private EnumAllowNull _allowNull = EnumAllowNull.AllowNullAndOmitStoreAsNull;
        private EnumVisibility _visibility = EnumVisibility.Private;
        private EnumIndexType _indexType = EnumIndexType.NotIndexed;
        private EnumDataType _dataType = EnumDataType.StringType;
        private bool _validateWithRegex;
        private bool _blockOnFalseRegexMatch;
        private bool _blockOnModelError;
        private bool _isImmutable;
        private string _regex;

        public enum EnumAllowNull
        {
            AllowNullAndStoreAsNull = 0,
            AllowNullAndOmitStoreAsNull = 1,
            BlockNull = 2,
        }

        public enum EnumVisibility
        {
            Private = 0,
            PrivateSensitive = 1,
            PrivateCalculated = 2,
            Sensitive = 3,
            Public = 4,
            PublicCalculated = 5
        }

        public enum EnumIndexType
        {
            NotIndexed = 0,
            IndexNoDuplicates_FIFO = 1,
            IndexNoDuplicates_LIFO = 2,
            IndexAllowDuplicates = 3,
        }

        public enum EnumDataType
        {
            StringType = 1,
            IntegerType = 2,
            DateType = 3,
            UTCDateTimeType = 4,
            BooleanType = 5,
            MoneyType = 6,
            DecimalType = 7,
            BinaryType = 8,
            FixedEnumerable = 9
        }

        public IAmAHarborProperty Property
        {
            get { return this; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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

        public EnumAllowNull AllowNull
        {
            get { return _allowNull; }
            set
            {
                if (value == _allowNull) return;
                _allowNull = value;
                OnPropertyChanged();
            }
        }

        public EnumVisibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (value == _visibility) return;
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public EnumIndexType IndexType
        {
            get { return _indexType; }
            set
            {
                if (value == _indexType) return;
                _indexType = value;
                OnPropertyChanged();
            }
        }

        public EnumDataType DataType
        {
            get { return _dataType; }
            set
            {
                if (value == _dataType) return;
                _dataType = value;
                OnPropertyChanged();
            }
        }

        public bool ValidateWithRegex
        {
            get { return _validateWithRegex; }
            set
            {
                if (value.Equals(_validateWithRegex)) return;
                _validateWithRegex = value;
                OnPropertyChanged();
            }
        }

        public string Regex
        {
            get { return _regex; }
            set
            {
                if (value == _regex) return;
                _regex = value;
                OnPropertyChanged();
            }
        }

        public bool BlockOnFalseRegexMatch
        {
            get { return _blockOnFalseRegexMatch; }
            set
            {
                if (value.Equals(_blockOnFalseRegexMatch)) return;
                _blockOnFalseRegexMatch = value;
                OnPropertyChanged();
            }
        }

        public bool BlockOnModelError
        {
            get { return _blockOnModelError; }
            set
            {
                if (value.Equals(_blockOnModelError)) return;
                _blockOnModelError = value;
                OnPropertyChanged();
            }
        }

        public bool IsImmutable
        {
            get { return _isImmutable; }
            set
            {
                if (value.Equals(_isImmutable)) return;
                _isImmutable = value;
                OnPropertyChanged();
            }
        }
    }
}
