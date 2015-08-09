﻿using System;
using System.Collections.Generic;
using System.Linq;
using Termine.Promises.Base.Interfaces;
using Termine.Promises.Config;

namespace Termine.Promises.Base
{
    public class PromiseConfigurator<TC, TW, TR, TE>
        where TC : IHandlePromiseConfig
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
		private static PromiseConfigurator<TC, TW, TR,TE> _instance;

		private PromiseConfigurator()
		{
			Configure();
		}

		public static PxConfigSection PxConfigSection => PxConfigSection.Get();

		public List<IConfigurePromise<TC, TW, TR, TE>> Configurators { get; private set; } = new List<IConfigurePromise<TC, TW, TR, TE>>(); 

		private void Configure()
		{
			var pxInits = PxConfigSection.PxContexts.AsQueryable().FirstOrDefault(f => f.Name == "default")?.PxInits;
			if (pxInits == null) return;

			Configurators = pxInits.OrderBy(f => f.Order)
				.Select(pxInit => Type.GetType(pxInit.Type, false, true))
				.Where(type => type != default(Type))
				.Select((Activator.CreateInstance))
				.Select(f => f as IConfigurePromise<TC, TW, TR, TE>).ToList();
		}

		public static PromiseConfigurator<TC, TW, TR, TE> Instance => _instance ?? (_instance = new PromiseConfigurator<TC, TW, TR, TE>());
	}
}
