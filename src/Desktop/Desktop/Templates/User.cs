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