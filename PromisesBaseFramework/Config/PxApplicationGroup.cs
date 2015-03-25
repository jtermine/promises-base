using System.Configuration;

namespace Termine.Promises.Config
{
	public class PxApplicationGroup : ConfigurationElement
	{

		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return base["name"] as string; }
			set { base["name"] = value; }
		}

		[ConfigurationProperty("secret", IsKey = true, IsRequired = true)]
		public string Secret
		{
			get { return base["secret"] as string; }
			set { base["secret"] = value; }
		}

	}
}