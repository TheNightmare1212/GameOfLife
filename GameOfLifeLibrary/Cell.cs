using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Cell
    {
        int lifeParam;

        public override string ToString()
        {
            return isAlive.ToString();
        }

        //конструктор для инициализации расстановки
        public Cell(bool isAlive)
        {
            this.isAlive = isAlive;
        }

        bool isAlive;
        public bool IsAlive { get { return isAlive; } }

        public Cell(Cell[,] xy, (int y, int x) point, int lifeParam = 3)
        {
            int x = point.x;
            int y = point.y;
            if (y <= xy.GetLength(0) && x <= xy.GetLength(1))
            {
                if (xy[y, x] == null)
                {
                    xy[y, x] = new Cell(false);
                }
                this.lifeParam = lifeParam;
                int neighbourCount=0;
                
                //определение границ соседей
                var Yinc = (y + 1) % xy.GetLength(0);
                var Ydec = (xy.GetLength(0) + y - 1) % xy.GetLength(0);
                var Xinc = (x + 1) % xy.GetLength(1);
                var Xdec = (xy.GetLength(1) + x - 1) % xy.GetLength(1);
                
                //пересчет живых соседей
                if (xy[Yinc, Xdec] != null && xy[Yinc, Xdec].IsAlive) //снизу слева
                {
                    neighbourCount++;
                }

                if (xy[Yinc, x] != null && xy[Yinc, x].IsAlive) //снизу
                {
                    neighbourCount++;
                }    

                if (xy[Yinc, Xinc] != null && xy[Yinc, Xinc].IsAlive)  //снизу справа
                {
                    neighbourCount++;
                }

                if (xy[y, Xdec] != null && xy[y, Xdec].IsAlive)  //слева
                {
                    neighbourCount++;
                }  

                if (xy[y, Xinc] != null && xy[y, Xinc].IsAlive) //справа
                {
                    neighbourCount++;
                } 

                if (xy[Ydec, Xdec] != null && xy[Ydec, Xdec].IsAlive) //сверху слева
                {
                    neighbourCount++;
                } 

                if (xy[Ydec, x] != null && xy[Ydec, x].IsAlive)  //сверху
                {
                    neighbourCount++;
                }  

                if (xy[Ydec, Xinc] != null && xy[Ydec, Xinc].IsAlive) //сверху справа
                {
                    neighbourCount++;
                } 

                //выбор состояния клетки относительно количества соседей

                if ( !xy[y, x].IsAlive & neighbourCount == lifeParam)
                {
                    isAlive = true;
                }
                else
                {
                    if (xy[y, x].IsAlive & (neighbourCount == lifeParam - 1 || neighbourCount == lifeParam))
                    {
                        isAlive = true;
                    }
                    else
                    {
                        isAlive = false;
                    }
                }
            }

        }
    }
}
