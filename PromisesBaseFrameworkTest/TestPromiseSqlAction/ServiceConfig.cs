using System.Configuration;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.TestPromiseSqlAction
{
    public class ServiceConfig: GenericConfig
    {
        public ServiceConfig()
        {
            TswDataConnString = "Data Source=TSWDEVSQL1;Initial Catalog=TSWDATA;Integrated Security=True";
        }

        public string TswDataConnString { get; set; }
    }
}