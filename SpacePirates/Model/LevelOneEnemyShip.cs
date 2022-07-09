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
    /// A level one enemy ship.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.EnemyShip" />
    public class LevelOneEnemyShip : EnemyShip
    {
        #region Data Memebers

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LevelOneEnemyShip" /> class.
        /// </summary>
        public LevelOneEnemyShip()
        {
            Sprite = new LevelOneEnemyShipSprite();
        }

        /// <summary>
        /// Animates this instance.
        /// </summary>
        public override void Animate()
        {

        }

        #endregion

    }
}
