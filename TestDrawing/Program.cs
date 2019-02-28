using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrawing
{
    class Program
    {
        static void Main(string[] args)
        {
            int X = 20;
            int Y = 10;

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < ((y - Y) / -((double)Y / (double)X)); x++)
                {
                    Console.Write("0 ");
                }

                Console.WriteLine();
            }

            Console.ReadKey(true);
        }
    }
}
