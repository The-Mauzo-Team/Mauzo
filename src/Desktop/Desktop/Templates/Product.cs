using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                    Bitmap auxBits = new Bitmap(prodPicArr);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        auxBits.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        auxPic = Convert.ToBase64String(ms.ToArray());
                    }
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