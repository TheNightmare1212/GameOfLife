using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;

namespace GameOfLife
{
    public class Game
    {
        Cell[,] environment;
        int lifeParam = 3;
        int generation = 1;

        public int Generation { get { return generation; } }

        readonly List<string> states = new();

        public void SetEnvironment(Cell[,] inputEnv)
        {
            environment = inputEnv;
        }

        readonly IDrawer drawer;
        public Game()
        {

        }

        public Game(IDrawer drawingMethod, int lifeParam = 3)
        {
            drawer = drawingMethod;
            this.lifeParam = lifeParam;
        }

        public void Draw()
        {
            drawer.Draw(environment);
        }

        public void Defenvironment()
        {
            environment = new Cell[0, 0];
            Random r = new((int)(DateTime.Now.Ticks));
            switch (r.Next(0,3))
            {
                case 0:
                    {
                        //палка(5х5)
                        environment = new Cell[5, 5];
                        SetBlank(ref environment);

                        environment[1, 2] = new Cell(true);
                        environment[2, 2] = new Cell(true);
                        environment[3, 2] = new Cell(true);
                        break;
                    }
                case 1:
                    {
                        //Птичка(7х30)
                        environment = new Cell[7, 30];
                        SetBlank(ref environment);

                        environment[4, 1] = new Cell(true);
                        environment[5, 2] = new Cell(true);
                        environment[5, 3] = new Cell(true);
                        environment[5, 4] = new Cell(true);
                        environment[5, 5] = new Cell(true);
                        environment[5, 7] = new Cell(true);
                        environment[5, 6] = new Cell(true);
                        environment[4, 7] = new Cell(true);
                        environment[3, 7] = new Cell(true);
                        environment[2, 6] = new Cell(true);
                        break;
                    }
                case 2:
                    {
                        // два пистолетика (31х31)
                        environment = new Cell[31, 31];
                        SetBlank(ref environment);

                        environment[16, 12] = new Cell(true);
                        environment[16, 13] = new Cell(true);
                        environment[16, 14] = new Cell(true);
                        environment[15, 13] = new Cell(true);
                        environment[16, 18] = new Cell(true);
                        environment[16, 19] = new Cell(true);
                        environment[16, 20] = new Cell(true);
                        environment[15, 19] = new Cell(true);
                        break;
                    }
            }
        }

        
        public static void SetBlank(ref Cell[,] x)
        {
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    x[i, j] = new Cell(false);
                }
            }

        }


        public bool ToEnd()
        {
            while (OneStep())
            {
 
            }
            return false;
        }

        //проверка состояния на уникальность
        public bool IsNotCycle()
        {

            string state = FileWorker.GetState(environment);

            if (states.Contains(state))  
            {
                return false;
            }
            else
            {
                states.Add(state);
                generation++;
                return true;

            }  
        }

        public bool OneStep()
        {
            if (IsNotCycle())
            {
                environment = NextGen();
                Draw();
                return true;
            }
            return false;
        }


        public Cell[,] NextGen()
        {
            Cell[,] newGen = new Cell[environment.GetLength(0), environment.GetLength(1)];
            SetBlank(ref newGen);

            for (int i = 0; i < environment.GetLength(0); i++)
            {
                for (int j = 0; j < environment.GetLength(1); j++)
                {
                    newGen[i, j] = new Cell(environment, (i, j),lifeParam);
                }
            }
            return newGen;
        }
    }
}
