using System.Collections.Generic;
using Atom;
using Atom.Entity;
using Atom.Graphics;
using Atom.Input;
using Atom.Physics;

namespace AtomDemo
{
    public class PlayerEntity : Entity
    {
        public override List<Component> CreateCommonComponents()
        {
            List<Component> components = new List<Component>
            {
                new PositionComponent(),
                new VelocityComponent(),
                new AnimatedSpriteComponent(),
                new FirstPersonKeyComponent()
            };

            return components;
        }
    }
}
