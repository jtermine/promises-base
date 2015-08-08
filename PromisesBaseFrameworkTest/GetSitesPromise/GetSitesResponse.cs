using System.Collections.Generic;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.GetSitesPromise
{
    public class GetSitesResponse: GenericResponse
    {
        public List<SiteEntity> Sites { get; set; } = new List<SiteEntity>();
    }
}