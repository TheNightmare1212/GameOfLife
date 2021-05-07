using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife;

namespace GameOfLife
{
    class ConsoleDrawer : IDrawer
    {

        string aliveSymbol;
        string deadSymbol = " ";
        public ConsoleDrawer(char aliveSymbol) 
        {
            this.aliveSymbol = aliveSymbol.ToString();
        }
        public void Draw(Cell[,] x)
        {
            Console.Clear();

            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(x[i,j]!=null && x[i, j].IsAlive? aliveSymbol : deadSymbol);
                }
                Console.WriteLine();
            }
        }
    }
}
