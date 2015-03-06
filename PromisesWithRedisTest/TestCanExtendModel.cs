using Microsoft.VisualStudio.TestTools.UnitTesting;
using Termine.Promises.WithRedis;
using Termine.Promises.WithRedis.Harbor;

namespace PromisesWithRedisTest
{
    [TestClass]
    public class TestCanExtendModel
    {
        [TestMethod]
        public void TestHarborModel()
        {
            using (var harborModel = new HarborModel())
            {
                harborModel
                    .UpdateProperty("new", "caption")
                    .SetCaption("caption-2")
                    .AllowNullAndOmitStoreAsNull();

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
                    .IndexWithNoDuplicatesUsingLIFO();

                Assert.IsTrue(harborModel.IsPublic);
            }
        }

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
            }
        }
    }
}