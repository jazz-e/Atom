using Microsoft.Xna.Framework.Input;

namespace Atom.Input
{
    public class FirstPersonKeyComponent : Component
    {
        /// <summary>
        /// The avaiable actions that can be triggered
        /// </summary>
        public enum FirstPersonActions
        {
            Up = 0,
            Down,
            Left, 
            Right, 
            Fire, 
            AltFire
        }

        /// <summary>
        /// The action to be initiated when Key is pressed
        /// </summary>
        public FirstPersonActions Action { get; set; }

        /// <summary>
        /// The key to trigger the action defined by Action
        /// </summary>
        public Keys Key { get; set; }
    }
}
