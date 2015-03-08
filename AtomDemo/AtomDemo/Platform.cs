using System.Collections.Generic;
using Atom;
using Atom.Entity;
using Atom.Graphics.Rendering;
using Atom.Physics;
using Atom.Physics.Collision.BoundingBox;
using Microsoft.Xna.Framework.Graphics;

namespace AtomDemo
{
    public class Platform : BaseEntity
    {
        protected override List<Component> CreateDefaultComponents()
        {
            var components = new List<Component>
            {
                new PositionComponent() { X = 300f, Y = 400f},
                new VelocityComponent(),
                new AccelerationComponent(),
                new MassComponent() { Mass = 1 },

                new BoundingBoxComponent()
                {
                    Active = true,
                    Width = 100,
                    Height = 100,
                },

                new SpriteComponent()
                {
                    Image = Content.Load<Texture2D>("metal_texture"),
                    FrameHeight = 20,
                    FrameWidth = 100
                }

            };

            return components;
        }
    }
}
