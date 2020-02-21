using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class CommandHandler
    {
        public readonly Dictionary<string, Command> commands;

        public CommandHandler()
        {
            commands = new Dictionary<string, Command>();
        }

        public void Add(string commandName, Command.Executor executor)
        {
            commands.Add(commandName, new Command(commandName, executor));
        }
        public void Add(string commandName, Command.Executor executor, string description)
        {
            commands.Add(commandName, new Command(commandName, executor, description));
        }

        public void ExecuteCommand(string command, object args)
        {
            commands[command].Execute(args);
        }
    }

    public class Command
    {
        public readonly string commandName;
        public readonly string description;
        public delegate void Executor(object args);
        private Executor executor;

        public Command(string commandName, Executor executor)
        {
            this.executor = executor;
            this.commandName = commandName;
            description = "";
        }
        public Command(string commandName, Executor executor, string description)
        {
            this.executor = executor;
            this.commandName = commandName;
            this.description = description;
        }

        public void Execute(object args)
        {
            executor?.Invoke(args);
        }
    }
}
