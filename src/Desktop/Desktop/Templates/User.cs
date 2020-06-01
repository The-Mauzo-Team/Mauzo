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
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.Templates
{
    class User
    {
        private int id;
        private string username;
        private string password;
        private string firstName;
        private string lastName;
        private string email;
        private bool isAdmin;
        private Bitmap userPicArr;

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo id.
        /// </summary>
        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo username.
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Propiedad que hace un get o un set convirtiendo 
        /// la cadena a MD5 del atributo password.
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                // Conversor de texto plano UTF8 a MD5.
                using (var provider = MD5.Create())
                {
                    StringBuilder builder = new StringBuilder();

                    // Computamos el valor de ASCII a MD5 Hash
                    foreach (byte b in provider.ComputeHash(Encoding.ASCII.GetBytes(value)))
                        builder.Append(b.ToString("x2").ToUpper());

                    // Guardamos el resultado en el valor password
                    password = builder.ToString();
                }
            }
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo firstname.
        /// </summary>
        public string Firstname 
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo lastname.
        /// </summary>
        public string Lastname
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo email.
        /// </summary>
        public string Email 
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo isAdmin.
        /// </summary>
        public bool IsAdmin 
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        /// <summary>
        /// Propiedad que actua de conversor cuando le hacen un get o un ser 
        /// sobre el atributo userPicArr.
        /// </summary>
        public string UserPic
        {
            get {
                string auxPic = null;

                if (userPicArr != null)
                {
                    Bitmap auxBits = new Bitmap(userPicArr);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        auxBits.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        auxPic = Convert.ToBase64String(ms.ToArray());
                    }
                }

                return auxPic;
            }

            set { 
                if (value != null)
                {
                    BitmapSource userPicSource = (BitmapSource)new ImageSourceConverter().ConvertFrom(Convert.FromBase64String(value));

                    using (MemoryStream outStream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new BmpBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(userPicSource));
                        encoder.Save(outStream);
                        userPicArr = new Bitmap(outStream);
                    }
                } 
                else
                {
                    userPicArr = null;
                }
            }
        }

        /// <summary>
        /// Propiedad que hace un get o un set directo al atributo userPicArr.
        /// </summary>
        public Bitmap UserPicArr
        {
            get { return userPicArr; }
            set { userPicArr = value; }
        }
    }
}