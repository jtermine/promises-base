using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
	public class IntSpinEditType : IAmATabularPropertyEditor
	{
		public IntSpinEditType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}

		
		public IntSpinEditType WithRange(int minValue, int maxValue = int.MaxValue)
		{
			MinValue = minValue;
			MaxValue = maxValue;

			return this;
		}

		public int DefaultValue => TabularProperty.PropertyValue.GetInt();
		public int MinValue { get; private set; }
		public int MaxValue { get; private set; }

		public string Name => TabularProperty.Name;
		public string Caption => TabularProperty.Caption;
		public EnumColumnValueType ValueType => EnumColumnValueType.IntSpinEdit;
		public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
		public GridColumn GetColumn()
		{
			return new IntSpinEditGridColumn(this);
		}
	}
}