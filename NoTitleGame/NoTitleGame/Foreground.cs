namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    class Foreground : Terrain, ICollidable
    {
        // Default terrain sprite
        private Texture2D groundTexture;
        // Stores the generated foreground
        private Texture2D generatedForeground;
        // ICollidable colour matrix
        private Color[,] foregrounColourArray;

        // Default constructor
        public Foreground(int width, int height)
            : base(width, height)
        { 
        }

        // Properties
        public Texture2D GroundTexture
        {
            get { return this.groundTexture; }
            set { this.groundTexture = value; }
        }

        public Texture2D GeneratedForeground
        {
            get { return this.generatedForeground; }
            set { this.generatedForeground = value; }
        }

        // ICollidable colour matrix
        public Color[,] colourArray
        {
            get { return this.foregrounColourArray; }
            set { this.foregrounColourArray = value; }
        }

        // Methods
        // Generate foreground
        public void CreateForeground()
        {
            Color[,] groundColours = TextureToArray.TextureTo2DArray(groundTexture);

            Color[] foregroundColours = new Color[this.Width * this.Height];

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    if (y > Terrain.terrainContour[x])
                    {
                        foregroundColours[x + y * this.Width] = groundColours[x % GroundTexture.Width, y % GroundTexture.Height];
                    }
                    else
                    {
                        foregroundColours[x + y * this.Width] = Color.Transparent;
                    }
                }
            }

            this.GeneratedForeground = new Texture2D(Game1.device, this.Width, this.Height, false, SurfaceFormat.Color);
            this.GeneratedForeground.SetData(foregroundColours);
            this.colourArray = TextureToArray.TextureTo2DArray(GeneratedForeground);
            
        }
    }
}
