using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoTitleGame
{
    public class DrawRectangle
    {
        // Fields
        private Rectangle rectangle;
        private Texture2D pixel;

        // Default constructor
        public DrawRectangle(GraphicsDevice device)
        {
            this.Rectangle = device.Viewport.TitleSafeArea;
            this.Pixel = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            this.Pixel.SetData(new[] { Color.White });
        }

        // Overloaded constructor
        public DrawRectangle(GraphicsDevice device, Texture2D pixel)
        {
            this.Rectangle = device.Viewport.TitleSafeArea;
            this.Pixel = pixel;
        }

        // Properties
        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; }
        }

        public Texture2D Pixel
        {
            get { return this.pixel; }
            set { this.pixel = value; }
        }
    }
}
