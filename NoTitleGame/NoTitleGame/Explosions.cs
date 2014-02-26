namespace NoTitleGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    // Explosion particle info
    public struct ParticleData
    {
        public float BirthTime;
        public float MaxAge;
        public float Scaling;
        public Vector2 OriginalPosition;
        public Vector2 Acceleration;
        public Vector2 Direction;
        public Vector2 Position;
        public Color ModColour;
    }

    public static class Explosions
    {
        // Explosion textures
        public static Texture2D explosion;
        // Collor matrix of the explosion texture
        public static Color[,] explosionColourArray;
        // Store active particles
        public static List<ParticleData> particleList;

        // Generate explosion
        public static void AddExplosion(Vector2 explosionPos, int numberOfParticles, float size, float maxAge, GameTime gameTime, Random random, 
            Texture2D explosionTexture, TurnInfo players, Foreground foreground, GameWorld world)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                AddExplosionParticle(explosionPos, size, maxAge, gameTime, random);
            }

            float rotation = (float)random.Next(10);

            Matrix mat = Matrix.CreateTranslation(-explosionTexture.Width / 2, -explosionTexture.Height / 2, 0) *
                Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(size / (float)explosionTexture.Width * 2.0f) *
                 Matrix.CreateTranslation(explosionPos.X, explosionPos.Y, 0);

            Terrain.AddCrater(explosionColourArray, mat, world);

            for (int i = 0; i < players.Players.Count; i++)
            {
                players.Players[i].PositionY = Terrain.terrainContour[(int)players.Players[i].PositionX];
                //FlattenTerrainBelowPlayers();
                foreground.CreateForeground();
            }
        }

        public static void AddExplosionParticle(Vector2 explosionPos, float explosionSize, float maxAge, GameTime gameTime, Random random)
        {
            ParticleData particle = new ParticleData();

            particle.OriginalPosition = explosionPos;
            particle.Position = particle.OriginalPosition;

            particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            particle.MaxAge = maxAge;
            particle.Scaling = 0.25f;
            particle.ModColour = Color.White;

            float particleDistance = (float)random.NextDouble() * explosionSize;
            Vector2 displacement = new Vector2(particleDistance, 0);
            float angle = MathHelper.ToRadians(random.Next());
            displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));

            particle.Direction = displacement * 2.0f;
            particle.Acceleration = -particle.Direction;

            Explosions.particleList.Add(particle);
        }

        // Move the explosion
        public static void UpdateParticles(GameTime gameTime)
        {
            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;

            for (int i = particleList.Count - 1; i >= 0; i--)
            {
                ParticleData particle = particleList[i];
                float timeAlive = now - particle.BirthTime;

                if (timeAlive > particle.MaxAge)
                {
                    particleList.RemoveAt(i);
                }
                else
                {
                    float relAge = timeAlive / particle.MaxAge;
                    particle.Position = 0.5f * particle.Acceleration * relAge * relAge + particle.Direction * relAge + particle.OriginalPosition;

                    float invAge = 1.0f - relAge;
                    particle.ModColour = new Color(new Vector4(invAge, invAge, invAge, invAge));

                    Vector2 positionFromCenter = particle.Position - particle.OriginalPosition;
                    float distance = positionFromCenter.Length();
                    particle.Scaling = (50.0f + distance) / 200.0f;

                    particleList[i] = particle;
                }
            }
        }

        public static void DrawExplosion(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                ParticleData particle = particleList[i];
                spriteBatch.Draw(explosion, particle.Position, null, particle.ModColour, i, new Vector2(256, 256),
                    particle.Scaling, SpriteEffects.None, 1);
            }
        }
    }
}
