using System;
using System.Linq;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class QuerySearchExtensions
    {
        private static Expression<Func<T, bool>> BuildSearchExpression<T>(string search)
        {
            Expression baseExpression = null; // Expression.Constant(true);
            var parameter = Expression.Parameter(typeof(T), "x");

            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                Expression expression = null;

                if (property.GetGetMethod().IsVirtual)
                {
                    // ignore
                }
                else if (property.PropertyType.Equals(typeof(string)))
                {
                    Expression member = Expression.Property(parameter, property.Name);
                    member = Expression.Coalesce(member, Expression.Constant(""));

                    Type[] properties = new Type[0];
                    var methodToLower = typeof(string).GetMethod("ToLower", properties);
                    member = Expression.Call(member, methodToLower);

                    var methodContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var valueSearch = Expression.Constant(search.ToLower(), typeof(string));
                    expression = Expression.Call(member, methodContains, valueSearch);
                }
                else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Expression member = Expression.Property(parameter, property.Name);

                    var stringConvertMethod = typeof(Object).GetMethod("ToString");
                    member = Expression.Call(member, stringConvertMethod);

                    member = Expression.Coalesce(member, Expression.Constant(""));


                    var methodContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var valueSearch = Expression.Constant(search, typeof(string));
                    expression = Expression.Call(member, methodContains, valueSearch);
                }
                else
                {
                    Expression member = Expression.Property(parameter, property.Name);

                    var stringConvertMethod = typeof(Object).GetMethod("ToString");
                    member = Expression.Call(member, stringConvertMethod);

                    var methodContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var valueSearch = Expression.Constant(search, typeof(string));
                    expression = Expression.Call(member, methodContains, valueSearch);
                }

                if (expression != null)
                {
                    if (baseExpression == null)
                        baseExpression = expression;
                    else
                        baseExpression = Expression.Or(expression, baseExpression);
                }
            }

            return Expression.Lambda<Func<T, bool>>(baseExpression, parameter);
        }


        public static IQueryable<T> Search<T>(this IQueryable<T> queryable, string search)
        {
            if (string.IsNullOrEmpty(search))
                return queryable;

            var func = BuildSearchExpression<T>(search).Compile();
            return queryable.Where<T>(m => func(m));
        }

    }
}
