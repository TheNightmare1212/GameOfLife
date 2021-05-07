using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Tests
{
    [TestClass()]
    public class GameTests
    {

        [TestMethod()]
            public void NextGenTest()
        {
            var game = new Game(); 

            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);
            test[1, 2] = new Cell(true);
            test[2, 2] = new Cell(true);
            test[3, 2] = new Cell(true);

            game.SetEnvironment(test);
            test = game.NextGen();


            Cell[,] expected = new Cell[5, 5];
            Game.SetBlank(ref expected);

            expected[2, 1] = new Cell(true);
            expected[2, 2] = new Cell(true);
            expected[2, 3] = new Cell(true);

            Assert.AreEqual(FileWorker.GetState(test), FileWorker.GetState(expected));

        }
        [TestMethod()]
        public void NextGenTestGarbage()
        {
            Cell[,] test = new Cell[2,2];

            test[0, 0] = new Cell(true);
            var game = new Game();
            game.SetEnvironment(test);

            test = game.NextGen();

            var expected = new Cell[2, 2];
            Game.SetBlank(ref expected);

            Assert.AreEqual(FileWorker.GetState(test), FileWorker.GetState(expected));


        }
    }
}