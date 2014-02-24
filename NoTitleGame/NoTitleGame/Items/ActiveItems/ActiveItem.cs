namespace ActiveItems
{
    using Items;    
    //Active items are all items that can be fired - for example bazookas or shotguns
    public abstract class ActiveItem : Item
    {
        //Fields
        private int damage;
        private ActiveItemType type;

        //Properties
        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public ActiveItemType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        //Methods
        public ActiveItem(int positionX, int positionY, string name, int damage, ActiveItemType type) : base(positionX, positionY, name) 
        {
            this.Damage = damage;
            this.Type = type;
        }
    }
}
