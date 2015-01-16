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

        public TypeFilter AddFilter(Type type)
        {
            _filters.Add(type);

            return this;
        }

        public bool Contains(Type type)
        {
            return _filters.FindAll(filterType => filterType.FullName == type.FullName).Count > 0;
        }
    }
}
