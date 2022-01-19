using System;
using System.IO;
using System.Linq;

namespace FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirname2 = @"C:\User\Visual";
            DirectoryInfo root = new DirectoryInfo(dirname2);
            // отдали на подсчет
            CountFolder.SizeFolders(root);
            Console.WriteLine("Вывод размера каталога  : C:-User-Visual ");
            var size = CountFolder.SizeFolders(root); //работает
                                                      // Console.WriteLine($"исходный : {size}") ; //выводит
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"Исходный размер :{CountFolder.SizeFolders(root)} байт");
            // отдали на очистку по интервалу 30мин
            FolderExicute.Checkfolder(root);// передали путь @"C:\User" для очистки папки
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------");
            // снова посчитали 
            CountFolder.SizeFolders(root);
            Console.WriteLine($"Текущий размер :{CountFolder.SizeFolders(root)} байт ");
        }
        public static class FolderExicute // Класс для очистки папок
        {
            public static void Checkfolder(DirectoryInfo root)
            {
                if (root.Exists)
                {
                    // Console.WriteLine($"Проверка пофайлово в папке {root}");
                    //  Console.WriteLine("Передаем методу CheckFiles ");
                    CheckFiles(root);// отдали путь на проверку файлов
                    DirectoryInfo[] dir = root.GetDirectories(); // получили массив каталогов в
                    foreach (var r in dir)
                    {
                        // Console.WriteLine($"Имя паки  {r}");
                        Checkfolder(r);
                    }
                }
                else { Console.WriteLine("Кончились папки"); }
            }
            public static void CheckFiles(DirectoryInfo root) // проверка и удаление файлов по времени 
            {
                FileInfo[] namepath = root.GetFiles();// получили массив файлов
                foreach (var v in namepath)
                {
                    DateTime lastaccess = v.LastAccessTime;
                    TimeSpan fileacc = DateTime.Now.Subtract(lastaccess);
                    Console.Write(v);
                    if (fileacc > TimeSpan.FromMinutes(30)) // проверили условие
                    {
                        Console.WriteLine("Время вышло");
                        try
                        {
                            v.Delete();
                            Console.Write("Файл успешно удален");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Время не пришло ");
                    }
                }
            }
        }
    }


    public static class CountFolder // Рабочий Метод подсчета файлов
    {
        public static long SizeFolders(DirectoryInfo dirname) // Метод подсчета файлов
        {
            //DirectoryInfo[] folders = root.GetDirectories();
            long size = 0;
            FileInfo[] files = dirname.GetFiles();

            foreach (FileInfo f in files)
            {
                size += f.Length;
            }
            DirectoryInfo[] dirs = dirname.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                size += SizeFolders(dir);
            }
            return size;
        }

    }


}







