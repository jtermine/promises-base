using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
	public class IdentityColumnType : IAmATabularPropertyEditor
	{
		public IdentityColumnType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}

		public string Name => TabularProperty.Name;
		public string Caption => TabularProperty.Caption;
		public EnumColumnValueType ValueType => EnumColumnValueType.IdentityColumn;
		public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
		public GridColumn GetColumn()
		{
			return new IdentityGridColumn(this);
		}
	}
}
