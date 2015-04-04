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

                const float upForce = 20F;
                const float sideForce = 3F;

                switch (actions)
                {
                    case StandardInputActions.Up:
                        PostOffice.SendMessage(new MoveMessage(entityId, new Vector2(0, -upForce)));
                        break;
                    case StandardInputActions.Down:
                        PostOffice.SendMessage(new MoveMessage(entityId, new Vector2(0, upForce)));
                        break;
                    case StandardInputActions.Left:
                        PostOffice.SendMessage(new MoveMessage(entityId, new Vector2(-sideForce, 0)));
                        break;
                    case StandardInputActions.Right:
                        PostOffice.SendMessage(new MoveMessage(entityId, new Vector2(sideForce, 0)));
                        break;
                }
            }
        }
    }
}
