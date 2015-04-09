using System;
using System.Collections.Generic;
using System.Linq;
using Termine.Promises.Base.Interfaces;
using Termine.Promises.Config;

namespace Termine.Promises.Base
{
	public class PromiseConfigurator
	{
		private static PromiseConfigurator _instance;

		private PromiseConfigurator()
		{
			Configure();
		}

		public static PxConfigSection PxConfigSection => PxConfigSection.Get();

		public List<IConfigurePromise> Configurators { get; private set; } = new List<IConfigurePromise>(); 

		public static bool IsCofigured { get; set; }

		private void Configure()
		{
			if (IsCofigured) return;
			var pxInits = PxConfigSection.PxContexts.AsQueryable().FirstOrDefault(f => f.Name == "default")?.PxInits;
			IsCofigured = true;
			if (pxInits == null) return;

			Configurators = pxInits.OrderBy(f => f.Order)
				.Select(pxInit => Type.GetType(pxInit.Type, false, true))
				.Where(type => type != default(Type))
				.Select((Activator.CreateInstance))
				.Select(f => f as IConfigurePromise).ToList();
		}

		public static PromiseConfigurator Instance => _instance ?? (_instance = new PromiseConfigurator());
	}
}
