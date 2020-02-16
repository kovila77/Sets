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
            //SimpleSet mySet1 = new SimpleSet(2);
            //SimpleSet mySet2 = new SimpleSet(3);
            //mySet1.FillSet(new int[] { 0, 1, 2 });
            //mySet1.Print(Console.WriteLine);
            //mySet2.FillSet("0 3");
            //mySet2.Print(Console.WriteLine);
            //Console.WriteLine();
            //(mySet1 + mySet2).Print(Console.WriteLine);
            //(mySet1 * mySet2).Print(Console.WriteLine);
            //Console.WriteLine();

            //BitSet mySet1 = new BitSet(8);
            //BitSet mySet2 = new BitSet(345);
            //mySet1.FillSet(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            //mySet1.Print(Console.WriteLine);
            //mySet2.FillSet("0 7 8");
            //mySet2.DelElem(0);
            //mySet2.Print(Console.WriteLine);
            //Console.WriteLine();
            //(mySet1 + mySet2).Print(Console.WriteLine);
            //(mySet1 * mySet2).Print(Console.WriteLine);
            //Console.WriteLine();

            BitSet mySet1 = new BitSet(8);
            BitSet mySet2 = new BitSet(345);
            mySet1.FillSet(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            mySet1.Print(Console.WriteLine);
            mySet2.FillSet("0 7 8");
            mySet2.DelElem(0);
            mySet2.Print(Console.WriteLine);
            Console.WriteLine();
            (mySet1 + mySet2).Print(Console.WriteLine);
            (mySet1 * mySet2).Print(Console.WriteLine);

            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
