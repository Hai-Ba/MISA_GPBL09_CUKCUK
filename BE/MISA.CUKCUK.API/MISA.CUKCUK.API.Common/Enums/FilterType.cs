using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Enums
{
    public enum FilterType 
    {
        //Loai filter binh thuong (Where a=b)
        NormalFilter = 0,

        //Where a LIKE "%b%"
        Like = 1,

        //Where a NOT LIKE "%b%"
        NotLike = 2,

        //Where a = "b"
        Equal = 3,

        //Where a LIKE "b%"
        BeginWith = 4,

        //Where a LIKE "%b"
        EndWith = 5,

        //Where a < b
        Smaller = 6,

        //Where a > b
        Higher = 7,

        //Where a <= b
        SmallerEqual = 8,

        //Where a >= b
        HigherEqual = 9,

        //Where a = "b"
        FilterTrueFalse = 10,
    }
}
