﻿using System.Drawing;
using Microsoft.Xna.Framework.Content;

namespace Atom
{
    public abstract class Component
    {
        /// <summary>
        /// The id of the entity that the component is related to
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Flag to set whether the system should use the component
        /// </summary>
        public bool Disabled { get; set; }

        public ContentManager Content
        {
            get { return GameServices.GetService<ContentManager>(); }
        }

    }
}
