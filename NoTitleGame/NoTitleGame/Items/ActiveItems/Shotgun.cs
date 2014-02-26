namespace ActiveItems
{
    class Shotgun : ActiveItem
    {
        public Shotgun(int positionX, int positionY) : base(positionX, positionY, "Shotgun", DAMAGE_DONE_BY_SHOTGUN, ActiveItemType.Shotgun) { }
    }
}