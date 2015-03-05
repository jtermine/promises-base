using System;
using System.Linq.Expressions;
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

        public static IAmAHarborProperty UpdateProperty(this ICanExtendAnyHarborBaseType<IAmAHarborProperty> baseType, string name, string caption = "", string description = "")
        {
            if (baseType.Properties.ContainsKey(name)) return baseType.Properties[name];

            var property = new HarborProperty {Name = name, Caption = caption, Description = description};

            baseType.Properties.Add(name, property);

            return property;
        }

        public static TT SetCollectionAsPublic<TT>(this TT n)
            where TT: ICanExtendAnyHarborBaseType<IAmAHarborProperty>
        {
            var harborModel = n as IAmAHarborModel;

            if (harborModel != null) harborModel.IsPublic = true;

            return n;
        }

        public static ICanExtendAnyProperty SetCaption(this ICanExtendAnyProperty n, string caption)
        {
            n.Property.Caption = caption;
            return n;
        }

        public static ICanExtendAnyProperty AllowNullAndStoreAsNull(this ICanExtendAnyProperty n)
        {
            n.Property.AllowNull = HarborProperty.EnumAllowNull.AllowNullAndStoreAsNull;
            return n;
        }

        public static ICanExtendAnyProperty AllowNullAndOmitStoreAsNull(this ICanExtendAnyProperty n)
        {
            n.Property.AllowNull = HarborProperty.EnumAllowNull.AllowNullAndOmitStoreAsNull;
            return n;
        }

        public static ICanExtendAnyProperty BlockNull(this ICanExtendAnyProperty n)
        {
            n.Property.AllowNull = HarborProperty.EnumAllowNull.BlockNull;
            return n;
        }

        public static ICanExtendAnyProperty MakePublicProperty(this ICanExtendAnyProperty n)
        {
            n.Property.Visibility = HarborProperty.EnumVisibility.Public;
            return n;
        }

        public static ICanExtendAnyProperty MakePublicCalculatedProperty(this ICanExtendAnyProperty n)
        {
            n.Property.Visibility = HarborProperty.EnumVisibility.PublicCalculated;
            return n;
        }

        public static ICanExtendAnyProperty MakeSensitiveProperty(this ICanExtendAnyProperty n)
        {
            n.Property.Visibility = HarborProperty.EnumVisibility.Sensitive;
            return n;
        }

        public static ICanExtendAnyProperty MakePrivateProperty(this ICanExtendAnyProperty n)
        {
            n.Property.Visibility = HarborProperty.EnumVisibility.Private;
            return n;
        }

        public static ICanExtendAnyProperty MakePrivateCalculatedProperty(this ICanExtendAnyProperty n)
        {
            n.Property.Visibility = HarborProperty.EnumVisibility.PrivateCalculated;
            return n;
        }

        public static ICanExtendAnyProperty MakePrivateSensitiveProperty(this ICanExtendAnyProperty n)
        {
            n.Property.Visibility = HarborProperty.EnumVisibility.PrivateSensitive;
            return n;
        }

        public static ICanExtendAnyProperty IndexWithNoDuplicatesUsingLIFO(this ICanExtendAnyProperty n)
        {
            n.Property.IndexType = HarborProperty.EnumIndexType.IndexNoDuplicates_LIFO;
            return n;
        }

        public static ICanExtendAnyProperty IndexWithNoDuplicatesUsingFIFO(this ICanExtendAnyProperty n)
        {
            n.Property.IndexType = HarborProperty.EnumIndexType.IndexNoDuplicates_FIFO;
            return n;
        }

        public static ICanExtendAnyProperty NotIndexed(this ICanExtendAnyProperty n)
        {
            n.Property.IndexType = HarborProperty.EnumIndexType.NotIndexed;
            return n;
        }

        public static ICanExtendAnyProperty IndexAllowDuplicates(this ICanExtendAnyProperty n)
        {
            n.Property.IndexType = HarborProperty.EnumIndexType.IndexAllowDuplicates;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsString(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.StringType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsInteger(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.IntegerType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsMoney(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.MoneyType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsDecimalNotMoney(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.DecimalType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsDate(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.DateType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsDateTime(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.UTCDateTimeType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsBinary(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.BinaryType;
            return n;
        }

        public static ICanExtendAnyProperty TypeIsFixedEnumerable(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.FixedEnumerable;
            return n;
        }
        
        public static ICanExtendAnyProperty TypeIsBoolean(this ICanExtendAnyProperty n)
        {
            n.Property.DataType = HarborProperty.EnumDataType.BooleanType;
            return n;
        }

        public static ICanExtendAnyProperty ValidateWithRegexAndIgnoreFalseMatch(this ICanExtendAnyProperty n, string regex)
        {
            n.Property.ValidateWithRegex = true;
            n.Property.BlockOnFalseRegexMatch = false;
            n.Property.Regex = regex;
            return n;
        }

        public static ICanExtendAnyProperty ValidateWithRegexAndBlockFalseMatch(this ICanExtendAnyProperty n, string regex)
        {
            n.Property.ValidateWithRegex = true;
            n.Property.BlockOnFalseRegexMatch = true;
            n.Property.Regex = regex;
            return n;
        }

        public static ICanExtendAnyProperty WheNullOmitValue(this ICanExtendAnyProperty n)
        {
            n.Property.AllowNull = HarborProperty.EnumAllowNull.AllowNullAndOmitStoreAsNull;
            return n;
        }

        public static ICanExtendAnyProperty WhenNullStoreAsNull(this ICanExtendAnyProperty n)
        {
            n.Property.AllowNull = HarborProperty.EnumAllowNull.AllowNullAndStoreAsNull;
            return n;
        }

        public static ICanExtendAnyProperty WhenNullBlockChange(this ICanExtendAnyProperty n)
        {
            n.Property.AllowNull = HarborProperty.EnumAllowNull.BlockNull;
            return n;
        }

        public static ICanExtendAnyProperty IsImmutable(this ICanExtendAnyProperty n)
        {
            n.Property.IsImmutable = true;
            return n;
        }

        public static ICanExtendAnyProperty IsNotImmutable(this ICanExtendAnyProperty n)
        {
            n.Property.IsImmutable = false;
            return n;
        }

        public static ICanExtendAnyProperty WhenErrorIgnore(this ICanExtendAnyProperty n)
        {
            n.Property.BlockOnModelError = false;
            return n;
        }

        public static ICanExtendAnyProperty WhenErrorBlockChange(this ICanExtendAnyProperty n)
        {
            n.Property.BlockOnModelError = true;
            return n;
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
