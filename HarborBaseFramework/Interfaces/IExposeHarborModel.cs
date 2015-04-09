using System.Collections.Generic;
using System.ComponentModel;
using Termine.HarborData.Models;

namespace Termine.HarborData.Interfaces
{
	public interface IExposeHarborModel : INotifyPropertyChanged
	{
		HarborPropertyValue this[string propertyName] { get; }

		string Caption { get; }
		string Description { get; }
		object DirtyLock { get; }
		bool IsPublic { get; }
		string Name { get; }
		// IDictionary<string, HarborProperty> Properties { get; }
		int Version { get; }

		HarborProperty AddProperty(string name, string caption = "", string description = "");
		void MarkClean();
		void MarkDirty();
		void OnPropertyChanged(string propertyName);
		HarborModel SetCollectionAsPublic();
		HarborModel Update(string name, string caption = "", string description = "");
	}
}