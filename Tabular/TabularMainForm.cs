namespace Tabular
{
    public partial class TabularMainForm : DevExpress.XtraEditors.XtraForm
    {
        public TabularMainForm()
        {
            InitializeComponent();
            
            var x = new TableForm
            {
                MdiParent = this
            };
            x.Show();
        }
    }
}