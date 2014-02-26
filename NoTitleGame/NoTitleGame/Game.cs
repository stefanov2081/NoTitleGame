namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Audio;
    using Exceptions;
    using System.Collections.Generic;
    using System;
    using System.Runtime.InteropServices;
    using Characters;
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        // Declare different objects
        // Declare graphics device
        public static GraphicsDevice device;

        // Declare random generator
        Random random;

        // Declare game world
        GameWorld world;

        // Declare background
        Background background;

        // Declare terrain
        Terrain terrain;

        // Declare foreground
        Foreground foreground;

        // Declare test characters
        Character darthVader;
        Character lukeSkywallker;
        Texture2D checkCharColl;

        // Holds the total number of characters for turned based game and current player turn here could be added a turn timer
        TurnInfo turnInfo;

        // Declare test projectile
        Projectlie rocket;

        // Declare camera
        Camera camera;

        // Declare user interface rectangle and background pixel
        DrawRectangle UIBackground;
        DrawRectangle progressBar;
        
        SpriteFont font;

        // Default graphics device
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Declare test character's inventory
 		Inventory vaderInventory;
 		
 		// Declare mouse fields
 		CursorCoordinates mouseCoords;
 		Rectangle mouseRectangle;
 		MouseState currentMouseState;
 		MouseState previousMouseState;
 		
 		// Delegate definition of the event
 		public delegate void FiredEvent(object sender);
 		
 		// Instances of delegate event
 		public FiredEvent OnMouseClick;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set window size
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            // Enable mouse
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            // Initialize graphics device
            device = graphics.GraphicsDevice;

            // Initialize new random generator
            random = new Random();

            // Load game world
            world = new GameWorld(device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);

            // Load background
            background = new Background(world.Width, world.Height);
            background.BackgroundTexture = Content.Load<Texture2D>("background");

            // Load and generate terrain
            terrain = new Terrain(world.Width, world.Height);
            terrain.GenerateTerrainContour();

            // Load foreground
            foreground = new Foreground(world.Width, world.Height);
            foreground.GroundTexture = Content.Load<Texture2D>("ground");
            foreground.CreateForeground();

            // Load test character
            darthVader = new Master(0, 0, "Darth Vader", 999, 999, 999, 999, 15, 0);
            darthVader.CharacterTexture = Content.Load<Texture2D>("nssheet");
            darthVader.CrosshairTexture = Content.Load<Texture2D>("crosshair");
            darthVader.CrosshairTextureL = Content.Load<Texture2D>("crosshairLeft");
            darthVader.JumpSound = Content.Load<SoundEffect>("jump");

            lukeSkywallker = new Master(0, 0, "Luke Skywallker", 888, 888, 888, 888, 14, 0);
            lukeSkywallker.CharacterTexture = Content.Load<Texture2D>("nssheet");
            lukeSkywallker.JumpSound = Content.Load<SoundEffect>("jump");

            turnInfo = new TurnInfo(2);
            turnInfo.Players.Add(darthVader);
            turnInfo.Players.Add(lukeSkywallker);
            turnInfo.SetUpPlayers(random, 1.0f);

            checkCharColl = Content.Load<Texture2D>("checkCharCollision");

            // Load test rocket and smoke trail
            rocket = new Projectlie(new Vector2(0, 0), 0.1f, 1.0f);
            rocket.ProjectileTexture = Content.Load<Texture2D>("rocket");
            rocket.SmokeTexture = Content.Load<Texture2D>("smoke");

            // Load particle list
            Explosions.particleList = new List<ParticleData>();

            // Load Explosion
            Explosions.explosion = Content.Load<Texture2D>("explosion");

            // Load stats bar
            UIBackground = new DrawRectangle(GraphicsDevice, Content.Load<Texture2D>("UIBack"));
            progressBar = new DrawRectangle(GraphicsDevice, Content.Load<Texture2D>("progressBar"));
            progressBar.Rectangle = GraphicsDevice.Viewport.TitleSafeArea;

            // Create colour arrays of every collidable object
            foreground.colourArray = TextureToArray.TextureTo2DArray(foreground.GroundTexture);
            darthVader.ColourArray = TextureToArray.TextureTo2DArray(checkCharColl);
            lukeSkywallker.ColourArray = TextureToArray.TextureTo2DArray(checkCharColl);
            rocket.ColourArray = TextureToArray.TextureTo2DArray(rocket.ProjectileTexture);
            Explosions.explosionColourArray = TextureToArray.TextureTo2DArray(Explosions.explosion);

            // Load font
            font = Content.Load<SpriteFont>("Font");

            // Load camera
            camera = new Camera(GraphicsDevice.Viewport);

            // Load inventory
 		    vaderInventory = new Inventory(device, Content.Load<Texture2D>("inventory"), 990, 300);
 		    
 		    // Inventory starts listening to events
 		    vaderInventory.OnMouseClick += new FiredEvent(vaderInventory.MouseClick);
 		
 		    // Load mouse state
 		    currentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
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

            // Proccess animations
            darthVader.ProccessAnimations(100, gameTime);
            lukeSkywallker.ProccessAnimations(100, gameTime);
            rocket.UpdateProjectile(random);

            // Get current cursor position
            mouseCoords = new CursorCoordinates(Mouse.GetState().X, Mouse.GetState().Y);

            // Get rectangle around mouse
            mouseRectangle = new Rectangle(mouseCoords.X, mouseCoords.Y, 1, 1);

            // Get previous mouse state because it is now old
            previousMouseState = currentMouseState;

            // Get current mouse state
            currentMouseState = Mouse.GetState();

            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (vaderInventory.InventoryRectangle.Contains(mouseRectangle))
                {
                    // Update test character's inventory 
                    spriteBatch.Begin();
                    vaderInventory.UpdateInventoryLocation(device, spriteBatch);
                    spriteBatch.End();
                    vaderInventory.OnMouseClick(this);
                }
            }

            if (rocket.ProjectileFlying)
            {
                rocket.UpdateProjectile(random);
                turnInfo.CheckCollisions(gameTime, rocket, random, turnInfo, foreground, world);
            }

            if (Explosions.particleList.Count > 0)
            {
                Explosions. UpdateParticles(gameTime);
            }
            
            try
            {
                darthVader.ProcessKeyboard(darthVader, rocket);
            }
            catch (CharacterHasDiedException)
            {
            }
            
            // Follow camera
            camera.Update(new Vector2(darthVader.PositionX, darthVader.PositionY));
            camera.UpdateCameraPosition(new Vector2(darthVader.PositionX, darthVader.PositionY));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Global transformation
            Vector3 screenScalingFactor = new Vector3(1, 1, 1);
            Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);

            // Draw background
            spriteBatch.Begin();
            spriteBatch.Draw(background.BackgroundTexture, world.GameWorldDimensions, Color.White);
            spriteBatch.End();

            // Draw game content
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            // Draw foreground
            spriteBatch.Draw(foreground.GeneratedForeground, world.GameWorldDimensions, Color.White);
            // Draw test characters
            // Dart Vader
            spriteBatch.Draw(darthVader.CharacterTexture, new Vector2(darthVader.PositionX, darthVader.PositionY), darthVader.SourceRect, Color.White, 0,
                   new Vector2(55 / 2, 58), darthVader.Scale, SpriteEffects.None, 0);
            // Luke Skywallker
            spriteBatch.Draw(lukeSkywallker.CharacterTexture, new Vector2(lukeSkywallker.PositionX, lukeSkywallker.PositionY),
                lukeSkywallker.SourceRect, Color.White, 0, new Vector2(55 / 2, 58), lukeSkywallker.Scale, SpriteEffects.None, 0);

            // Crosshair
            if (darthVader.FacingRight)
            {

                spriteBatch.Draw(darthVader.CrosshairTexture,
                    new Rectangle((int)(darthVader.PositionX), (int)(darthVader.PositionY - 50 * darthVader.Scale), 80, 20),
                    new Rectangle(0, 0, darthVader.CrosshairTexture.Width, darthVader.CrosshairTexture.Height), Color.Yellow,
                    darthVader.Angle - 1.57079632f,
                    new Vector2(0, darthVader.CrosshairTexture.Height / 2), SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(darthVader.CrosshairTextureL,
                    new Rectangle((int)(darthVader.PositionX - 20), (int)(darthVader.PositionY - 50 * darthVader.Scale), 80, 20),
                    new Rectangle(0, 0, darthVader.CrosshairTexture.Width, darthVader.CrosshairTexture.Height), Color.Yellow,
                    -(darthVader.Angle - 1.57079632f),
                    new Vector2(darthVader.CrosshairTextureL.Width, darthVader.CrosshairTextureL.Height / 2), SpriteEffects.None, 0);
            }
            spriteBatch.End();

            if (rocket.ProjectileFlying)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                // Draw projectile - rocket and smoke
                spriteBatch.Draw(rocket.ProjectileTexture, rocket.ProjectilePosition, null, Color.Red,
                        rocket.ProjectileAngle, new Vector2(42, 240), rocket.ProjectileScaling, SpriteEffects.None, 1);
                rocket.DrawSmoke(spriteBatch);
                spriteBatch.End();
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, camera.Transform);
            Explosions.DrawExplosion(spriteBatch);
            spriteBatch.End();

            // Draw stats
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            DirectDraw.DrawBottomStats(UIBackground, progressBar, darthVader, graphics, device, spriteBatch, font);
            spriteBatch.DrawString(font, darthVader.Angle.ToString(), new Vector2(10, 10), Color.Yellow);
            spriteBatch.DrawString(font, darthVader.Angle.ToString(), new Vector2(10, 30), Color.Yellow);
            spriteBatch.DrawString(font, rocket.ProjectilePosition.X.ToString(), new Vector2(10, 50), Color.Yellow);
            spriteBatch.DrawString(font, rocket.ProjectilePosition.X.ToString(), new Vector2(10, 70), Color.Yellow);
            
            // Draw test character's inventory
            DirectDraw.DrawInventory(vaderInventory, device, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
