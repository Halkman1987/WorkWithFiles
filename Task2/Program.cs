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

            CountFolder.SizeFolders(root);
            //CountFolder.SizeFolders(dirname2);
            Console.WriteLine("Вывод размера каталога  : C:-User-Visual ");
            var size = CountFolder.SizeFolders(root);
            Console.WriteLine(size);
            Console.WriteLine();
            Console.WriteLine($"сумма всех файлов и папок :{CountFolder.SizeFolders(root)} ");


        }

        public static class CountFolder
        {
            public static long SizeFolders(DirectoryInfo dirname)
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
}   








