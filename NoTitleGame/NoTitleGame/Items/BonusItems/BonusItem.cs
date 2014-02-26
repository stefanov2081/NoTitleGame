namespace BonusItems
{
    using Items;   
    //Bonus items are all items that give bonuses - for example health pack, or full body shield
    public abstract class BonusItem : Item
    {
        //Fields
        Bonus statToImprove;        //Which stat the bonus upgrades
        int valueOfImprovement;     //The value with which it increases it
        
        //Properties
        public Bonus StatToImprove
        {
            get { return this.statToImprove; }
            set { this.statToImprove = value; }
        }
        public int ValueOfImprovement
        {
            get { return this.valueOfImprovement; }
            set { this.valueOfImprovement = value; }
        }

        //Methods
        public BonusItem(int positionX, int positionY, string name, Bonus statToImprove, int valueOfImprovement)
            : base(positionX, positionY, name) 
        {
            this.StatToImprove = statToImprove;
            this.ValueOfImprovement = valueOfImprovement;
        }
    }
}
