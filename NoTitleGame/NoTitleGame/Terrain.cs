using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoTitleGame
{
    class Terrain : GameWorld
    {
        // Store the y values of the terrain contour
        public static int[] terrainContour;
        // Declare random number generator
        private Random randomNumberGenerator;

        // Default constructor
        public Terrain(int width, int heigth)
            : base(width, heigth)
        {
            // Initialiaze random generator
            randomNumberGenerator = new Random();
        }

        // Methods
        // Generate terrain contour
        public void GenerateTerrainContour()
        {
            terrainContour = new int[this.Width];

            double rand1 = randomNumberGenerator.NextDouble() + 1;
            double rand2 = randomNumberGenerator.NextDouble() + 2;
            double rand3 = randomNumberGenerator.NextDouble() + 3;

            float offset = this.Height / 2;
            float peakheight = 100;
            float flatness = 70;

            for (int x = 0; x < this.Width; x++)
            {
                double height = peakheight / rand1 * Math.Sin((float)x / flatness * rand1 + rand1);
                height += peakheight / rand2 * Math.Sin((float)x / flatness * rand2 + rand2);
                height += peakheight / rand3 * Math.Sin((float)x / flatness * rand3 + rand3);
                height += offset;
                terrainContour[x] = (int)height;
            }
        }
    }
}
