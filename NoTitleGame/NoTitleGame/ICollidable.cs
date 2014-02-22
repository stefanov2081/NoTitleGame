using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoTitleGame
{
    interface ICollidable
    {
        // Store colour matrix of collidable objects
        Color[,] colourArray { get; set; }
    }
}
