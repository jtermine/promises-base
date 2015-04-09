using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.Interfaces
{
	public interface IAmATabularPropertyEditor
	{
		
		string Name { get; }
		string Caption { get;  }
		EnumColumnValueType ValueType { get; }
		bool IsBlockMultiChange { get; }

		TabularProperty TabularProperty { get; }
		GridColumn GetColumn();
	}
}