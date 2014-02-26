namespace NoTitleGame
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Characters;
    public class Projectlie
    {
        // Fields
        private List<Vector2> smokeList;
        private Texture2D projectileTexture;
        private Texture2D smokeTexture;
        private Vector2 projectilePosition;
        private Vector2 projectileDirection;
        private Color[,] colourArray;
        private bool projectileFlying;
        private float projectileAngle;
        private float projectileScaling;
        private float gravity;


        // Default constructor
        public Projectlie(Vector2 position, float scale, float gravity)
        {
            this.ProjectileScaling = scale;
            this.Gravity = gravity;
            this.ProjectilePosition = position;
            this.smokeList = new List<Vector2>();
        }

        // Properties
        public List<Vector2> SmokeList
        {
            get { return this.smokeList; }
            set { this.smokeList = value; }
        }

        public Texture2D ProjectileTexture
        {
            get { return this.projectileTexture; }
            set { this.projectileTexture = value; }
        }

        public Texture2D SmokeTexture
        {
            get { return this.smokeTexture; }
            set { this.smokeTexture = value; }
        }

        public Vector2 ProjectilePosition
        {
            get { return this.projectilePosition; }
            set { this.projectilePosition = value; }
        }

        public Vector2 ProjectileDirection
        {
            get { return this.projectileDirection; }
            set { this.projectileDirection = value; }
        }

        public bool ProjectileFlying
        {
            get { return this.projectileFlying; }
            set { this.projectileFlying = value; }
        }

        public float ProjectileAngle
        {
            get { return this.projectileAngle; }
            set { this.projectileAngle = value; }
        }

        public float ProjectileScaling
        {
            get { return this.projectileScaling; }
            set { this.projectileScaling = value; }
        }

        public float Gravity
        {
            get { return this.gravity; }
            set { this.gravity = value; }
        }

        public Color[,] ColourArray
        {
            get { return this.colourArray; }
            set { this.colourArray = value; }
        }

        // Methods
        public void DrawProjectile(SpriteBatch spriteBatch, Color colour)
        {
            if (this.ProjectileFlying)
            {
                spriteBatch.Draw(this.projectileTexture, this.ProjectilePosition, null, colour,
                    this.ProjectileAngle, new Vector2(42, 240), this.ProjectileScaling, SpriteEffects.None, 1);
            }
        }

        // Update rocket position
        public void UpdateProjectile(Random random)
        {
            if (this.ProjectileFlying)
            {
                // Rocket
                Vector2 gravity = new Vector2(0, this.Gravity);
                this.ProjectileDirection += gravity / 10.0f;
                this.ProjectilePosition += this.ProjectileDirection;
                this.ProjectileAngle = (float)Math.Atan2(this.ProjectileDirection.X, -this.ProjectileDirection.Y);

                // Smoke
                for (int i = 0; i < 5; i++)
                {
                    Vector2 smokePos = this.ProjectilePosition;
                    smokePos.X += random.Next(10) - 5;
                    smokePos.Y += random.Next(10) - 5;
                    this.SmokeList.Add(smokePos);
                }
            }
        }

        // Draw smoke trail
        public void DrawSmoke(SpriteBatch spriteBatch)
        {
            foreach (Vector2 smokePos in this.SmokeList)
            {
                spriteBatch.Draw(this.SmokeTexture, smokePos, null, Color.White, 0, new Vector2(40, 35),
                    0.2f, SpriteEffects.None, 1);
            }
        }

        private void DrawRocket(SpriteBatch spriteBatch, Projectlie currProj, TurnInfo infor)
        {
            if (currProj.ProjectileFlying)
            {
                spriteBatch.Draw(currProj.ProjectileTexture, currProj.ProjectilePosition, null, Color.Red,
                    currProj.ProjectileAngle, new Vector2(42, 240), currProj.ProjectileScaling, SpriteEffects.None, 1);
            }
        }

        // Fire projectile
        public void FireProjectile(Character currentCharacter)
        {
            if (!this.ProjectileFlying)
            {
                this.ProjectileFlying = true;
                //if (!launch.Play())
                //{
                //    launch.Play();
                //}

                this.ProjectilePosition = new Vector2(currentCharacter.PositionX, currentCharacter.PositionY - 40 * currentCharacter.Scale);
                this.ProjectileAngle = currentCharacter.Angle;

                if (currentCharacter.FacingRight)
                {
                    // Rotate rocket
                    Vector2 up = new Vector2(0, -1);
                    Matrix rotMatrix = Matrix.CreateRotationZ(this.ProjectileAngle);
                    this.ProjectileDirection = Vector2.Transform(up, rotMatrix);
                    this.ProjectileDirection *= currentCharacter.Power / 50.0f;
                }
                else
                {
                    // Rotate rocket
                    Vector2 up = new Vector2(0, -1);
                    Matrix rotMatrix = Matrix.CreateRotationZ(this.ProjectileAngle);
                    this.ProjectileDirection = Vector2.Transform(up, rotMatrix);
                    this.ProjectileDirection *= currentCharacter.Power / 50.0f;
                    this.projectileDirection *= -1;
                }
            }
        }
    }
}
       
   
