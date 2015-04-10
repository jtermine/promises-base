using System;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
    public class DateEditType: IAmATabularPropertyEditor
    {
		public DateEditType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}

		public DateEditType WithRange(DateTime minValue, DateTime maxValue)
		{
			MinValue = minValue;
			MaxValue = maxValue;

			return this;
	    }

	    public string Name => TabularProperty.HarborProperty.Name;
	    public string Caption => TabularProperty.HarborProperty.Caption;
		public EnumColumnValueType ValueType  => EnumColumnValueType.TextEdit;
		public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }

	    public GridColumn GetColumn()
	    {
		    return new DateEditGridColumn(this);
	    }

	    public string DefaultValue => TabularProperty.HarborProperty.PropertyValue.GetString();
        public DateTime MinValue { get; private set; }
        public DateTime MaxValue { get; private set; }

    }
}
