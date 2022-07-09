using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    /// A LevelThreeEnemyShip.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.LevelOneEnemyShip" />
    public class LevelThreeEnemyShip : EnemyShip
    {
        #region Data Memebers

        /// <summary>
        /// The score of a LevelThreeEnemyShip
        /// </summary>
        public static int Score = 300;

        /// <summary>
        /// X Coordinate center.
        /// </summary>
        public const double LevelThreeEnemyShipCenterX = 20;

        /// <summary>
        /// Y Coordinate center.
        /// </summary>
        public const double LevelThreeEnemyShipCenterY = 29;

        /// <summary>
        /// Placement of Level 3 enemy ship offset.
        /// </summary>
        public const int LevelThreeEnemyShipPlacementOffset = 25;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LevelThreeEnemyShip" /> class.
        /// </summary>
        public LevelThreeEnemyShip()
        {
            Sprite = new LevelThreeEnemyShipSprite();
        }

        #endregion

        #region methods

        /// <summary>
        /// Animates this instance.
        /// </summary>
        public override void Animate()
        {
            LevelThreeEnemyShipSprite levelThreeEnemyShipSprite = (LevelThreeEnemyShipSprite) Sprite;
            levelThreeEnemyShipSprite.Animate();
        }

        #endregion
    }


}
