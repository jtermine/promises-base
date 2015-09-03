using System.Collections.Generic;
using System.Runtime.Serialization;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.TestPromiseSqlAction
{
    [DataContract]
    public class GetFRCTBySiteRx: GenericResponse
    {
        [DataMember(Name= "folioRoomChargeTypes")]
        public List<FRCTEntity> FolioRoomChargeTypes { get; set; } = new List<FRCTEntity>();

    }
}