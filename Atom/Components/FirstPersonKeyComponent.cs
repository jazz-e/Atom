using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Atom.Components
{
    public class FirstPersonKeyComponent
    {
        public enum FirstPersonActions
        {
            Up = 0,
            Down, Left, Right, Fire, Alt_Fire
        }

        public FirstPersonActions Action
        {
            get;
            set;
        }
        public Keys Key
        {
            get;
            set;
        }
    }
}
