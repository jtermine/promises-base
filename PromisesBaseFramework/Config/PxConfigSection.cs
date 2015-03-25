using System.Configuration;

namespace Termine.Promises.Config
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class PxConfigSection : ConfigurationSection
	{
		[ConfigurationProperty("PxContexts", IsRequired = true)]
		public PxContextsCollection PxContexts => base["PxContexts"] as PxContextsCollection ?? new PxContextsCollection();

		[ConfigurationProperty("PxApplicationGroup", IsRequired = true)]
		public PxApplicationGroup PxApplicationGroup => base ["PxApplicationGroup"] as PxApplicationGroup ?? new PxApplicationGroup();

		public static PxConfigSection Get()
		{
			var config = ConfigurationManager.GetSection("PxConfig") as PxConfigSection;
			return config ?? new PxConfigSection();
		}
	}
}