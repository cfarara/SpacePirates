using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    /// A GameOverBlock.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.GameObject" />
    public class GameOverBlock: GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverBlock"/> class.
        /// </summary>
        public GameOverBlock()
        {
            Sprite = new GameOverSprite();
        }

        /// <summary>
        /// Sets the text to visible.
        /// </summary>
        public void SetText(string text)
        {
            GameOverSprite gameOverSprite = (GameOverSprite) Sprite;
            gameOverSprite?.SetText(text);
        }
    }
}
