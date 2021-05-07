using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GameOfLifeWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, IUserInteraction
    {
        readonly Game game;
        public MainWindow()
        {
            InitializeComponent();

            //Инициализация игры 
            game = new Game(new WPFDrawer(this, 150));
            game.Defenvironment();
            game.Draw();
        }

        //создание нового окна
        public void StartInteraction(string[] args)
        {
            MainWindow main = new();
            main.Show();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            game.OneStep();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //Выделение нового потока для пошагового отображения игры до зацикливания жизни 

            Task task = new( ()=> 
            { 
                game.ToEnd();

                Dispatcher.Invoke(() =>
                Score.Content = "Поколений: " + game.Generation);
            }
            );
            task.Start();
        }
    }
}
