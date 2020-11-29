using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06
{
    struct data 
    {
        public int y;
        public int m;
        public int d;
    }
    class DataSubtracter 
    {
        public static int DateSubt(data d1, data d2)
        {
            try
            {
                if(d1.d > 30 ||d2.d > 30||d1.m >12 ||d2.m > 12)
                    throw new InvalidCastException();
                if (d1.y < d2.y)
                    throw new InvalidCastException();
                if (d1.y == d2.y && d1.m < d2.m)
                    throw new InvalidCastException();
                if (d1.y == d2.y && d1.m == d2.m && d1.d < d2.d)
                    throw new InvalidCastException();

                return (d1.y - d2.y) * 360 + (d1.m - d2.m) * 30 + (d1.d - d2.d);
            }
            catch 
            {
                return -1;
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
        }
    }
}
