using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Templates
{
    class Product
    {
        private int id;
        private int prodName;
        private string prodCode;
        private float prodPrice;
        private string prodDesc;
        private Bitmap  prodPic;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public int ProdName
        {
            get => prodName;
            set => prodName = value;
        }

        public string ProdCode
        {
            get => prodCode;
            set => prodCode = value;
        }

        public float ProdPrice
        {
            get => prodPrice;
            set => prodPrice = value;
        }

        public string ProdDesc
        {
            get => prodDesc;
            set => prodDesc = value;
        }

        public Bitmap ProdPic
        {
            get => prodPic;
            set => prodPic = value;
        }
    }
}
