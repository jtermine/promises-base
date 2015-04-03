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

			_personModel.AddProperty(nameof(NumCorrect), "Num. Correct")
				.TypeIsInteger(55);

			_personModel.AddProperty(nameof(NumPossible), "Num. Possible")
				.TypeIsInteger(200);

			_personModel.AddProperty(nameof(Grade), "Grade")
				.TypeIsComputedDecimal((model, response) =>
				{
					var result = new decimal(NumCorrect)/new decimal(NumPossible);
					response.Value = result;
				});

			_personModel.AddProperty(nameof(DoubleGrade), "DoubleGrade")
				.TypeIsComputedDecimal((model, response) =>
				{
					response.Value = Grade*2;
				});

			/*
			_personModel.AddProperty(nameof(LetterGrade), "Letter Grade")
				.TypeIsComputedString($"if({nameof(Grade)} > .9m, \"A\", \"F\")");
				*/

			_personModel.AddProperty(nameof(LetterGrade), "Letter Grade")
				.TypeIsComputedString((model, response) =>
				{
					if (Grade >= 0 & Grade < .5m) response.Value = "N";
					if (Grade >= .5m & Grade < .6m) response.Value = "F";
					if (Grade >= .6m & Grade < .7m) response.Value = "D";
					if (Grade >= .7m & Grade < .8m) response.Value = "C";
					if (Grade >= .8m & Grade < .9m) response.Value = "B";
					if (Grade >= .9m) response.Value = "A";
				});


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

		public int NumCorrect
		{
			get { return _personModel.GetInt(nameof(NumCorrect)); }
			set { _personModel.SetInt(nameof(NumCorrect), value); }
		}

		public int NumPossible {
			get { return _personModel.GetInt(nameof(NumPossible)); }
			set { _personModel.SetInt(nameof(NumPossible), value); }
		}

		//public decimal Grade => 
		//	_personModel.GetDecimal(nameof(Grade));

		//public decimal DoubleGrade => _personModel.GetDecimal(nameof(DoubleGrade));

		public decimal Grade => _personModel.GetDecimal(nameof(Grade));

		public decimal DoubleGrade => _personModel.GetDecimal(nameof(DoubleGrade));

		public string LetterGrade => _personModel.GetString(nameof(LetterGrade));

		}
}