using System;
using System.Collections.Generic;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis.Harbor
{
	public sealed class HarborContainer 
	{
		private sealed class HarborContainerInstance : IDisposable
		{
			public Dictionary<string, IAmAHarborModel> Models { get; set; } = new Dictionary<string, IAmAHarborModel>();

			public Dictionary<string, HarborFixedRelationship> FixedRelationships { get; set; } = new Dictionary<string, HarborFixedRelationship>();

			public Dictionary<string, HarborTemporalRelationship> TemporalRelationships { get; set; } = new Dictionary<string, HarborTemporalRelationship>();

			public void Dispose()
			{

			}
		}

		private readonly HarborContainerInstance _harborContainerInstance = new HarborContainerInstance();
		
		public HarborContainer AddModel(IAmAHarborModel model)
		{
			if (_harborContainerInstance.Models.ContainsKey(model.Name))
			{
				_harborContainerInstance.Models[model.Name] = model;
			}
			else
			{
				_harborContainerInstance.Models.Add(model.Name, model);
			}

			return this;

		}

		public HarborContainer AddFixedRelationship(HarborFixedRelationship relationship)
		{
			if (_harborContainerInstance.FixedRelationships.ContainsKey(relationship.Name))
			{
				_harborContainerInstance.FixedRelationships[relationship.Name] = relationship;
			}
			else
			{
				_harborContainerInstance.FixedRelationships.Add(relationship.Name, relationship);
			}

			return this;
		}

		public HarborContainer AddTemporalRelationship(HarborFixedRelationship relationship)
		{
			if (_harborContainerInstance.TemporalRelationships.ContainsKey(relationship.Name))
			{
				_harborContainerInstance.TemporalRelationships[relationship.Name] = relationship;
			}
			else
			{
				_harborContainerInstance.TemporalRelationships.Add(relationship.Name, relationship);
			}

			return this;
		}

	}
}

