using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Termine.HarborData.Models;
using Termine.HarborData.Properties;

namespace Termine.HarborTabularData.TabularModels
{
	public sealed class TabularModel
	{
		public string Name => HarborModel.Name;

		public IDictionary<string, TabularProperty> TabularProperties { get; } = new Dictionary<string, TabularProperty>();

		public HarborPropertyValue this[string propertyName] => HarborModel[propertyName];

		public string Caption => HarborModel.Caption;
		public string Description => HarborModel.Description;
		public bool IsPublic => HarborModel.IsPublic;
		public object DirtyLock => HarborModel.DirtyLock;
		public int Version => HarborModel.Version;

		public TabularProperty AddProperty(string name, string caption = "", string description = "")
		{
			var tabularProperty = new TabularProperty(HarborModel.AddProperty(name, caption, description));

			TabularProperties.Add(tabularProperty.Name, tabularProperty);

			return tabularProperty;
		}

		public void MarkClean()
		{
			HarborModel.MarkClean();
		}

		public void MarkDirty()
		{
			HarborModel.MarkDirty();
		}

		private static HarborModel HarborModel = new HarborModel();

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public HarborModel SetCollectionAsPublic()
		{
			return HarborModel.SetCollectionAsPublic();
		}

		public HarborModel Update(string name, string caption = "", string description = "")
		{
			return HarborModel.Update(name, caption, description);
		}
	}
}
