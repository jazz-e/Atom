using System;
using System.Collections.Generic;
using System.Linq;
using Atom.Messaging;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Movement
{
    public class MovementSystem : BaseSystem, IReceiver
    {
        private VelocityComponent _velocityComponent;
        private PositionComponent _positionComponent;
        private MassComponent _massComponent;
        private AccelerationComponent _accelerationComponent;


        public MovementSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof (PositionComponent))
                .AddFilter(typeof (VelocityComponent))
                .AddFilter(typeof (AccelerationComponent))
                .AddFilter(typeof (MassComponent));

            PostOffice.Subscribe(this);
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            GetComponents(entityId);

            if (_velocityComponent == null || _positionComponent == null || _accelerationComponent == null || _massComponent == null) return;

            _accelerationComponent.PreviousAcceleration = _accelerationComponent.Acceleration;
            _velocityComponent.PreviousVelocity = _velocityComponent.Velocity;

            _positionComponent.Position +=
                (_velocityComponent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds
                + (0.5F * _accelerationComponent.Acceleration * ((int)gameTime.ElapsedGameTime.TotalSeconds ^ 2)));

            Vector2 newAcceleration = _massComponent.Force / _massComponent.Mass;
            Vector2 averageAcceleration = ( _accelerationComponent.PreviousAcceleration + newAcceleration ) / 2;
            _velocityComponent.Velocity += averageAcceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _accelerationComponent.Acceleration = newAcceleration;

            _massComponent.Force = Vector2.Zero;
        }
        
        public void OnMessage(IMessage message)
        {
            if (message.GetType() == typeof (MoveMessage))
            {
                MoveMessage moveMessage = (MoveMessage) message;

                GetComponents(moveMessage.GetEntityId());

                if (_massComponent == null) return;

                _massComponent.Force += moveMessage.GetForce();
            }
        }

        public void GetComponents(int entityId)
        {
            _velocityComponent =
                GetComponentsByEntityId<VelocityComponent>(entityId).FirstOrDefault();

            _positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();

            _massComponent =
                GetComponentsByEntityId<MassComponent>(entityId).FirstOrDefault();

            _accelerationComponent =
                GetComponentsByEntityId<AccelerationComponent>(entityId).FirstOrDefault();
        }

        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof(MoveMessage));
        }
    }
}
