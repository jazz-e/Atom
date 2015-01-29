using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.Graphics;
using Atom.Physics;

namespace Atom.Rendering.Animated
{
    class AnimatedRenderSystem : BaseSystem
    {
         public AnimatedRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(AnimatedSpriteComponent))
                .AddFilter((typeof(PositionComponent)));
        }
    }
}
