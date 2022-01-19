
using System;
using System.IO;
using System.Linq;

namespace FirstApp
{

    class Program
    {
        static void Main(string[] args)
        {

            string dirname = @"C:\User\123";
            if (Directory.Exists(dirname))
            { Console.WriteLine($"Папка доступна {dirname} "); }
            FolderExicute.Checkfolder(dirname);// передали путь @"C:\User\123"

        }
        public static class FolderExicute
        {
            public static void Checkfolder(string pathname) // приняли путь
            {
                if (Directory.Exists(pathname)) //Проверили на наличие
                {
                    Console.WriteLine("Пеершли к Чек Фаилс");
                    CheckFiles(pathname);// отдали путь на проверку файлов

                    string[] dirpath = Directory.GetDirectories(pathname);
                    foreach (string dir in dirpath)
                    {
                        Checkfolder(dir);
                    }
                }
                else
                {
                    Console.WriteLine("Кончились папки");
                }
            }
            public static void CheckFiles(string pathname)
            {

                string[] namepath = Directory.GetFiles(pathname);// получили массив файлов

                foreach (var v in namepath)
                {
                    DateTime lastaccess = File.GetLastAccessTime(v);
                    TimeSpan fileacc = DateTime.Now.Subtract(lastaccess);
                    Console.Write(v);
                    if (fileacc > TimeSpan.FromMinutes(30)) // проверили условие
                    {
                        Console.WriteLine("Время вышло");
                        try
                        {
                            File.Delete(v);
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

}






