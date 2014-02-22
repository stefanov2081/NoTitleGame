namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    class GameWorld
    {
        // Fields for the dimensions of the game world
        private int width;
        private int height;
        private Vector2 gameWorldDimension;

        // Game world dimension constructor
        public GameWorld(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public GameWorld(Vector2 dimensions)
        {
            this.GameWorldDimensions = dimensions;
        }

        // Properties for the dimensions of the game world
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public Vector2 GameWorldDimensions
        {
            get { return this.gameWorldDimension; }
            set { this.gameWorldDimension = value; }
        }
    }
}
