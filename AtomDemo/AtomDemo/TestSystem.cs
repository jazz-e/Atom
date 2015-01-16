using System;
using System.Collections.Generic;
using Atom.Messaging;
using Microsoft.Xna.Framework;

namespace AtomDemo
{
    public class TestSystem : Atom.System
    {
        public TestSystem()
        {
            PostOffice.GetInstance().Subscribe(new List<Type>());
        }
    }
}
