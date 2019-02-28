using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Grid
{
    public int[,] grid;
    const int defaultValue = 0;

    public int this[int y, int x]
    {
        get
        {
            if (y < Y && x < X)
                return grid[y, x];
            else
                throw new IndexOutOfRangeException();
        }
        set
        {
            if (y < Y && x < X)
                grid[y, x] = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    public int Y
    {
        get => grid.GetLength(0);
        set
        {
            //Create temporary array with changed X
            int[,] temp = new int[value, grid.GetLength(1)];

            //Set temp to all 1s
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = defaultValue;
                }
            }

            int smallerX = (value < grid.GetLength(0) ? value : grid.GetLength(0));

            //For the values that overlap
            for (int i = 0; i < smallerX; i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //Copy old values
                    temp[i, j] = grid[i, j];
                }
            }

            grid = temp;
        }
    }

    public int X
    {
        get => grid.GetLength(1);
        set
        {
            //Create temporary array with changed X
            int[,] temp = new int[grid.GetLength(0), value];

            //Set temp to all 1s
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = defaultValue;
                }
            }

            int smallerY = (value < grid.GetLength(1) ? value : grid.GetLength(1));

            //For the values that overlap
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < smallerY; j++)
                {
                    //Copy old values
                    temp[i, j] = grid[i, j];
                }
            }

            grid = temp;
        }
    }

    public Grid(int x, int y)
    {
        grid = new int[y, x];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = defaultValue;
            }
        }
    }

    public void Draw()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void Trim()
    {
        int maxX = 0;
        int maxY = 0;

        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                if (grid[y, x] != 0 && y > maxY)
                    maxY = y;
            }
        }

        for (int y = 0; y < Y; y++)
        {
            for (int x = 0; x < X; x++)
            {
                if (grid[y, x] != 0 && x > maxX)
                    maxX = x;
            }
        }

        X = maxX;
        Y = maxY;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < Y; i++)
        {
            for (int j = 0; j < X; j++)
            {
                if (grid[i, j] != 0)
                    return false;
            }
        }

        return true;
    }
}