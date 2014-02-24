namespace ActiveItems
{
    class Bazooka : ActiveItem
    {
        public Bazooka(int positionX, int positionY) : base(positionX, positionY, "Bazooka", DAMAGE_DONE_BY_BAZOOKA, ActiveItemType.Bazooka) { }
    }
}
