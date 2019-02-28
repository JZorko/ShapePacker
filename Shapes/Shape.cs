using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Shape
{
    protected int x;
    protected int y;

    public abstract int X { get; set; }
    public abstract int Y { get; set; }


    public abstract bool InsertIntoGrid(Grid grid);

    public abstract bool InsertIntoGrid(Grid grid, int color);

    public void ChangeToLandscape()
    {
        if (X < Y)
        {
            int temp = X;
            X = Y;
            Y = temp;
        }
    }

    public void ChangeToPortrait()
    {
        if (Y < X)
        {
            int temp = X;
            X = Y;
            Y = temp;
        }
    }
}
