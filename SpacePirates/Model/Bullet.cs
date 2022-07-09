using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirates.View.Sprites;

namespace SpacePirates.Model
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Bullet" /> class.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.Bullet" />
    public class Bullet : GameObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public Bullet()
        {
            Sprite = new BulletSprite();
        }

        #endregion

    }
}
