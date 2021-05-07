using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameOfLife;

namespace GameOfLife
{
    public class FileWorker
    {
        static public byte[] EnvToStream(Cell[,] environment)
        {
            //преобразование массива клеток в массив bool для последующей обработки
            bool[] temp = new bool[environment.GetLength(0) * environment.GetLength(1)];
            for (int i = 0; i < environment.GetLength(0); i++)
            {
                for (int j = 0; j < environment.GetLength(1); j++)
                {
                    temp[i * (environment.GetLength(1)) + j] = environment[i, j].IsAlive;
                }
            }

            //на основе bool-ей получается массив байт
            int byteLength = 8;
            byte[] bytes = new byte[temp.Length % byteLength == 0 ? temp.Length / byteLength : temp.Length / byteLength + 1];
            for (int i = 0; i < temp.Length; i += byteLength)
            {
                int byteadder = 0;
                for (int j = 0; j < (temp.Length - 1 - i > 8 ? 8 : temp.Length - 1 - i); j++)
                {
                    byteadder += (temp[i + j] ? 1 : 0) << j;
                }
                bytes[i / byteLength] = (byte)byteadder;
            }
            return bytes;
        }

        
        static public string GetState(Cell[,] env) 
        {

            return FileWorker.ByteArrayToString
                (
                new MD5CryptoServiceProvider().ComputeHash
                    (
                    FileWorker.EnvToStream(env)
                    )
                );
        }

        static public string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder stringOutput = new(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                stringOutput.Append(arrInput[i].ToString("X2"));
            }
            return stringOutput.ToString();
        }

        public static bool ReadFromFile(string path, out Cell[,] txter)
        {
            txter = new Cell[0, 0];
            try
            {
                using (StreamReader streamReader = new(path))
                {
                    string[] fieldIJ = streamReader.ReadLine().Split(' ');
                    txter = new Cell[Convert.ToInt32(fieldIJ[0]), Convert.ToInt32(fieldIJ[1])];
                    string lastString = string.Empty;

                    for (int i = 0; i < Convert.ToInt32(fieldIJ[0]); i++)
                    {
                        lastString = streamReader.ReadLine();
                        for (int j = 0; j < Convert.ToInt32(fieldIJ[1]); j++)
                        {
                            txter[i, j] = Convert.ToInt32(lastString[j * 2].ToString()) == 1 ? new Cell(true) : new Cell(false);
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        static public void SaveToFile(string path, Cell[,] map) 
        {
            try
            {
                using (StreamWriter streamWriter = new(path))
                { 
                    streamWriter.WriteLine(map.GetLength(0) + " " + map.GetLength(1));
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        string oneLine = "";
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            oneLine+=((map[i, j].IsAlive ? 1 : 0) + " ");
                        }
                        oneLine = oneLine.TrimEnd(' ');
                        streamWriter.WriteLine(oneLine);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
