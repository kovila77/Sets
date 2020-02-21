using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class MultiSet : Set
    {
        private int[] _data;

        public MultiSet(int maxElem) : base()
        {
            this._maxElem = maxElem;
            this._data = new int[maxElem + 1];
        }
        public override void AddElem(int newElem)
        {
            CheckCanExists(newElem);
            _data[newElem]++;
        }
        public override void DelElem(int delElem)
        {
            if (IsExists(delElem))
            {
                _data[delElem]--;
            }
        }
        public override bool IsExists(int elem)
        {
            return CanExists(elem) && (_data[elem] > 0);
        }

        public static MultiSet operator +(MultiSet op1, MultiSet op2)
        {
            (MultiSet biggest, MultiSet smallest) = op1.MaxElem > op2.MaxElem ? (op1, op2) : (op2, op1);
            MultiSet resultSimpleSet = new MultiSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = biggest._data[i] + smallest._data[i];

            }
            for (int i = smallest.MaxElem + 1; i <= biggest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = biggest._data[i];
            }
            return resultSimpleSet;
        }
        public static MultiSet operator *(MultiSet op1, MultiSet op2)
        {
            (MultiSet biggest, MultiSet smallest) = op1.MaxElem > op2.MaxElem ? (op1, op2) : (op2, op1);
            MultiSet resultSimpleSet = new MultiSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = Math.Min(biggest._data[i], smallest._data[i]);
            }
            return resultSimpleSet;
        }
        public int this[int elem]
        {
            get
            {
                return IsExists(elem) ? _data[elem] : 0;
            }
        }
    }
}