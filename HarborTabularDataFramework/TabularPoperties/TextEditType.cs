using DevExpress.XtraGrid.Columns;
using Termine.HarborTabularData.Enumerables;
using Termine.HarborTabularData.GridColumnExtensions;
using Termine.HarborTabularData.Interfaces;
using Termine.HarborTabularData.TabularModels;

namespace Termine.HarborTabularData.TabularPoperties
{
    public class TextEditType: IAmATabularPropertyEditor
    {
		public TextEditType(TabularProperty tabularProperty)
		{
			TabularProperty = tabularProperty;
		}

		public TextEditType WithLength(int minLength, int maxLength = int.MaxValue)
	    {
		    MinLength = minLength;
		    MaxLength = maxLength;

			return this;
	    }

	    public string Name => TabularProperty.HarborProperty.Name;
	    public string Caption => TabularProperty.HarborProperty.Caption;
		public EnumColumnValueType ValueType  => EnumColumnValueType.TextEdit;
		public bool IsBlockMultiChange => TabularProperty.IsBlockMultiChange;
		public TabularProperty TabularProperty { get; }
	    public GridColumn GetColumn()
	    {
		    return new TextEditGridColumn(this);
	    }

	    public string DefaultValue => TabularProperty.HarborProperty.PropertyValue.GetString();
        public int MinLength { get; private set; }
        public int MaxLength { get; private set; }

    }
}
