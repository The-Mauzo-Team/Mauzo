/*
 * MIT License
 *
 * Copyright (c) 2020 The Mauzo Team
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.Templates
{
    /// <remarks>@lluminar - Lidia Martínez</remarks>
    class Product
    {
        private int id;
        private string prodName;
        private string prodCode;
        private float prodPrice;
        private string prodDesc;
        private Bitmap  prodPicArr;

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo id
        /// </summary>
        public int Id
        {
            get => id;
            set => id = value;
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo ProdName
        /// </summary>
        public string ProdName
        {
            get => prodName;
            set => prodName = value;
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo ProdCode
        /// </summary>
        public string ProdCode
        {
            get => prodCode;
            set => prodCode = value;
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo ProdPrice
        /// </summary>
        public float ProdPrice
        {
            get => prodPrice;
            set => prodPrice = value;
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo ProdDesc
        /// </summary>
        public string ProdDesc
        {
            get => prodDesc;
            set => prodDesc = value;
        }

        /// <summary>
        /// Este atributo nos pasa a Base64 la imagen elegida al hacer un get o set y nos devulve esa cadena.
        /// </summary>
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

        /// <summary>
        /// Propiedad para hacer un get o set sobre el atributo prodPicArr
        /// </summary>
        public Bitmap ProdPicArr
        {
            get { return prodPicArr; }
            set { prodPicArr = value; }
        }
    }
}