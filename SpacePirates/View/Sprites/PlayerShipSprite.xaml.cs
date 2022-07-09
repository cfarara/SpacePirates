﻿using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SpacePirates.View.Sprites
{
    /// <summary>
    ///     Draws a player ship.
    ///     Must implemente the ISpriteRenderer so will be displayed as a game object.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="SpacePirates.View.Sprites.ISpriteRenderer" />
    public sealed partial class PlayerShipSprite : UserControl, ISpriteRenderer
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShipSprite" /> class.
        ///     Precondition: none
        ///     Postconditon: Sprite created.
        /// </summary>
        public PlayerShipSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Renders the sprite at the specified location.
        ///     Precondition: none
        ///     Postcondition: sprite drawn at location (x,y)
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void RenderAt(double x, double y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        #endregion
    }
}