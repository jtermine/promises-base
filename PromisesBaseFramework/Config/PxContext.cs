using System.Configuration;

namespace Termine.Promises.Config
{
	public class PxContext : ConfigurationElement
	{

		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return base["name"] as string; }
			set { base["name"] = value; }
		}

		[ConfigurationProperty("PxInits")]
		public PxInitsCollection PxInits => base["PxInits"] as PxInitsCollection ?? new PxInitsCollection();
	}
}