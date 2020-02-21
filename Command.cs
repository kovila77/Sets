using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class Command
    {
        public readonly string commandName;
        public readonly string description;
        public delegate void Executor(object args);
        public Executor executor;

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
            executor(args);
        }
    }
}
