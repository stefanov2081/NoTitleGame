using Characters;
using NoTitleGame;

namespace NoTitleGame
{
    
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
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

        // Declare user interface rectangle and background pixel
        DrawRectangle UIBackground;
        DrawRectangle progressBar;
        SpriteFont font;

        // Default graphics device
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set window size
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = true;
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
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            // Initialize graphics device
            device = graphics.GraphicsDevice;

            // Load game world
            world = new GameWorld(device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            //world = new GameWorld(2048, 1024);

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
            darthVader.JumpSound = Content.Load<SoundEffect>("jump");
            darthVader.scale = 1.0f;
            darthVader.SetOnRadnomPosition();

            // Load stats bar
            UIBackground = new DrawRectangle(GraphicsDevice, Content.Load<Texture2D>("UIBack"));
            progressBar = new DrawRectangle(GraphicsDevice, Content.Load<Texture2D>("progressBar"));
            progressBar.Rectangle = GraphicsDevice.Viewport.TitleSafeArea;

            // Load font
            font = Content.Load<SpriteFont>("Font");

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

            // Proccess animations
            darthVader.ProccessAnimations(100, gameTime);
            
            // Is called without conditions due to the jumping
            darthVader.Move();
            //TODO: Before the character fires call the character.UpdateSelectedWeaponPosition()
            //And make sure the character has selected the weapon he wishes to fire

            // Tests stats bars logic
            darthVader.CurrentHealth --;
            darthVader.CurrentMana -= 2;
            darthVader.CurrentShield -= 3;

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

            // Draw background
            spriteBatch.Begin();
            spriteBatch.Draw(background.BackgroundTexture, world.GameWorldDimensions, Color.White);
            spriteBatch.End();

            // Draw game content
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            // Draw foreground
            spriteBatch.Draw(foreground.GeneratedForeground, world.GameWorldDimensions, Color.White);
            // Draw test character
            spriteBatch.Draw(darthVader.CharacterTexture, new Vector2(darthVader.PositionX, darthVader.PositionY), darthVader.sourceRect, Color.White, 0, 
                new Vector2(55 / 2, 58), darthVader.scale, SpriteEffects.None, 0);
            spriteBatch.End();

            // Draw stats
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            DirectDraw.DrawBottomStats(UIBackground, progressBar, darthVader, graphics, device, spriteBatch, font);
            spriteBatch.End();

            base.Draw(gameTime);

            

            
            
            
        }
    }
}
