﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class SimpleSet : Set, IEnumerable
    {
        private bool[] _data;
        private int _maxElem;
        public override int MaxElem { get { return _maxElem; } }

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
            CheckCanExists(delElem);
            _data[delElem] = false;
        }
        public override bool IsExists(int elem)
        {
            return CanExists(elem) && _data[elem];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public SimpleSetEnum GetEnumerator()
        {
            return new SimpleSetEnum(this);
        }

        public static SimpleSet operator +(SimpleSet op1, SimpleSet op2)
        {
            SimpleSet biggest;
            SimpleSet smallest;
            if (op1.MaxElem > op2.MaxElem)
            {
                biggest = op1;
                smallest = op2;
            }
            else
            {
                biggest = op2;
                smallest = op1;
            }
            SimpleSet resultSimpleSet = new SimpleSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                if (biggest._data[i] || smallest._data[i])
                {
                    resultSimpleSet._data[i] = true;
                }
            }
            for (int i = smallest.MaxElem + 1; i <= biggest.MaxElem; i++)
            {
                if (biggest._data[i])
                    resultSimpleSet._data[i] = true;
            }
            return resultSimpleSet;
        }
        public static SimpleSet operator *(SimpleSet op1, SimpleSet op2)
        {
            SimpleSet biggest;
            SimpleSet smallest;
            if (op1.MaxElem > op2.MaxElem)
            {
                biggest = op1;
                smallest = op2;
            }
            else
            {
                biggest = op2;
                smallest = op1;
            }
            SimpleSet resultSimpleSet = new SimpleSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                if (biggest._data[i] && smallest._data[i])
                {
                    resultSimpleSet._data[i] = true;
                }
            }
            return resultSimpleSet;
        }
        public bool this[int elem]
        {
            get
            {
                CheckCanExists(elem);
                return IsExists(elem);
            }
        }
    }

    public class SimpleSetEnum : IEnumerator
    {
        public SimpleSet simpleSet;

        int position = -1;

        public SimpleSetEnum(SimpleSet simpleSet)
        {
            this.simpleSet = simpleSet;
        }

        public bool MoveNext()
        {
            position++;
            while (!simpleSet.IsExists(position))
            {
                position++;
                if (position > simpleSet.MaxElem)
                {
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public int Current
        {
            get
            {
                try
                {
                    return position;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
