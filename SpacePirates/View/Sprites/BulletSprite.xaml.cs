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
    /// Creates a BulletSprite class for use on the canvas.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="SpacePirates.View.Sprites.ISpriteRenderer" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class BulletSprite : UserControl, ISpriteRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulletSprite"/> class.
        /// </summary>
        public BulletSprite()
        {
            this.InitializeComponent();
        }

        #region Methods

        /// <summary>
        ///     Renders the bullet at the specified location.
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
