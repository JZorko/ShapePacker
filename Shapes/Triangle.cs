using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Triangle : Shape
{
    // X X X X
    // X X X
    // X X
    // X

    public override int X
    {
        get => x;
        set => x = value;
    }

    public override int Y
    {
        get => y;
        set => y = value;
    }

    public Triangle(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool InsertIntoGrid(Grid grid)
    {
        bool canInsert = true;

        //Loop over rows
        for (int i = 0; i < grid.Y; i++)
        {
            //Loop over collumns
            for (int j = 0; j < grid.X; j++)
            {
                if (grid[i, j] != 0)
                    continue;

                if (i + Y > grid.Y || j + X > grid.X)
                    continue;

                //Check all the spaces the shape would take
                for (int k = i; k < i + Y; k++)
                {
                    //for (int l = j; l < j + X; l++)
                    //x < ((y - Y) / -((double)Y / (double)X))
                    for (int l = j; l < j + ((k - Y) / -((double)Y / (double)X)); l++)
                    {
                        if (grid.grid[k, l] != 0)
                        {
                            canInsert = false;
                        }
                    }
                }

                if (canInsert)
                {
                    //Actually insert shape
                    for (int k = i; k < i + Y; k++)
                    {
                        for (int l = j; l < j + ((k - Y) / -((double)Y / (double)X)); l++)
                        {
                            grid.grid[k, l] = 255;
                        }
                    }

                    return true;
                }

                canInsert = true;
            }
        }

        return false;
    }

    public override bool InsertIntoGrid(Grid grid, int color)
    {
        bool canInsert = true;

        //Loop over rows
        for (int i = 0; i < grid.Y; i++)
        {
            //Loop over collumns
            for (int j = 0; j < grid.X; j++)
            {
                if (grid[i, j] != 0)
                    continue;

                if (i + Y > grid.Y || j + X > grid.X)
                    continue;

                //Check all the spaces the shape would take
                for (int k = i; k < i + Y; k++)
                {
                    //for (int l = j; l < j + X; l++)
                    //x < ((y - Y) / -((double)Y / (double)X))
                    for (int l = j; l < ((k - Y) / -((double)Y / (double)X)) + X; l++)
                    {
                        if (grid.grid[k, l] != 0)
                        {
                            canInsert = false;
                        }
                    }
                }

                if (canInsert)
                {
                    //Actually insert shape
                    for (int k = i; k < i + Y; k++)
                    {
                        for (int l = j; l < j + ((k - Y) / -((double)Y / (double)X)); l++)
                        {
                            grid.grid[k, l] = color % 256;
                        }
                    }

                    return true;
                }

                canInsert = true;
            }
        }

        return false;
    }
}

