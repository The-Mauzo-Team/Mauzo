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
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

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
        private Bitmap userPic;

        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

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
                        builder.Append(b.ToString("x2").ToLower());

                    // Guardamos el resultado en el valor password
                    password = builder.ToString();
                }
            }
        }

        public string Firstname 
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string Lastname
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email 
        {
            get { return email; }
            set { email = value; }
        }

        public bool IsAdmin 
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        public Bitmap UserPic 
        {
            get { return userPic; }
            set { userPic = value; }
        }
    }
}