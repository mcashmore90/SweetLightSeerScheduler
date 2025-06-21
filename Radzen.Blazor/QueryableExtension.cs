using Radzen;
using Radzen.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Radzen
{
    /// <summary>
    /// Class QueryableExtension.
    /// </summary>
    public static class QueryableExtension
    {
        static Expression notNullCheck(Expression property) => Nullable.GetUnderlyingType(property.Type) != null || property.Type == typeof(string) ?
            Expression.Coalesce(property, property.Type == typeof(string) ? Expression.Constant(string.Empty) : Expression.Constant(null, property.Type)) : property;

        internal static Expression GetNestedPropertyExpression(Expression expression, string property, Type type = null)
        {
            var parts = property.Split(new char[] { '.' }, 2);
            string currentPart = parts[0];
            Expression member;

            if (expression.Type.IsGenericType && typeof(IDictionary<,>).IsAssignableFrom(expression.Type.GetGenericTypeDefinition()) ||
                typeof(IDictionary).IsAssignableFrom(expression.Type))
            {
                var key = currentPart.Split('"')[1];
                var typeString = currentPart.Split('(')[0];

                var indexer = Expression.Property(expression, expression.Type.GetProperty("Item"), Expression.Constant(key));
                member = Expression.Convert(
                    indexer,
                    parts.Length > 1 ? indexer.Type : type ?? Type.GetType(typeString.EndsWith("?") ? $"System.Nullable`1[System.{typeString.TrimEnd('?')}]" : $"System.{typeString}") ?? typeof(object));
            }
            else if (currentPart.Contains("[")) // Handle array or list indexing
            {
                var indexStart = currentPart.IndexOf('[');
                var propertyName = currentPart.Substring(0, indexStart);
                var indexString = currentPart.Substring(indexStart + 1, currentPart.Length - indexStart - 2);

                member = Expression.PropertyOrField(expression, propertyName);
                if (int.TryParse(indexString, out int index))
                {
                    if (member.Type.IsArray)
                    {
                        member = Expression.ArrayIndex(member, Expression.Constant(index));
                    }
                    else if (member.Type.IsGenericType &&
                             (member.Type.GetGenericTypeDefinition() == typeof(List<>) ||
                              typeof(IList<>).IsAssignableFrom(member.Type.GetGenericTypeDefinition())))
                    {
                        var itemProperty = member.Type.GetProperty("Item");
                        if (itemProperty != null)
                        {
                            member = Expression.Property(member, itemProperty, Expression.Constant(index));
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid index format: {indexString}");
                }
            }
            else if (expression.Type.IsInterface)
            {
                member = Expression.Property(expression,
                    new[] { expression.Type }.Concat(expression.Type.GetInterfaces()).FirstOrDefault(t => t.GetProperty(currentPart) != null),
                    currentPart
                );
            }
            else
            {
                member = Expression.PropertyOrField(expression, currentPart);
            }

            if (expression.Type.IsValueType && Nullable.GetUnderlyingType(expression.Type) == null)
            {
                expression = Expression.Convert(expression, typeof(object));
            }

            return parts.Length > 1 ? GetNestedPropertyExpression(member, parts[1], type) :
                (Nullable.GetUnderlyingType(member.Type) != null || member.Type == typeof(string)) ?
                    Expression.Condition(Expression.Equal(expression, Expression.Constant(null)), Expression.Constant(null, member.Type), member) :
                    member;
        }

        /// <summary>
        /// Filters using the specified filter descriptors.
        /// </summary>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IEnumerable<FilterDescriptor> filters,
            LogicalFilterOperator logicalFilterOperator, FilterCaseSensitivity filterCaseSensitivity)
        {
            if (filters == null || !filters.Any())
                return source;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression combinedExpression = null;

            foreach (var filter in filters)
            {
                var expression = GetExpression<T>(parameter, filter, filterCaseSensitivity, filter.Type);
                if (expression == null) continue;

                combinedExpression = combinedExpression == null
                    ? expression
                    : logicalFilterOperator == LogicalFilterOperator.And ?
                        Expression.AndAlso(combinedExpression, expression) :
                            Expression.OrElse(combinedExpression, expression);
            }

            if (combinedExpression == null)
                return source;

            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

            return source.Where(lambda);
        }

        internal static Expression GetExpression<T>(ParameterExpression parameter, FilterDescriptor filter, FilterCaseSensitivity filterCaseSensitivity, Type type)
        {
            Type valueType = filter.FilterValue != null ? filter.FilterValue.GetType() : null;
            var isEnumerable = valueType != null && IsEnumerable(valueType) && valueType != typeof(string);

            Type secondValueType = filter.SecondFilterValue != null ? filter.SecondFilterValue.GetType() : null;

            Expression p = GetNestedPropertyExpression(parameter, filter.Property, type);

            Expression property = GetNestedPropertyExpression(parameter, !isEnumerable && !IsEnumerable(p.Type) ? filter.FilterProperty ?? filter.Property : filter.Property, type);

            Type collectionItemType = IsEnumerable(property.Type) && property.Type.IsGenericType ? property.Type.GetGenericArguments()[0] : null;

            ParameterExpression collectionItemTypeParameter = collectionItemType != null ? Expression.Parameter(collectionItemType, "x") : null;

            if (collectionItemType != null && !string.IsNullOrEmpty(filter.FilterProperty) && filter.Property != filter.FilterProperty)
            {
                property = GetNestedPropertyExpression(collectionItemTypeParameter, filter.FilterProperty);

                filter.FilterOperator = filter.FilterOperator == FilterOperator.In ? FilterOperator.Contains :
                    filter.FilterOperator == FilterOperator.NotIn ? FilterOperator.DoesNotContain : filter.FilterOperator;
            }

            var isEnum = !isEnumerable && (PropertyAccess.IsEnum(property.Type) || PropertyAccess.IsNullableEnum(property.Type));
            var caseInsensitive = property.Type == typeof(string) && !isEnumerable && filterCaseSensitivity == FilterCaseSensitivity.CaseInsensitive;

            var isEnumerableProperty = IsEnumerable(property.Type) && property.Type != typeof(string);

            var constant = Expression.Constant(caseInsensitive ?
                $"{filter.FilterValue}".ToLowerInvariant() :
                    isEnum && !isEnumerable && filter.FilterValue != null ? Enum.ToObject(Nullable.GetUnderlyingType(property.Type) ?? property.Type, filter.FilterValue) : filter.FilterValue,
                    !isEnum && isEnumerable ? valueType : isEnumerableProperty ? valueType : property.Type);

            if (caseInsensitive && !isEnumerable)
            {
                property = Expression.Call(notNullCheck(property), typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            }

            var secondConstant = filter.SecondFilterValue != null ?
                Expression.Constant(caseInsensitive ?
                $"{filter.SecondFilterValue}".ToLowerInvariant() :
                    isEnum && filter.SecondFilterValue != null ? Enum.ToObject(Nullable.GetUnderlyingType(property.Type) ?? property.Type, filter.SecondFilterValue) : filter.SecondFilterValue,
                    secondValueType != null && !isEnum && IsEnumerable(secondValueType) ? secondValueType : property.Type) : null;

            Expression primaryExpression = filter.FilterOperator switch
            {
                FilterOperator.Equals => Expression.Equal(notNullCheck(property), constant),
                FilterOperator.NotEquals => Expression.NotEqual(notNullCheck(property), constant),
                FilterOperator.LessThan => Expression.LessThan(notNullCheck(property), constant),
                FilterOperator.LessThanOrEquals => Expression.LessThanOrEqual(notNullCheck(property), constant),
                FilterOperator.GreaterThan => Expression.GreaterThan(notNullCheck(property), constant),
                FilterOperator.GreaterThanOrEquals => Expression.GreaterThanOrEqual(notNullCheck(property), constant),
                FilterOperator.Contains => isEnumerable ?
                    Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new Type[] { property.Type }, constant, notNullCheck(property)) :
                         isEnumerableProperty ?
                            Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new Type[] { collectionItemType }, notNullCheck(property), constant) :
                                Expression.Call(notNullCheck(property), typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
                FilterOperator.In => isEnumerable &&
                                    isEnumerableProperty ?
                    Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new Type[] { collectionItemType },
                        Expression.Call(typeof(Enumerable), nameof(Enumerable.Intersect), new Type[] { collectionItemType }, constant, notNullCheck(property))) : Expression.Constant(true),
                FilterOperator.DoesNotContain => isEnumerable ?
                    Expression.Not(Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new Type[] { property.Type }, constant, notNullCheck(property))) :
                        isEnumerableProperty ?
                            Expression.Not(Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new Type[] { collectionItemType }, notNullCheck(property), constant)) :
                                Expression.Not(Expression.Call(notNullCheck(property), typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant)),
                FilterOperator.NotIn => isEnumerable &&
                                    isEnumerableProperty ?
                    Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new Type[] { collectionItemType },
                        Expression.Call(typeof(Enumerable), nameof(Enumerable.Except), new Type[] { collectionItemType }, constant, notNullCheck(property))) : Expression.Constant(true),
                FilterOperator.StartsWith => Expression.Call(notNullCheck(property), typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant),
                FilterOperator.EndsWith => Expression.Call(notNullCheck(property), typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant),
                FilterOperator.IsNull => Expression.Equal(property, Expression.Constant(null, property.Type)),
                FilterOperator.IsNotNull => Expression.NotEqual(property, Expression.Constant(null, property.Type)),
                FilterOperator.IsEmpty => Expression.Equal(property, Expression.Constant(String.Empty)),
                FilterOperator.IsNotEmpty => Expression.NotEqual(property, Expression.Constant(String.Empty)),
                _ => null
            };

            if (collectionItemType != null && primaryExpression != null &&
                !(filter.FilterOperator == FilterOperator.In || filter.FilterOperator == FilterOperator.NotIn))
            {
                primaryExpression = Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new Type[] { collectionItemType },
                    GetNestedPropertyExpression(parameter, filter.Property), Expression.Lambda(primaryExpression, collectionItemTypeParameter));
            }

            Expression secondExpression = null;
            if (secondConstant != null)
            {
                secondExpression = filter.SecondFilterOperator switch
                {
                    FilterOperator.Equals => Expression.Equal(notNullCheck(property), secondConstant),
                    FilterOperator.NotEquals => Expression.NotEqual(notNullCheck(property), secondConstant),
                    FilterOperator.LessThan => Expression.LessThan(notNullCheck(property), secondConstant),
                    FilterOperator.LessThanOrEquals => Expression.LessThanOrEqual(notNullCheck(property), secondConstant),
                    FilterOperator.GreaterThan => Expression.GreaterThan(notNullCheck(property), secondConstant),
                    FilterOperator.GreaterThanOrEquals => Expression.GreaterThanOrEqual(notNullCheck(property), secondConstant),
                    FilterOperator.Contains => Expression.Call(notNullCheck(property), typeof(string).GetMethod("Contains", new[] { typeof(string) }), secondConstant),
                    FilterOperator.DoesNotContain => Expression.Not(Expression.Call(notNullCheck(property), property.Type.GetMethod("Contains", new[] { typeof(string) }), secondConstant)),
                    FilterOperator.StartsWith => Expression.Call(notNullCheck(property), typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), secondConstant),
                    FilterOperator.EndsWith => Expression.Call(notNullCheck(property), typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), secondConstant),
                    FilterOperator.IsNull => Expression.Equal(property, Expression.Constant(null, property.Type)),
                    FilterOperator.IsNotNull => Expression.NotEqual(property, Expression.Constant(null, property.Type)),
                    FilterOperator.IsEmpty => Expression.Equal(property, Expression.Constant(String.Empty)),
                    FilterOperator.IsNotEmpty => Expression.NotEqual(property, Expression.Constant(String.Empty)),
                    _ => null
                };
            }

            if (collectionItemType != null && secondExpression != null &&
                !(filter.SecondFilterOperator == FilterOperator.In || filter.SecondFilterOperator == FilterOperator.NotIn))
            {
                secondExpression = Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new Type[] { collectionItemType },
                    GetNestedPropertyExpression(parameter, filter.Property), Expression.Lambda(secondExpression, collectionItemTypeParameter));
            }

            if (primaryExpression != null && secondExpression != null)
            {
                return filter.LogicalFilterOperator switch
                {
                    LogicalFilterOperator.And => Expression.AndAlso(primaryExpression, secondExpression),
                    LogicalFilterOperator.Or => Expression.OrElse(primaryExpression, secondExpression),
                    _ => primaryExpression
                };
            }

            return primaryExpression;
        }

        /// <summary>
        /// Gets if type is IEnumerable.
        /// </summary>
        public static bool IsEnumerable(Type type)
        {
            return (typeof(IEnumerable).IsAssignableFrom(type) || typeof(IEnumerable<>).IsAssignableFrom(type)) && type != typeof(string);
        }
    }
}