using System;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
    public class TimeEditType: IAmATabularPropertyEditor
    {
		public TimeEditType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}

	    public string Name => TabularProperty.HarborProperty.Name;
	    public string Caption => TabularProperty.HarborProperty.Caption;
		public EnumColumnValueType ValueType  => EnumColumnValueType.TextEdit;
		public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
	    public GridColumn GetColumn()
	    {
		    return new TimeEditGridColumn(this);
	    }

	    public DateTime DefaultValue => TabularProperty.HarborProperty.PropertyValue.GetDateTime();

    }
}
