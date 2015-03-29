using Termine.HarborData.Models;

namespace HarborDataFrameworkTest.HarborTestObjects
{
	public class PersonTestObject
	{
		private readonly HarborContainer _personContainer = new HarborContainer();
		private readonly HarborModel _personModel;

		public PersonTestObject()
		{
			_personModel = _personContainer.AddModel("person", "Person");

			_personModel.AddProperty(nameof(FirstName), "First Name")
				.TypeIsString("Joseph")
				.BlockNull();

			_personModel.AddProperty(nameof(LastName), "LastName")
				.TypeIsString("Termine")
				.BlockNull();

			_personModel.AddProperty(nameof(IsStudent), "Student?")
				.TypeIsBoolean(true);


			_personModel.AddProperty(nameof(IsAthlete), "Athlete?")
				.TypeIsBoolean(true);

		}

		public void ChangeFirstName()
		{
			_personModel.Properties[nameof(FirstName)]
				.TypeIsString("Justin");
		}

		public string FirstName
		{
			get { return _personModel.GetString(nameof(FirstName)); }
			set { _personModel.SetString(nameof(FirstName), value); }
		}

		public string LastName
		{
			get { return _personModel.GetString(nameof(LastName)); }
			set { _personModel.SetString(nameof(LastName), value); }
		}

		public bool IsStudent
		{
			get { return _personModel.GetBool(nameof(IsStudent)); }
			set { _personModel.Set(nameof(IsStudent), value); }
		}

		public bool IsAthlete
		{
			get { return _personModel.GetBool(nameof(IsAthlete)); }
			set { _personModel.Set(nameof(IsAthlete), value); }
		}


	}
}