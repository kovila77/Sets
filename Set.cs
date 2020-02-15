using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public abstract class Set
    {
        protected int _maxElem;
        public delegate void PrintFunction(string stringToPrint);

        public int MaxElem { get { return _maxElem; } }

        public abstract void AddElem(int newElem);
        public abstract void DelElem(int delElem);
        public abstract bool IsExists(int elem);
        public bool CanExists(int elem)
        {
            if (elem >= _maxElem || elem < 0)
            {
                return false;
            }
            return true;
        }
        public void CheckCanExists(int elem)
        {
            if (elem >= _maxElem || elem < 0)
            {
                throw new Exception();
            }
        }
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
            for (int i = 0; i < _maxElem; i++)
            {
                if (IsExists(i))
                {
                    result += $@" {i}";
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