using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoTitleGame
{
    public static class TextureToArray
    {
        // Methods
        // Get texture to array
        public static Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colours1D = new Color[texture.Width * texture.Height];
            texture.GetData(colours1D);

            Color[,] colours2D = new Color[texture.Width, texture.Height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    colours2D[x, y] = colours1D[x + y * texture.Width];
                }
            }

            return colours2D;
        }
    }
}
