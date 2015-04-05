using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarborDataFrameworkTest.Annotations;
using Termine.HarborData.Models;

namespace HarborDataFrameworkTest.HarborTestObjects
{
	public sealed class PersonTestObject : INotifyPropertyChanged
	{
		private readonly HarborContainer _personContainer = new HarborContainer();
		private readonly HarborModel _personModel;

		public PersonTestObject()
		{
			_personModel = _personContainer.AddModel("person", "Person");

			_personModel.PropertyChanged += PersonModelOnPropertyChanged;

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
				}, "d", 7m);

			_personModel.AddProperty(nameof(DoubleGrade), "DoubleGrade")
				.TypeIsComputedDecimal((model, response) =>
				{
					response.Value = Grade*2;
				});

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

			_personModel.AddProperty(nameof(CanPlaySports), "CanPlaySports?")
				.TypeIsComputedBool((model, response) =>
				{
					response.Value = new List<string> { "A", "B", "C" }.Contains(LetterGrade);
				});
		}

		private void PersonModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			OnPropertyChanged(propertyChangedEventArgs.PropertyName);
		}

		public void ChangeFirstName()
		{
			_personModel.Properties[nameof(FirstName)]
				.TypeIsString("Justin");
		}

		public string FirstName
		{
			get { return _personModel[nameof(FirstName)]?.GetString(); }
			set { _personModel[nameof(FirstName)].Set(value); }
		}

		public string LastName
		{
			get { return _personModel[nameof(LastName)]?.GetString(); }
			set { _personModel[nameof(LastName)].Set(value); }
		}

		public bool IsStudent
		{
			get { return _personModel[nameof(IsStudent)].GetBool(); }
			set { _personModel[nameof(IsStudent)].Set(value); }
		}

		public bool IsAthlete
		{
			get { return _personModel[nameof(IsAthlete)].GetBool(); }
			set { _personModel[nameof(IsAthlete)].Set(value); }
		}

		public int NumCorrect
		{
			get { return _personModel[nameof(NumCorrect)].GetInt(); }
			set { _personModel[nameof(NumCorrect)].Set(value); }
		}

		public int NumPossible
		{
			get { return _personModel[nameof(NumPossible)].GetInt(); }
			set { _personModel[nameof(NumPossible)].Set(value); }
		}

		public decimal Grade => _personModel[nameof(Grade)].GetDecimal();

		public decimal DoubleGrade => _personModel[nameof(DoubleGrade)].GetDecimal();

		public string LetterGrade => _personModel[nameof(LetterGrade)].GetString();

		public bool CanPlaySports => _personModel[nameof(CanPlaySports)].GetBool();

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}