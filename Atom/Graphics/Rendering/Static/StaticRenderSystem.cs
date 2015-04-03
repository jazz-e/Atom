using System.Linq;
using Atom.Graphics;
using Atom.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.Graphics.Rendering.Static
{
    public class StaticRenderSystem : BaseSystem
    {
        public int Id { get; set; }
        SpriteComponent spriteComponent;
        PositionComponent positionComponent;
        Rectangle spriteRectangle;

        public StaticRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(SpriteComponent))
                .AddFilter(typeof(PositionComponent));
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            GetComponents(entityId);
            if (spriteComponent == null || positionComponent == null) return;
            
            base.Update(gameTime, entityId);
        }

        public void GetComponents(int entityId)
        {
            positionComponent =
                  GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();
            spriteComponent =
                GetComponentsByEntityId<SpriteComponent>(entityId).FirstOrDefault();

            if (positionComponent == null || spriteComponent == null) return;

            spriteRectangle =
                new Rectangle(spriteComponent.Location.X, spriteComponent.Location.Y,
                    spriteComponent.FrameWidth, spriteComponent.FrameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch, int entityId)
        {
            GetComponents(entityId);
            if (spriteComponent == null || positionComponent == null) return;

            spriteBatch.Draw(spriteComponent.Image, positionComponent.Position, spriteRectangle, Color.White, 0,
                Vector2.Zero, spriteComponent.Scale, SpriteEffects.None, 0);
        }
    }
}
