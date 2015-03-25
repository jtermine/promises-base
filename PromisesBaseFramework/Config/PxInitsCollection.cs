using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Termine.Promises.Config
{
	[ConfigurationCollection(typeof (PxInit), AddItemName = "PxInit")]
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class PxInitsCollection : ConfigurationElementCollection, IEnumerable<PxInit>
	{

		protected override ConfigurationElement CreateNewElement()
		{
			return new PxInit();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			var l_configElement = element as PxInit;
			return l_configElement?.Name ;
		}

		private PxInit this[int index] => BaseGet(index) as PxInit;

		#region IEnumerable<ConfigSubElement> Members

		IEnumerator<PxInit> IEnumerable<PxInit>.GetEnumerator()
		{
			return (from i in Enumerable.Range(0, this.Count)
				select this[i])
				.GetEnumerator();
		}

		#endregion
	}
}