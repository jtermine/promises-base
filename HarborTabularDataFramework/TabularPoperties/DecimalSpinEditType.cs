using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
    public class DecimalSpinEditType: IAmATabularPropertyEditor
    {
		public DecimalSpinEditType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}


		public DecimalSpinEditType WithRange(decimal minValue, decimal maxValue = decimal.MaxValue)
		{
			MinValue = minValue;
			MaxValue = maxValue;

			return this;
		}

		public decimal DefaultValue => TabularProperty.HarborProperty.PropertyValue.GetInt();
		public decimal MinValue { get; private set; }
		public decimal MaxValue { get; private set; }

		public string Name => TabularProperty.HarborProperty.Name;
		public string Caption => TabularProperty.HarborProperty.Caption;
		public EnumColumnValueType ValueType => EnumColumnValueType.DecimalSpinEdit;
		public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
	    public GridColumn GetColumn()
	    {
		    return new DecimalSpinEditGridColumn(this);
	    }
    }
}