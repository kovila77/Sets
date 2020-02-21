using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{

    [Serializable]
    public class NoSetException : Exception
    {
        public NoSetException() { }
        public NoSetException(string message) : base(message) { }
        public NoSetException(string message, Exception inner) : base(message, inner) { }
        protected NoSetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class Communication
    {
        private int typeOfSet;
        private int typeOfInput;
        private string FileWithElementsName;
        private int max;
        private Set set = null;
        private delegate void Executor(string[] arg);
        private Dictionary<string, Executor> commands;

        public Communication()
        {
            typeOfSet = Settings.Default.TypeSet;
            typeOfInput = Settings.Default.TypeInput;
            FileWithElementsName = Settings.Default.FileWithElements;
            max = Settings.Default.MaxElem;
            commands = new Dictionary<string, Executor>();
            commands.Add("help", new Executor(ShowHelp));
            commands.Add("add", new Executor(AddCom));
            commands.Add("del", new Executor(DelCom));
            commands.Add("check", new Executor(CheckCom));
            commands.Add("show", new Executor(ShowCom));
            commands.Add("chgset", new Executor(SetCom));
            commands.Add("max", new Executor(MaxCom));
            commands.Add("reset", new Executor(ResetCom));
            commands.Add("input", new Executor(InputCom));
            commands.Add("showmax", new Executor(ShowMaxCom));
        }

        public void SetTypeOfSet(string[] arg)
        {
            Console.WriteLine("Выбирете вариант представления:");
            Console.WriteLine("1 Логический");
            Console.WriteLine("2 Битовый");
            Console.WriteLine("3 мультимножество");
            Console.WriteLine("Установить предыдущий вариант - enter");
            try
            {
                typeOfSet = Convert.ToInt32(GetCommandString());
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
        public void SetMax(string[] arg)
        {
            Console.WriteLine("Введите максимум во множестве:");
            try
            {
                max = Convert.ToInt32(GetCommandString());
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
        public void ResetSet(string[] arg)
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
        public void EnterSet(string[] arg)
        {
            Console.WriteLine("Ввод множества:");
            Console.WriteLine("1 Строка");
            Console.WriteLine("2 Файл");
            try
            {
                typeOfInput = Convert.ToInt32(GetCommandString());
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
                        string choice = GetCommandString();
                        if (choice != "н" && choice != "n") { FileWithElementsName = Settings.Default.FileWithElements; }
                        else
                        {
                            Console.WriteLine("Введите имя файла:");
                            choice = GetCommandString();
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
                            set.FillSet(GetCommandString());
                        }
                        catch (ElemOutOfSetExeption e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
            }
        }

        public void AddCom(string[] arg)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            int newE;
            if (IsThereIntInCommand(arg, out newE))
            {
                try
                {
                    set.AddElem(newE);
                }
                catch (ElemOutOfSetExeption e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void DelCom(string[] arg)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            int elem;
            if (IsThereIntInCommand(arg, out elem))
            {
                set.DelElem(elem);
            }
        }
        public void CheckCom(string[] arg)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            int newE;
            try
            {
                newE = Convert.ToInt32(arg[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка преобразования числа:");
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine(set.IsExists(newE) ? "Такой элемент существует во множестве." : "Такого элемента нет во множестве.");
            return;
        }
        public void ShowCom(string[] arg)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            set.Print(Console.WriteLine);
        }
        public void SetCom(string[] arg)
        {
            SetTypeOfSet(null);
        }
        public void MaxCom(string[] arg)
        {
            SetMax(null);
        }
        public void ResetCom(string[] arg)
        {
            ResetSet(null);
        }
        public void InputCom(string[] arg)
        {
            if (set == null)
            {
                Console.WriteLine("Множество не создано (reset)!");
                return;
            }
            EnterSet(null);
        }
        public void ShowMaxCom(string[] arg)
        {
            if (set == null)
            {
                Console.WriteLine("Множество не создано (reset)!");
                return;
            }
            Console.WriteLine(set.MaxElem);
        }

        public string GetCommandString()
        {
            Console.Write(": ");
            return Console.ReadLine().Trim().ToLower();
        }

        public void ExecuteCommand(string command)
        {
            string[] command_ = command.Split();
            try
            {
                commands[command_[0]](command_);
            }
            catch (NoSetException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Невозможно исполнить комманду!");
            }
            return;
        }

        private bool IsThereIntInCommand(string[] args, out int number)
        {
            number = -1;
            try
            {
                number = Convert.ToInt32(args[1]);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Вы неправильно ввели число!");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка преобразования числа:");
                Console.WriteLine(e.Message);
                return false;

            }
            return true;
        }

        public void ShowHelp(string[] arg) { ShowHelp(); }
        public void ShowHelp()
        {
            Console.WriteLine("Доступные команды");
            Console.WriteLine("help - помощь");
            Console.WriteLine("add <число> - добавить элемент во множество");
            Console.WriteLine("del <число> - удалить элемент из множества (если такой существует)");
            Console.WriteLine("check <число> - проверь существование <число> во множестве");
            Console.WriteLine("show - вывести множество на экран");
            Console.WriteLine("chgset - установить тип множества");
            Console.WriteLine("max - установить максимум (требуется пересоздание множества)");
            Console.WriteLine("showmax - показать текущий максимум во множестве");
            Console.WriteLine("reset - пересоздание множества на основе текущих параметров");
            Console.WriteLine("input - заполнить множество элементами (файл/строка)");
            Console.WriteLine("exit - выход");
        }
    }
}
