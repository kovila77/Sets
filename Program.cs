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

            Communication com = new Communication();

            //com.SetTypeOfSet(null);

            //com.SetMax(null);

            //com.ResetSet(null);

            //com.EnterSet(null);

            //com.ShowHelp();

            string command = com.GetCommandString();
            while (command != "exit")
            {
                com.ExecuteCommand(command);
                command = com.GetCommandString();
            }
        }
    }
}
