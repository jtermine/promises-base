using System;
using System.Linq;
using System.Linq.Expressions;
using Termine.Promises.WithRedis.Enumerables;
using Termine.Promises.WithRedis.Harbor;
using Termine.Promises.WithRedis.Interfaces;

namespace Termine.Promises.WithRedis
{
    public static partial class Extensions
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
    }
}
