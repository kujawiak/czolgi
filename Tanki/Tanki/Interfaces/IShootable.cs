using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    interface IShootable
    {
        int LastShot { get; set; }
        int ReloadTime { get; set; }

        void Shoot(GameTime gameTime);
        bool ReadyToShoot(GameTime gameTime);
    }
}
