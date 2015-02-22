using Topshelf;

namespace PromisesTopshelf
{
    internal class Program
    {
        private static void Main()
        {
            HostFactory.Run(x =>
            {
                x.UseNLog();

                x.Service<PromiseService>(s => 
                {
                    s.ConstructUsing(name => new PromiseService()); 
                    s.WhenStarted(tc => PromiseService.Start()); 
                    s.WhenStopped(tc => PromiseService.Stop()); 
                });

                x.RunAsLocalSystem();

                x.SetDescription("Promises TopShelf Host");
                x.SetDisplayName("Promises-TopShelf");
                x.SetServiceName("Promises-TopShelf");
            });
        }
    }
}