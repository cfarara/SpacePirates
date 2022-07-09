using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    /// A LevelTwoeEnemyShip.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.LevelOneEnemyShip" />
    public class LevelTwoEnemyShip : EnemyShip
    {
        #region Data Memebers

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LevelTwoEnemyShip" /> class.
        /// </summary>
        public LevelTwoEnemyShip()
        {
            Sprite = new LevelTwoEnemyShipSprite();
        }

        /// <summary>
        /// Animates this instance.
        /// </summary>
        public override void Animate()
        {
            LevelTwoEnemyShipSprite levelTwoEnemyShipSprite = (LevelTwoEnemyShipSprite)Sprite;
            levelTwoEnemyShipSprite.Animate();
        }

        #endregion
    }
}
