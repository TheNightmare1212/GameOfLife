using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife;

namespace GameOfLife
{
    class ConsoleInteraction : IUserInteraction
    {
        public void StartInteraction(string[] args) 
        {
            //инициализация игры
            Game gameProcess = new Game(new ConsoleDrawer('▒'));       
            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    Cell[,] environment;
                    if (FileWorker.ReadFromFile(args[0], out environment))
                    {
                        gameProcess.SetEnvironment(environment);

                        while(gameProcess.OneStep())
                        { 
                            Console.ReadKey(); 
                        } 
                    }
                    else
                    {
                        Console.WriteLine("Сохранение битое");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Файл {0} не найден", args[0]);
                    Console.WriteLine("Искользовать дефолтный паттерн?");
                    Console.WriteLine("1-Да   Любая клавиша-Нет");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            Console.WriteLine("рисуем дефолт");
                            gameProcess.Defenvironment();

                            while (gameProcess.OneStep())
                            { 
                                Console.ReadKey();
                            }
                            break;
                        default: Console.WriteLine("Конец работы"); break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Файл не указан");
                Console.WriteLine("Искользовать дефолтный паттерн?");
                Console.WriteLine("1-Да   Любая клавиша-Нет");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.WriteLine("рисуем дефолт");
                        gameProcess.Defenvironment();

                        while (gameProcess.OneStep())
                        { 
                            Console.ReadKey(); 
                        }
                        break;
                    default: Console.WriteLine("Конец работы"); break;
                }
            }

            Console.WriteLine("Жизнь зациклилась или вымерла");
            Console.WriteLine("На поколении"+gameProcess.Generation);
            Console.ReadKey();
        } 
    }
}

