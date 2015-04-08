using System.Collections.Generic;
using Tabular.Types;

namespace Tabular.TabModels
{
    public class StudentTabModel : List<IColumnDefinitionType>
    {
        public StudentTabModel()
        {
            Add(new IdentityColumnType("_id", "Id"));
            Add(new TextEditType("FirstName", "First Name", "First_name", 0, 1000));
            Add(new TextEditType("LastName", "Last Name", "Last_name", 0, 1000));
            Add(new TextEditType("City", "City", "City_", 0, 1000));
            Add(new TextEditType("State", "State", "NJ", 0, 1000));
            Add(new IntSpinEditType("NumRecords", "# Records"));
            Add(new DecimalSpinEditType("TotalDollars", "Total Dollars", 125.00m, 0m, 1000m));

            var studentTypes = new Dictionary<string, string>
            {
                {"0", "PartTime"}, {"1", "Full Time"}, {"2", "Inactive"}
            };

            Add(new ComboBoxType("StudentType", "Student Type", "0", studentTypes));

            var genderTypes = new Dictionary<string, string>
            {
                {"1", "Male"}, {"2", "Female"}, {"0", "Not specified"}
            };

            Add(new ComboBoxType("GenderType", "Gender", "0", genderTypes));
        }
    }
}