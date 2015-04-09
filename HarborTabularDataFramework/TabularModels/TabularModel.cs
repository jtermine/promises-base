using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraGrid.Columns;
using Termine.HarborData.Models;
using Termine.HarborData.Properties;

namespace Termine.HarborTabularData.TabularModels
{
	public sealed class TabularModel : INotifyPropertyChanged
	{
		public TabularModel()
		{
			_harborModel.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);

			AddIdProperty().UseIdentityColumnType();
		}

		public string Name => _harborModel.Name;

		public IDictionary<string, TabularProperty> TabularProperties { get; } = new Dictionary<string, TabularProperty>();

		public HarborPropertyValue this[string propertyName] => _harborModel[propertyName];

		public string Caption => _harborModel.Caption;
		public string Description => _harborModel.Description;
		public bool IsPublic => _harborModel.IsPublic;
		public object DirtyLock => _harborModel.DirtyLock;
		public int Version => _harborModel.Version;

		private TabularProperty AddIdProperty()
		{
			var tabularProperty = new TabularProperty(_harborModel["_id"].HarborProperty);

			TabularProperties.Add(tabularProperty.Name, tabularProperty);

			return tabularProperty;
		}

		public TabularProperty AddProperty(string name, string caption = "", string description = "")
		{
			var tabularProperty = new TabularProperty(_harborModel.AddProperty(name, caption, description));

			TabularProperties.Add(tabularProperty.Name, tabularProperty);

			return tabularProperty;
		}

		public void MarkClean()
		{
			_harborModel.MarkClean();
		}

		public void MarkDirty()
		{
			_harborModel.MarkDirty();
		}

		private readonly HarborModel _harborModel = new HarborModel();

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		public void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public HarborModel SetCollectionAsPublic()
		{
			return _harborModel.SetCollectionAsPublic();
		}

		public HarborModel Update(string name, string caption = "", string description = "")
		{
			return _harborModel.Update(name, caption, description);
		}

		public GridColumn[] GetColumns()
		{
			return TabularProperties.Values.Select(f => f.PropertyEditor.GetColumn()).ToArray();
		}
	}
}
