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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpacePirates.View.Sprites
{
    public sealed partial class LivesSprite : UserControl, ISpriteRenderer
    {
        public LivesSprite()
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
    }
}
