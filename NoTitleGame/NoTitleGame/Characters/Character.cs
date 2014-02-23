namespace Characters
{
    using System;
    using NoTitleGame;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    class Character : GameObject, IAnimate
    {
        // Default terrain sprite
        private Texture2D characterTexture;
        //Fields
        private int strength;       //affects health
        private int health;         //health pounts    5 strength = 10 health
        private int armor;          //mitigates damage 5 agility = 2 armor
        private int agility;        //affects armor
        private int shield;         //is the same as health
        private int intelligence;   //affects mana
        private int mana;           //needed for skills 5 intelligence = 5 mana
        private int level;          //self-explanatory
        private int experience;     //needed for leveling
        //private bool isAlive; //no need for a field it's only a property

        // IAnimate automatic fields
        public int frames { get; set; }
        public float elapsed { get; set; }
        public float scale { get; set; }
        public bool facingRight { get; set; }
        public Rectangle sourceRect { get; set; }

        //Properties
        public Texture2D CharacterTexture
        {
            get { return this.characterTexture; }
            set { this.characterTexture = value; }
        }
        public int Strength 
        {
            get { return this.strength; }
            set { this.strength = value; } 
        }
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }
        public int Armor
        {
            get { return this.armor; }
            set { this.armor = value; }
        }
        public int Agility
        {
            get { return this.agility; }
            set { this.agility = value; }
        }
        public int Shield
        {
            get { return this.shield; }
            set { this.shield = value; }
        }
        public int Intelligence
        {
            get { return this.intelligence; }
            set { this.intelligence = value; }
        }
        public int Mana
        {
            get { return this.mana; }
            set { this.mana = value; }
        }
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        public int Experience
        {
            get { return this.experience; }
            set { this.experience = value; }
        }
        public bool IsAlive
        {
            get { return this.health > 0; }
        }

        //Methods
        // Set the character on random position
        public void SetOnRadnomPosition()
        {
            Random rand = new Random();

            this.PositionX = rand.Next(0, Terrain.terrainContour.Length);
            this.PositionY = Terrain.terrainContour[PositionX];
        }

        // Animate character when idle
        public void AnimateCharacterIdle(float delay, GameTime gameTime = null)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsed >= delay)
            {
                if (this.frames >= 3)
                {
                    this.frames = 0;
                }
                else
                {
                    this.frames++;
                }

                this.elapsed = 0;
            }

            if (facingRight)
            {
                this.sourceRect = new Rectangle(58 * frames, 10, 58, 55);
            }
            else
            {
                this.sourceRect = new Rectangle(232 + 58 * frames, 10, 58, 55);
            }
        }

        //Constructor
        public Character(int positionX, int positionY, string name, int strength, int agility,
                         int shield, int intelligence, int level, int experience)
            : base(positionX, positionY, name)
        {
            //Setting main stats
            this.Strength = strength;
            this.Agility = agility;
            this.Intelligence = intelligence;
            this.Shield = shield;

            //Setting dependables
            this.Health = this.Strength * 2;
            this.Armor = (this.Agility / 5) * 2;
            this.Mana = this.Intelligence;

            //Level-related
            this.Experience = experience;
            this.Level = level;

            // Animation-related
            this.facingRight = true;
            this.elapsed = 0;
            this.frames = 0;
        }
        //Constructor---
    }
}
