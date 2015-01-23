using System.Linq;
using Atom.Graphics;
using Atom.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.Rendering.Animated
{
    public class AnimatedRenderSystem : BaseSystem
    {
        public AnimatedRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(AnimatedSpriteComponent))
                .AddFilter((typeof(PositionComponent)));
        }

        public override void Draw(SpriteBatch spriteBatch, int entityId)
        {
            AnimatedSpriteComponent spriteComponent =
                GetComponentsByEntityId<AnimatedSpriteComponent>(entityId).FirstOrDefault();

            PositionComponent positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();

            if (spriteComponent == null || positionComponent == null) return;

            spriteBatch.Draw(spriteComponent.Image, new Vector2(positionComponent.X, positionComponent.Y),
                spriteComponent.SourceRectangle, Color.White, 0f, Vector2.Zero,
                spriteComponent.Scale, SpriteEffects.None, 0f);

/*            spriteBatch.Draw(spriteComponent.Image, new Vector2(positionComponent.X, positionComponent.Y), 
                spriteComponent.SourceRectangle, Color.White, spriteComponent.Rotation, spriteComponent.Origin,
                spriteComponent.Scale, SpriteEffects.None, spriteComponent.LayerDepth);
 */
        }
    }
}
