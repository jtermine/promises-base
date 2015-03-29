using System;
using System.Collections.Generic;

namespace Termine.HarborData.Models
{
	public sealed class HarborContainer
	{
		public Dictionary<string, HarborModel> Models => _harborContainerInstance.Models;

		public Dictionary<string, HarborFixedRelationship> FixedRelationships => _harborContainerInstance.FixedRelationships;

		public Dictionary<string, HarborTemporalRelationship> TemporalRelationships => _harborContainerInstance.TemporalRelationships;

		private sealed class HarborContainerInstance : IDisposable
		{
			public Dictionary<string, HarborModel> Models { get; set; } = new Dictionary<string, HarborModel>();

			public Dictionary<string, HarborFixedRelationship> FixedRelationships { get; set; } = new Dictionary<string, HarborFixedRelationship>();

			public Dictionary<string, HarborTemporalRelationship> TemporalRelationships { get; set; } = new Dictionary<string, HarborTemporalRelationship>();

			public void Dispose()
			{

			}
		}

		private readonly HarborContainerInstance _harborContainerInstance = new HarborContainerInstance();
		
		public HarborContainer AddModel(HarborModel model)
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

		public HarborModel AddModel(string name, string caption="", string description = "")
		{
			var harborModel = new HarborModel();

			harborModel.Update(name, caption, description);

			AddModel(harborModel);

			return harborModel;
		}

		public HarborFixedRelationship AddFixedRelationship(string name, string caption = "")
		{
			var fixedRelationship = new HarborFixedRelationship();

			fixedRelationship.Update(name, caption);

			AddFixedRelationship(fixedRelationship);

			return fixedRelationship;
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

		public HarborContainer AddTemporalRelationship(HarborTemporalRelationship relationship)
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

		public HarborTemporalRelationship AddTemporalRelationship(string name, string caption = "")
		{
			var temporalRelationship = new HarborTemporalRelationship();

			temporalRelationship.Update(name, caption);

			AddTemporalRelationship(temporalRelationship);

			return temporalRelationship;
		}

	}
}

