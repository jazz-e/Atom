using System.Linq;
using Atom.Graphics;
using Atom.Messaging;
using Atom.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.Rendering.Static
{
    public class StaticRenderSystem : BaseSystem
    {
        public int Id { get; set; }

        public StaticRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(SpriteComponent))
                .AddFilter((typeof(PositionComponent)));
        }

        public override void Draw(SpriteBatch spriteBatch, int entityId)
        {
            SpriteComponent spriteComponent = 
                GetComponentsByEntityId<SpriteComponent>(entityId).FirstOrDefault();

            PositionComponent positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();

            if (spriteComponent == null || positionComponent == null) return;

            Rectangle spriteRectangle = 
                new Rectangle((int)positionComponent.X, (int)positionComponent.Y, 
                    spriteComponent.FrameWidth, spriteComponent.FrameHeight);
 
            spriteBatch.Draw(spriteComponent.Image, spriteRectangle, Color.White);
        }
    }
}
