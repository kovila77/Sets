﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    class SimpleSet : Set
    {
        private bool[] _data;

        public SimpleSet(int maxElem) : base()
        {
            this._maxElem = maxElem + 1;
            this._data = new bool[_maxElem];
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
            CheckCanExists(elem);
            return _data[elem];
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
            for (int i = 0; i < smallest.MaxElem; i++)
            {
                if (biggest._data[i] || smallest._data[i])
                    resultSimpleSet._data[i] = true;
            }
            for (int i = smallest.MaxElem + 1; i < biggest.MaxElem; i++)
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
            for (int i = 0; i < smallest.MaxElem; i++)
            {
                if (biggest._data[i] && smallest._data[i])
                    resultSimpleSet._data[i] = true;
            }
            return resultSimpleSet;
        }
    }
}
