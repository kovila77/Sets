using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleSet mySet1 = new SimpleSet(3);
            SimpleSet mySet2 = new SimpleSet(5);
            mySet2.FillSet("4 2 5");
            mySet1.FillSet(new int[] { 3 });
            (mySet1 + mySet2).Print(Console.WriteLine);
            Console.ReadKey();
        }
    }
}
