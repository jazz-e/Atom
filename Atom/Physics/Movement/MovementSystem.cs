using System;
using System.Collections.Generic;
using System.Linq;
using Atom.Messaging;
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
                    GetComponentByEntityId<VelocityComponent>(entityId);

            PositionComponent positionComponent =
                    GetComponentByEntityId<PositionComponent>(entityId);

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
                    GetComponentByEntityId<SpeedComponent>(moveMessage.GetEntityId());

                VelocityComponent velocityComponent =
                    GetComponentByEntityId<VelocityComponent>(moveMessage.GetEntityId());

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
