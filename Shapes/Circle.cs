using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Circle : Shape
{
    public override int X
    {
        get => x;
        set => x = y = value;
    }

    public override int Y
    {
        get => y;
        set => x = y = value;
    }

    public Circle(int x)
    {
        X = x;
    }

    public override bool InsertIntoGrid(Grid grid)
    {
        bool canInsert = true;

        //Loop over rows
        for (int row = 0; row < grid.Y; row++)
        {
            //Loop over collumns
            for (int collumn = 0; collumn < grid.X; collumn++)
            {
                if (grid[row, collumn] != 0)
                    continue;

                //Check all the spaces the shape would take
                for (int y = 0; y < Y; y++)
                {
                    for (int x = 0; x < X; x++)
                    {
                        if (Math.Pow(x - (X / 2), 2) + Math.Pow(y - (X / 2), 2) < Math.Pow(X / 2, 2))
                            if (grid.grid[y + row, x + collumn] != 0)
                            {
                                canInsert = false;
                            }
                    }
                }

                if (canInsert)
                {
                    //Actually insert shape
                    for (int y = 0; y < Y; y++)
                    {
                        for (int x = 0; x < X; x++)
                        {
                            if (Math.Pow(x - (X / 2), 2) + Math.Pow(y - (X / 2), 2) < Math.Pow(X / 2, 2))
                                grid.grid[y + row, x + collumn] = 255;
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
        for (int row = 0; row < grid.Y; row++)
        {
            //Loop over collumns
            for (int collumn = 0; collumn < grid.X; collumn++)
            {
                if (grid[row, collumn] != 0)
                    continue;

                if (row + Y > grid.Y || collumn + X > grid.X)
                    continue;

                //Check all the spaces the shape would take
                for (int y = 0; y < Y; y++)
                {
                    for (int x = 0; x < X; x++)
                    {
                        if (Math.Pow(x - (X / 2), 2) + Math.Pow(y - (X / 2), 2) < Math.Pow(X / 2, 2))
                            if (grid.grid[y + row, x + collumn] != 0)
                            {
                                canInsert = false;
                            }
                    }
                }

                if (canInsert)
                {
                    //Actually insert shape
                    for (int y = 0; y < Y; y++)
                    {
                        for (int x = 0; x < X; x++)
                        {
                            if (Math.Pow(x - (X / 2), 2) + Math.Pow(y - (X / 2), 2) < Math.Pow(X / 2, 2))
                                grid.grid[y + row, x + collumn] = color;
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

