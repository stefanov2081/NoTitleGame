namespace Characters
{
    using System;
    using NoTitleGame;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    
    public abstract class Character : GameObject, IAnimate, IMoveable
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
        
        //Variables needed for jumping
        private bool isJumping;     //Determines wether the char is jumping
        private int jumpspeed;      //How fast the char jumps

        // IAnimate states
        private bool idle = true;
        private bool running;

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
        public int Jumpspeed
        {
            get { return this.jumpspeed; }
            set { this.jumpspeed = value; }
        }
        public bool IsAlive
        {
            get { return this.health > 0; }
        }
        public bool IsJumping
        {
            get { return this.isJumping; }
            set { this.isJumping = value; }
        }

        //Constructor
        public Character(int positionX, int positionY, string name, int strength, int agility,
                         int intelligence, int shield, int level, int experience)
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

            //Jump-related
            this.IsJumping = false;
            this.Jumpspeed = 0;
        }
        //Constructor---

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
                this.sourceRect = new Rectangle(406 - 58 * frames, 10, 58, 55);
            }
        }

        // Animate character when moving
        public void AnimateCharacterRun(float delay, GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsed >= delay)
            {
                if (this.frames >= 7)
                {
                    this.frames = 0;
                }
                else
                {
                    this.frames++;
                }

                this.elapsed = 0;
            }

            if (this.facingRight)
            {
                this.sourceRect = new Rectangle(58 * frames, 143, 58, 55);
            }
            else
            {
                this.sourceRect = new Rectangle(406 - 58 * frames, 220, 58, 55);
            }
        }

        // Animate character when jumping
        public void AnimateCharacterJump(float delay, GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsed >= delay)
            {
                if (this.frames >= 7)
                {
                    this.frames = 0;
                }
                else
                {
                    this.frames++;
                }

                this.elapsed = 0;

                if (facingRight)
                {
                    this.sourceRect = new Rectangle(58 * frames, 790, 58, 55);
                }
                else
                {
                    //this.sourceRect = new Rectangle(928 - 58 * frames, 790, 58, 55);
                    this.sourceRect = new Rectangle(870 - 58 * frames, 790, 58, 55);
                }
            }
        }

        // Proccess animations
        public void ProccessAnimations(float delay = 200, GameTime gameTime = null)
        {
            if (this.idle)
            {
                AnimateCharacterIdle(delay, gameTime);
            }
            if (this.running)
            {
                AnimateCharacterRun(delay, gameTime);
            }
            if (this.IsJumping)
            {
                AnimateCharacterJump(delay, gameTime);
            }
        }

        //Moving the character
        public void Move()
        {
            KeyboardState keybState = Keyboard.GetState();

            // Idle
            if (keybState.GetPressedKeys().Length == 0 && !IsJumping)
            {
                this.idle = true;
                this.running = false;
            }
            //Move left
            if (keybState.IsKeyDown(Keys.Left))
            {
                this.idle = false;
                this.facingRight = false;
                if (!this.IsJumping)
                {
                    this.running = true;
                }

                this.PositionX -= 5;

                if (this.PositionY != Terrain.terrainContour[(int)this.PositionX] && this.IsJumping == false)
                {
                    this.PositionY = Terrain.terrainContour[(int)this.PositionX];
                }
            }
            
            //Move right
            if (keybState.IsKeyDown(Keys.Right))
            {
                this.idle = false;
                this.facingRight = true;
                if (!this.IsJumping)
                {
                    this.running = true;
                }

                this.PositionX += 5;

                if (this.PositionY != Terrain.terrainContour[(int)this.PositionX] && this.IsJumping == false)
                {
                    this.PositionY = Terrain.terrainContour[(int)this.PositionX];
                }
            }

            //Jumping
            if (this.IsJumping)
            {
                this.idle = false;
                this.running = false;
                this.PositionY += this.jumpspeed;   //Make the char go up
                this.jumpspeed += 1;                //Needed for the character to fall down
                if (this.PositionY >= Terrain.terrainContour[(int)this.PositionX])
                //If the char is farther than ground
                {
                    this.PositionY = Terrain.terrainContour[(int)this.PositionX];//Then set it on the ground
                    this.IsJumping = false;
                }

            }

            else
            {
                if (keybState.IsKeyDown(Keys.Up))
                {
                    this.IsJumping = true;
                    this.jumpspeed = -14;       //Give the char an upward thrust
                }
            }
        }
    }
}
