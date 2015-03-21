using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.Promises.WithRedis.Annotations;

namespace Termine.Promises.WithRedis.Harbor
{
    public sealed class HarborProperty 
    {
	    private class HarborPropertyInstance : INotifyPropertyChanged
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
			private string _description;

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

			public event PropertyChangedEventHandler PropertyChanged;

			[NotifyPropertyChangedInvocator]
			private void OnPropertyChanged([CallerMemberName] string propertyName = null)
			{
				var handler = PropertyChanged;
				handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

		private readonly HarborPropertyInstance _harborPropertyInstance = new HarborPropertyInstance();

	    public HarborProperty Update(string name, string caption = "", string description = "")
	    {
		    _harborPropertyInstance.Name = name;
		    _harborPropertyInstance.Caption = caption;
		    _harborPropertyInstance.Description = description;

		    return this;
	    }

		public HarborProperty SetCaption(string caption)
		{
			_harborPropertyInstance.Caption = caption;
			return this;
		}

		public HarborProperty AllowNull_StoreAsNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndStoreAsNull;
			return this;
		}

		public HarborProperty AllowNull_OmitStoreAsNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndOmitStoreAsNull;
			return this;
		}

		public HarborProperty BlockNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.BlockNull;
			return this;
		}

		public HarborProperty MakePublicProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.Public;
			return this;
		}

		public HarborProperty MakePublicCalculatedProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.PublicCalculated;
			return this;
		}

		public HarborProperty MakeSensitiveProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.Sensitive;
			return this;
		}

		public HarborProperty MakePrivateProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.Private;
			return this;
		}

		public HarborProperty MakePrivateCalculatedProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.PrivateCalculated;
			return this;
		}

		public HarborProperty MakePrivateSensitiveProperty()
		{
			_harborPropertyInstance.Visibility = HarborPropertyInstance.EnumVisibility.PrivateSensitive;
			return this;
		}

		public HarborProperty IndexWithNoDuplicates_UsingLIFO()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.IndexNoDuplicates_LIFO;
			return this;
		}

		public HarborProperty IndexWithNoDuplicates_UsingFIFO()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.IndexNoDuplicates_FIFO;
			return this;
		}

		public HarborProperty NotIndexed()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.NotIndexed;
			return this;
		}

		public HarborProperty IndexAllowDuplicates()
		{
			_harborPropertyInstance.IndexType = HarborPropertyInstance.EnumIndexType.IndexAllowDuplicates;
			return this;
		}

		public HarborProperty TypeIsString()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.StringType;
			return this;
		}

		public HarborProperty TypeIsInteger()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.IntegerType;
			return this;
		}

		public HarborProperty TypeIsMoney()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.MoneyType;
			return this;
		}

		public HarborProperty TypeIsDecimalNotMoney()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.DecimalType;
			return this;
		}

		public HarborProperty TypeIsDate()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.DateType;
			return this;
		}

		public HarborProperty TypeIsDateTime()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.UTCDateTimeType;
			return this;
		}

		public HarborProperty TypeIsBinary()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.BinaryType;
			return this;
		}

		public HarborProperty TypeIsFixedEnumerable()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.FixedEnumerable;
			return this;
		}

		public HarborProperty TypeIsBoolean()
		{
			_harborPropertyInstance.DataType = HarborPropertyInstance.EnumDataType.BooleanType;
			return this;
		}

		public HarborProperty ValidateWithRegexAndIgnoreFalseMatch(string regex)
		{
			_harborPropertyInstance.ValidateWithRegex = true;
			_harborPropertyInstance.BlockOnFalseRegexMatch = false;
			_harborPropertyInstance.Regex = regex;
			return this;
		}

		public HarborProperty ValidateWithRegexAndBlockFalseMatch(string regex)
		{
			_harborPropertyInstance.ValidateWithRegex = true;
			_harborPropertyInstance.BlockOnFalseRegexMatch = true;
			_harborPropertyInstance.Regex = regex;
			return this;
		}

		public HarborProperty WheNullOmitValue()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndOmitStoreAsNull;
			return this;
		}

		public HarborProperty WhenNullStoreAsNull()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.AllowNullAndStoreAsNull;
			return this;
		}

		public HarborProperty WhenNullBlockChange()
		{
			_harborPropertyInstance.AllowNull = HarborPropertyInstance.EnumAllowNull.BlockNull;
			return this;
		}

		public HarborProperty IsImmutable()
		{
			_harborPropertyInstance.IsImmutable = true;
			return this;
		}

		public HarborProperty IsNotImmutable()
		{
			_harborPropertyInstance.IsImmutable = false;
			return this;
		}

		public HarborProperty WhenErrorIgnore()
		{
			_harborPropertyInstance.BlockOnModelError = false;
			return this;
		}

		public HarborProperty WhenErrorBlockChange()
		{
			_harborPropertyInstance.BlockOnModelError = true;
			return this;
		}

	}
}
