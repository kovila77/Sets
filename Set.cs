using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{

    [Serializable]
    public class ElemOutOfSetExeption : Exception
    {
        public ElemOutOfSetExeption() { }
        public ElemOutOfSetExeption(string message) : base(message) { }
        public ElemOutOfSetExeption(string message, Exception inner) : base(message, inner) { }
        protected ElemOutOfSetExeption(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public abstract class Set
    {
        protected int _maxElem;
        public delegate void PrintFunction(string stringToPrint);

        public int MaxElem { get { return _maxElem; } }
        //public abstract int MaxElem { get; }

        /// <summary>
        /// ElemOutOfSetExeption
        /// </summary>
        /// <param name="newElem"></param>
        public abstract void AddElem(int newElem);
        public abstract void DelElem(int delElem);
        public abstract bool IsExists(int elem);

        /// <summary>
        /// just check can exists or not
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public bool CanExists(int elem)
        {
            if (elem > MaxElem || elem < 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// throw exception ElemOutOfSetExeption if cant extists
        /// </summary>
        /// <param name="elem"></param>
        public void CheckCanExists(int elem)
        {
            if (elem > MaxElem || elem < 0)
            {
                throw new ElemOutOfSetExeption($"Текущий элемент не входит в пределы множества.\nГраницы: {0}-{MaxElem}");
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
                CheckCanExists(newElem);
                AddElem(newElem);
            }
        }
        public void FillSet(int[] intData)
        {
            foreach (int newElem in intData)
            {
                CheckCanExists(newElem);
                AddElem(newElem);
            }
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i <= MaxElem; i++)
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
        //public abstract bool this[int elem]
        //{
        //    get;
        //}
    }
}