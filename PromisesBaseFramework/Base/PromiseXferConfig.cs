
namespace Termine.Promises.Base
{
    public class PromiseXferConfig
    {
        public string BaseUri { get; set; }
        public string EndpointUri { get; set; }
        public int TimeoutInMs { get; set; } = 10000;
        public bool UseNtlm { get; set; }
    }
}