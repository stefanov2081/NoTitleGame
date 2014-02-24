namespace BonusItems
{
    class HealthPack : BonusItem
    {
        public HealthPack(int positionX, int positionY)
            : base(positionX, positionY, "Health pack", Bonus.Health, AMOUNT_OF_HEALTH_FROM_A_HEALTHPACK) { }
    }
}
