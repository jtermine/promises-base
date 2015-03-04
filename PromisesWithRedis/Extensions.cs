using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Termine.Promises.WithRedis.Harbor;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis
{
    public static class Extensions
    {
        public static string PropertyName<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;
            var body = lambda.Body as UnaryExpression;
            
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }

        public static ICanExtendAnyProperty UpdateProperty<TT>(this TT harborModel, string name, string caption = "", string description = "") where TT: ICanExtendAnyHarborBaseType
        {
            var property = new HarborProperty();

            return property;
        }

        public static ICanExtendAnyModel SetCollectionAsPublic(this ICanExtendAnyModel harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty SetCaption(this ICanExtendAnyProperty harborModel, string caption)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty AllowNullAndStoreAsNull(this ICanExtendAnyProperty harborModel)
            
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty AllowNullAndOmitStoreAsNull(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty BlockNull(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty MakePublicProperty(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty MakePublicDerivedProperty(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty MakeSensitiveProperty(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty MakePrivateProperty(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty MakePrivateSensitiveProperty(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty IndexWithNoDuplicatesUsingLIFO(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty IndexWithNoDuplicatesUsingFIFO(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty NotIndexed(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty TypeIsString(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty TypeIsInteger(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty TypeIsMoney(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty TypeIsDecimalNotMoney(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty TypeIsBoolean(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty ValidateWithRegexAndIgnoreFalseMatch(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty ValidateWithRegexAndBlockFalseMatch(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty WheNullOmitValue(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty WhenNullStoreAsNull(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty WhenNullBlockChange(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty IsImmutable(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty IsNotImmutable(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty WhenErrorIgnore(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static ICanExtendAnyProperty WhenErrorBlockChange(this ICanExtendAnyProperty harborModel)
        {
            return harborModel;
        }

        public static void Commit(this ICanExtendAnyModel harborModel)
        {
            
        }

        public static void Commit(this ICanExtendAnyProperty harborModel)
        {
            
        }

        public static ICanExtendAnyFixedRelationship HarborFixedRelationship(this ICanExtendAnyModel harborModel, string name, string caption = "")
        {
            var fixedRelationship = new HarborFixedRelationship();

            return fixedRelationship;
        }

        public static ICanExtendAnyTemporalRelationship HarborTemporalRelationship(this ICanExtendAnyModel harborModel, string name, string caption = "")
        {
            var temporalRelationship = new HarborTemporalRelationship();

            return temporalRelationship;
        }

        public static TT IsASingletonAlways<TT>(this TT relationship) where TT: ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT IsASingletonWhenActive<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT IsNotASingleton<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT MakeActive<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT MakeInactive<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT WhenMovedIntoAConflict_TakeOverSlot<TT>(this TT relationship) where TT : ICanExtendAnyTemporalRelationship
        {
            return relationship;
        }

        public static TT WhenMovedIntoAConflict_BlockMove<TT>(this TT relationship) where TT : ICanExtendAnyTemporalRelationship
        {
            return relationship;
        }

        public static TT HasMaxCapacity<TT>(this TT relationship, int maxCapacity) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT CanWaitlist<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT CannotWaitlist<TT>(this TT relationship) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        public static TT LinkModels<TT>(this TT relationship, params string[] modelNames) where TT : ICanExtendAnyRelationship
        {
            return relationship;
        }

        
    }
}
