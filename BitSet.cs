﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class BitSet : Set
    {
        private ulong[] _data;
        /// <summary>
        /// size of int in bits
        /// </summary>
        private int _sizeOfInt = sizeof(ulong) * 8;
        private int CountOfСell { get { return _data.Length; } }

        public BitSet(int maxElem) : base()
        {
            this._maxElem = maxElem;
            this._data = new ulong[(maxElem / _sizeOfInt) + 1];
        }

        public override void AddElem(int newElem)
        {
            CheckCanExists(newElem);
            _data[newElem / _sizeOfInt] |=  1UL << (newElem % _sizeOfInt);
        }
        public override void DelElem(int delElem)
        {
            if (IsExists(delElem))
            {
                _data[delElem / _sizeOfInt] &= ~(1UL << (delElem % _sizeOfInt));
            }
        }
        public override bool IsExists(int elem)
        {
            return CanExists(elem) && ((_data[elem / _sizeOfInt] & (1UL << (elem % _sizeOfInt))) > 0);
        }
        public static BitSet operator +(BitSet op1, BitSet op2)
        {
            (BitSet biggest, BitSet smallest) = op1.MaxElem > op2.MaxElem ? (op1, op2) : (op2, op1);
            BitSet resultBitSet = new BitSet(biggest.MaxElem);
            for (int i = 0; i < smallest.CountOfСell; i++)
            {
                resultBitSet._data[i] = smallest._data[i] | biggest._data[i];
            }
            for (int i = smallest.CountOfСell; i < biggest.CountOfСell; i++)
            {
                resultBitSet._data[i] = biggest._data[i];
            }
            return resultBitSet;
        }
        public static BitSet operator *(BitSet op1, BitSet op2)
        {
            (BitSet biggest, BitSet smallest) = op1.MaxElem > op2.MaxElem ? (op1, op2) : (op2, op1);
            BitSet resultBitSet = new BitSet(biggest.MaxElem);
            for (int i = 0; i < smallest.CountOfСell; i++)
            {
                resultBitSet._data[i] = smallest._data[i] & biggest._data[i];
            }
            return resultBitSet;
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
