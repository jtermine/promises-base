using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Termine.Promises;
using Termine.Promises.BaseNUnit;
using Termine.Promises.Generics;

namespace PromisesBaseNUnitTest
{
	[TestFixture]
    public class Class1
    {
		[Test]
		public void TestInitializeATest()
		{
			var genericMessage = new GenericEventMessage(100, "Generic Message");

			var promise = new TestPromise();

			promise.Abort()
		}
    }
}
