using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.Templates
{
    class Product
    {
        private int id;
        private string prodName;
        private string prodCode;
        private float prodPrice;
        private string prodDesc;
        private Bitmap  prodPicArr;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string ProdName
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

        public string ProdPic
        {
            get
            {
                string auxPic = null;

                if (prodPicArr != null)
                {
                    auxPic = Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(prodPicArr, typeof(byte[])));
                }

                return auxPic;
            }

            set
            {
                if (value != null)
                {
                    BitmapSource userPicSource = (BitmapSource)new ImageSourceConverter().ConvertFrom(Convert.FromBase64String(value));

                    using (MemoryStream outStream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new BmpBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(userPicSource));
                        encoder.Save(outStream);
                        prodPicArr = new Bitmap(outStream);
                    }
                }
                else
                {
                    prodPicArr = null;
                }
            }
        }

        public Bitmap ProdPicArr
        {
            get { return prodPicArr; }
            set { prodPicArr = value; }
        }
    }
}