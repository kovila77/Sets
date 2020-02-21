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

    [Serializable]
    public class NoNumberException : Exception
    {
        public NoNumberException() { }
        public NoNumberException(string message) : base(message) { }
        public NoNumberException(string message, Exception inner) : base(message, inner) { }
        protected NoNumberException(
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
        CommandHandler commandHandler = new CommandHandler();

        public Communication()
        {
            typeOfSet = Settings.Default.TypeSet;
            typeOfInput = Settings.Default.TypeInput;
            FileWithElementsName = Settings.Default.FileWithElements;
            max = Settings.Default.MaxElem;
            commandHandler.Add("help", new Command.Executor(ShowHelp), "help - помощь");
            commandHandler.Add("add", new Command.Executor(AddCom), "add <число> - добавить элемент во множество");
            commandHandler.Add("del", new Command.Executor(DelCom), "del <число> - удалить элемент из множества(если такой существует)");
            commandHandler.Add("check", new Command.Executor(CheckCom), "check <число> - проверь существование <число> во множестве");
            commandHandler.Add("show", new Command.Executor(ShowCom), "show - вывести множество на экран");
            commandHandler.Add("chset", new Command.Executor(ChgCom), "chset [<тип>] - установить тип множества (log, bin, mul)");
            commandHandler.Add("max", new Command.Executor(MaxCom), "max [<максимальное_значение>] - установить максимум (требуется пересоздание множества)");
            commandHandler.Add("reset", new Command.Executor(ResetCom), "reset - пересоздание множества на основе текущих параметров");
            commandHandler.Add("fill", new Command.Executor(FillCom), "fill - заполнить множество элементами");
            commandHandler.Add("showmax", new Command.Executor(ShowMaxCom), "showmax - показать текущий максимум во множестве");
            commandHandler.Add("showmaxs", new Command.Executor(ShowMaxSCom), "showmaxs - показать максимум в настройках");
            commandHandler.Add("showtype", new Command.Executor(ShowTypeCom), "showtype - показать текущий тип множества");
            commandHandler.Add("showtypes", new Command.Executor(ShowTypeSCom), "showtypes - показать тип множества в настройках");
            commandHandler.Add("tun", new Command.Executor(TestUnionAndIntersection), "tun - тест объяденения и пересечения");
        }

        public void TestUnionAndIntersection(object args)
        {
            int tmpTypeOfSet = typeOfSet;
            Set set1;
            Set set2;
            SetTypeOfSet(null);
            switch (typeOfSet)
            {
                case 1: { set1 = new SimpleSet(max); set2 = new SimpleSet(max); break; }
                case 2: { set1 = new BitSet(max); set2 = new BitSet(max); break; }
                case 3: { set1 = new MultiSet(max); set2 = new MultiSet(max); break; }
                default:
                    { set1 = new SimpleSet(max); set2 = new SimpleSet(max); break; }
            }
            typeOfSet = tmpTypeOfSet;
            Settings.Default.TypeSet = typeOfSet;
            Settings.Default.Save();
            string s1, s2;
            Console.WriteLine("Введите первую строку, с которой будет заполнено множество один");
            s1 = GetCommandString();
            Console.WriteLine("Введите вторую строку, с которой будет заполнено множество два");
            s2 = GetCommandString();
            set1.FillSet(s1);
            set2.FillSet(s2);
            Console.WriteLine("Объединение множеств:");
            if (set1 is SimpleSet) { Console.WriteLine(((SimpleSet)set1 + (SimpleSet)set2)); }
            else if (set1 is BitSet) { Console.WriteLine(((BitSet)set1 + (BitSet)set2)); }
            else if (set1 is MultiSet) { Console.WriteLine(((MultiSet)set1 + (MultiSet)set2)); }
            Console.WriteLine("Пересечение множеств:");
            if (set1 is SimpleSet) { Console.WriteLine(((SimpleSet)set1 * (SimpleSet)set2)); }
            else if (set1 is BitSet) { Console.WriteLine(((BitSet)set1 * (BitSet)set2)); }
            else if (set1 is MultiSet) { Console.WriteLine(((MultiSet)set1 * (MultiSet)set2)); }
        }

        public void SetTypeOfSet(object args)
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

            Settings.Default.TypeSet = typeOfSet;
            Settings.Default.Save();
        }
        public void SetMax(object args)
        {
            Console.WriteLine("Введите максимум во множестве:");
            try
            {
                max = Convert.ToInt32(GetCommandString());
                if (max > 100000000)
                    max = 100000000;
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
        public void ResetSet(object args)
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
        public void EnterSet(object args)
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
                typeOfInput = Settings.Default.TypeInput;
            }
            if (typeOfInput == 2)
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
                Console.WriteLine($"Будет использован файл {FileWithElementsName}");
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
                Settings.Default.TypeInput = 2;
                Console.WriteLine("Готово");
            }
            else
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
                Settings.Default.TypeInput = 1;
            }
        }

        public void AddCom(object args)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            string[] arg = (string[])args;
            int newE;
            try
            {
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
            catch (NoNumberException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DelCom(object args)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            string[] arg = (string[])args;
            int elem;
            try
            {
                if (IsThereIntInCommand(arg, out elem))
                {
                    set.DelElem(elem);
                }
            }
            catch (NoNumberException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void CheckCom(object args)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            string[] arg = (string[])args;
            int elem;
            try
            {
                if (IsThereIntInCommand(arg, out elem))
                {
                    Console.WriteLine(set.IsExists(elem) ? "Такой элемент существует во множестве." : "Такого элемента нет во множестве.");
                }
            }
            catch (NoNumberException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ShowCom(object args)
        {
            if (set == null)
            {
                throw new NoSetException("Множество не создано (необходимо выполнить команду \"reset\")!");
            }
            set.Print(Console.WriteLine);
        }
        public void ChgCom(object args)
        {
            string[] arg = (string[])args;
            try
            {
                if (arg[1] == "log") { typeOfSet = 1; }
                else
                if (arg[1] == "bit") { typeOfSet = 2; }
                else
                if (arg[1] == "mul") { typeOfSet = 3; }
                else
                {
                    SetTypeOfSet(null);
                }
            }
            catch (IndexOutOfRangeException) { SetTypeOfSet(null); }

            Console.Write("Выбрано ");
            switch (typeOfSet)
            {
                case 1: { Console.WriteLine("Логический"); break; }
                case 2: { Console.WriteLine("Битовый"); break; }
                case 3: { Console.WriteLine("Мультимножество"); break; }
                default:
                    { typeOfSet = 1; Console.WriteLine("Логический"); break; }
            }
            Console.WriteLine("Для смены множества на новое введите reset");
            Settings.Default.TypeSet = typeOfSet;
            Settings.Default.Save();
        }
        public void MaxCom(object args)
        {
            string[] arg = (string[])args;
            try
            {
                if (IsThereIntInCommand(arg, out max))
                {
                    if (max > 100000000)
                        max = 100000000;
                    if (max < 0)
                    {
                        Console.WriteLine("Максимум не может быть меньше 0");
                        max = Settings.Default.MaxElem;
                        Console.WriteLine($"Установлен предыдущий максимум");
                    }
                    Settings.Default.MaxElem = max;
                    Settings.Default.Save();
                    Console.WriteLine($"Максимум = {max}");
                }
            }
            catch (NoNumberException)
            {
                SetMax(null);
            }
            Console.WriteLine("Для смены максимума на новое значение введите reset");
        }
        public void ResetCom(object args)
        {
            ResetSet(null);
        }
        public void FillCom(object args)
        {
            if (set == null)
            {
                Console.WriteLine("Множество не создано (reset)!");
                return;
            }
            string[] arg = (string[])args;
            List<int> numbersToAdd = new List<int>();
            for (int i = 1; i < arg.Length; i++)
            {
                try
                {
                    numbersToAdd.Add(Convert.ToInt32(arg[i]));
                }
                catch (Exception) { continue; }
            }
            if (numbersToAdd.Count > 0) { set.FillSet(numbersToAdd.ToArray()); }
            else
            {
                EnterSet(null);
            }
        }
        public void ShowMaxCom(object args)
        {
            if (set == null)
            {
                Console.WriteLine("Множество не создано (reset)!");
                return;
            }
            Console.WriteLine(set.MaxElem);
        }
        public void ShowMaxSCom(object args)
        {
            Console.WriteLine(max);
        }
        public void ShowTypeCom(object args)
        {
            Console.Write("Текущий тип множества ");
            if (set is SimpleSet) Console.WriteLine("Логический");
            else if (set is BitSet) Console.WriteLine("Битовый");
            else if (set is MultiSet) Console.WriteLine("Мультимножество");
            else Console.WriteLine("ошибка! неизвестный тип!");
        }
        public void ShowTypeSCom(object args)
        {
            Console.Write("Настроенный тип множества ");
            switch (typeOfSet)
            {
                case 1: { Console.WriteLine("Логический"); break; }
                case 2: { Console.WriteLine("Битовый"); break; }
                case 3: { Console.WriteLine("Мультимножество"); break; }
                default: { Console.WriteLine("ошибка! неизвестный тип!"); break; }
            }
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
                commandHandler.ExecuteCommand(command_[0], command_);
            }
            catch (NoSetException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Невозможно исполнить комманду!");
                Console.WriteLine(e.Message);
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
                throw new NoNumberException("Вы не ввели число!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка преобразования числа:");
                Console.WriteLine(e.Message);
                return false;

            }
            return true;
        }

        public void ShowHelp(object args) { ShowHelp(); }
        public void ShowHelp()
        {
            Console.WriteLine("Доступные команды");
            foreach (var com in commandHandler.commands)
            {
                Console.WriteLine(com.Value.description);
            }
            Console.WriteLine("exit - выход");
        }
    }
}
