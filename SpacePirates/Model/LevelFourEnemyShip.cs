using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    /// A LevelFourEnemyShip.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.LevelFourEnemyShip" />
    public class LevelFourEnemyShip : EnemyShip
    {
        #region Data Memebers

        /// <summary>
        /// The score of a LevelFourEnemyShip
        /// </summary>
        public static int Score = 400;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LevelFourEnemyShip" /> class.
        /// </summary>
        public LevelFourEnemyShip()
        {
            Sprite = new LevelFourEnemyShipSprite();
        }

        #endregion

        #region methods

        /// <summary>
        /// Animates this instance.
        /// </summary>
        public override void Animate()
        {
            LevelFourEnemyShipSprite levelFourEnemyShipSprite = (LevelFourEnemyShipSprite) Sprite;
            levelFourEnemyShipSprite.Animate();
        }

        #endregion
    }


}
