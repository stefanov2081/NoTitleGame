namespace Characters
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using ActiveItems;
    using Exceptions;
    using NoTitleGame;
    using Interfaces;
    using Microsoft.Xna.Framework.Audio;

    public abstract class Character : GameObject, IAnimate, IMoveable, ISound
    {
        //CONSTANTS
        private const int DEFAULT_NUMBER_OF_BAZOOKAS = 1;
        private const int DEFAULT_NUMBER_OF_SHOTGUNS = 2;

        // Default terrain sprite
        private Texture2D characterTexture;
        private Color[,] colourArray;
        // Sounds
        private SoundEffect jumpSound;

        //Fields
        private int strength;                   //affects health
        private int maxHealth;                  //health pounts    5 strength = 10 health
        private int currentHealth;              // Stores the current health of the character
        private int armor;                      //mitigates damage 5 agility = 2 armor
        private int agility;                    //affects armor
        private int currentShield;              //is the same as health
        private int maxShield;                  // Stores the max shield of the character
        private int intelligence;               //affects mana
        private int maxMana;                    //needed for skills 5 intelligence = 5 mana
        private int currentMana;                // Stores the current mana of the character
        private int level;                      //self-explanatory
        private int experience;                 //needed for leveling
        public List<ActiveItem> Inventory;      //how many weapons the char has
        private ActiveItemType selectedWeapon;  //which weapon the char will fire
        private float angle;                    //the angle with which the char fires
        private float power;                    //the power with which the char fires
        private bool isAlive;                   //self-explanatory

        //Variables needed for jumping
        private bool isJumping;     //Determines wether the char is jumping
        private int jumpspeed;      //How fast the char jumps

        // IAnimate states
        private bool idle = true;
        private bool running;

        // IAnimate automatic fields
        public int Frames { get; set; }
        public float Elapsed { get; set; }
        public float Scale { get; set; }
        public bool FacingRight { get; set; }
        public Rectangle SourceRect { get; set; }

        // Keyboard state fields
        private KeyboardState keybState;
        private KeyboardState previousKeyboardState;

        //Properties
        public Texture2D CharacterTexture
        {
            get { return this.characterTexture; }
            set { this.characterTexture = value; }
        }
        public SoundEffect JumpSound
        {
            get { return this.jumpSound; }
            set { this.jumpSound = value; }
        }
        public Color[,] ColourArray
        {
            get { return this.colourArray; }
            set { this.colourArray = value; }
        }
        public int Strength
        {
            get { return this.strength; }
            set { this.strength = value; }
        }
        public int MaxHealth
        {
            get { return this.maxHealth; }
            set { this.maxHealth = value; }
        }
        public int CurrentHealth
        {
            get { return this.currentHealth; }
            set { this.currentHealth = value; }
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
        public int CurrentShield
        {
            get { return this.currentShield; }
            set { this.currentShield = value; }
        }
        public int MaxShield
        {
            get { return this.maxShield; }
            set { this.maxShield = value; }
        }
        public int Intelligence
        {
            get { return this.intelligence; }
            set { this.intelligence = value; }
        }
        public int MaxMana
        {
            get { return this.maxMana; }
            set { this.maxMana = value; }
        }
        public int CurrentMana
        {
            get { return this.currentMana; }
            set { this.currentMana = value; }
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
            get { return this.isAlive; }
            set { this.isAlive = value; }
        }
        public bool IsJumping
        {
            get { return this.isJumping; }
            set { this.isJumping = value; }
        }
        public ActiveItemType SelectedWeapon
        {
            get { return this.selectedWeapon; }
            set { this.selectedWeapon = value; }
        }

        public float Angle
        {
            get { return this.angle; }
            set { this.angle = value; }
        }

        public float Power
        {
            get { return this.power; }
            set { this.power = value; }
        }

        //Constructor
        public Character(int positionX, int positionY, string name, int strength, int agility,
                         int intelligence, int shield, int level, int experience, int amountOfBazookas = DEFAULT_NUMBER_OF_BAZOOKAS, int amountOfShotguns = DEFAULT_NUMBER_OF_SHOTGUNS)
            : base(positionX, positionY, name)
        {
            //Setting main stats
            this.Strength = strength;
            this.Agility = agility;
            this.Intelligence = intelligence;
            this.CurrentShield = shield;
            this.MaxShield = this.CurrentShield;

            //Setting dependables
            this.MaxHealth = this.Strength * 2;
            this.CurrentHealth = this.MaxHealth;
            this.Armor = (this.Agility / 5) * 2;
            this.MaxMana = this.Intelligence;
            this.CurrentMana = this.MaxMana;

            //Level-related
            this.Experience = experience;
            this.Level = level;

            // Animation-related
            this.FacingRight = true;
            this.Elapsed = 0;
            this.Frames = 0;

            //Jump-related
            this.IsJumping = false;
            this.Jumpspeed = 0;

            //Inventory-related
            this.Inventory = new List<ActiveItem>();
            LoadActiveItem(DEFAULT_NUMBER_OF_BAZOOKAS, ActiveItemType.Bazooka);
            LoadActiveItem(DEFAULT_NUMBER_OF_SHOTGUNS, ActiveItemType.Shotgun);

            this.IsAlive = true;
        }

        // Empty constructor
        public Character()
        {
        }

        //Constructor---

        //Methods
        //Fill inventory with weapons
        private void LoadActiveItem(int amount, ActiveItemType type)
        {
            //Initialize
            ActiveItem itemToAdd = null;
            //Check what to add
            if (type == ActiveItemType.Bazooka)
            {
                itemToAdd = new Bazooka(this.PositionX, this.PositionY);
            }
            if (type == ActiveItemType.Shotgun)
            {
                itemToAdd = new Shotgun(this.PositionX, this.PositionY);
            }
            //Add it amount many times
            for (int i = 0; i < amount; i++)
            {
                this.Inventory.Add(itemToAdd);
            }
        }

        //Needed to move the char's inventory with him
        public void UpdateSelectedWeaponPosition()
        {
            foreach (var item in this.Inventory)
            {
                if (item.Type == this.SelectedWeapon)
                {
                    item.PositionX = this.PositionX;
                    item.PositionY = this.PositionY;
                }
            }
        }

        // Set the character on random position
        public void SetOnRadnomPosition(Random random)
        {
            this.PositionX = random.Next(0, Terrain.terrainContour.Length);
            this.PositionY = Terrain.terrainContour[PositionX];
        }

        // Animate character when idle
        public void AnimateCharacterIdle(float delay, GameTime gameTime = null)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Elapsed >= delay)
            {
                if (this.Frames >= 3)
                {
                    this.Frames = 0;
                }
                else
                {
                    this.Frames++;
                }

                this.Elapsed = 0;
            }

            if (FacingRight)
            {
                this.SourceRect = new Rectangle(58 * Frames, 10, 58, 55);
            }
            else
            {
                this.SourceRect = new Rectangle(406 - 58 * Frames, 10, 58, 55);
            }
        }

        // Animate character when moving
        public void AnimateCharacterRun(float delay, GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Elapsed >= delay)
            {
                if (this.Frames >= 7)
                {
                    this.Frames = 0;
                }
                else
                {
                    this.Frames++;
                }

                this.Elapsed = 0;
            }

            if (this.FacingRight)
            {
                this.SourceRect = new Rectangle(58 * Frames, 143, 58, 55);
            }
            else
            {
                this.SourceRect = new Rectangle(406 - 58 * Frames, 220, 58, 55);
            }
        }

        // Animate character when jumping
        public void AnimateCharacterJump(float delay, GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Elapsed >= delay)
            {
                if (this.Frames >= 7)
                {
                    this.Frames = 0;
                }
                else
                {
                    this.Frames++;
                }

                this.Elapsed = 0;

                if (FacingRight)
                {
                    this.SourceRect = new Rectangle(58 * Frames, 790, 58, 55);
                }
                else
                {
                    //this.sourceRect = new Rectangle(928 - 58 * frames, 790, 58, 55);
                    this.SourceRect = new Rectangle(870 - 58 * Frames, 790, 58, 55);
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

        // Play sound
        public void PlaySound(SoundEffect sound)
        {
            sound.Play();
        }

        // Read user input from keyboard
        public void ProcessKeyboard(Character currChar, Projectlie currentProj)
        {
            // We will need this for later, when fireing weapons
            this.previousKeyboardState = keybState;
            this.keybState = Keyboard.GetState();

            try
            {
                this.Move();
                this.Jump();
                this.RotateWeaponAngle(currChar);
                this.Fire(currChar, currentProj);
            }
            catch (IndexOutOfRangeException)
            {
                throw new CharacterHasDiedException("Your character has died");
            }
        }

        //Moving the character
        public void Move()
        {
            // Idle
            if (this.keybState.GetPressedKeys().Length == 0 && !IsJumping)
            {
                this.idle = true;
                this.running = false;
            }

            //Move left
            if (this.keybState.IsKeyDown(Keys.Left))
            {
                this.idle = false;
                this.FacingRight = false;
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
            if (this.keybState.IsKeyDown(Keys.Right))
            {
                this.idle = false;
                this.FacingRight = true;
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
        }

        public void Jump()
        {
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
                if (this.keybState.IsKeyDown(Keys.Enter))
                {
                    PlaySound(JumpSound);
                    this.IsJumping = true;
                    this.jumpspeed = -14;       //Give the char an upward thrust
                }
            }
        }

        // Fire weapon
        public void Fire(Character currentCharacter, Projectlie currentProjectile)
        {
            if (this.previousKeyboardState.IsKeyDown(Keys.Space) && this.keybState.IsKeyUp(Keys.Space) || currentCharacter.Power >= 1000)
            {
                currentProjectile.FireProjectile(currentCharacter);

                currentCharacter.Power = 0;
            }

            if (this.keybState.IsKeyDown(Keys.Space))
            {
                currentCharacter.Power += 2;

                if (currentCharacter.Power >= 1000)
                {
                    currentProjectile.FireProjectile(currentCharacter);
                }
            }
        }

        public void RotateWeaponAngle(Character currChar)
        {
            if (keybState.IsKeyDown(Keys.Up))
            {
                currChar.Angle -= 0.01f;
            }
            if (keybState.IsKeyDown(Keys.Down))
            {
                currChar.Angle += 0.01f;
            }
        }
    }
}
