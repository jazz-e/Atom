using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Atom;
using Atom.Physics;

namespace Atom.Input
{
    public abstract class InputComponent
    {
        public PhysicsComponent Physics;

        public InputComponent(PhysicsComponent physics)
        {
            Physics = physics;
        }
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
