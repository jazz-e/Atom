using Microsoft.Xna.Framework.Input;

namespace Atom.Input
{
    public class StandardKeyComponent : Component
    {
        /// <summary>
        /// The action to be initiated when Key is pressed
        /// </summary>
        public StandardInputActions Action { get; set; }

        /// <summary>
        /// The key to trigger the action defined by Action
        /// </summary>
        public Keys Key { get; set; }
    }
}
