namespace Interfaces
{
    using Microsoft.Xna.Framework;
    interface ICollidable
    {
        // Store colour matrix of collidable objects
        Color[,] colourArray { get; set; }
    }
}
