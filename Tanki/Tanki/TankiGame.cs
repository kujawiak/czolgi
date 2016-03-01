using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tanki
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TankiGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tank player;
        List<Tank> drawPool; //TODO: Change to List<IDrawable> and let each tank/object implement IDrawable.
                             //TODO: Refactor drawPool to more meaningful name

        public TankiGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            drawPool = new List<Tank>();
            player = new Tank();
            drawPool.Add(player);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: add tank graphic to Content and load it here instead of this simple rectangle
            player.Texture = new Texture2D(GraphicsDevice, 1, 1);
            player.Texture.SetData(new[] { Color.LightGray });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            int speed = 2;

            //TODO: Export to separate class (Controls?) that will handle inputs logic to avoid if-flooding here
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                player.Move(Direction.Right, speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                player.Move(Direction.Left, speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                player.Move(Direction.Up, speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                player.Move(Direction.Down, speed);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                player.Shoot(drawPool);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            player.SpriteBatch = spriteBatch;

            spriteBatch.Begin();
            //TODO: Shall we remove every sprite that is not needed anymore here or let IDrawable interface handle that?
            foreach(Tank t in drawPool)
            {
                t.Draw();
            }
            spriteBatch.End();
        }
    }

    //TODO: Move to separate file
    public class Tank
    {
        public Rectangle CurrentPosition; //TODO: Inherit from more general class (EnviromentObject?)
        public Direction TurretFacing;
        public Color CurrentColor; //TODO: Obsolote after texture implementaiton
        private bool IsShell; //TODO: Remove after shell will have dedicated class (Shell?)
        private DateTime lastShot = DateTime.Now; //TODO: This is not pretty :/

        public Tank()
        {
            CurrentColor = Color.White;
            CurrentPosition = new Rectangle(100, 130, 30, 30);
            TurretFacing = Direction.Right;
        }
        public void Draw()
        {
            if (null != this.SpriteBatch)
            {
                if (this.IsShell) //TODO: Move this logic to Shell class
                {
                    switch (TurretFacing)
                    {
                        case Direction.Left:
                            this.CurrentPosition.X -= 4;
                            break;
                        case Direction.Right:
                            this.CurrentPosition.X += 4;
                            break;
                        case Direction.Up:
                            this.CurrentPosition.Y -= 4;
                            break;
                        case Direction.Down:
                            this.CurrentPosition.Y += 4;
                            break;
                        default:
                            break;
                    }
                }
                this.SpriteBatch.Draw(this.Texture, CurrentPosition, CurrentColor);
            }
        }

        public Texture2D Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        internal bool Move(Direction direction, int speed)
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

        internal void Shoot(List<Tank> pool) //TODO: I don't like the idea to pass a whole drawingPool object here
                                             //there should be separate mechanism of creating new actors and adding them to pool
        {
            if (DateTime.Now > this.lastShot.AddMilliseconds(250)) //TODO: Refactor 250 to property (RelaodTime?)
            {
                //TODO: This is very bad and it should be in Shell class logic
                Tank shell = new Tank();
                shell.Texture = this.Texture;
                shell.SpriteBatch = this.SpriteBatch;
                shell.CurrentPosition = this.CurrentPosition;
                shell.CurrentPosition.X += 13;
                shell.CurrentPosition.Y += 13;
                shell.CurrentPosition.Width = 4;
                shell.CurrentPosition.Height = 4;
                shell.CurrentColor = Color.Red;
                shell.TurretFacing = TurretFacing;
                shell.IsShell = true;
                pool.Add(shell);
                this.lastShot = DateTime.Now;
            }
            
        }
    }

    //TODO: Move to separate file
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
