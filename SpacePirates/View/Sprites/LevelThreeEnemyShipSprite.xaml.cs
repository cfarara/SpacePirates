using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SpacePirates.View.Sprites
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LevelThreeEnemyShipSprite"/> class.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="SpacePirates.View.Sprites.ISpriteRenderer" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class LevelThreeEnemyShipSprite : UserControl, ISpriteRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelThreeEnemyShipSprite"/> class.
        /// </summary>
        public LevelThreeEnemyShipSprite()
        {
            this.InitializeComponent();
        }

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

        /// <summary>
        /// Animates this instance.
        /// </summary>
        public void Animate()
        {
            if (this.originalTopRectangle.Visibility == Visibility.Visible)
            {
                this.originalTopRectangle.Visibility = Visibility.Collapsed;
                this.animatedTopRectangle.Visibility = Visibility.Visible;
            }
            else if (this.animatedTopRectangle.Visibility == Visibility.Visible)
            {
                this.animatedTopRectangle.Visibility = Visibility.Collapsed;
                this.originalTopRectangle.Visibility = Visibility.Visible;
            }
        }
    }
}
