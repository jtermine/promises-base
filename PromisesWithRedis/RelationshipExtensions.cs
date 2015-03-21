using System;
using System.Linq;
using Termine.Promises.WithRedis.Enumerables;
using Termine.Promises.WithRedis.Harbor;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis
{
	public static partial class Extensions
	{
		public static ICanExtendAnyFixedRelationship HarborFixedRelationship(this ICanExtendAHarborContainer baseType, string name, string caption = "")
		{
			var fixedRelationship = new HarborFixedRelationship
			{
				Name = name,
				Caption = caption
			};

			baseType.C.AddRelationship(fixedRelationship );

			return fixedRelationship;
		}

		public static ICanExtendAnyTemporalRelationship HarborTemporalRelationship(this ICanExtendAHarborContainer baseType, string name, string caption = "")
		{
			var temporalRelationship = new HarborTemporalRelationship()
			{
				Name = name,
				Caption = caption
			};

			baseType.C.AddRelationship(temporalRelationship);

			return temporalRelationship;
		}

		

		public static TT WhenMovedIntoAConflict_TakeOverSlot<TT>(this TT relationship) where TT : ICanExtendAnyTemporalRelationship
		{
			relationship.T.ConflictMode = EnumTemporalRelationship_ConflictMode.TakeOverSlot;

			return relationship;
		}

		public static TT WhenMovedIntoAConflict_BlockMove<TT>(this TT relationship) where TT : ICanExtendAnyTemporalRelationship
		{
			relationship.T.ConflictMode = EnumTemporalRelationship_ConflictMode.BlockMove;

			return relationship;
		}

		public static TT HasMaxCapacity<TT>(this TT relationship, int maxCapacity) where TT : ICanExtendAnyRelationship
		{
			relationship.R.MaxCapacity = maxCapacity;

			return relationship;
		}

		public static TT HasInfiniteCapacity<TT>(this TT relationship, int maxCapacity) where TT : ICanExtendAnyRelationship
		{
			relationship.R.MaxCapacity = 0;

			return relationship;
		}

		public static TT CanWaitlist<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
		{
			relationship.R.CanWaitlist = true;

			return relationship;
		}

		public static TT CannotWaitlist<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
		{
			relationship.R.CanWaitlist = false;

			return relationship;
		}

		public static TT LinkModels<TT>(this TT relationship, params string[] modelNames) where TT : ICanExtendAnyRelationship
		{
			if (modelNames == default(string[]) || modelNames.Length < 1) return relationship;

			var modelsToAdd = modelNames.Where(modelName => !relationship.R.Models.Contains(modelName)).ToArray();

			relationship.R.Models.AddRange(modelsToAdd);

			return relationship;
		}
	}
}