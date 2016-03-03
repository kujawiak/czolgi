using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Tanki
{
    public interface IDrawable
    {
        List<IDrawable> DrawPool { get; set; }
        SpriteBatch SpriteBatch { get; set; }
        Texture2D Texture { get; set; }

        void Draw();
    }
}
