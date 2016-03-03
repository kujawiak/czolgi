using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    class Projectile : Actor, IDrawable, IMovable
    {
        public int Speed { get; set; }
        public Direction ShotDirection { get; set; } 

        public Projectile()
        {
            CurrentColor = Color.Red;
            Speed = 4;
        }

        public Projectile(Tank tank, SpriteBatch spriteBatch, Texture2D texture2D) : this()
        {
            this.ShotDirection = tank.TurretFacing;
            this.CurrentPosition = tank.CurrentPosition;
            this.CurrentPosition.X += 13; //TODO: repalce hardcoded values
            this.CurrentPosition.Y += 13;
            this.CurrentPosition.Width = 4;
            this.CurrentPosition.Height = 4;
            this.SpriteBatch = spriteBatch;
            this.Texture = texture2D;
        }

        public override void Draw()
        {
            if (null != this.SpriteBatch)
            {
                this.Move(ShotDirection, Speed);
                this.SpriteBatch.Draw(Texture, CurrentPosition, CurrentColor);
            }
        }

        public bool Move(Direction direction, int speed)
        {
            if (Impact())
                return false;
            switch (ShotDirection)
            {
                case Direction.Left:
                    this.CurrentPosition.X -= Speed;
                    break;
                case Direction.Right:
                    this.CurrentPosition.X += Speed;
                    break;
                case Direction.Up:
                    this.CurrentPosition.Y -= Speed;
                    break;
                case Direction.Down:
                    this.CurrentPosition.Y += Speed;
                    break;
                default:
                    break;
            }
            return true;
        }

        private bool Impact()
        {
            // TODO: to implement impact check logic, we need collection of each actors with their positions
            return false;
        }
    }
}
