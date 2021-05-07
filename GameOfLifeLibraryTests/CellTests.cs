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
    public class CellTests
    {
        [TestMethod()]
        public void CellDeadThreeNbrs()
        {
            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);
            test[0, 1] = new Cell(true);
            test[1, 1] = new Cell(true);
            test[1, 0] = new Cell(true);


            var actual = new Cell(test,(0,0));
         
            Assert.AreEqual(true, actual.IsAlive);

        }
        [TestMethod()]
        public void CellDeadFourNbrs()
        {
            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);
            test[0, 1] = new Cell(true);
            test[1, 0] = new Cell(true);
            test[1, 2] = new Cell(true);
            test[2, 1] = new Cell(true);


            var actual = new Cell(test, (1, 1));

            Assert.AreEqual(false, actual.IsAlive);

        }
        [TestMethod()]
        public void CellDeadTwoNbrs()
        {
            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);
            test[0, 1] = new Cell(true);
            test[1, 1] = new Cell(true);


            var actual = new Cell(test, (0, 0));

            Assert.AreEqual(false, actual.IsAlive);

        }


        [TestMethod()]
        public void CellAliveThreeandTwoNbrs()
        {
            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);
            test[0, 1] = new Cell(true);
            test[1, 1] = new Cell(true);
            test[2, 1] = new Cell(true);



            var actual = new Cell(test, (1, 1)); //2 соседа


            test[0, 0] = new Cell(true);

            var actual2 = new Cell(test, (0, 0)); //3 соседа

            Assert.IsTrue(actual.IsAlive);
            Assert.IsTrue(actual2.IsAlive);

        }



        [TestMethod()]
        
        public void CellAliveOneNbr()
        {
            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);

            test[0, 0] = new Cell(true);
            test[1, 1] = new Cell(true);

            var actual = new Cell(test, (1, 1));

            Assert.IsFalse(actual.IsAlive);
        }
        [TestMethod()]
        public void CellAliveFourNbrs()
        {
            Cell[,] test = new Cell[5, 5];
            Game.SetBlank(ref test);

            test[0, 0] = new Cell(true);
            test[1, 1] = new Cell(true);
            test[2, 2] = new Cell(true);
            test[0, 2] = new Cell(true);
            test[2, 0] = new Cell(true);

            var actual = new Cell(test, (1, 1));

            Assert.IsFalse(actual.IsAlive);
        }
    }
}