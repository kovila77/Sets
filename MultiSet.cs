using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class MultiSet : Set, IEnumerable
    {
        private int[] _data;
        private int _maxElem;
        public override int MaxElem { get { return _maxElem; } }

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
            CheckCanExists(delElem);
            if (_data[delElem] > 0)
            {
                _data[delElem]--;
            }
        }
        public override bool IsExists(int elem)
        {
            return CanExists(elem) && (_data[elem] > 0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public MultiSetEnum GetEnumerator()
        {
            return new MultiSetEnum(this);
        }

        public static MultiSet operator +(MultiSet op1, MultiSet op2)
        {
            MultiSet biggest;
            MultiSet smallest;
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
            MultiSet biggest;
            MultiSet smallest;
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
            MultiSet resultSimpleSet = new MultiSet(biggest.MaxElem);
            for (int i = 0; i <= smallest.MaxElem; i++)
            {
                resultSimpleSet._data[i] = biggest._data[i] < smallest._data[i] ?
                                            biggest._data[i] : smallest._data[i];
            }
            return resultSimpleSet;
        }
        public int this[int elem]
        {
            get
            {
                CheckCanExists(elem);
                return _data[elem];
            }
        }
    }

    public class MultiSetEnum : IEnumerator
    {
        public MultiSet multiSet;

        int position = -1;

        public MultiSetEnum(MultiSet simpleSet)
        {
            this.multiSet = simpleSet;
        }

        public bool MoveNext()
        {
            position++;
            while (!multiSet.IsExists(position))
            {
                position++;
                if (position > multiSet.MaxElem)
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
                    return multiSet[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}