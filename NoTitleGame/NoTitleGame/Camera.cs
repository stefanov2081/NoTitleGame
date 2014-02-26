namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    class Camera
    {
        // Camera fields
        private Matrix transform;
        private Vector2 centre;
        private Viewport viewport;
        private float zoom = 1;
        private float rotation = 0;

        // Default constructor
        public Camera(Viewport newViewport)
        {
            this.viewport = newViewport;
        }

        // Properties
        public Matrix Transform
        {
            get { return this.transform; }
        }

        public float X
        {
            get { return this.centre.X; }
            set { this.centre.X = value; }
        }

        public float Y
        {
            get { return this.centre.Y; }
            set { this.centre.Y = value; }
        }

        public float Zoom
        {
            get { return this.zoom; }
            set
            {
                this.zoom = value;
                if (this.zoom < 0.4f)
                {
                    this.zoom = 0.4f;
                }
                else if (this.zoom > 3.0f)
                {
                    this.zoom = 3.0f;
                }
            }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        // Methods
        public void Update(Vector2 position)
        {
            this.centre = position;

            this.transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                Matrix.CreateRotationZ(this.rotation) *
                Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(this.viewport.Width / 2, this.viewport.Height / 2, 0));
        }

        public void UpdateCameraPosition(Vector2 cameraPosition)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                this.Zoom += 0.01f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                this.Zoom -= 0.01f;
            }
        }
    }
}
