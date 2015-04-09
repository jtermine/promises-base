using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
    public class CheckBoxType: IAmATabularPropertyEditor
	{

		public CheckBoxType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}
		
        public string Name => TabularProperty.HarborProperty.Name;
		public string Caption => TabularProperty.HarborProperty.Caption;
		public EnumColumnValueType ValueType => EnumColumnValueType.CheckEdit;
	    public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
	    public GridColumn GetColumn()
	    {
		    return new CheckBoxGridColumn(this);
	    }

	    public bool DefaultValue => TabularProperty.HarborProperty.PropertyValue.GetBool();
	}
}
