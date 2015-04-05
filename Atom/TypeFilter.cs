using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom
{
    public class TypeFilter
    {
        private List<Type> _filters = new List<Type>();

        public TypeFilter()
        {

        }

        public TypeFilter(List<Type> filters)
        {
            _filters = filters;
        }

        /// <summary>
        /// Adds the type to the filter list.
        /// This method can be chained. E.g. AddFilter(NewType).AddFilter(NewType2).AddFilter(NewType3);
        /// </summary>
        /// <param name="type">The type that is to be added to the filter</param>
        /// <returns>Returns the TypeFilter to allow the chaining of the AddFilter</returns>
        public TypeFilter AddFilter(Type type)
        {
            _filters.Add(type);

            return this;
        }

        /// <summary>
        /// Checks to see if the type given is in the filter
        /// </summary>
        /// <param name="type">The type to check in the filter</param>
        /// <returns></returns>
        public bool Contains(Type type)
        {
            return _filters.Count(filterType => filterType == type) > 0;
        }

        /// <summary>
        /// Checks to see if the given list has one of each of the filters
        /// </summary>
        /// <param name="types">The types to check</param>
        /// <returns></returns>
        public bool ContainsAll(List<Type> types)
        {
            int matches = 0;

            foreach (Type filter in _filters)
            {
                if (types.Exists(type => type == filter))
                {
                    matches++;
                }
            }

            return matches == _filters.Count;
        }

        /// <summary>
        /// Returns objects that only match the filters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">The objects you want to filter</param>
        /// <returns></returns>
        public List<T> FilterList<T>(IEnumerable<T> objects)
        {
            List<T> filteredList = objects.Where(obj => Contains(obj.GetType())).ToList();

            return filteredList;
        }

        public List<T> ExcludeList<T>(IEnumerable<T> objects)
        {
            List<T> filteredList = objects.Where(obj => !Contains(obj.GetType())).ToList();

            return filteredList;
        }
    }
}
