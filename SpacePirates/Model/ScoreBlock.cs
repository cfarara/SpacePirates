using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    /// A ScoreBlock.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.EnemyShip" />
    public class ScoreBlock: GameObject
    {
        #region Data Memebers

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBlock"/> class.
        /// </summary>
        public ScoreBlock ()
        {
            Sprite = new ScoreBlockSprite();
        }

        /// <summary>
        /// Sets the score text of the ScoreBlock.
        /// </summary>
        /// <param name="score">The score.</param>
        public void SetText(string score)
        {
            ScoreBlockSprite sprite = (ScoreBlockSprite) Sprite;
            sprite?.SetText(score);
        }
    }
}
