using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attestation
{
    class MatComparer : IComparer<mat_t>
    {
        public int Compare(mat_t x, mat_t y)
        {
            /*if (x.Name.Length > y.Name.Length)
                return 1;
            else if (x.Name.Length < y.Name.Length)
                return -1;
            else
                return 0;*/
            return x.Name.CompareTo(y.Name);
        }
    }
}
