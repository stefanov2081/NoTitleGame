namespace Items
{
    using Microsoft.Xna.Framework.Graphics;
    using NoTitleGame;
    public abstract class Item : GameObject
    {
        //CONSTANTS
        protected const int DAMAGE_DONE_BY_BAZOOKA = 20;
        protected const int DAMAGE_DONE_BY_SHOTGUN = 10;
        protected const int AMOUNT_OF_HEALTH_FROM_A_HEALTHPACK = 25;
        protected const int AMOUNT_OF_SHIELD_FROM_A_FULLBODYSHIELD = 40;

        //Fields
        private Texture2D itemTexture;

        //Properties
        public Texture2D CharacterTexture
        {
            get { return this.itemTexture; }
            set { this.itemTexture = value; }
        }

        //Methods
        //Constructor
        public Item(int positionX, int positionY, string name) : base(positionX, positionY, name) { }
    }
}
