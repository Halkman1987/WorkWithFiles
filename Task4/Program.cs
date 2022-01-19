using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FinalTask
{
    class Program
    {
        [Serializable]
        public class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
            public Student(string name, string group, DateTime dat)
            {
                Name = name;
                Group = group;
                DateOfBirth = dat;
            }
        }


        static void Main()
        {
            List<string> group = new List<string>();
            Student[] student = new Student[3];
            student[0] = new Student("Dima", "one", new DateTime(1987, 03, 02));
            student[1] = new Student("Igor", "two", new DateTime(1988, 04, 03));
            student[2] = new Student("вася", "three", new DateTime(1989, 05, 03));
            string Stud = @"C:\Users\User\Desktop\MyStudentsForm.dat";
            // Создали папку 
            string filePath = @"C:\Users\User\Desktop\Student";
            DirectoryInfo dirInfo = new DirectoryInfo(filePath);
            if (!dirInfo.Exists)
                dirInfo.Create();

            try
            {
                //создаем форматтер 
                BinaryFormatter formatter = new BinaryFormatter();
                //открываем поток 
                using (FileStream fs = new FileStream(@"C:\Users\User\Desktop\MyStudentsForm.dat", FileMode.OpenOrCreate))
                {
                    // сериализация файла 
                    formatter.Serialize(fs, student);
                    Console.WriteLine("File Serialized ");
                    Console.WriteLine("--------------------------------------------------------");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


            //---------Рабочий Вариант Десериализации и чтения из файла в консоль -----------
            if (File.Exists(Stud))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream fs = new FileStream(Stud, FileMode.Open, FileAccess.Read))
                    {
                        List<string> Grouplist = new List<string>();

                        Student[] nstudent = (Student[])bf.Deserialize(fs);
                        Console.WriteLine("Прошла Десериализация");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine(" Считали  :");
                        foreach (Student stud in nstudent)
                        {

                            //получили данные 
                            string NewPath = Path.Combine(filePath, stud.Group + ".txt");//имя файла
                            if (!File.Exists(NewPath))
                            {
                                Console.WriteLine($"Создадим файл {NewPath}");
                            }
                            else if (!Grouplist.Contains(stud.Group))
                            {
                                File.Delete(NewPath);
                            }
                            FileInfo fileInfo = new FileInfo(NewPath);
                            try
                            {
                                using (StreamWriter fss = fileInfo.AppendText())
                                {
                                    fss.Write(stud.Name + "___");
                                    fss.Write(stud.DateOfBirth.ToString("D"));
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            if (!Grouplist.Contains(stud.Group))
                            {
                                Grouplist.Add(stud.Name);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"{Stud} - Ненайден");
            }


        }


    }
}

