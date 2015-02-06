using System.Collections.Generic;
using System.Linq;
using Atom;
using Atom.Entity;
using Atom.Graphics;
using Atom.Input;
using Atom.Logging;
using Atom.Logging.Loggers;
using Atom.Physics;
using Atom.Physics.Collision;
using Atom.Physics.Collision.BoundingBox;
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
            // TODO: Add your initialization logic here

            world = new World();

            EntityFactory entityFactory = EntityFactory.GetInstance();

            EntityFactory.GetInstance().Register<PlayerEntity>();

            
            entity = entityFactory.Construct<PlayerEntity>();
            PlayerEntity entity1 = entityFactory.Construct<PlayerEntity>();

            PlayerEntity entity2 = entityFactory.Construct<PlayerEntity>();
            PlayerEntity entity3 = entityFactory.Construct<PlayerEntity>();


            List<Component> components = entity.CreateDefaultComponents();
            SpriteComponent spriteComponent = new SpriteComponent()
            {
                EntityId = entity.Id,
                Image = this.Content.Load<Texture2D>("space_invader"),
                FrameWidth = 100,
                FrameHeight = 100,
            };

            SpriteComponent spriteComponent1 = new SpriteComponent()
            {
                EntityId = entity1.Id,
                Image = this.Content.Load<Texture2D>("space_invader"),
                FrameWidth = 100,
                FrameHeight = 100,
            };

            SpriteComponent spriteComponent2 = new SpriteComponent()
            {
                EntityId = entity2.Id,
                Image = this.Content.Load<Texture2D>("space_invader"),
                FrameWidth = 100,
                FrameHeight = 100,
            };

            AnimatedSpriteComponent aniSprite = new AnimatedSpriteComponent()
            {
                EntityId  = entity.Id,
                Image = this.Content.Load<Texture2D>("runningcat"),
                FrameWidth = 512, FrameHeight = 256, FrameCount = 7, FramesPerSecond = 16, SequenceStartFrame = 0,
            };

            AnimatedSequenceComponent aniSequence = new AnimatedSequenceComponent()
            {
                EntityId = entity.Id,
                AnimationSequence = new int[] {0, 1, 2, 3, 4, 5, 6, 7 }, CurrentSequenceDirection = SequenceDirection.NONE,
            };

            components.Add(aniSprite);
            components.Add(aniSequence);

            ILogger logger = LogFactory.GetInstance().Construct("Console");

            logger.Log(LogLevel.Info, components.ToString());
            logger.Log(LogLevel.Warning, "Warning Message");
            logger.Log(LogLevel.Error, "Error message");
            
            List<Component> entity1Components = entity1.CreateDefaultComponents();
            List<Component> entity2Components = entity2.CreateDefaultComponents();
            List<Component> entity3Components = entity3.CreateDefaultComponents();

            entity1Components.Add(spriteComponent1);
            entity2Components.Add(spriteComponent2);

            entity3Components.Add(aniSprite);
            entity3Components.Add(aniSequence);

            entity1Components.RemoveAll(component => component.GetType() == typeof (StandardKeyComponent));
            entity2Components.RemoveAll(component => component.GetType() == typeof(StandardKeyComponent));

            entity2Components.Where(component => component.GetType() == typeof(PositionComponent)).ToList().ForEach(
                (Component component) =>
                {
                    if (component.GetType() == typeof (PositionComponent))
                    {
                        var position = (PositionComponent) component;

                        position.X += 100;
                        position.Y -= 100;
                    }
                    
                });

            components.Where(component => component.GetType() == typeof(PositionComponent)).ToList().ForEach(
                (Component component) =>
                {
                    if (component.GetType() == typeof(PositionComponent))
                    {
                        var position = (PositionComponent)component;

                        position.Y -= 300;
                    }

                });

            world.AddSystem(new StandardKeyboardSystem());
            world.AddSystem(new MovementSystem());
            world.AddSystem(new BoundingBoxSystem());
            world.AddSystem(new BoundingBoxCollisionResponseSystem());
            
            world.AddSystem(new StaticRenderSystem());
            world.AddSystem(new AnimatedRenderSystem());
            
            world.AddEntity(entity, components);
            //world.AddEntity(entity1, entity1Components);
            //world.AddEntity(entity2, entity2Components);
            //world.AddEntity(entity3, entity3Components);

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
