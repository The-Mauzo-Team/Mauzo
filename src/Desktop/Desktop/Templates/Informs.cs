using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Templates
{
    class Informs
    {
        private int id;
        private int nSales;
        private int nRefunds;
        private int nDiscounts;

        private DateTime dStart;
        private DateTime dEnd;

        public int Id
        {
            get => id;
            set => id = value;
        }
        public int NSales 
        {
            get => nSales;
            set => nSales = value;
        }
        public int NRefunds
        {
            get => nRefunds;
            set => nRefunds = value;
        }
        public int NDiscounts 
        {
            get => nDiscounts;
            set => nDiscounts = value;
        }

        public DateTime DStart
        {
            get => dStart;
            set => dStart = value;
        }

        public DateTime DEnd
        {
            get => dEnd;
            set => dEnd = value;
        }
    }
}
