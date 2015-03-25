using System.Configuration;

namespace Termine.Promises.Config
{
	public class PxInit : ConfigurationElement
	{

		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return base["name"] as string; }
			set { base["name"] = value; }
		}

		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get { return base["type"] as string; }
			set { base["type"] = value; }
		}

		[ConfigurationProperty("order", DefaultValue = 0)]
		public int Order
		{
			get { return (int) base["order"]; }
			set { base["order"] = value; }
		}

	}
}