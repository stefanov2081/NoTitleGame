namespace NoTitleGame
{
    using Microsoft.Xna.Framework.Graphics;
    class Background : GameWorld
    {
        // Default background sprite
        private Texture2D backgroundTexture;

        // Default constructor
        public Background(int width, int height)
            : base(width, height)
        {
        }

        public Background(Texture2D texture, int width, int height)
            : base(width, height)
        {
            this.BackgroundTexture = texture;
        }

        // Properties
        public Texture2D BackgroundTexture
        {
            get { return this.backgroundTexture; }
            set { this.backgroundTexture = value; }
        }
    }
}
