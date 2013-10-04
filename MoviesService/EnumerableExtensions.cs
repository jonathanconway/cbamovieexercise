using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoviesService
{
    public static class EnumerableExtensions
    {
        private static PropertyInfo GetProperty<T>(string name)
        {
            return typeof(T).GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Like .Where() but allows you to supply the property name as a string.
        /// </summary>
        public static IEnumerable<T> WhereField<T>(this IEnumerable<T> enumerable, string field, Func<string, bool> action)
        {
            var property = GetProperty<T>(field);
            if (property != null)
                return enumerable.Where(element => action.Invoke(property.GetValue(element, null).ToString()));
            return enumerable;
        }

        /// <summary>
        /// Like .OrderBy() but allows you to supply the property name as a string.
        /// </summary>
        public static IEnumerable<T> OrderByField<T>(this IEnumerable<T> enumerable, string field)
        {
            var property = GetProperty<T>(field);
            if (property != null && !property.PropertyType.IsArray)
                return enumerable.OrderBy(element => property.GetValue(element, null));
            return enumerable;
        }
    }
}