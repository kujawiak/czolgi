using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
        Controls controls = new Controls();
        List<Actor> DrawPool;
        public Direction direction { get; set; }

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
            DrawPool = new List<Actor>();
            // Create player's tank
            player = new Tank(DrawPool);
            player.Type = ActorType.Player;
            DrawPool.Add(player);

            // Create enemy
            Tank enemy = new Tank();
            enemy.CurrentPosition = new Rectangle(200, 100, 50, 80);
            DrawPool.Add(enemy);
            
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
            Texture2D defaultTexture = new Texture2D(GraphicsDevice, 1, 1);
            defaultTexture.SetData(new[] { Color.LightGray });

            DrawPool.Where(a => a.Texture == null).Select(a => a.Texture = defaultTexture).ToList();
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
            if (controls.CheckIfCanClose) Exit();
            int speed = 2;
            direction = controls.MoveControler();
            Console.WriteLine(direction);
            if (controls.ShootPressed) player.Shoot(gameTime);
            switch(direction)
            {
                case Direction.Left:
                    player.Move(Direction.Left, speed);
                    return;
                case Direction.Right:
                    player.Move(Direction.Right, speed);
                    return;
                case Direction.Up:
                    player.Move(Direction.Up, speed);
                    return;
                case Direction.Down:
                    player.Move(Direction.Down, speed);
                    return;
                    

            }

            if (controls.GenerateEnemiesPressed = controls.OneKeyPress(Keys.N))
            {
                var rnd = new Random();
                Tank enemy = new Tank();
                enemy.CurrentPosition = new Rectangle(rnd.Next(0, 700), rnd.Next(0, 400), 65, 65);
                enemy.Texture = player.Texture;
                DrawPool.Add(enemy);
            }

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

            DrawPool.Where(a => a.SpriteBatch == null).Select(a => a.SpriteBatch = spriteBatch).ToList();

            spriteBatch.Begin();

            // Remove marked actors
            DrawPool.RemoveAll(a => a.ToRemove);
            DrawPool.RemoveAll(a => a.Type == ActorType.Projectile && a.OutOfBounds);
            foreach (Actor actor in DrawPool)
            {
                actor.Draw();
            }

            spriteBatch.End();
        }
    }    
}
