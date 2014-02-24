namespace BonusItems
{
    class FullBodyShield : BonusItem
    {
        public FullBodyShield(int positionX, int positionY)
            : base(positionX, positionY, "Full body shield", Bonus.Shield, AMOUNT_OF_SHIELD_FROM_A_FULLBODYSHIELD) { }
    }
}