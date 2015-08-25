using System.Runtime.Serialization;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.TestPromiseSqlAction
{
    [DataContract]
    public class GetFRCTBySiteRq : GenericRequest
    {
        [DataMember(Name="siteId")]
        public int SiteId { get; set; }

    }
}