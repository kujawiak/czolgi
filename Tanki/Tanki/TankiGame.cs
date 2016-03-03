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
        List<IDrawable> DrawPool;

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
            DrawPool = new List<IDrawable>();
            // Create player's tank
            player = new Tank();
            player.DrawPool = DrawPool;
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
                player.Shoot(gameTime);

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

            foreach(IDrawable actor in DrawPool) 
                actor.Draw();

            spriteBatch.End();
        }
    }    
}
