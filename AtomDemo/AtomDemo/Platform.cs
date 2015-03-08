using System.Collections.Generic;
using Atom;
using Atom.Entity;
using Atom.Physics;
using Atom.Physics.Collision.BoundingBox;

namespace AtomDemo
{
    public class Platform : BaseEntity
    {
        protected override List<Component> CreateDefaultComponents()
        {
            var components = new List<Component>
            {
                new PositionComponent() { X = 300f, Y = 300f},
                new VelocityComponent(),
                new AccelerationComponent(),
                new MassComponent() { Mass = 1 },

                new BoundingBoxComponent()
                {
                    Active = true,
                    Width = 100,
                    Height = 100,
                }

            };

            return components;
        }
    }
}
