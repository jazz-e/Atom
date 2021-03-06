﻿using System.Collections.Generic;

namespace Atom.Entity
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identifier given to each entity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Creates all the components needed for the entity and returns them in a list
        /// </summary>
        /// <returns>List of all the components that the entity uses</returns>
        public virtual List<Component> CreateDefaultComponents()
        {
            return new List<Component>();
        }
    }
}
