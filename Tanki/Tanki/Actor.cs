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
        public ActorType Type { get; set; }
        public List<Actor> DrawPool { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public bool ToRemove { get; set; }

        public Rectangle CurrentPosition;
        public Color CurrentColor; //TODO: Obsolote after texture implementaiton

        public Actor()
        {
            Type = ActorType.Unset;
            ToRemove = false;
        }

        public virtual List<Actor> Collisions()
        {
            List<Actor> results = new List<Actor>();
            foreach (Actor actor in DrawPool)
            {
                if (actor.CurrentPosition.Intersects(CurrentPosition) && actor.Type == ActorType.EnemyTank)
                    results.Add(actor);
            }
            return results;
        }

        public virtual void Draw()
        {
            if (null != this.SpriteBatch)
            {
                this.SpriteBatch.Draw(Texture, CurrentPosition, CurrentColor);
            }
        }

        public virtual void GotHit(Projectile projectile)
        {
            throw new NotImplementedException();
        }

        public bool OutOfBounds
        {
            get
            {
                if (this.CurrentPosition.X > 700 || this.CurrentPosition.X < 20)
                    return true;
                if (this.CurrentPosition.Y > 400 || this.CurrentPosition.Y < 20)
                    return true;
                return false;
            }
        }
    }
}
