using System;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    ///     Manages the player ship.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.EnemyShip" />
    public class PlayerShip : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 7;
        private const int SpeedYDirection = 0;
        
        /// <summary>
        /// Bottom offset to place player ship on canvas.
        /// </summary>
        public const double PlayerShipBottomOffset = 30;

        /// <summary>
        /// X Coordinate offet to place player ship on canvas.
        /// </summary>
        public const double PlayerShipTipXCoordinateOffset = 22;

        /// <summary>
        /// Y Coordinate offset to place player ship on canvas.
        /// </summary>
        public const double PlayerShipTipYCoordinateOffset = 15;
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// Precondition: none
        /// Postcondition: creates a new player ship
        /// </summary>
        public PlayerShip()
        {
            Sprite = new PlayerShipSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
        #endregion

    }
}