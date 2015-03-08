using System.Collections.Generic;
using Atom;
using Atom.Entity;
using Atom.Input;
using Atom.Physics.Collision.BoundingBox;
using Atom.Physics.Gravity;
using Atom.Physics.Movement;
using Atom.Graphics.Rendering;
using Atom.Graphics.Rendering.Static;
using Atom.Graphics.Rendering.Animated;
using Atom.World;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace AtomDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayerEntity entity;
        private World world;

        public Game1()
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
            GameServices.Initialize(Content, graphics);

            world = new World();

            EntityFactory.GetInstance().Register<PlayerEntity>();
            EntityFactory.GetInstance().Register<Platform>();

            entity = EntityFactory.GetInstance().Construct<PlayerEntity>();
            Platform platform = EntityFactory.GetInstance().Construct<Platform>();
            

            world.AddSystem(new StandardKeyboardSystem());
            world.AddSystem(new GravitySystem());
            world.AddSystem(new MovementSystem());
            world.AddSystem(new BoundingBoxSystem());
            world.AddSystem(new BoundingBoxCollisionResponseSystem());
            
            world.AddSystem(new StaticRenderSystem());
            world.AddSystem(new AnimatedRenderSystem());
            
            world.AddEntity(entity, entity.GetDefaultComponents());
            world.AddEntity(platform, platform.GetDefaultComponents());

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

            // TODO: use this.Content to load your game content here
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
                this.Exit();

            world.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            world.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
