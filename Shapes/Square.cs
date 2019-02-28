using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Square : Rectangle
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


    public Square(int a) : base(a, a) { }
}

