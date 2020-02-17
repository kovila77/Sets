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

            //com.SetTypeOfSet();

            //com.SetMax();

            //com.ResetSet();

            //com.EnterSet();

            com.ShowHelp();

            string command = com.GetCommand();
            while (command != "exit")
            {
                com.ExecuteCommand(command);

                command = com.GetCommand();
            }
        }
    }
}
