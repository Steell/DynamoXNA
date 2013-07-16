using System;
using System.Collections.Generic;
using System.Linq;
using Dynamo;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DynamoXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DynamoGame : Game
    {
        private readonly FSharpFunc<FSharpList<FScheme.Value>, FScheme.Value> _update;
        private readonly FSharpFunc<FSharpList<FScheme.Value>, FScheme.Value> _draw;
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public static Texture2D Square;

        private FScheme.Value _world;

        public DynamoGame(
            FSharpFunc<FSharpList<FScheme.Value>, FScheme.Value> update,
            FSharpFunc<FSharpList<FScheme.Value>, FScheme.Value> draw, 
            FScheme.Value world0,
            int width, int height)
        {
            _update = update;
            _draw = draw;
            _world = world0;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };

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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Square = Content.Load<Texture2D>("square");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _world = _update.Invoke(
                ListModule.OfSeq(new List<FScheme.Value>
                {
                    _world, 
                    FScheme.Value.NewContainer(gameTime)
                }));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _draw.Invoke(
                ListModule.OfSeq(new List<FScheme.Value>
                {
                    _world,
                    FScheme.Value.NewContainer(_spriteBatch)
                }));

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
