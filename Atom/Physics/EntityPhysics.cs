using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.World;
using Microsoft.Xna.Framework;
using Atom.Graphics;
using Atom.Physics.Components;

namespace Atom.Physics
{
    public class EntityPhysics : PhysicsComponent
    {
        protected MovementPhysics MovementPhysics;
        protected GravityPhysics GravityPhysics;
        protected CollisionPhysics CollisionPhysics;

        public Vector2 Velocity 
        {
            get 
            {
                return new Vector2(MovementPhysics.Velocity.X + GravityPhysics.Velocity.X,
                    MovementPhysics.Velocity.Y + GravityPhysics.Velocity.Y);
            }
        }

        public EntityPhysics(MovementPhysics movement, GravityPhysics gravity, CollisionPhysics collision)
        {
            MovementPhysics = movement;
            GravityPhysics = gravity;
            CollisionPhysics = collision;
        }

        public void Update(GameTime gameTime, GameObject gameObject)
        {
            MovementPhysics.Update(gameTime, gameObject);

            GravityPhysics.Update(gameTime, gameObject);

            CollisionPhysics.Update(gameTime, gameObject, Velocity);

            base.Update(gameTime);
        }

    }
}
