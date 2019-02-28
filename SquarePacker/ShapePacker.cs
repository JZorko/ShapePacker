using System;

namespace ShapePacker
{
    class ShapePacker
    {
        static void Main(string[] args)
        {
            Grid testGrid = new Grid(50, 50);

            /*
            Shape[] shapes = new Shape[6]
            {
                new Rectangle(5, 5),
                new Rectangle(3, 4),
                new Rectangle(4, 3),
                new Rectangle(5, 2),
                new Rectangle(2, 4),
                new Rectangle(1, 3)
            };
            */

            
            Shape[] shapes = new Shape[32];

            for (int i = 0; i < shapes.Length; i++)
            {
                shapes[i] = new Rectangle(2 * i, i);
            }
            

            foreach (Shape shape in shapes)
                if(!shape.InsertIntoGrid(testGrid))
                    Console.WriteLine("Can not insert shape " + shape.X + "x" + shape.Y);
            
            testGrid.Draw();

            Console.ReadKey(true);
        }
    }
}
