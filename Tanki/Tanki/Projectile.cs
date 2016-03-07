using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    public class Projectile : Actor, IMovable
    {
        public int Speed { get; set; }
        public Direction ShotDirection { get; set; } 

        public Projectile()
        {
            Type = ActorType.Projectile;
            CurrentColor = Color.Red;
            Speed = 4;
        }

        public Projectile(Tank tank) : this()
        {
            this.ShotDirection = tank.TurretFacing;
            this.CurrentPosition = tank.CurrentPosition;
            this.CurrentPosition.X += 1; //TODO: repalce hardcoded values
            this.CurrentPosition.Y += 1;
            this.CurrentPosition.Width = 4;
            this.CurrentPosition.Height = 4;
            this.SpriteBatch = tank.SpriteBatch;
            this.Texture = tank.Texture;
            this.DrawPool = tank.DrawPool;
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
            var collisons = this.Collisions();
            if (collisons.Any())
            {
                collisons.First().GotHit(this);
                this.ToRemove = true;
                return false;
            }
                
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
    }
}
