using System;
using System.Collections.Generic;
using System.IO;

namespace Humanization_Apple
{
    class Program
    {
        static List<string> files = new List<string>();
        static void Main(string[] args)
        {
            string path_of;
            string path_to;
            
            Console.WriteLine("Привет! Я помогу тебе очеловечить технологии Apple!");
            Console.WriteLine("Введи путь до папки с подкаталогами фотографий: ");
            path_of = Console.ReadLine();

            Console.WriteLine("Введи путь до папки, куда перенести всю медиа: ");
            path_to = Console.ReadLine();

            Console.WriteLine("Отчет: ");

            MoveTo(path_of, path_to);

            Console.ReadLine();
        }

        static public void MoveTo(string path_of, string path_to)
        {
            File_Search(path_of);
            foreach (var file in files) // Перебераем все файлы из List<> для работы с ними
            {
                DirectoryInfo f_info = new DirectoryInfo(file);
                string FULL_path_to = path_to + @"\" + f_info.Name;
                Random rnd = new Random();
                try
                {
                    Moving(file, FULL_path_to);
                }
                catch (Exception e)
                {
                    if (File.Exists(FULL_path_to)) // Если такой файл уже существует 
                    {
                        FileInfo FILE_INFO = new FileInfo(FULL_path_to);
                        Directory.Move(FULL_path_to,  path_to + @"\" + rnd.Next(10000,99999) + FILE_INFO.Extension);
                        Moving(file, FULL_path_to);
                    }
                    else
                    {
                        Console.WriteLine("Файл " + file + " не был перенесён. Ошибка: " + e.Message);
                    }
                }
            }
        }

        private static void Moving(string file, string FULL_path_to)
        {
            File.Move(file, FULL_path_to);
            Console.WriteLine(file + " перенесён в " + FULL_path_to); 
            Console.WriteLine("-");
        }

        static public void File_Search(string path_of)
        {
            string[] pathFile = Directory.GetFiles(path_of);
            string[] dirS = Directory.GetDirectories(path_of);
            foreach (var s in pathFile)
            {
                files.Add(s);
            }
            foreach (var s in dirS)
            {
                File_Search(s);
            }
        } 
    }
}
