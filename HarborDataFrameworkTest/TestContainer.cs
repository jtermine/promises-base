using System;
using System.Collections.Generic;
using HarborDataFrameworkTest.HarborTestObjects;
using NLog;
using NUnit.Framework;
using Termine.HarborData.Models;

namespace HarborDataFrameworkTest
{
	[TestFixture]
	public class TestContainer
	{
		[Test]
		public void TestConstructContainer()
		{
			var harborContainer = new HarborContainer();

			harborContainer
				.AddTemporalRelationship("name")
				.CanWaitlist()
				.HasInfiniteCapacity()
				.WhenMovedIntoAConflict_BlockMove()
				;

			harborContainer
				.AddFixedRelationship("match")
				.MakeSingleton_Always()
				;

			Assert.IsTrue(harborContainer.TemporalRelationships.Count == 1);
			Assert.IsTrue(harborContainer.FixedRelationships.Count == 1);
		}

		[Test]
		public void TestPersonModel()
		{
			var personTestObject = new StudentTestObject();

			personTestObject.PropertyChanged += (sender, args) =>
			{
				Assert.IsTrue(!string.IsNullOrEmpty(args.PropertyName));
				LogManager.GetCurrentClassLogger().Trace(args.PropertyName);
			};

			Assert.IsTrue(personTestObject.FirstName == "Joseph");
			Assert.IsTrue(personTestObject.LastName == "Termine");
			Assert.IsTrue(personTestObject.IsAthlete);
			Assert.IsTrue(personTestObject.IsStudent);

			personTestObject.ChangeFirstName();

			Assert.IsTrue(personTestObject.FirstName == "Justin");

			personTestObject.FirstName = "Jacqueline";
			personTestObject.LastName = "M";
			personTestObject.NumTestingBlocksRequired = 4;

			Assert.IsTrue(personTestObject.FirstName == "Jacqueline");
			Assert.IsTrue(personTestObject.LastName == "M");

			personTestObject.NumCorrect = personTestObject.NumPossible - 2;

			Assert.IsTrue(personTestObject.Grade == 0.99m);
			Assert.IsTrue(personTestObject.DoubleGrade == 1.98m);
			Assert.IsTrue(personTestObject.LetterGrade == "A");
			Assert.IsTrue(personTestObject.CanPlaySports);

			Assert.IsTrue(personTestObject.TestingTimeRequired == 120);
            
			for (var i = 0; i < 10; i++)
			{
				Assert.IsTrue(personTestObject.LetterGrade == "A");
			}

			Assert.IsTrue(personTestObject.TestingEndTime == new DateTime(2015, 4, 10, 10, 0, 0, DateTimeKind.Local));
		}

		[Test]
		public void TestCollection()
		{
			var modelCollection = new List<HarborModel>();
			var model1 = new HarborModel();

			model1.AddProperty("firstName", "First Name")
				.TypeIsString("Joe");

			Assert.IsTrue(string.Equals(model1["firstName"].GetString(), "Joe"));

			modelCollection.Add(model1);

			var model2 = new HarborModel();

			model2.AddProperty("firstName", "First Name")
				.TypeIsString("Justin");

			modelCollection.Add(model2);

			Assert.IsTrue(string.Equals(modelCollection[0]["firstName"].GetString(), "Joe"));
			Assert.IsTrue(string.Equals(modelCollection[1]["firstName"].GetString(), "Justin"));

			modelCollection[0]["firstName"].Set("Jacqueline");

			Assert.IsTrue(string.Equals(modelCollection[0]["firstName"].GetString(), "Jacqueline"));
		}

		[Test]
		public void TestEnrollmentApp()
		{
			// Shivani creates a harbor container to hold the models for Enrollment.App

			var container = new HarborContainer()
				.WithName("enrollment.app");

			// Shivani starts with the student
			var student = container.AddModel("student", "Student", "A student enrolls in courses at the school");
				
			// Shivani adds the firstName property

			student.AddProperty("firstName", "First Name", "The first name of the student (e.g. Cassandra")
				.TypeIsString()				// defines its type as string
				.BlockNull()				// tells the property to prohibit null values
				.MakePublicProperty();		// tells the HDM to consider this property to be PUBLIC, which means visible to all applications that access HDM

			student.AddProperty("lastName", "Last Name", "The last name of the student (e.g. Loew")
				.TypeIsString()				// defines its type as string
				.BlockNull()				// tells the property to prohibit null values
				.MakePublicProperty();		// tells the HDM to consider this property to be PUBLIC, which means visible to all applications that access HDM

			student.AddProperty("fullName", "Full Name", "The last name, first name of the student (e.g. Loew, Cassandra)")
				.TypeIsComputedString((m, s) =>
				{
					s.Value = $"{m["lastName"].GetString()}, {m["firstName"].GetString()}";	 // defines its type as a computed string that returns LastName, FirstName
				})							
				.IndexAllowDuplicates()		// tells the HDM that one can execute searches against this property and to expect multiple results
				.MakePublicCalculatedProperty();		// tells the HDM to consider this property to be PUBLIC, which means visible to all applications that access HDM, and
														// apply the "calculated" flag to it, so that model consumers regard it as READ-ONLY

			student.AddProperty("invFullName", "Full Name", "The first name + last name of the student (e.g. Cassandra Loew)")
				.TypeIsComputedString((m, s) =>
				{
					s.Value = $"{m["firstName"].GetString()} {m["lastName"].GetString()}";	 // defines its type as a computed string that returns FirstName LastName
				})
				.IndexAllowDuplicates()		// tells the HDM that one can execute searches against this property and to expect multiple results
				.MakePublicCalculatedProperty();		// tells the HDM to consider this property to be PUBLIC, which means visible to all applications that access HDM, and
														// apply the "calculated" flag to it, so that model consumers regard it as READ-ONLY

			student.AddProperty("emailAddress", "Email Address", "The home email address for a student")
				.BlockNull()
				.IndexAllowDuplicates();

			student["firstName"].Set("Joseph");
			student["lastName"].Set("Termine");

			Assert.IsTrue(student["FullName"].GetString() == "Termine, Joseph");

		}

		[Test]
		public void TestOnePlusOne()
		{
			var var1 = 2;
			var var2 = 0;

			var result = var1 + var2;

			Assert.IsTrue(result == 2, $"{var1}+{var2} does not equal 2");
		}
	}
}