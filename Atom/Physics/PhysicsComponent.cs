using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atom.Physics
{
    public abstract class PhysicsComponent
    {
        public virtual void Update(GameTime gameTime){}
    }
}
