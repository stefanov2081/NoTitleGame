namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Characters;
    using Microsoft.Xna.Framework.Audio;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Declare different objects
        // Declare graphics device
        public static GraphicsDevice device;

        // Declare game world
        GameWorld world;

        // Declare background
        Background background;

        // Declare terrain
        Terrain terrain;

        // Declare foreground
        Foreground foreground;

        // Declare test character
        Character darthVader;

        // Declare camera
        Camera camera;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set window size
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize graphics device
            device = graphics.GraphicsDevice;

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
            darthVader = new Master(0, 0, "Darth Vader", 999, 999, 999, 999, 1, 0);
            darthVader.CharacterTexture = Content.Load<Texture2D>("nssheet");
            darthVader.JumpSound = Content.Load<SoundEffect>("jump");
            darthVader.scale = 1.0f;
            darthVader.SetOnRadnomPosition();

            // Load camera
            camera = new Camera(GraphicsDevice.Viewport);
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

            darthVader.ProccessAnimations(100, gameTime);
            
            // Is called without conditions due to the jumping
            darthVader.Move();
            //TODO: Before the character fires call the character.UpdateSelectedWeaponPosition()
            //And make sure the character has selected the weapon he wishes to fire

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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            // Draw background
            spriteBatch.Draw(background.BackgroundTexture, world.GameWorldDimensions, Color.White);
            // Draw foreground
            spriteBatch.Draw(foreground.GeneratedForeground, world.GameWorldDimensions, Color.White);
            // Draw test character
            spriteBatch.Draw(darthVader.CharacterTexture, new Vector2(darthVader.PositionX, darthVader.PositionY), darthVader.sourceRect, Color.White, 0, 
                new Vector2(55 / 2, 58), darthVader.scale, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
