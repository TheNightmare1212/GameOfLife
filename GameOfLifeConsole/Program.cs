using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using GameOfLife;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserInteraction NewGame = new ConsoleInteraction();
            NewGame.StartInteraction(args);
        }
    }
}
