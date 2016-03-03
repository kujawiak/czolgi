using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    public abstract class Actor
    {
        public List<IDrawable> DrawPool { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public Rectangle CurrentPosition;
        public Color CurrentColor; //TODO: Obsolote after texture implementaiton

        public bool IsOutOfBounds()
        {
            if (CurrentPosition.X < 0 || CurrentPosition.Y < 0)
                return true;
            //TODO: replace hardcoded values
            if (CurrentPosition.X > 100 || CurrentPosition.Y > 300)
                return true;
            return false;
        }

        public virtual void Draw()
        {
            if (null != this.SpriteBatch)
            {
                this.SpriteBatch.Draw(Texture, CurrentPosition, CurrentColor);
            }
        }
    }
}
