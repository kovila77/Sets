using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public abstract class Set
    {
        public int[] _data;
        public delegate void PrintFunction(string stringToPrint);

        public abstract void AddElem(int newElem);
        public abstract void DelElem(int delElem);
        public abstract bool IsExists(int elem);

        public void FillSet(string stringData)
        {
            string[] newStrData = stringData.Split();
            foreach (string newStrElem in newStrData)
            {
                int newElem;
                try
                {
                    newElem = Convert.ToInt32(newStrElem);
                }
                catch
                {
                    continue;
                }
                AddElem(newElem);
            }
        }
        public void FillSet(int[] intData)
        {
            foreach (int newElem in intData)
            {
                AddElem(newElem);
            }
        }
        public override string ToString()
        {
            string result = "";
            foreach (int elem in _data)
            {
                if (IsExists(elem))
                {
                    result += " " + elem.ToString();
                }
            }
            return result;
        }
        public void Print(PrintFunction printFunction)
        {
            printFunction(this.ToString());
        }
    }
}