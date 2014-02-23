namespace ActiveItems
{
    using Items;    
    //Active items are all items that can be fired - for example bazookas or shotguns
    public abstract class ActiveItem : Item
    {
        //Fields
        private int damage;

        //Properties
        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        //Methods
        public ActiveItem(int positionX, int positionY, string name, int damage) : base(positionX, positionY, name) 
        {
            this.Damage = damage;
        }
    }
}
