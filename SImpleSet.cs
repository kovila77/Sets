using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class SimpleSet : Set
    {
        private bool[] _data;

        public SimpleSet(int maxElem) : base()
        {
            this._maxElem = maxElem;
            this._data = new bool[maxElem + 1];
        }
        public override void AddElem(int newElem)
        {
            CheckCanExists(newElem);
            _data[newElem] = true;
        }
        public override void DelElem(int delElem)
        {
            if (IsExists(delElem))
            {
                _data[delElem] = false;
            }
        }
        public override bool IsExists(int elem)
        {
            return CanExists(elem) && _data[elem];
        }

        public static SimpleSet operator +(SimpleSet op1, SimpleSet op2)
        {
            (SimpleSet biggest, SimpleSet smallest) = op1.MaxElem > op2.MaxElem ? (op1, op2) : (op2, op1);
            SimpleSet resultSimpleSet = new SimpleSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = biggest._data[i] || smallest._data[i];
            }
            for (int i = smallest.MaxElem + 1; i <= biggest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = biggest._data[i];
            }
            return resultSimpleSet;
        }
        public static SimpleSet operator *(SimpleSet op1, SimpleSet op2)
        {
            (SimpleSet biggest, SimpleSet smallest) = op1.MaxElem > op2.MaxElem ? (op1, op2) : (op2, op1);
            SimpleSet resultSimpleSet = new SimpleSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = biggest._data[i] && smallest._data[i];
            }
            return resultSimpleSet;
        }
        public bool this[int elem]
        {
            get
            {
                return IsExists(elem);
            }
        }
    }
}
