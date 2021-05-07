using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfLife;
using Microsoft.Win32;

namespace FieldMaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cell[,] map;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            map = new Cell[Convert.ToInt32(Height.Text), Convert.ToInt32(Width.Text)];
            Game.SetBlank(ref map);
            
            Draw();
           
        }

        public void Draw() 
        {

            special.Children.Clear();
            special.ColumnDefinitions.Clear();
            special.RowDefinitions.Clear();

            for (int i = 0; i < map.GetLength(1); i++)
            {
                special.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                special.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    //if (map[i, j].IsAlive)
                    {

                        // var rect = new Rectangle();
                        // rect.Fill = Brushes.CadetBlue;

                        // var img = new Image();
                        // img.Source = new BitmapImage(new Uri(@"https://www.pngrepo.com/png/184896/180/cell.png"));

                        // img.Stretch = Stretch.Fill;

                        // Grid.SetRow(rect, i);
                        // Grid.SetColumn(rect, j);
                        //special.Children.Add(rect);

                        // Grid.SetRow(img, i);
                        // Grid.SetColumn(img, j);

                        // special.Children.Add(img);
                        // //special.Children.;
                        Button button = new();
                        button.Click += Button_Click_2;
                        //button.Content = "ClickMe";
                        button.Background = map[i, j].IsAlive ? Brushes.Black : Brushes.White;

                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);

                        special.Children.Add(button);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            map = new Cell[Convert.ToInt32(Height.Text), Convert.ToInt32(Width.Text)];
            Game.SetBlank(ref map);

            Draw();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var xy = (sender as Button);
            var x= Grid.GetColumn(xy);
            var y =Grid.GetRow(xy);
            map[y, x] = new Cell(xy.Background == Brushes.White);
            Draw();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new();
            save.ShowDialog();
            FileWorker.SaveToFile(save.FileName,map);
        }
    }
}
