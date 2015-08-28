using System.Reflection;
using Dapper;
using Newtonsoft.Json;
using NUnit.Framework;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Handlers;

namespace PromisesBaseFrameworkTest.TestPromiseSqlAction
{
    [TestFixture]
    class GetSitesTest
    {
        [Test]
        public void TestGetSites()
        {
            var promise = new Promise<ServiceConfig, GetFRCTBySiteW, GetFRCTBySiteRq, GetFRCTBySiteRx>(true);

            var request = new GetFRCTBySiteRq { SiteId = 1 };

            var js = JsonConvert.SerializeObject(request);

            promise.DeserializeRequest(js);

            //Validate we have a valid siteid
            promise.WithValidator("validateSiteId", (p, c, w, rq, rx) =>
            {
                if (rq.SiteId < 1)
                {
                    p.Abort("The SiteId was either a negative integer or not provided.");
                    return;
                }

                w.SiteId = rq.SiteId;
            });

            promise.WithSqlAction("getFolioRoomChargeTypeBySite",
                (p, c, w, rq, rx) =>
                    new WorkloadSqlHandlerConfig
                    {
                        Assembly = Assembly.GetExecutingAssembly(),
                        ConnectionString = c.TswDataConnString,
                        SqlFile = "GetFolioRoomChargeTypesBySite",
                        DoesAllowNullResponse = true,
                        SendResultsTo = rx.FolioRoomChargeTypes
                    }, (conn, cmd) => conn.Query<FRCTEntity>(cmd));
            
            promise.Run();
        }
    }
}