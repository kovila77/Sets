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

            string dataFileName = @"data.txt";
            string setFileName = @"set.txt";
            int lastChoice;
            int max;
            Set set;

            Console.WriteLine("Выбирете вариант представления:");
            Console.WriteLine("1 Логический");
            Console.WriteLine("2 Битовый");
            Console.WriteLine("3 мультимножество");
            Console.WriteLine("Установить предыдущий вариант - enter");

            try
            {
                lastChoice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                //Загружаем прошлый выбор
                try
                {
                    using (StreamReader dataReader = new StreamReader(dataFileName))
                    {
                        lastChoice = Convert.ToInt32(dataReader.ReadLine());
                    }
                }
                catch (Exception)
                {
                    lastChoice = 1;
                }
            }
            Console.Write("Выбрано ");
            switch (lastChoice)
            {
                case 1:
                    {
                        Console.WriteLine("Логический");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Битовый");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Мультимножество");
                        break;
                    }
                default:
                    {
                        lastChoice = 1;
                        Console.WriteLine("Логический");
                        break;
                    }
            }
            //Сохраняем текущий выбор
            try
            {
                using (StreamWriter dataWriter = new StreamWriter(dataFileName))
                {
                    dataWriter.WriteLine(lastChoice.ToString());
                }
            }
            catch (Exception) { }

            Console.WriteLine("Введите максимум во множестве:");
            try
            {
                max = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                max = 1;
            }

            Console.ReadKey();
        }
    }
}
