using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarborDataFrameworkTest.Annotations;
using Termine.HarborData.Models;

namespace HarborDataFrameworkTest.HarborTestObjects
{
	public sealed class PersonTestObject : INotifyPropertyChanged
	{
		private readonly HarborContainer _studentContainer = new HarborContainer();
		private readonly HarborModel _studentModel;

		public PersonTestObject()
		{
			_studentModel = _studentContainer.AddModel("person", "Person");

			_studentModel.PropertyChanged += StudentModelOnPropertyChanged;

			_studentModel.AddProperty(nameof(FirstName), "First Name")
				.TypeIsString("Joseph")
				.BlockNull();

			_studentModel.AddProperty(nameof(LastName), "LastName")
				.TypeIsString("Termine")
				.BlockNull();

			_studentModel.AddProperty(nameof(IsStudent), "Student?")
				.TypeIsBoolean(true);

			_studentModel.AddProperty(nameof(IsAthlete), "Athlete?")
				.TypeIsBoolean(true);

			_studentModel.AddProperty(nameof(NumCorrect), "Num. Correct")
				.TypeIsInteger(55);

			_studentModel.AddProperty(nameof(NumPossible), "Num. Possible")
				.TypeIsInteger(200);

			_studentModel.AddProperty(nameof(Grade), "Grade")
				.TypeIsComputedDecimal((model, response) =>
				{
					var result = new decimal(NumCorrect)/new decimal(NumPossible);
					response.Value = result;
				});

			_studentModel.AddProperty(nameof(DoubleGrade), "DoubleGrade")
				.TypeIsComputedDecimal((model, response) =>
				{
					response.Value = Grade*2;
				});

			_studentModel.AddProperty(nameof(LetterGrade), "Letter Grade")
				.TypeIsComputedString((model, response) =>
				{
					if (Grade >= 0 & Grade < .5m) response.Value = "N";
					if (Grade >= .5m & Grade < .6m) response.Value = "F";
					if (Grade >= .6m & Grade < .7m) response.Value = "D";
					if (Grade >= .7m & Grade < .8m) response.Value = "C";
					if (Grade >= .8m & Grade < .9m) response.Value = "B";
					if (Grade >= .9m) response.Value = "A";
				});

			_studentModel.AddProperty(nameof(CanPlaySports), "CanPlaySports?")
				.TypeIsComputedBool((model, response) =>
				{
					response.Value = new List<string> {"A", "B", "C"}.Contains(LetterGrade);
				});

			_studentModel.AddProperty(nameof(NumTestingBlocksRequired), "Num. Testing Blocks")
				.TypeIsInteger(1);

			_studentModel.AddProperty(nameof(TestingTimeRequired), "Testing Time Req'd")
				.TypeIsComputedInt((model, response) =>
				{
					response.Value = NumTestingBlocksRequired*30;
				});

			_studentModel.AddProperty(nameof(TestingStartTime), "Testing Start Time")
				.TypeIsDateTime(new DateTime(2015, 4, 10, 8, 0, 0, DateTimeKind.Local));

			_studentModel.AddProperty(nameof(TestingEndTime), "Testing End Time")
				.TypeIsComputedDateTimeUTC((model, response) =>
				{
					var timeSpan = new TimeSpan(0, TestingTimeRequired, 0);
					response.Value = TestingStartTime.Add(timeSpan);
				});
		}

		private void StudentModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			OnPropertyChanged(propertyChangedEventArgs.PropertyName);
		}

		public void ChangeFirstName()
		{
			_studentModel.Properties[nameof(FirstName)]
				.TypeIsString("Justin");
		}

		public string FirstName
		{
			get { return _studentModel[nameof(FirstName)]?.GetString(); }
			set { _studentModel[nameof(FirstName)].Set(value); }
		}

		public string LastName
		{
			get { return _studentModel[nameof(LastName)]?.GetString(); }
			set { _studentModel[nameof(LastName)].Set(value); }
		}

		public bool IsStudent
		{
			get { return _studentModel[nameof(IsStudent)].GetBool(); }
			set { _studentModel[nameof(IsStudent)].Set(value); }
		}

		public bool IsAthlete
		{
			get { return _studentModel[nameof(IsAthlete)].GetBool(); }
			set { _studentModel[nameof(IsAthlete)].Set(value); }
		}

		public int NumCorrect
		{
			get { return _studentModel[nameof(NumCorrect)].GetInt(); }
			set { _studentModel[nameof(NumCorrect)].Set(value); }
		}

		public int NumPossible
		{
			get { return _studentModel[nameof(NumPossible)].GetInt(); }
			set { _studentModel[nameof(NumPossible)].Set(value); }
		}

		public int NumTestingBlocksRequired {
			get { return _studentModel[nameof(NumTestingBlocksRequired)].GetInt(); }
			set { _studentModel[nameof(NumTestingBlocksRequired)].Set(value); }
		}

		public int TestingTimeRequired => _studentModel[nameof(TestingTimeRequired)].GetInt();

		public DateTime TestingStartTime
		{
			get { return _studentModel[nameof(TestingStartTime)].GetDateTime(); }
			set { _studentModel[nameof(TestingStartTime)].Set(value); }
		}

		public DateTime TestingEndTime => _studentModel[nameof(TestingEndTime)].GetDateTime();

		public decimal Grade => _studentModel[nameof(Grade)].GetDecimal();

		public decimal DoubleGrade => _studentModel[nameof(DoubleGrade)].GetDecimal();

		public string LetterGrade => _studentModel[nameof(LetterGrade)].GetString();

		public bool CanPlaySports => _studentModel[nameof(CanPlaySports)].GetBool();

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}