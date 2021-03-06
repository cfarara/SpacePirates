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
    /// A ScoreBlockSprite.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// <seealso cref="SpacePirates.View.Sprites.ISpriteRenderer" />
    public sealed partial class ScoreBlockSprite : UserControl, ISpriteRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBlockSprite"/> class.
        /// </summary>
        public ScoreBlockSprite()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Renders the sprite at the specified location.
        /// Precondition: none
        /// Postcondition: sprite drawn at location (x,y)
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void RenderAt(double x, double y)
        {
          Canvas.SetLeft(this, x);
          Canvas.SetTop(this, y);
        }

        /// <summary>
        /// Sets the score text of the ScoreBlockSprite.
        /// </summary>
        /// <param name="text">The score.</param>
        public void SetText(string text)
        {
            this.ScoreBlock.Text = text;
        }
    }
}
