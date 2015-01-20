using System.Linq;
using Atom.Graphics;
using Atom.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.Rendering.Static
{
    public class StaticRenderSystem : BaseSystem, IReceiver
    {
        public int Id { get; set; }

        public StaticRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(SpriteComponent));

            PostOffice.Subscribe(this);
        }

        public override void Draw(SpriteBatch spriteBatch, int entityId)
        {
            SpriteComponent spriteComponent = 
                GetComponentsByEntityId<SpriteComponent>(entityId).FirstOrDefault();

            if (spriteComponent == null) return;

            Rectangle spriteRectangle = 
                new Rectangle(spriteComponent.Location.X, spriteComponent.Location.Y, 
                    spriteComponent.FrameWidth, spriteComponent.FrameHeight);
 
            spriteBatch.Draw(spriteComponent.Image, spriteRectangle, Color.White);
        }

        public void OnMessage(IMessage message)
        {
            if (message.GetType() == typeof(PositionMessage))
            {
                PositionMessage positionMessage = (PositionMessage) message;

                Point position = positionMessage.GetPostion();

                SpriteComponent spriteComponent = 
                    GetComponentsByEntityId<SpriteComponent>(positionMessage.GetEntityId()).FirstOrDefault();

                if (spriteComponent == null) return;

                spriteComponent.Location = position;
            }
        }

        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof(PositionMessage));
        }
    }
}
