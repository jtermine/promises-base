using System.Collections.Generic;
using NUnit.Framework;
using Termine.HarborTabularData.TabularModels;

namespace HarborTabularDataFrameworkTest
{
	[TestFixture]
    public class TestTabularModels
    {
		[Test]
		public void TestCollection()
		{
			var modelCollection = new List<TabularModel>();
			var model1 = new TabularModel();

			model1.AddProperty("firstName", "First Name")
				.TypeIsString("Joe");

			Assert.IsTrue(string.Equals(model1["firstName"].GetString(), "Joe"));

			modelCollection.Add(model1);

			var model2 = new TabularModel();

			model2.AddProperty("firstName", "First Name")
				.TypeIsString("Justin");

			modelCollection.Add(model2);

			Assert.IsTrue(string.Equals(modelCollection[0]["firstName"].GetString(), "Joe"));
			Assert.IsTrue(string.Equals(modelCollection[1]["firstName"].GetString(), "Justin"));

			modelCollection[0]["firstName"].Set("Jacqueline");

			Assert.IsTrue(string.Equals(modelCollection[0]["firstName"].GetString(), "Jacqueline"));
		}
    }
}
