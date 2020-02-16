using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    class BitSet : Set
    {
        private ulong[] _data;
        /// <summary>
        /// size of int in bits
        /// </summary>
        private int _sizeOfInt = sizeof(ulong) * 8;
        private int _maxElem;
        public override int MaxElem { get { return _maxElem; } }
        private int CountOfСell { get { return _data.Length; } }

        public BitSet(int maxElem) : base()
        {
            this._maxElem = maxElem;
            this._data = new ulong[(maxElem / _sizeOfInt) + 1];
        }

        public override void AddElem(int newElem)
        {
            CheckCanExists(newElem);
            _data[newElem / _sizeOfInt] = _data[newElem / _sizeOfInt] | (((ulong)1) << (newElem % _sizeOfInt));
        }
        public override void DelElem(int delElem)
        {
            CheckCanExists(delElem);
            _data[delElem / _sizeOfInt] = _data[delElem / _sizeOfInt] & ~(((ulong)1) << (delElem % _sizeOfInt));
        }
        public override bool IsExists(int elem)
        {
            return CanExists(elem) && ((_data[elem / _sizeOfInt] & (((ulong)1) << (elem % _sizeOfInt))) > 0);
        }
        public static BitSet operator +(BitSet op1, BitSet op2)
        {
            BitSet biggest;
            BitSet smallest;
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
            BitSet biggest;
            BitSet smallest;
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
            BitSet resultBitSet = new BitSet(biggest.MaxElem);
            for (int i = 0; i < smallest.CountOfСell; i++)
            {
                resultBitSet._data[i] = smallest._data[i] & biggest._data[i];
            }
            return resultBitSet;
        }
    }
}
