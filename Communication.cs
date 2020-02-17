using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class Communication
    {
        private int typeOfSet;
        private int typeOfInput;
        private string FileWithElementsName;
        private int max;
        private Set set = null;

        public Communication()
        {
            typeOfSet = Settings.Default.TypeSet;
            typeOfInput = Settings.Default.TypeInput;
            FileWithElementsName = Settings.Default.FileWithElements;
            max = Settings.Default.MaxElem;
        }

        public void SetTypeOfSet()
        {
            Console.WriteLine("Выбирете вариант представления:");
            Console.WriteLine("1 Логический");
            Console.WriteLine("2 Битовый");
            Console.WriteLine("3 мультимножество");
            Console.WriteLine("Установить предыдущий вариант - enter");
            try
            {
                typeOfSet = Convert.ToInt32(GetCommand());
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
        }

        public void SetMax()
        {
            Console.WriteLine("Введите максимум во множестве:");
            try
            {
                max = Convert.ToInt32(GetCommand());
                if (max < 0)
                {
                    Console.WriteLine("Максимум не может быть меньше 0");
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                max = Settings.Default.MaxElem;
            }
            Settings.Default.MaxElem = max;
            Settings.Default.Save();
            Console.WriteLine($"Максимум = {max}");
        }

        public void ResetSet()
        {
            switch (typeOfSet)
            {
                case 1: { set = new SimpleSet(max); break; }
                case 2: { set = new BitSet(max); break; }
                case 3: { set = new MultiSet(max); break; }
                default:
                    { set = new SimpleSet(max); break; }
            }
        }

        public void EnterSet()
        {
            Console.WriteLine("Ввод множества:");
            Console.WriteLine("1 Строка");
            Console.WriteLine("2 Файл");
            try
            {
                typeOfInput = Convert.ToInt32(GetCommand());
            }
            catch (Exception)
            {
                typeOfInput = Settings.Default.TypeSet;
            }
            switch (typeOfInput)
            {
                case 2:
                    {
                        Console.WriteLine($"Использовать файл {Settings.Default.FileWithElements}? (\"н\" или \"n\" для выбора другого файла)");
                        string choice = GetCommand();
                        if (choice != "н" && choice != "n") { FileWithElementsName = Settings.Default.FileWithElements; }
                        else
                        {
                            Console.WriteLine("Введите имя файла:");
                            choice = GetCommand();
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
                        Console.WriteLine($"Используется файл {FileWithElementsName}");
                        Settings.Default.FileWithElements = FileWithElementsName;
                        Settings.Default.Save();
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
                        try
                        {
                            set.FillSet(tmp.ToArray());
                        }
                        catch (ElemOutOfSetExeption e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Введите строку элементов множества,\nразделённых символом пробел (пример \"1 2 4 5\"):");
                        try
                        {
                            set.FillSet(GetCommand());
                        }
                        catch (ElemOutOfSetExeption e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
            }
        }

        public string GetCommand()
        {
            Console.Write(": ");
            return Console.ReadLine();
        }

        public void ExecuteCommand(string command)
        {
            string[] command_ = command.Split();
            switch (command_[0])
            {
                case "add":
                    {
                        if (set == null)
                        {
                            Console.WriteLine("Множество не создано (reset)!");
                            return;
                        }
                        int newE;
                        try
                        {
                            newE = Convert.ToInt32(command_[1]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                        try
                        {
                            set.AddElem(newE);
                        }
                        catch (ElemOutOfSetExeption e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
                case "del":
                    {
                        if (set == null)
                        {
                            Console.WriteLine("Множество не создано (reset)!");
                            return;
                        }
                        int newE;
                        try
                        {
                            newE = Convert.ToInt32(command_[1]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ошибка преобразования числа:");
                            Console.WriteLine(e.Message);
                            break;
                        }
                        set.DelElem(newE);
                        break;
                    }
                case "check":
                    {
                        if (set == null)
                        {
                            Console.WriteLine("Множество не создано (reset)!");
                            return;
                        }
                        int newE;
                        try
                        {
                            newE = Convert.ToInt32(command_[1]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ошибка преобразования числа:");
                            Console.WriteLine(e.Message);
                            break;
                        }
                        Console.WriteLine(set.IsExists(newE)?"такой элемент есть":"такого элемента нет");
                        break;
                    }
                case "show":
                    {
                        if (set == null)
                        {
                            Console.WriteLine("Множество не создано (reset)!");
                            return;
                        }
                        set.Print(Console.WriteLine);
                        break;
                    }
                case "set":
                    {
                        SetTypeOfSet();
                        break;
                    }
                case "max":
                    {
                        SetMax();
                        break;
                    }
                case "reset":
                    {
                        ResetSet();
                        break;
                    }
                case "input":
                    {
                        if (set == null)
                        {
                            Console.WriteLine("Множество не создано (reset)!");
                            return;
                        }
                        EnterSet();
                        break;
                    }
                case "showmax":
                    {
                        if (set == null)
                        {
                            Console.WriteLine("Множество не создано (reset)!");
                            return;
                        }
                        Console.WriteLine(set.MaxElem);
                        break;
                    }
                default:
                    {
                        ShowHelp();
                        break;
                    }
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine("Доступные команды");
            Console.WriteLine("help - помощь");
            Console.WriteLine("add <число> - добавить элемент во множество");
            Console.WriteLine("del <число> - удалить элемент из множества (если такой существует)");
            Console.WriteLine("check <число> - проверь существование <число> во множестве");
            Console.WriteLine("show - напечатать множества");
            Console.WriteLine("set - установить тип множества");
            Console.WriteLine("max - установить максимум (требуется пересоздание множества)");
            Console.WriteLine("showmax - показать текущий максимум во множестве");
            Console.WriteLine("reset - пересоздание множества на основе текущих параметров");
            Console.WriteLine("input - заполнить множество элементами (файл/строка)");
            Console.WriteLine("exit - выход");
        }
    }
}
