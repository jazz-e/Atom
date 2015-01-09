using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Components
{
    abstract class Component
    {
        public int EntityID
        {
            get;
            set;
        }

        public bool Disabled
        {
            get;
            set;
        }

    }
}
