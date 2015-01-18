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

                new FirstPersonKeyComponent() { 
                    EntityId = Id, 
                    Action = FirstPersonKeyComponent.FirstPersonActions.Up, 
                    Key = Keys.W
                },

                new FirstPersonKeyComponent() { 
                    EntityId = Id, 
                    Action = FirstPersonKeyComponent.FirstPersonActions.Down, 
                    Key = Keys.S
                },

                new FirstPersonKeyComponent() { 
                    EntityId = Id, 
                    Action = FirstPersonKeyComponent.FirstPersonActions.Left, 
                    Key = Keys.A
                },

                new FirstPersonKeyComponent() { 
                    EntityId = Id, 
                    Action = FirstPersonKeyComponent.FirstPersonActions.Right, 
                    Key = Keys.D
                },

                new FirstPersonKeyComponent() { 
                    EntityId = Id, 
                    Action = FirstPersonKeyComponent.FirstPersonActions.Fire, 
                    Key = Keys.Space
                },

                new FirstPersonKeyComponent() { 
                    EntityId = Id, 
                    Action = FirstPersonKeyComponent.FirstPersonActions.AltFire, 
                    Key = Keys.Q
                }

            };

            return components;
        }
    }
}
