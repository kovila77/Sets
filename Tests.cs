using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public static class Tests
    {
        public static void SimpleSetTest1()
        {
            SimpleSet mySet1 = new SimpleSet(2);
            SimpleSet mySet2 = new SimpleSet(3);
            mySet1.FillSet(new int[] { 0, 1, 2 });
            mySet1.Print(Console.WriteLine);
            mySet2.FillSet("0 3");
            mySet2.Print(Console.WriteLine);
            Console.WriteLine();
            (mySet1 + mySet2).Print(Console.WriteLine);
            (mySet1 * mySet2).Print(Console.WriteLine);
            Console.WriteLine();
        }
        public static void BitSetTest1()
        {
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
        }
        public static void MultiSetTest1()
        {
            MultiSet mySet1 = new MultiSet(1);
            MultiSet mySet2 = new MultiSet(10);
            mySet1.FillSet(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            mySet1.Print(Console.WriteLine);
            mySet2.FillSet("0 7 8 8 1");
            mySet2.DelElem(8);
            mySet2.DelElem(8);
            mySet2.Print(Console.WriteLine);
            Console.WriteLine();
            MultiSet m1 = (mySet1 + mySet2);
            MultiSet m2 = (mySet1 * mySet2);
            m1.Print(Console.WriteLine);
            m2.Print(Console.WriteLine);
            Console.WriteLine();
            m2.DelElem(0);
            m1.Print(Console.WriteLine);
            m2.Print(Console.WriteLine);
            Console.WriteLine();
            Console.WriteLine(m1[-123]);
            Console.WriteLine();
            for (int i = 0; i < m1.MaxElem; i++)
            {
                if (m1[i] > 0)
                    Console.WriteLine($"{i} - {m1[i]} раз");
            }
            Console.WriteLine();
            try
            {
                m1.AddElem(2134234234);
            }
            catch (ElemOutOfSetExeption e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
        }
    }
}
