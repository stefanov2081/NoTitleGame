namespace Items
{
    using Microsoft.Xna.Framework.Graphics;
    using NoTitleGame;
    public abstract class Item : GameObject
    {
        //Only thing that all items have in common
        //Fields
        private Texture2D itemTexture;

        //Properties
        public Texture2D CharacterTexture
        {
            get { return this.itemTexture; }
            set { this.itemTexture = value; }
        }

        //Methods
        //TODO: 
        //Something about the textures in the constructor
        public Item(int positionX, int positionY, string name) : base(positionX, positionY, name) { }
    }
}
