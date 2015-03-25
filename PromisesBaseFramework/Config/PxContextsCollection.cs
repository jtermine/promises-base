using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Termine.Promises.Config
{
	[ConfigurationCollection(typeof (PxContext), AddItemName = "PxContext")]
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class PxContextsCollection : ConfigurationElementCollection, IEnumerable<PxContext>
	{

		protected override ConfigurationElement CreateNewElement()
		{
			return new PxContext();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			var l_configElement = element as PxContext;
			return l_configElement?.Name;
		}

		private PxContext this[int index] => BaseGet(index) as PxContext;

		#region IEnumerable<ConfigElement> Members

		IEnumerator<PxContext> IEnumerable<PxContext>.GetEnumerator()
		{
			return (from i in Enumerable.Range(0, this.Count)
				select this[i])
				.GetEnumerator();
		}

		#endregion
	}
}