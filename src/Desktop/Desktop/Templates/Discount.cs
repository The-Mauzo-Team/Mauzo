using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Templates
{
    class Discount
    {
        private int id;
        private string code;
        private string desc;
        private float priceDisc;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Code
        {
            get => code;
            set => code = value;
        }

        public string Desc
        {
            get => desc;
            set => desc = value;
        }

        public float PriceDisc
        {
            get => priceDisc;
            set => priceDisc = value;
        }
    }
}
