using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Threading;
using System.Windows.Media.Imaging;

namespace GameOfLifeWpf
{
    class WPFDrawer : IDrawer
    {
        MainWindow main;
        int delay;
        public WPFDrawer(MainWindow wherePrint, int delay)
        {
            main = wherePrint;
            this.delay = delay;
        }
       
        public void Draw(Cell[,] map)
        {
            //передача управления главному потоку для отрисовки
            main.Dispatcher.Invoke(
                      () =>
                  {
                      main.MainGrid.Children.Clear();
                      main.MainGrid.ColumnDefinitions.Clear();
                      main.MainGrid.RowDefinitions.Clear();

                      for (int i = 0; i < map.GetLength(1); i++)
                      {
                          main.MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                      }

                      for (int i = 0; i < map.GetLength(0); i++)
                      {
                          main.MainGrid.RowDefinitions.Add(new RowDefinition());
                          for (int j = 0; j < map.GetLength(1); j++)
                          {
                              if (map[i, j].IsAlive)
                              {

                                  var rect = new Rectangle();
                                  rect.Fill = Brushes.CadetBlue;
                                  
                                  Grid.SetRow(rect, i);
                                  Grid.SetColumn(rect, j);
                                  main.MainGrid.Children.Add(rect);


                                  var img = new Image();
                                  img.Source = new BitmapImage(new Uri(@"https://www.pngrepo.com/png/184896/180/cell.png"));
                                  img.Stretch = Stretch.Fill;

                                  Grid.SetRow(img, i);
                                  Grid.SetColumn(img, j);
                                  main.MainGrid.Children.Add(img);
                              }
                          }
                      }
                  }
                );

            //пауза между отрисовками         
            Thread.Sleep(delay);
        }
    }
}
