using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace life.Models
{
    public class GameGrid
    {
        public static int x = 32;
        public static int y = 32;
        public int milliseconds = 1000;
        public int[,] grid = new int[y, x];
        public bool isRunning = false;

        public void advance()
        {
            int[,] oldGrid = (int[,])grid.Clone();
            int neighbors = 0;

            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == grid.GetLength(0)-1 || j == grid.GetLength(1) - 1)
                    {
                        continue;
                    }

                    if (oldGrid[i, j - 1] == 1) neighbors++; //left neighbor
                    if (oldGrid[i - 1, j - 1] == 1) neighbors++; //top left neighbor
                    if (oldGrid[i - 1, j] == 1) neighbors++; //top neighbor
                    if (oldGrid[i - 1, j + 1] == 1) neighbors++; //top right neighbor
                    if (oldGrid[i, j + 1] == 1) neighbors++; //right neighbor
                    if (oldGrid[i + 1, j + 1] == 1) neighbors++; //bottom right neighbor
                    if (oldGrid[i + 1, j] == 1) neighbors++; //bottom neighbor
                    if (oldGrid[i + 1, j - 1] == 1) neighbors++; //bottom left neighbor

                    if (oldGrid[i,j] == 1)
                    {   //Active cell rules
                        if (neighbors < 2) grid[i, j] = 0;
                        else if (neighbors > 3) grid[i, j] = 0;
                        else grid[i, j] = 1;
                    } else
                    {   //Inactive cell rules
                        if (neighbors == 3) grid[i, j] = 1;
                    }

                    neighbors = 0;
                }
            }
        }

        public void run()
        {
            while (isRunning)
            {
                advance();
                System.Threading.Thread.Sleep(milliseconds);
            }
        }
    }
}
