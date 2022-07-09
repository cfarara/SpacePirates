using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirates.Model
{
    /// <summary>
    /// An Enemy Ship.
    /// </summary>
    /// <seealso cref="SpacePirates.Model.EnemyShip" />
    public abstract class EnemyShip: GameObject
    {
        /// <summary>
        /// Animates this instance.
        /// </summary>
        public abstract void Animate();

    }
}
