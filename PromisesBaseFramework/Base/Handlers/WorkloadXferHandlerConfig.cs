namespace Termine.Promises.Base.Handlers
{
    public class WorkloadXferHandlerConfig
    {
        public string BaseUri { get; set; }
        public string EndpointUri { get; set; }
        public int TimeoutInMs { get; set; } = 10000;
    }
}
