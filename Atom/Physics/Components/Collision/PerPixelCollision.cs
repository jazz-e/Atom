
using System;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Components.Collision
{
    public class PerPixelCollision
    {
        public bool CollisionCheck(IPerPixelCollidable firstColliable, IPerPixelCollidable secondCollidable)
        {
            int top = Math.Max(firstColliable.BoundingBox.Top, secondCollidable.BoundingBox.Top);
            int bottom = Math.Max(firstColliable.BoundingBox.Bottom, secondCollidable.BoundingBox.Bottom);
            int right = Math.Max(firstColliable.BoundingBox.Right, secondCollidable.BoundingBox.Right);
            int left = Math.Max(firstColliable.BoundingBox.Left, secondCollidable.BoundingBox.Left);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color firstColor = firstColliable.SpriteColors[
                        (x - firstColliable.BoundingBox.Left) +
                        (y - firstColliable.BoundingBox.Top)*firstColliable.BoundingBox.Width
                        ];
                    Color secondColor = secondCollidable.SpriteColors[
                        (x - secondCollidable.BoundingBox.Left) +
                        (y - secondCollidable.BoundingBox.Top)*secondCollidable.BoundingBox.Width
                        ];

                    if (firstColor.A != 0 && secondColor.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
