using System.Collections.Generic;
using Atom;
using Atom.Entity;
using Atom.Graphics;
using Atom.Input;
using Atom.Physics;
using Microsoft.Xna.Framework.Input;

namespace AtomDemo
{
    public class PlayerEntity : BaseEntity
    {
        public override List<Component> CreateDefaultComponents()
        {
            List<Component> components = new List<Component>
            {
                new PositionComponent() { EntityId = Id },
                new VelocityComponent() { EntityId = Id },
                new SpeedComponent() { EntityId = Id, Speed = 10.0F },

                new AnimatedSpriteComponent() { EntityId = Id },

                new StandardKeyComponent() { 
                    EntityId = Id, 
                    Action = StandardInputActions.Up, 
                    Key = Keys.W
                },

                new StandardKeyComponent() { 
                    EntityId = Id, 
                    Action = StandardInputActions.Down, 
                    Key = Keys.S
                },

                new StandardKeyComponent() { 
                    EntityId = Id, 
                    Action = StandardInputActions.Left, 
                    Key = Keys.A
                },

                new StandardKeyComponent() { 
                    EntityId = Id, 
                    Action = StandardInputActions.Right, 
                    Key = Keys.D
                },

                new StandardKeyComponent() { 
                    EntityId = Id, 
                    Action = StandardInputActions.Fire, 
                    Key = Keys.Space
                },

                new StandardKeyComponent() { 
                    EntityId = Id, 
                    Action = StandardInputActions.AltFire, 
                    Key = Keys.Q
                }

            };

            return components;
        }
    }
}
