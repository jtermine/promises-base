using System.Collections.Generic;
using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
    public class ComboBoxType: IAmATabularPropertyEditor
	{

		public ComboBoxType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}
		
        public ComboBoxType WithItems (IDictionary<string, string> items)
        {
            ItemsDictionary = new Dictionary<string, string>(items);
	        return this;
        }

		public string Name => TabularProperty.HarborProperty.Name;
		public string Caption => TabularProperty.HarborProperty.Caption;
		public EnumColumnValueType ValueType => EnumColumnValueType.ComboBox;
	    public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
	    public GridColumn GetColumn()
	    {
		    return new ComboBoxGridColumn(this);
	    }

	    public Dictionary<string, string> ItemsDictionary { get; private set; } = new Dictionary<string, string>();
	    public string DefaultValue => TabularProperty.HarborProperty.PropertyValue.GetString();
	}
}
