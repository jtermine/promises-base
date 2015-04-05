using System;
using HarborDataFrameworkTest.HarborTestObjects;
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
			var personTestObject = new PersonTestObject();

			personTestObject.PropertyChanged += (sender, args) =>
			{
				Assert.IsTrue(!string.IsNullOrEmpty(args.PropertyName));
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

            Assert.IsTrue(personTestObject.TestingEndTime == new DateTime(2015, 4, 10, 10, 0, 0, DateTimeKind.Local));
		}

    }
}