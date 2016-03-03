using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    public class Tank : Actor, IDrawable, IMovable, IShootable
    {
        public int LastShot { get; set; }
        public Direction TurretFacing;
        private int ReloadTime = 250;

        public Tank()
        {
            CurrentColor = Color.White;
            CurrentPosition = new Rectangle(100, 130, 30, 30);
            TurretFacing = Direction.Right;
            LastShot = 0;
        }

        public override void Draw()
        {
            if (null != this.SpriteBatch)
            {
                this.SpriteBatch.Draw(Texture, CurrentPosition, CurrentColor);
            }
        }

        public bool Move(Direction direction, int speed)
        {
            switch (direction)
            {
                case Direction.Left:
                    this.TurretFacing = Direction.Left;
                    CurrentPosition.X -= speed;
                    return true;
                case Direction.Right:
                    this.TurretFacing = Direction.Right;
                    CurrentPosition.X += speed;
                    return true;
                case Direction.Up:
                    this.TurretFacing = Direction.Up;
                    CurrentPosition.Y -= speed;
                    return true;
                case Direction.Down:
                    this.TurretFacing = Direction.Down;
                    CurrentPosition.Y += speed;
                    return true;
                default:
                    return false;
            }
        }

        public void Shoot(GameTime gameTime)
        {
            if (ReadyToShoot(gameTime))
            {
                Projectile projectile = new Projectile(this, this.SpriteBatch, this.Texture);
                DrawPool.Add(projectile);
                LastShot = (int)gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public bool ReadyToShoot(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > this.LastShot + ReloadTime)
                return true;
            return false;
        }
    }
}
