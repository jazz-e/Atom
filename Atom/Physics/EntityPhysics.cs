using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Atom.Graphics;
using Atom.Physics.Components;

namespace Atom.Physics
{
    public class EntityPhysics : PhysicsComponent
    {
        protected MovementPhysics _movementPhysics;
        protected GravityPhysics _gravityPhysics;
        protected CollisionPhysics _collisionPhysics;

        public Vector2 Velocity 
        {
            get 
            {
                return new Vector2(_movementPhysics.Velocity.X + _gravityPhysics.Velocity.X,
                    _movementPhysics.Velocity.Y + _gravityPhysics.Velocity.Y);
            }
        }

        public EntityPhysics(MovementPhysics movement, GravityPhysics gravity, CollisionPhysics collision)
        {
            _movementPhysics = movement;
            _gravityPhysics = gravity;
            _collisionPhysics = collision;
        }

        public override void Update(GameTime gameTime, GameObject gameObject)
        {
            _movementPhysics.Update(gameTime, gameObject);

            _gravityPhysics.Update(gameTime, gameObject);

            _collisionPhysics.Update(gameTime, gameObject, Velocity);

            base.Update(gameTime);
        }

    }
}
