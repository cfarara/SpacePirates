using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    
    public class Life : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Life"/> class.
        /// </summary>
        public Life()
        {
           Sprite = new LivesSprite();
        }
    }
}
