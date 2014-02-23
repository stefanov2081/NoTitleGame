namespace BonusItems
{
    class FullBodyShield : BonusItem
    {
        //The stat for improvement - shield in this case
        //Shold be set here maybe...?
        public FullBodyShield(int positionX, int positionY, string name, Bonus statToImprove, int valueOfImprovement)
            : base(positionX, positionY, name, statToImprove, valueOfImprovement) { }

    }
}