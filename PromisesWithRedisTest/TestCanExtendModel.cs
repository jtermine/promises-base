using NUnit.Framework;
using Termine.Promises.WithRedis.Harbor;

namespace PromisesWithRedisTest
{
    [TestFixture]
    public class TestCanExtendModel
    {
        [Test]
        public void TestHarborModel()
        {
            using (var harborModel = new HarborModel())
            {
                harborModel
                    .UpdateProperty("new", "caption")
                    .SetCaption("caption-2")
                    .AllowNull_OmitStoreAsNull();

                harborModel
                    .UpdateProperty("new-2", "caption")
                    .SetCaption("caption-3");

                harborModel
                    .UpdateProperty("new-3", "caption-2")
                    .BlockNull()
                    .IsNotImmutable();

                harborModel
                    .UpdateProperty("new-4", "caption-4")
                    .TypeIsMoney()
                    .WhenErrorBlockChange();

                harborModel
                    .SetCollectionAsPublic()
                    .UpdateProperty("name")
                    .IsImmutable()
                    .IndexWithNoDuplicates_UsingLIFO();

                Assert.IsTrue(harborModel.IsPublic);
            }
        }

		[Test]
        public void TestHarborTemporalRelationship()
        {
            using (var harborModel = new HarborModel())
            {
                harborModel
                    .HarborTemporalRelationship("widgets")
					.MakeActive()
                    .WhenMovedIntoAConflict_TakeOverSlot()
                    .MakeInactive()
                    .LinkModels("modelName-4", "modelName-5")
                    .UpdateProperty("property-name", "Property Name", "This is what you should call the property.");

				Assert.IsTrue(harborModel.Relationships.Count > 0);
				Assert.IsTrue(harborModel.Relationships["widgets"].Models.Count == 2);
            }
        }

	    [Test]
	    public void TestHarborModelAndRelationship()
	    {
		    using (var harborContainer = new HarborContainer())
		    {
			    harborContainer
				    .HarborModel("person")
				    .SetCollectionAsPublic()
				    .UpdateProperty("firstName", "First name")
				    .AllowNull_OmitStoreAsNull();

			    harborContainer
				    .HarborFixedRelationship("city")
				    .HasMaxCapacity(10);

				harborContainer
					.HarborModel("person2")
					.HarborModelInstance

			    harborContainer
					.HarborModel("person")
						.SetCollectionAsPublic()
						.UpdateProperty("firstName", "First name")
							.AllowNull_OmitStoreAsNull();

					//.HarborModel("place")
					//.HarborModel("thing")
		    }
	    }
    }
}