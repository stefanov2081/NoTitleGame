namespace NoTitleGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Characters;
    using Microsoft.Xna.Framework;

    public class TurnInfo
    {
        // Fields
        private List<Character> players;
        private int numberOfPlayers;
        private int currentPlayer;

        // Defalut constructor
        public TurnInfo(int numberOfPlayers)
        {
            this.NumberOfPlayers = numberOfPlayers;
            players = new List<Character>(numberOfPlayers);
        }

        // Properties
        public List<Character> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public int NumberOfPlayers
        {
            get { return this.numberOfPlayers; }
            set { this.numberOfPlayers = value; }
        }

        public int CurrentPlayer
        {
            get { return this.currentPlayer; }
        }

        // Methods
        public void SetUpPlayers(Random random, float scale)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].SetOnRadnomPosition(random);
                players[i].Angle = MathHelper.ToRadians(90);
                players[i].Scale = scale;
            }
        }

        public void CheckCollisions(GameTime gameTime, Projectlie currProj, Random random, TurnInfo info, Foreground foreground, GameWorld world)
        {
            Vector2 terrainCollisionPoint = CheckTerrainCollision(currProj);
            Vector2 playerCollisionPoint = CheckPlayersCollision(currProj);
            bool projectileOutOfScreen = CheckOutOfScreen(currProj);

            if (playerCollisionPoint.X > -1)
            {
                //Play.Sound();
                Explosions.AddExplosion(playerCollisionPoint, 12, 90.0f, 2000.0f, gameTime, random, Explosions.explosion, info, foreground, world);

                currProj.ProjectileFlying = false;

                currProj.SmokeList = new List<Vector2>();
                NextPlayer();
            }

            if (terrainCollisionPoint.X > -1)
            {
                //Play.Sound();
                Explosions.AddExplosion(terrainCollisionPoint, 6, 50.0f, 1400.0f, gameTime, random, Explosions.explosion, info, foreground, world);

                currProj.ProjectileFlying = false;

                currProj.SmokeList = new List<Vector2>();
                NextPlayer();
            }

            if (projectileOutOfScreen)
            {
                currProj.ProjectileFlying = false;

                currProj.SmokeList = new List<Vector2>();
                NextPlayer();
            }
        }

        // Detect collision between projectile and player
        private Vector2 CheckPlayersCollision(Projectlie currProj)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Character player = Players[i];

                if (player.IsAlive)
                {
                    if (i != currentPlayer)
                    {
                        Vector2 playerCollisionPoint = ObjectsCollide(player, currProj);

                        if (playerCollisionPoint.X > -1)
                        {
                            int damage = (int)Players[CurrentPlayer].Power / 20 + 50 - (player.Armor / 10);
                            int damageLeft = damage - player.CurrentShield;

                            if (player.CurrentShield > 0)
                            {
                                player.CurrentShield -= damage;
                            }
                            if (damageLeft > 0)
                            {
                                player.CurrentShield = 0;
                                player.CurrentHealth -= damageLeft;
                            }

                            Players[CurrentPlayer].Experience += damage;
                            return playerCollisionPoint;
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }

        // Detect collision between projectile and ground
        private Vector2 CheckTerrainCollision(Projectlie currProj)
        {
            if (currProj.ProjectilePosition.X > 0 && currProj.ProjectilePosition.X < Terrain.terrainContour.Length - 1)
            {
                if ((int)currProj.ProjectilePosition.Y >= Terrain.terrainContour[(int)currProj.ProjectilePosition.X])
                {
                    return new Vector2(currProj.ProjectilePosition.X, currProj.ProjectilePosition.Y);
                }
            }

            return new Vector2(-1, -1);
        }

        // Check if the projectile is out of screen
        private bool CheckOutOfScreen(Projectlie currProj)
        {
            bool rocketOutOfScreen = currProj.ProjectilePosition.Y > 1024;

            return rocketOutOfScreen;
        }

        // Check for collision between two textures
        private Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }

        private Vector2 ObjectsCollide(Character player, Projectlie proj)
        {
            if (proj.ProjectilePosition.X > player.PositionX - player.Width / 2 &&
                proj.ProjectilePosition.X < player.PositionX + player.Width / 2)
            {
                if (proj.ProjectilePosition.Y < player.PositionY &&
                    proj.ProjectilePosition.Y > player.PositionY - player.Heigth)
                {
                    return new Vector2(player.PositionX, player.PositionY);
                }
            }

            return new Vector2(-1, -1);
        }

        // Change player turn
        private void NextPlayer()
        {
            currentPlayer = currentPlayer + 1;
            currentPlayer = currentPlayer % numberOfPlayers;

            while (!players[currentPlayer].IsAlive)
            {
                currentPlayer = ++currentPlayer % numberOfPlayers;
            }
        }
    }
}
