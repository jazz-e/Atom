using System;
using System.Collections.Generic;
using System.Linq;
using Atom.Messaging;
using Atom.Rendering.Static;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Movement
{
    public class MovementSystem : BaseSystem, IReceiver
    {
        public int Id { get; set; }

        public MovementSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof (PositionComponent))
                .AddFilter(typeof (VelocityComponent))
                .AddFilter(typeof (SpeedComponent));

            PostOffice.Subscribe(this);
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            VelocityComponent velocityComponent =
                    GetComponentsByEntityId<VelocityComponent>(entityId).FirstOrDefault();

            PositionComponent positionComponent =
                    GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();

            if (velocityComponent == null || positionComponent == null) return;

            positionComponent.X += velocityComponent.Velocity.X;
            positionComponent.Y += velocityComponent.Velocity.Y;

            velocityComponent.Velocity = Vector2.Zero;
        }
        
        public void OnMessage(IMessage message)
        {
            if (message.GetType() == typeof (MoveMessage))
            {
                MoveMessage moveMessage = (MoveMessage) message;

                MoveDirection direction = moveMessage.GetMoveDirection();

                SpeedComponent speedComponent = 
                    GetComponentsByEntityId<SpeedComponent>(moveMessage.GetEntityId()).FirstOrDefault();

                VelocityComponent velocityComponent =
                    GetComponentsByEntityId<VelocityComponent>(moveMessage.GetEntityId()).FirstOrDefault();

                if (velocityComponent == null || speedComponent == null) return;

                switch (direction)
                {
                    case MoveDirection.Up:
                        velocityComponent.Velocity += new Vector2(0, -speedComponent.Speed);
                        break;
                    case MoveDirection.Down:
                        velocityComponent.Velocity += new Vector2(0, speedComponent.Speed);
                        break;
                    case MoveDirection.Left:
                        velocityComponent.Velocity += new Vector2(-speedComponent.Speed, 0);
                        break;
                    case MoveDirection.Right:
                        velocityComponent.Velocity += new Vector2(speedComponent.Speed, 0);
                        break;
                }
            }
        }

        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof(MoveMessage));
        }
    }
}
