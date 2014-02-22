using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoTitleGame
{
    public abstract class GamObject
    {
        // Fields store the position of the game object
        private int positionX;
        private int positionY;

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

        abstract void Move();
    }
}
