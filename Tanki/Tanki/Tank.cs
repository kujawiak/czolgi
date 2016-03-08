using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    public class Tank : Actor, IMovable, IShootable
    {
        public int LastShot { get; set; }
        public Direction TurretFacing { get; set; }
        public int ReloadTime { get; set; }

        public Tank()
        {
            Type = ActorType.EnemyTank;
            CurrentColor = Color.White;
            CurrentPosition = new Rectangle(100, 130, 30, 30);
            TurretFacing = Direction.Right;
            LastShot = 0;
            ReloadTime = 250;
        }

        public Tank(UnitManager UnitManager)
            : this()
        {
            this.UnitManager = UnitManager;
        }

        public bool Move(Direction direction, int speed)
        {
            var collisons = this.Collisions();
            if (collisons.Any())
                return false;
            
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
                Projectile projectile = new Projectile(this);
                UnitManager.Add(projectile);
                LastShot = (int)gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public bool ReadyToShoot(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > this.LastShot + ReloadTime)
                return true;
            return false;
        }

        public override List<Actor> Collisions()
        {
            List<Actor> results = new List<Actor>();
            foreach (Actor actor in UnitManager.Units)
            {
                var reference = CurrentPosition;
                reference.Width += 2;
                reference.Height += 2;
                if (actor.CurrentPosition.Intersects(reference) && actor.Type == ActorType.EnemyTank)
                    results.Add(actor);
            }
            return results;
        }

        public override void GotHit(Projectile projectile)
        {
            if (CurrentColor == Color.White)
            {
                CurrentColor = Color.Lime;
                return;
            }
            if (CurrentColor == Color.Lime)
            {
                CurrentColor = Color.Orange;
                return;
            }
            if (CurrentColor == Color.Orange)
            {
                CurrentColor = Color.Red;
                return;
            }
            if (CurrentColor == Color.Red)
            {
                RemoveMe = true;
                return;
            }
        }
    }
}
