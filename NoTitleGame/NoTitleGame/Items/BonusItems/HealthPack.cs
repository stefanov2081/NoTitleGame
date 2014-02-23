namespace BonusItems
{
    class HealthPack : BonusItem
    {
        //The stat for improvement - health in this case
        //Shold be set here maybe...?
        public HealthPack(int positionX, int positionY, string name, Bonus statToImprove, int valueOfImprovement)
            : base(positionX, positionY, name, statToImprove, valueOfImprovement) { }

    }
}
