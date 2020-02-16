using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sets
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tests.MultiSetTest1();

            int typeOfSet;
            int typeOfInput;
            string FileWithElementsName;
            int max;
            Set set;

            Console.WriteLine("Выбирете вариант представления:");
            Console.WriteLine("1 Логический");
            Console.WriteLine("2 Битовый");
            Console.WriteLine("3 мультимножество");
            Console.WriteLine("Установить предыдущий вариант - enter");

            try
            {
                typeOfSet = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                typeOfSet = Settings.Default.TypeSet;
            }
            Console.Write("Выбрано ");
            switch (typeOfSet)
            {
                case 1: { Console.WriteLine("Логический"); break; }
                case 2: { Console.WriteLine("Битовый"); break; }
                case 3: { Console.WriteLine("Мультимножество"); break; }
                default:
                    { typeOfSet = 1; Console.WriteLine("Логический"); break; }
            }
            Settings.Default.TypeSet = typeOfSet;
            Settings.Default.Save();

            Console.WriteLine("Введите максимум во множестве:");
            try
            {
                max = Convert.ToInt32(Console.ReadLine());
                if (max < 0) throw new Exception();
            }
            catch (Exception)
            {
                max = Settings.Default.MaxElem;
            }
            Settings.Default.MaxElem = max;
            Console.WriteLine($"Максимум = {max}");

            switch (typeOfSet)
            {
                case 1: { set = new SimpleSet(max); break; }
                case 2: { set = new BitSet(max); break; }
                case 3: { set = new MultiSet(max); break; }
                default:
                    { set = new SimpleSet(max); break; }
            }

            Console.WriteLine("Ввод множества:");
            Console.WriteLine("1 Строка");
            Console.WriteLine("2 Файл");
            try
            {
                typeOfInput = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                typeOfInput = Settings.Default.TypeSet;
            }
            switch (typeOfInput)
            {
                case 2:
                    {
                        Console.WriteLine($"Использовать файл {Settings.Default.FileWithElements}? (\"н\" для выбора другого файла)");
                        string choice = Console.ReadLine();
                        if (choice != "н") { FileWithElementsName = Settings.Default.FileWithElements; }
                        else
                        {
                            Console.WriteLine("Введите имя файла:");
                            choice = Console.ReadLine();
                            if (File.Exists(choice))
                            {
                                FileWithElementsName = choice;
                            }
                            else
                            {
                                Console.WriteLine("Такого файла не существует");
                                FileWithElementsName = Settings.Default.FileWithElements;
                            }
                        }
                        Console.WriteLine($"Используется файл {FileWithElementsName}"); ;
                        //string str;
                        List<int> tmp = new List<int>();
                        try
                        {
                            using (StreamReader sr = new StreamReader(FileWithElementsName))
                            {
                                while (!sr.EndOfStream)
                                {
                                    try
                                    {
                                        tmp.Add(Convert.ToInt32(sr.ReadLine()));
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        set.FillSet(tmp.ToArray());
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Введите строку элементов множества,\nразделённых символом пробел (пример \"1 2 4 5\"):");
                        set.FillSet(Console.ReadLine());
                        break;
                    }
            }

            string command = Console.ReadLine();
            while (command != "exit")
            {
                set.Print(Console.WriteLine);
                command = Console.ReadLine();
            }

            Console.WriteLine("Для выхода нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
}
