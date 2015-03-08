using System.Collections.Generic;
using System.Net.Mime;
using Atom;
using Atom.Entity;
using Atom.Graphics.Rendering;
using Atom.Input;
using Atom.Physics;
using Atom.Physics.Collision.BoundingBox;
using Atom.Physics.Gravity;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AtomDemo
{
    public class PlayerEntity : BaseEntity
    {
        protected override List<Component> CreateDefaultComponents()
        {
            List<Component> components = new List<Component>
            {
                new PositionComponent() { X = 100f, Y = 0f},
                new VelocityComponent(),
                new AccelerationComponent(),
                new MassComponent() { Mass = 1 },

                new StandardKeyComponent() { 
                    Action = StandardInputActions.Up, 
                    Key = Keys.W
                },

                new StandardKeyComponent() { 
                    Action = StandardInputActions.Down, 
                    Key = Keys.S
                },

                new StandardKeyComponent() { 
                    Action = StandardInputActions.Left, 
                    Key = Keys.A
                },

                new StandardKeyComponent() { 
                    Action = StandardInputActions.Right, 
                    Key = Keys.D
                },

                new StandardKeyComponent() { 
                    Action = StandardInputActions.Fire, 
                    Key = Keys.Space
                },

                new StandardKeyComponent() {  
                    Action = StandardInputActions.AltFire, 
                    Key = Keys.Q
                },

                new BoundingBoxComponent()
                {
                    Active = true,
                    Width = 512,
                    Height = 256,
                },

                new AnimatedSpriteComponent()
                {
                    Image = Content.Load<Texture2D>("runningcat"),
                    FrameWidth = 512, 
                    FrameHeight = 256, 
                    FrameCount = 7, 
                    FramesPerSecond = 16, 
                    SequenceStartFrame = 0
                },

                new AnimatedSequenceComponent()
                {
                    AnimationSequence = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 },
                    CurrentSequenceDirection = SequenceDirection.Backward,
                },

                new GravityComponent()
                {
                    Gravity = 5F
                }
            };

            return components;
        }
    }
}
