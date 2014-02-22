namespace NoTitleGame
{
    public abstract class GameObject
    {
        // Fields store the position of the game object
        private int positionX;
        private int positionY;
        private string name;

        // Properties
        public int PositionX
        {
            get { return this.positionX; }
            set { this.positionX = value; }
        }

        public int PositionY
        {
            get { return this.positionY; }
            set { this.positionY = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        //Methods
        //Constructor
        public GameObject(int positionX, int positionY, string name)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Name = name;
        }
    }
}
