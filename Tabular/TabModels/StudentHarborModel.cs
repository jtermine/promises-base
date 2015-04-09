using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Termine.HarborData.Properties;
using Termine.HarborTabularData.TabularModels;

namespace Tabular.TabModels
{
	public sealed class StudentHarborModel : INotifyPropertyChanged
	{
		public TabularModel TabularModel = new TabularModel();
		public string this[string propertyName] => TabularModel?[propertyName]?.GetString();

		public StudentHarborModel()
		{
			TabularModel.PropertyChanged += StudentModelOnPropertyChanged;

			TabularModel.AddProperty(nameof(FirstName), "First Name")
				.TypeIsString("Joseph")
				.BlockNull()
				.UseTextEditType()
					.WithLength(0, 50);

			TabularModel.AddProperty(nameof(LastName), "LastName")
				.TypeIsString("Termine")
				.BlockNull()
				.UseTextEditType()
					.WithLength(0, 5);

			TabularModel.AddProperty(nameof(IsStudent), "Student?")
				.TypeIsBoolean(true)
				.UseCheckBox();
				
			TabularModel.AddProperty(nameof(IsAthlete), "Athlete?")
				.TypeIsBoolean(true)
				.UseCheckBox();

			TabularModel.AddProperty(nameof(NumCorrect), "Num. Correct")
				.TypeIsInteger(55);

			TabularModel.AddProperty(nameof(NumPossible), "Num. Possible")
				.TypeIsInteger(200);

			TabularModel.AddProperty(nameof(Grade), "Grade")
				.TypeIsComputedDecimal((model, response) =>
				{
					var result = new decimal(NumCorrect)/new decimal(NumPossible);
					response.Value = result;
				});

			TabularModel.AddProperty(nameof(DoubleGrade), "DoubleGrade")
				.TypeIsComputedDecimal((model, response) =>
				{
					response.Value = Grade*2;
				});

			TabularModel.AddProperty(nameof(LetterGrade), "Letter Grade")
				.TypeIsComputedString((model, response) =>
				{
					if (Grade >= 0 & Grade < .5m) response.Value = "N";
					if (Grade >= .5m & Grade < .6m) response.Value = "F";
					if (Grade >= .6m & Grade < .7m) response.Value = "D";
					if (Grade >= .7m & Grade < .8m) response.Value = "C";
					if (Grade >= .8m & Grade < .9m) response.Value = "B";
					if (Grade >= .9m) response.Value = "A";
				});

			TabularModel.AddProperty(nameof(CanPlaySports), "CanPlaySports?")
				.TypeIsComputedBool((model, response) =>
				{
					response.Value = new List<string> {"A", "B", "C"}.Contains(LetterGrade);
				});

			TabularModel.AddProperty(nameof(NumTestingBlocksRequired), "Num. Testing Blocks")
				.TypeIsInteger(1);

			TabularModel.AddProperty(nameof(TestingTimeRequired), "Testing Time Req'd")
				.TypeIsComputedInt((model, response) =>
				{
					response.Value = NumTestingBlocksRequired*30;
				});

			TabularModel.AddProperty(nameof(TestingStartTime), "Testing Start Time")
				.TypeIsDateTime(new DateTime(2015, 4, 10, 8, 0, 0, DateTimeKind.Local));

			TabularModel.AddProperty(nameof(TestingEndTime), "Testing End Time")
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

		[SuppressMessage("ReSharper", "InconsistentNaming")]
		public string _id => TabularModel["_id"]?.GetString();

		public string FirstName
		{
			get { return TabularModel[nameof(FirstName)]?.GetString(); }
			set { TabularModel[nameof(FirstName)].Set(value); }
		}

		public string LastName
		{
			get { return TabularModel[nameof(LastName)]?.GetString(); }
			set { TabularModel[nameof(LastName)].Set(value); }
		}

		public bool IsStudent
		{
			get { return TabularModel[nameof(IsStudent)].GetBool(); }
			set { TabularModel[nameof(IsStudent)].Set(value); }
		}

		public bool IsAthlete
		{
			get { return TabularModel[nameof(IsAthlete)].GetBool(); }
			set { TabularModel[nameof(IsAthlete)].Set(value); }
		}

		public int NumCorrect
		{
			get { return TabularModel[nameof(NumCorrect)].GetInt(); }
			set { TabularModel[nameof(NumCorrect)].Set(value); }
		}

		public int NumPossible
		{
			get { return TabularModel[nameof(NumPossible)].GetInt(); }
			set { TabularModel[nameof(NumPossible)].Set(value); }
		}

		public int NumTestingBlocksRequired {
			get { return TabularModel[nameof(NumTestingBlocksRequired)].GetInt(); }
			set { TabularModel[nameof(NumTestingBlocksRequired)].Set(value); }
		}

		public int TestingTimeRequired => TabularModel[nameof(TestingTimeRequired)].GetInt();

		public DateTime TestingStartTime
		{
			get { return TabularModel[nameof(TestingStartTime)].GetDateTime(); }
			set { TabularModel[nameof(TestingStartTime)].Set(value); }
		}

		public DateTime TestingEndTime => TabularModel[nameof(TestingEndTime)].GetDateTime();

		public decimal Grade => TabularModel[nameof(Grade)].GetDecimal();

		public decimal DoubleGrade => TabularModel[nameof(DoubleGrade)].GetDecimal();

		public string LetterGrade => TabularModel[nameof(LetterGrade)].GetString();

		public bool CanPlaySports => TabularModel[nameof(CanPlaySports)].GetBool();

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}