public class Rectangle : Shape
{
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


    public Rectangle(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Rectangle(int x) : this(x, x) { }


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
                    for (int l = j; l < j + X; l++)
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
                        for (int l = j; l < j + X; l++)
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
                    for (int l = j; l < j + X; l++)
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
                        for (int l = j; l < j + X; l++)
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
