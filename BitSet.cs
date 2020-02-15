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

        public BitSet(int maxElem) : base()
        {
            this._maxElem = maxElem + 1;
            this._data = new ulong[_maxElem/sizeof(ulong)];
        }
    }
}
