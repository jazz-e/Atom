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
            return _filters.FindAll(filterType => filterType.FullName == type.FullName).Count > 0;
        }
    }
}
