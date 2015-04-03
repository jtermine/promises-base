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

			Assert.IsTrue(personTestObject.FirstName == "Joseph");
			Assert.IsTrue(personTestObject.LastName == "Termine");
			Assert.IsTrue(personTestObject.IsAthlete);
			Assert.IsTrue(personTestObject.IsStudent);

			personTestObject.ChangeFirstName();
			
			Assert.IsTrue(personTestObject.FirstName == "Justin");

			personTestObject.FirstName = "Jacqueline";
			personTestObject.LastName = "M";

			Assert.IsTrue(personTestObject.FirstName == "Jacqueline");
			Assert.IsTrue(personTestObject.LastName == "M");

			personTestObject.NumCorrect = personTestObject.NumPossible - 2;

			Assert.IsTrue(personTestObject.Grade == 0.99m);
			Assert.IsTrue(personTestObject.DoubleGrade == 1.98m);
			Assert.IsTrue(personTestObject.LetterGrade == "F");
		}

    }
}