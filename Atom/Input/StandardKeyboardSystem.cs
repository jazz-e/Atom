using System;
using System.Collections.Generic;
using Atom.Messaging;
using Atom.Physics.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Atom.Input
{
    public class StandardKeyboardSystem : BaseSystem
    {
        public StandardKeyboardSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(StandardKeyComponent));
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            List<StandardKeyComponent> keyComponents = GetComponentsByEntityId<StandardKeyComponent>(entityId);

            foreach (StandardKeyComponent keyComponent in keyComponents)
            {
                if (keyboardState.IsKeyUp(keyComponent.Key)) continue;

                StandardInputActions actions = keyComponent.Action;

                switch (actions)
                {
                    case StandardInputActions.Up:
                        PostOffice.SendMessage(new MoveMessage(entityId, MoveDirection.Up));
                        break;
                    case StandardInputActions.Down:
                        PostOffice.SendMessage(new MoveMessage(entityId, MoveDirection.Down));
                        break;
                    case StandardInputActions.Left:
                        PostOffice.SendMessage(new MoveMessage(entityId, MoveDirection.Left));
                        break;
                    case StandardInputActions.Right:
                        PostOffice.SendMessage(new MoveMessage(entityId, MoveDirection.Right));
                        break;
                    case StandardInputActions.Fire:
                        break;
                    case StandardInputActions.AltFire:
                        break;
                }
            }
        }
    }
}
